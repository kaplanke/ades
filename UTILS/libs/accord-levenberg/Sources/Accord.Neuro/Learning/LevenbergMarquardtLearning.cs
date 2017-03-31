// Accord Neural Net Library
// Accord.NET framework
// http://www.crsouza.com
//
// Copyright © César Souza, 2009
// cesarsouza@gmail.com
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AForge.Neuro;
using Accord.Math;
using Accord.Math.Decompositions;

namespace Accord.Neuro.Learning
{

    /// <summary>
    ///   The Jacobian computation method used by the Levenberg-Marquardt.
    /// </summary>
    public enum JacobianMethod
    {
        ByFiniteDifferences,
        ByBackpropagation,
    }

    /// <summary>
    ///  Levenberg Marquardt Learning Algorithm with optional Bayesian Regularization.
    /// </summary>
    /// <remarks>
    /// <para>This class implements the Levenberg Marquardt learning algorithm,
    /// which treats the neural network learning as a function optimization
    /// problem. The Levenberg-Marquardt is one of the fastest and accurate
    /// learning algorithms for small to medium sized networks.</para>
    /// 
    /// <para>However, in general, the standard LM algorithm does not perform as well
    /// on pattern recognition problems as it does on function approximation problems.
    /// The LM algorithm is designed for least squares problems that are approximately
    /// linear. Because the output neurons in pattern recognition problems are generally
    /// saturated, it will not be operating in the linear region.</para>
    /// 
    /// <para>The advantages of the LM algorithm decreases as the number of network
    /// parameters increases. </para>
    /// 
    /// <para>Sample usage (training network to calculate XOR function):</para>
    /// <code>
    /// // initialize input and output values
    /// double[][] input = new double[4][] {
    ///     new double[] {0, 0}, new double[] {0, 1},
    ///     new double[] {1, 0}, new double[] {1, 1}
    /// };
    /// double[][] output = new double[4][] {
    ///     new double[] {0}, new double[] {1},
    ///     new double[] {1}, new double[] {0}
    /// };
    /// // create neural network
    /// ActivationNetwork   network = new ActivationNetwork(
    ///     SigmoidFunction( 2 ),
    ///     2, // two inputs in the network
    ///     2, // two neurons in the first layer
    ///     1 ); // one neuron in the second layer
    /// // create teacher
    /// LevenbergMarquardtLearning teacher = new LevenbergMarquardtLearning( network );
    /// // loop
    /// while ( !needToStop )
    /// {
    ///     // run epoch of learning procedure
    ///     double error = teacher.RunEpoch( input, output );
    ///     // check error value to see if we need to stop
    ///     // ...
    /// }
    /// </code>
    /// </remarks>
    /// 
    public class LevenbergMarquardtLearning : AForge.Neuro.Learning.ISupervisedLearning
    {
        // REFERENCES:
        // http://www.cs.otago.ac.nz/nnweb/FAQ2.html
        // http://www-alg.ist.hokudai.ac.jp/~jan/alpha.pdf
        // http://cs.olemiss.edu/~ychen/publications/conference/chen_ijcnn99.pdf
        // http://www.nict.go.jp/publication/shuppan/kihou-journal/journal-vol54no1.2/02D.pdf
        // http://matlab.izmiran.ru/help/toolbox/nnet/backpr12.html
        // http://www.inference.phy.cam.ac.uk/mackay/Bayes_FAQ.html
        // http://eprints.kfupm.edu.sa/40648/1/40648.pdf

        private const double lambdaMax = 1e25;


        // network to teach
        private ActivationNetwork network;


        // Bayesian Regularization variables
        private bool useBayesianRegularization;

        // Bayesian Regularization Hyperparameters
        private double alpha = 0.0;
        private double beta = 1.0;
        private double gamma = 0.0;

        // Levenber-Marquardt variables
        private double[,] jacobian;
        private double[,] hessian;
        
        private double[] diagonal;
        private double[] gradient;
        private double[] weigths;
        private double[] deltas;
        private double[] errors;

        private JacobianMethod method;

        // Levenberg dumping factor
        private double lambda = 0.1;

        // The ammount the dumping factor is adjusted
        //  when searching for the minimum error surface
        private double v = 10.0;

        // Total of weights in the network
        private int numberOfParameters;



