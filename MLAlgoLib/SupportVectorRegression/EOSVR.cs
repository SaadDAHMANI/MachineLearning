using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Accord;
using Accord.IO;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math;
using Accord.Statistics.Kernels;
using IOOperations;
using MonoObjectiveEOALib;

namespace MLAlgoLib
{

namespace SupportVectorRegression
{

public class EOSVR
{
    public EOSVR() {}
    public EOSVR(double[][] trainingIn, double[] trainingOut)
    {
    LearningInputs=trainingIn;
    LearningOutputs=trainingOut;
    }

    public EOSVR(double[][] trainingIn, double[] trainingOut, double[][] testingIn, double[] testingOut)
    {
    LearningInputs=trainingIn;
    LearningOutputs=trainingOut;
    TestingInputs=testingIn;
    TestingOutputs=testingOut;
    }
     Gaussian kernelG;
      IKernel kernel;  
     static SupportVectorMachine<IKernel> svm;

     public double[][] LearningInputs {get; set;}
     public double[] LearningOutputs {get; set;}
     public double[][] TestingInputs {get; set;}
     public double[] TestingOutputs{get; set;}
     private double[] _Computed_LearningOutputs;
     public double[] Computed_LearningOutputs {get {return _Computed_LearningOutputs;}}
     
     private double[] _Computed_TestingOutputs;
     public double[] Computed_TestingOutputs{get {return _Computed_TestingOutputs;}}

     private int _MaxIterations;
     public int MaxIterations 
     { get {return _MaxIterations;}
       set {_MaxIterations=Math.Max(0, value);}
     }
     
     private int _PopulationSize;
     public int PopulationSize 
     { get {return _PopulationSize;}
       set {_PopulationSize =Math.Max(2, value);}
     }

     public double[][] SupportVectorsWeights{
         get 
         {
           // Show support vectors on the Support Vectors tab page
            if(Equals(svm, null))
            { return null;}
            if(Equals(svm.SupportVectors, null))
            { return null;}
            if(Equals(svm.Weights,null))
            {return null;}

              return svm.SupportVectors.InsertColumn(svm.Weights);
         }}

     // SVM Params
     public double Param_Complexity {get; set;}=2;   

     public double Param_Tolerance {get; set;} = 0.001;

     public double Param_Epsilon {get; set;} =0.001;

     public KernelEnum UseKernel{get; set;}  = KernelEnum.Gaussian;
   
     double sigmaKernel;
     public double Sigma_Kernel 
     {
         get {return sigmaKernel;}
        set{sigmaKernel=value;}
     } 

     private  void InitilizeKernel()
     {
         // Create the specified Kernel
         switch(UseKernel)
         {
            case KernelEnum.Gaussian:
             kernel = new Accord.Statistics.Kernels.Gaussian(sigmaKernel);
            break;
            case KernelEnum.Polynomial:
            kernel= new Accord.Statistics.Kernels.Polynomial();            
            break;
            default:
             throw new NotImplementedException();
            break;
         }

     }   
     
     public void Learn()
     {
         if (Equals(LearningInputs, null)){return;}
         if (Equals(LearningOutputs, null)){return;}

         //Set kernal params :
         UseKernel= KernelEnum.Gaussian;   
         InitilizeKernel();
         
         // Creates a new SMO for regression learning algorithm
         var teacher = new SequentialMinimalOptimizationRegression()
         {
             // Set learning parameters
             Complexity = Param_Complexity,
             Tolerance = Param_Tolerance,
             Epsilon = Param_Epsilon,
             Kernel = kernel
         };

         // Use the teacher to create a machine
             svm = teacher.Learn(LearningInputs, LearningOutputs);

            // Check if we got support vectors
            if (svm.SupportVectors.Length == 0)
            {
                Console.WriteLine("Sorry, No SVMs.");
                return;
            }

            // Compute results for learning and testing data
            _Computed_LearningOutputs =svm.Score(LearningInputs);

            //foreach (double[] itm in TestingInputs)
            //{
            //    foreach (double value in itm)
            //    {
            //        Console.Write(value);
            //    }
            //    Console.WriteLine("");
            //}

            _Computed_TestingOutputs=svm.Score(TestingInputs);
            
            // foreach (double value in _Computed_TestingOutputs)
            //{
            //   Console.WriteLine(value);
            //}

            // Compute statistical results            

           BestLearningScore  = Statistics.Compute_DeterminationCoeff_R2(LearningOutputs, _Computed_LearningOutputs);            
           BestTestingScore= Statistics.Compute_DeterminationCoeff_R2(TestingOutputs, _Computed_TestingOutputs);     
                
           }
            
            public double[] Compute(double[][] inputs)
            {
                if (Equals(svm, null)) { return null;}

                return svm.Score(inputs);

            }

