# ades

<pre>
ADES: AUTOMATIC DRIVER EVALUATION SYSTEM

!!!
NOTE: PrologTestGUI can be used for testing Prolog bases Expert System...
!!!

1. Introduction

This document contains the information about the files and 
folders created for the implementation of the applications 
of the ADES project. The detailed information about the 
research topics can be found on the thesis documentation.

The UNREAL Engine, which is a commercial product and not 
provided in this media, is also required for the execution 
of the  simulation environment.


2. The File Structure

The files are arranged into two main folders. The first folder is
the ADES folder where the application files reside. The folder
structure and brief description of the files are as follows.

ADES
|- ADES                   : The ADES Detector C# project
|- ADESTest               : The ADES test C# project
|- AdesUnrealController   : The ADES unreal C#
|- BOUNLib.NET            : The common library which contains the main research topics
|- CSProlog               : The modified version of the CSProlog project
|- imageServer            : The imageServer binaries used for UNREAL Engine detours
|- UnrealFiles            : The UNREAD editor files
|- upisEx                 : The VC++ project for capturing the images from the UNREAL Client
|- ADES.sln               : The Visual Studio 2012 solution file for the entire project.

The second folder contains the supplementary files like training
images, trained neural network description files and the latex
files of the thesis document and the submitted papers to various 
conferences and journals.


UTILS
|- docs			  : The latex files for all materials prepared during the thesis study.
|- libs                   : The archive files for the referenced libraries.
|- resimler
   |- signs		  : The proper sign images for GUI display
   |- train               : The training files grouped by sign numbers
   |- train_circle        : The training files for circular signd grouped by sign numbers
   |- train_triangle      : The training files for triangular signd grouped by sign numbers
   |- all_sign.avi        : The compiled video sequence for sign/lane recognition examples
   |- labels_all_sign.txt : The sign labels for the all_sign video sequence
   |- labels_circle.txt   : The circular sign labels for the all_sign video sequence
   |- labels_triangle.txt : The triangular sign labels for the all_sign video sequence
   |- lda_01.dat          : The calculated LDA features for the training set
   |- model_01.dat        : The SVM model for the training files
   |- model_circle_01.dat : The SVM model for the circular sign training files
   |- nn_12x12_1.dat      : The neural network description for the sign labeled by 1
   |- nn_12x12_12.dat     : The neural network description for the sign labeled by 12
   |- nn_12x12_13.dat     : The neural network description for the sign labeled by 13
   |- nn_12x12_14.dat     : The neural network description for the sign labeled by 14
   |- nn_12x12_2.dat      : The neural network description for the sign labeled by 2
   |- nn_12x12_8.dat      : The neural network description for the sign labeled by 8


3. Project Compilation and Execution

The project binaries can be created by compiling the project 
sources. The ADES.sln file should be opened by Visual Studio 
2012 for the compilation. The ADES, ADESTest, ADESUnrealController 
projects can be used as startup projects.

Prerequisites:
 1- Download and install directx sdk - https://www.microsoft.com/en-us/download/details.aspx?id=6812 (For ADES controller)
 2- Register MSBN3.dll via regsvr32 and add it to the references (For ADES controller)
 3- Register Freeimage.dll (or use nuget) (For ADES controller)
 4- Register upisEx.dll 
 5- Install unreal Tournament for unreal engine hook (For ADES controller)
 


</pre>