        /// <summary>
        ///  Learning rate
        /// </summary>
        /// 
        /// <remarks><para>The value determines speed of learning.</para>
        /// 
        /// <para>Default value equals to <b>0.1</b>.</para>
        /// </remarks>
        ///
        public double LearningRate
        {
            get { return lambda; }
            set { lambda = value; }
        }

        /// <summary>
        /// Learning rate adjustment
        /// </summary>
        /// 
        /// <remarks><para>The value by which the learning rate
        /// is adjusted when searching for the minimum cost surface.</para>
        /// 
        /// <para>Default value equals to <b>10</b>.</para>
        /// </remarks>
        ///
        public double Adjustment
        {
            get { return v; }
            set { v = value; }
        }

        /// <summary>
        ///   Gets the total number of parameters in the network
        ///   being teached.
        /// </summary>
        public int NumberOfParameters
        {
            get { return numberOfParameters; }
        }

        /// <summary>
        ///   Gets the number of effective parameters being used
        ///   by the network as determined by the bayesian regularization.
        /// </summary>
        /// <remarks>
        ///   If no regularization is being used, the value will be 0.
        /// </remarks>
        public double EffectiveParameters
        {
            get { return gamma; }
        }

        /// <summary>
        ///   Gets or sets the importance of the squared sum of network
        ///   weights in the cost function. Used by the regularization.
        /// </summary>
        /// <remarks>
        ///   This is the first bayesian hyperparameter. The default
        ///   value is 0.
        /// </remarks>
        public double Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        /// <summary>
        ///   Gets or sets the importance of the squared sum of network
        ///   errors in the cost function. Used by the regularization.
        /// </summary>
        /// <remarks>
        ///   This is the second bayesian hyperparameter. The default
        ///   value is 1.
        /// </remarks>
        public double Beta
        {
            get { return beta; }
            set { beta = value; }
        }

        public bool UseRegularization
        {
            get { return useBayesianRegularization; }
            set { useBayesianRegularization = value; }
        }



        /// <summary>
        ///   Initializes a new instance of the <see cref="LevenbergMarquardtLearning"/> class.
        /// </summary>
        /// 
        /// <param name="network">Network to teach.</param>
        /// 
        public LevenbergMarquardtLearning(ActivationNetwork network) :
            this(network, false, JacobianMethod.ByBackpropagation)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="LevenbergMarquardtLearning"/> class.
        /// </summary>
        /// 
        /// <param name="network">Network to teach.</param>
        /// <param name="useRegularization">True to use bayesian regularization, false otherwise.</param>
        /// 
        public LevenbergMarquardtLearning(ActivationNetwork network, bool useRegularization) :
            this(network, useRegularization, JacobianMethod.ByBackpropagation)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="LevenbergMarquardtLearning"/> class.
        /// </summary>
        /// 
        /// <param name="network">Network to teach.</param>
        /// <param name="useRegularization">True to use bayesian regularization, false otherwise.</param>
        /// <param name="method">The method by which the Jacobian matrix will be calculated.</param>
        /// 
        public LevenbergMarquardtLearning(ActivationNetwork network, bool useRegularization, JacobianMethod method)
        {
            if (network.LayersCount > 2 || network[network.LayersCount - 1].NeuronsCount > 1)
            {
                throw new NotSupportedException(
                    "Currently only networks with up to one hidden layer and a single output are supported.");
            }


            this.network = network;
            this.numberOfParameters = GetNumberOfParameters(network);
            this.useBayesianRegularization = useRegularization;
            this.weigths = new double[numberOfParameters];
            this.hessian = new double[numberOfParameters, numberOfParameters];
            this.diagonal = new double[numberOfParameters];
            this.gradient = new double[numberOfParameters];
            this.method = method;


            // Will use backpropagation method for Jacobian computation
            if (method == JacobianMethod.ByBackpropagation)
            {
                // create weight derivatives arrays
                this.weightDerivatives = new double[network.LayersCount][][];
                this.thresholdsDerivatives = new double[network.LayersCount][];

                // initialize arrays
                for (int i = 0; i < network.LayersCount; i++)
                {
                    ActivationLayer layer = network[i];

                    this.weightDerivatives[i] = new double[layer.NeuronsCount][];
                    this.thresholdsDerivatives[i] = new double[layer.NeuronsCount];

                    for (int j = 0; j < layer.NeuronsCount; j++)
                    {
                        this.weightDerivatives[i][j] = new double[layer.InputsCount];
                    }
                }
            }
            else // Will use finite difference method for Jacobian computation
            {
                // create differential coefficient arrays
                this.differentialCoefficients = CreateCoefficients(3);
                this.derivativeStepSize = new double[numberOfParameters];

                // initialize arrays
                for (int i = 0; i < numberOfParameters; i++)
                {
                    this.derivativeStepSize[i] = derivativeStep;
                }
            }
        }