        private double _BestScore; 
        public double BestScore
        {get {return _BestScore;}}

       public List<double> BestChart 
            {
                get
                {
                    if (!Equals(Optimizer, null)) 
                    {
                        if (Equals(Optimizer.BestChart, null)) { return null; }
                        else { return Optimizer.BestChart; }
                    }
                    else
                    { return null; }
                }
            }

            private double[] _BestSolution;
        public double[] BestSolution
        {get {return _BestSolution;}}
        
        EvolutionaryAlgoBase Optimizer;

       private SequentialMinimalOptimizationRegression teacherSMOR ;
        public void LearnEO()
        {

         if (Equals(LearningInputs, null)){return;}
         if (Equals(LearningOutputs, null)){return;}

         //Set kernal params :
         UseKernel= KernelEnum.Gaussian; 
         if (Equals(kernel, null))
         { kernelG = new Gaussian(sigmaKernel); }
         else
         { kernelG.Sigma=sigmaKernel;}

         teacherSMOR = new SequentialMinimalOptimizationRegression();
         teacherSMOR.Kernel=kernelG;
         teacherSMOR.UseComplexityHeuristic= true;
         teacherSMOR.UseKernelEstimation=false;
        
         // Space dimension :must 4.
         int D=4;   
        
        List<MonoObjectiveEOALib.Range> ranges = new List<MonoObjectiveEOALib.Range>();
        ranges.Add(new MonoObjectiveEOALib.Range(0.1, 10)); //Sigma of Gaussian
        ranges.Add(new MonoObjectiveEOALib.Range(1, 500)); // Complexity
        ranges.Add(new MonoObjectiveEOALib.Range(0.001, 0.001)); // Tolerance        
        ranges.Add(new MonoObjectiveEOALib.Range(0.001, 0.05)); // Epsilon

        Optimizer= new PSOGSA_Optimizer(PopulationSize,D,ranges,MaxIterations);
        Optimizer.ObjectiveFunction += Optimizer_ObjectiveFunction;  

        Optimizer.Compute();        
   
        _BestScore=Optimizer.BestScore;    
        _BestSolution=Optimizer.BestSolution;   
        }


double LearningIndex, TestingIndex;
public double BestLearningScore=double.MinValue;
public double BestTestingScore=double.MinValue; 
     
    public void Optimizer_ObjectiveFunction(double[] solution, ref double fitnessValue)
     {

         Console.WriteLine(Optimizer.CurrentIteration);

         //Set kernal params :
         kernelG.Sigma=solution[0]  ;
                     
         // Set paramsfor regression learning algorithm
         teacherSMOR.Complexity=solution[1];
         teacherSMOR.Tolerance=solution[2];
         teacherSMOR.Epsilon=solution[3];
        
         // Use the teacher to create a machine
             svm = teacherSMOR.Learn(LearningInputs, LearningOutputs);

            // Check if we got support vectors
            if (svm.SupportVectors.Length == 0)
            {
                Console.WriteLine("Sorry, No SVMs.");
                return;
            }

            // Compute results for learning and testing data
            _Computed_LearningOutputs =svm.Score(LearningInputs);
            _Computed_TestingOutputs=svm.Score(TestingInputs);

            // Compute statistical 
            LearningIndex = Statistics.Compute_RMSE(LearningOutputs,_Computed_LearningOutputs);            
            TestingIndex =Statistics.Compute_RMSE(TestingOutputs, _Computed_TestingOutputs);

            // Compute correlation R for learning and testing to controle results :
              var Rlern  = Statistics.Compute_CorrelationCoeff_R(LearningOutputs,_Computed_LearningOutputs); 
              var Rtest = Statistics.Compute_CorrelationCoeff_R(TestingOutputs, _Computed_TestingOutputs);  
        
            Console.WriteLine("indexL= {0} | indexT= {1} ; Correlation : R (learn) = {2} | R (test) = {3}", LearningIndex, TestingIndex, Rlern,Rtest);
            if (BestLearningScore<LearningIndex && BestTestingScore < TestingIndex)
            {
             BestLearningScore=LearningIndex;
             BestTestingScore=TestingIndex;
             }
            //set the fitness value
            fitnessValue=Math.Pow(LearningIndex,2)+ Math.Pow(TestingIndex,2);   
 
     }

            public static double[][] ConvertToJagged(double[] vector)
            {
                if (Equals(vector, null)) { return null; }

                double[][] matrix = new double[vector.Length][];

                for (int i = 0; i < vector.Length; i++)
                {
                    matrix[i] = new double[] { vector[i] };
                }

                return matrix;
            }
        }
    public enum KernelEnum
    {
        Gaussian,
        Polynomial
    }
}

}