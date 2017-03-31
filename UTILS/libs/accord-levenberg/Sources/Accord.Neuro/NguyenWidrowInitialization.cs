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
using AForge;

namespace Accord.Neuro
{

    /// <summary>
    ///   Nguyen-Widrow weight initializer
    /// </summary>
    /// <remarks>
    ///   The Nguyen-Widrow initialization algorithm chooses values in
    ///   order to distribute the active region of each neuron in the
    ///   layer approximately evenly across the layer's input space.
    ///   
    ///   The values contain a degree of randomness, so they are not the
    ///   same each time this function is called.
    /// </remarks>
    public class NguyenWidrowInitializer
    {
        private ActivationNetwork network;
        private DoubleRange randRange;
        private double beta;

        public NguyenWidrowInitializer(ActivationNetwork network)
        {
            this.network = network;

            int hiddenNodes = network[0].NeuronsCount;
            int inputNodes = network[0].InputsCount;

            randRange = new DoubleRange(-0.5, 0.5);
            beta = 0.7 * System.Math.Pow(hiddenNodes, 1.0 / inputNodes);
        }

        public void Randomize()
        {
            Neuron.RandRange = randRange;
            
            for (int i = 0; i < network.LayersCount; i++)
            {
                for (int j = 0; j < network[i].NeuronsCount; j++)
                {
                    ActivationNeuron neuron = network[i][j];
                    neuron.Randomize();
                    double norm = 0.0;

                    // Calculate the Euclidean Norm for the weights
                    for (int k = 0; k < neuron.InputsCount; k++)
                    {
                        norm += neuron[k] * neuron[k];
                    }
                    norm += neuron.Threshold * neuron.Threshold;
                    norm = System.Math.Sqrt(norm);

                    // Rescale the weights using beta and the norm
                    for (int k = 0; k < neuron.InputsCount; k++)
                    {
                        neuron[k] = beta * neuron[k] / norm;
                    }
                    neuron.Threshold = beta * neuron.Threshold / norm;
                }
            }
        }
    
    }
}