        /// <summary>
        ///  This method should not be called. Use <see cref="RunEpoch"/> instead.
        /// </summary>
        /// 
        /// <param name="input">Array of input vectors.</param>
        /// <param name="output">Array of output vectors.</param>
        /// 
        /// <returns>Nothing.</returns>
        /// 
        /// <remarks><para>Online learning mode is not supported by the
        /// Levenberg Marquardt. Use batch learning mode instead.</para></remarks>
        ///
        public double Run(double[] input, double[] output)
        {
            throw new InvalidOperationException("Learning can only be done in batch mode.");
        }


        /// <summary>
        ///   Runs learning epoch.
        /// </summary>
        /// 
        /// <param name="input">Array of input vectors.</param>
        /// <param name="output">Array of output vectors.</param>
        /// 
        /// <returns>Returns summary learning error for the epoch.</returns>
        /// 
        /// <remarks><para>The method runs one learning epoch, by calling running necessary
        /// iterations of the Levenberg Marquardt to achieve an error decrease.</remarks>
        ///
        public double RunEpoch(double[][] input, double[][] output)
        {
            // Initial definitions and memmory allocations
            int N = input.Length;
            this.errors = new double[N];
            this.jacobian = new double[N, numberOfParameters];
            LuDecomposition decomposition = null;
            double sumOfSquaredErrors = 0.0;
            double sumOfSquaredWeights = 0.0;
            double trace = 0.0;


            // Compute the Jacobian matrix
            if (method == JacobianMethod.ByBackpropagation)
            {
                sumOfSquaredErrors = JacobianByChainRule(input, output);
            }
            else
            {
                sumOfSquaredErrors = JacobianByFiniteDifference(input, output);
            }


            // Create the initial weights vector
            sumOfSquaredWeights = CreateWeights();


            // Compute Jacobian errors and Hessian
            for (int i = 0; i < numberOfParameters; i++)
            {
                // Compute Jacobian Matrix Errors
                double s = 0.0;
                for (int j = 0; j < N; j++)
                {
                    s += jacobian[j, i] * errors[j];
                }
                gradient[i] = s;

                // Compute Quasi-Hessian Matrix using Jacobian (H = J'J)
                for (int j = 0; j < numberOfParameters; j++)
                {
                    double c = 0.0;
                    for (int k = 0; k < N; k++)
                    {
                        c += jacobian[k, i] * jacobian[k, j];
                    }
                    hessian[i, j] = beta * c;
                }
            }

            // Store the Hessian diagonal for future computations
            for (int i = 0; i < numberOfParameters; i++)
                diagonal[i] = hessian[i, i];


            // Define the objective function
            // bayesian regularization objective function
            double objective = beta * sumOfSquaredErrors + alpha * sumOfSquaredWeights;
            double current = objective + 1.0;



            // Begin of the main Levenberg-Macquardt method
            lambda /= v;

            // We'll try to find a direction with less error
            //  (or where the objective function is smaller)
            while (current >= objective && lambda < lambdaMax)
            {
                lambda *= v;

                // Update diagonal (Levenberg-Marquardt formula)
                for (int i = 0; i < numberOfParameters; i++)
                    hessian[i, i] = diagonal[i] + (lambda + alpha);

                // Decompose to solve the linear system
                decomposition = new LuDecomposition(hessian);

                // Check if the Jacobian has become non-invertible
                if (!decomposition.NonSingular) continue;

                // Solve using LU (or SVD) decomposition
                deltas = decomposition.Solve(gradient);

                // Update weights using the calculated deltas
                sumOfSquaredWeights = UpdateWeights();

                // Calculate the new error
                sumOfSquaredErrors = 0.0;
                for (int i = 0; i < N; i++)
                {
                    network.Compute(input[i]);
                    sumOfSquaredErrors += CalculateError(output[i]);
                }

                // Update the objective function
                current = beta * sumOfSquaredErrors + alpha * sumOfSquaredWeights;

                // If the object function is bigger than before, the method
                //  is tried again using a greater dumping factor.
            }

            // If this iteration caused a error drop, then next iteration
            //  will use a smaller damping factor.
            lambda /= v;



            // If we are using bayesian regularization, we need to
            //   update the bayesian hyperparameters alpha and beta
            if (useBayesianRegularization)
            {
                // References: 
                // - http://www-alg.ist.hokudai.ac.jp/~jan/alpha.pdf
                // - http://www.inference.phy.cam.ac.uk/mackay/Bayes_FAQ.html

                // Compute the trace for the inverse hessian
                trace = Matrix.Trace(decomposition.Inverse());

                // Poland update's formula:
                gamma = numberOfParameters - (alpha * trace);
                alpha = numberOfParameters / (2.0 * sumOfSquaredWeights + trace);
                beta = System.Math.Abs((N - gamma) / (2.0 * sumOfSquaredErrors));
                //beta = (N - gama) / (2.0 * sumOfSquaredErrors);

                // Original MacKay's update formula:
                //  gama = (double)networkParameters - (alpha * trace);
                //  alpha = gama / (2.0 * sumOfSquaredWeights);
                //  beta = (gama - N) / (2.0 * sumOfSquaredErrors);
            }

            return sumOfSquaredErrors;
        }

        /// <summary>
        ///   Calculates error values for the last network layer.
        /// </summary>
        /// 
        /// <param name="desiredOutput">Desired output vector.</param>
        /// 
        /// <returns>Returns summary squared error of the last layer divided by 2.</returns>
        /// 
        private double CalculateError(double[] desiredOutput)
        {
            double sumOfSquaredErrors = 0.0;
            double e = 0.0;

            for (int j = 0; j < desiredOutput.Length; j++)
            {
                e = desiredOutput[j] - network.Output[j];
                sumOfSquaredErrors += e * e;
            }

            return sumOfSquaredErrors / 2.0;
        }

        /// <summary>
        ///  Update network's weights.
        /// </summary>
        /// 
        /// <returns>The sum of squared weights divided by 2.</returns>
        /// 
        private double UpdateWeights()
        {
            double sumOfSquaredWeights = 0.0;
            double w;
            int j = 0;

            // For each layer:
            for (int layer = 0; layer < network.LayersCount; layer++)
            {
                // for each neuron:
                for (int neuron = 0; neuron < network[layer].NeuronsCount; neuron++)
                {
                    // for each weight:
                    for (int weight = 0; weight < network[layer][neuron].InputsCount; weight++)
                    {
                        w = weigths[j] + deltas[j];
                        sumOfSquaredWeights += w * w;
                        network[layer][neuron][weight] = w;
                        j++;
                    }
                    // for each threshold value (bias):
                    w = weigths[j] + deltas[j];
                    network[layer][neuron].Threshold = w;
                    sumOfSquaredWeights += w * w;
                    j++;
                }
            }

            return sumOfSquaredWeights / 2.0;
        }

        /// <summary>
        ///   Creates the initial weight vector w
        /// </summary>
        /// 
        /// <returns>The sum of squared weights divided by 2.</returns>
        /// 
        private double CreateWeights()
        {
            int j = 0;
            double sumOfSquaredWeights = 0.0;
            double w;

            // for each layer:
            for (int layer = 0; layer < network.LayersCount; layer++)
            {
                // for each neuron:
                for (int neuron = 0; neuron < network[layer].NeuronsCount; neuron++)
                {
                    // for each weight:
                    for (int weight = 0; weight < network[layer][neuron].InputsCount; weight++)
                    {
                        // We copy it to the starting weights vector
                        w = network[layer][neuron][weight];
                        weigths[j] = w;
                        sumOfSquaredWeights += w * w;
                        j++;
                    }
                    // also for each threshold value (bias):
                    w = network[layer][neuron].Threshold;
                    weigths[j] = w;
                    sumOfSquaredWeights += w * w;
                    j++;
                }
            }
            return sumOfSquaredWeights / 2.0;
        }


        /// <summary>
        ///   Gets the number of parameters in the network.
        /// </summary>
        /// <param name="network"></param>
        /// <returns></returns>
        private int GetNumberOfParameters(ActivationNetwork network)
        {
            int w = 0;
            for (int i = 0; i < network.LayersCount; i++)
            {
                for (int j = 0; j < network[i].NeuronsCount; j++)
                {
                    // number of weights plus the bias value
                    w += network[i][j].InputsCount + 1;
                }
            }
            return w;
        }





        #region Jacobian Calculation By Chain Rule

        private double[][][] weightDerivatives = null;
        private double[][] thresholdsDerivatives = null;

        /// <summary>
        ///   Calculates the Jacobian matrix by using the chain rule.
        /// </summary>
        /// <param name="input">The input vectors.</param>
        /// <param name="output">The desired output vectors.</param>
        /// <returns>The sum of squared errors for the last error divided by 2.</returns>
        private double JacobianByChainRule(double[][] input, double[][] output)
        {
            double sse = 0.0, e;
            int N = input.Length;

            // foreach training vector
            for (int i = 0; i < N; i++)
            {
                // Do a forward pass
                network.Compute(input[i]);

                int ji = i;
                int jj = 0;

                // Calculate the derivatives for the j-th output            
                //  by using a backpropagation pass
                e = CalculateDerivatives(input[i], output[i], jj);
                errors[ji] = e;
                sse += e * e;


                // Create the Jacobian matrix row
                for (int layer = 0; layer < network.LayersCount; layer++)
                {
                    // for each neuron of the layer
                    for (int neuron = 0; neuron < network[layer].NeuronsCount; neuron++)
                    {
                        // for each weight of the neuron
                        for (int weight = 0; weight < network[layer][neuron].InputsCount; weight++)
                        {
                            // copy derivative
                            jacobian[ji, jj] = weightDerivatives[layer][neuron][weight];
                            jj++;
                        }
                        // copy derivative
                        jacobian[ji, jj] = thresholdsDerivatives[layer][neuron];
                        jj++;
                    }
                }

            }
            return sse / 2.0;
        }

        /// <summary>
        ///   Calculates partial derivatives for all weights of the network.
        /// </summary>
        /// 
        /// <param name="desiredOutput">Desired output vector.</param>
        /// 
        /// <returns>Returns summary squared error of the last layer.</returns>
        /// 
        private double CalculateDerivatives(double[] input, double[] desiredOutput, int outputIndex)
        {
            // error values
            double e = 0.0;
            double sum = 0.0;

            // assume, that all neurons of the network have the same activation function
            IActivationFunction function = network[0][0].ActivationFunction;

            int layer = network.LayersCount - 1;

            double[] previousLayerOutput = (network.LayersCount == 1) ?
                input : network[layer - 1].Output;


            // Start by the output layer first (currently only 1 output is supported)
            double output = network[layer][outputIndex].Output;
            e = desiredOutput[outputIndex] - output;

            for (int i = 0; i < network[layer][outputIndex].InputsCount; i++)
            {
                weightDerivatives[layer][outputIndex][i] = function.Derivative2(output) * previousLayerOutput[i];
            }
            thresholdsDerivatives[layer][outputIndex] = function.Derivative2(output);



            // Hidden layer case (only 1 hidden layer is currently supported)
            if (network.LayersCount == 2)
            {
                layer = 0;
                previousLayerOutput = input;

                // for each neuron in the input layer
                for (int neuron = 0; neuron < network[layer].NeuronsCount; neuron++)
                {
                    output = network[layer][neuron].Output;

                    // for each weight of the input neuron
                    for (int i = 0; i < network[layer][neuron].InputsCount; i++)
                    {
                        sum = 0.0;
                        // for each neuron in the next layer
                        for (int j = 0; j < network[layer + 1].NeuronsCount; j++)
                        {
                            // for each weight of the next neuron
                            for (int k = 0; k < network[layer + 1].InputsCount; k++)
                            {
                                sum += network[layer + 1][j][k] * network[layer].Output[j];
                            }
                            sum += network[layer + 1][j].Threshold;
                        }

                        // consider there's only one output neuron
                        double w = network[layer + 1][outputIndex][neuron];

                        weightDerivatives[layer][neuron][i] = function.Derivative2(output) *
                            function.Derivative(sum) * w * previousLayerOutput[i];

                        thresholdsDerivatives[layer][neuron] = function.Derivative2(output) *
                            function.Derivative(sum) * w;
                    }
                }
            }

            // return error
            return e;
        }
        #endregion




        #region Jacobian by Finite Differences

        private double[] derivativeStepSize;
        private const double derivativeStep = 1e-2;
        private double[][,] differentialCoefficients;


        /// <summary>
        ///   Calculates the Jacobian Matrix using Finite Differences
        /// </summary>
        /// <returns>Returns the sum of squared errors of the network divided by 2.</returns>
        private double JacobianByFiniteDifference(double[][] input, double[][] desiredOutput)
        {
            double e;
            double[] networkOutput;
            double sumOfSquaredErrors = 0;
            int N = input.Length;

            // foreach training vector
            for (int i = 0; i < N; i++)
            {
                networkOutput = network.Compute(input[i]);

                int ji = i;

                // Calculate network error to build the residuals vector
                e = desiredOutput[i][0] - networkOutput[0];
                errors[ji] = e;
                sumOfSquaredErrors += e * e;

                // Computation of one of the Jacobian Matrix rows by nummerical differentiation:
                // for each weight wj in the network, we have to compute its partial
                //   derivative to build the jacobian matrix.
                int jj = 0;

                // So, for each layer:
                for (int layer = 0; layer < network.LayersCount; layer++)
                {
                    // for each neuron:
                    for (int neuron = 0; neuron < network[layer].NeuronsCount; neuron++)
                    {
                        // for each weight:
                        for (int weight = 0; weight < network[layer][neuron].InputsCount; weight++)
                        {
                            // Compute its partial derivative
                            jacobian[ji, jj] = ComputeDerivative(input[i], layer, neuron, weight, ref derivativeStepSize[jj], networkOutput[0]);
                            jj++;
                        }
                        // and also for each threshold value (bias)
                        jacobian[ji, jj] = ComputeDerivative(input[i], layer, neuron, -1, ref derivativeStepSize[jj], networkOutput[0]);
                        jj++;
                    }
                }
            }

            // returns the sum of squared errors / 2
            return sumOfSquaredErrors / 2.0;
        }



        /// <summary>
        ///   Creates the coefficients to be used when calculating
        ///   the approximate Jacobian by using finite differences.
        /// </summary>
        /// 
        private double[][,] CreateCoefficients(int points)
        {
            double[][,] coefficients = new double[points][,];

            for (int i = 0; i < points; i++)
            {
                double[,] delts = new double[points, points];

                for (int j = 0; j < points; j++)
                {
                    double delt = (double)(j - i);
                    double hterm = 1.0;

                    for (int k = 0; k < points; k++)
                    {
                        delts[j, k] = hterm / Math.Tools.Factorial(k);
                        hterm *= delt;
                    }
                }

                coefficients[i] = Matrix.Inverse(delts);
                double dNumPointsFactorial = Math.Tools.Factorial(points);

                for (int j = 0; j < points; j++)
                {
                    for (int k = 0; k < points; k++)
                    {
                        coefficients[i][j, k] = (System.Math.Round(coefficients[i][j, k] * dNumPointsFactorial, MidpointRounding.AwayFromZero)) / dNumPointsFactorial;
                    }
                }
            }

            return coefficients;
        }

        /// <summary>
        ///   Computes the derivative of the network in respect to the
        ///   weight passed as parameter.
        /// </summary>
        private double ComputeDerivative(double[] inputs,
            int layer, int neuron, int weight,
            ref double stepSize, double networkOutput)
        {
            int numPoints = differentialCoefficients.Length;
            double ret = 0.0;
            double originalValue;

            // Saves a copy of the original value in the neuron
            if (weight >= 0) originalValue = network[layer][neuron][weight];
            else originalValue = network[layer][neuron].Threshold;

            double[] points = new double[numPoints];

            if (originalValue != 0.0)
                stepSize = derivativeStep * System.Math.Abs(originalValue);
            else stepSize = derivativeStep;

            int centerPoint = (numPoints - 1) / 2;

            for (int i = 0; i < numPoints; i++)
            {
                if (i != centerPoint)
                {
                    double newValue = originalValue + ((double)(i - centerPoint)) * stepSize;

                    if (weight >= 0) network[layer][neuron][weight] = newValue;
                    else network[layer][neuron].Threshold = newValue;

                    points[i] = network.Compute(inputs)[0];
                }
                else
                {
                    points[i] = networkOutput;
                }
            }

            ret = 0.0;
            for (int i = 0; i < differentialCoefficients.Length; i++)
            {
                ret += differentialCoefficients[centerPoint][1, i] * points[i];
            }

            ret /= System.Math.Pow(stepSize, 1);


            // Changes back the modified value
            if (weight >= 0) network[layer][neuron][weight] = originalValue;
            else network[layer][neuron].Threshold = originalValue;

            return ret;
        }
        #endregion

    }
}
