using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using IOOperations;
using MonoObjectiveEOALib;

namespace MLAlgoLib
{

namespace ArtificialNeuralNetwork
{

public class EANN: EvolutionaryMLBase
{
    public EANN()
    {
       this.Chronos = new Stopwatch();
       this.Learning_Algorithm= LearningAlgorithmEnum.LevenbergMarquardtLearning;
       this.DefaultActivationFunction = ActivationFunctionEnum.SigmoidFunction;
    }

     public EANN(double[][] trainingIn, double[] trainingOut)
      {
                this.Chronos = new Stopwatch();
                this.Learning_Algorithm = LearningAlgorithmEnum.LevenbergMarquardtLearning;
                this.DefaultActivationFunction = ActivationFunctionEnum.SigmoidFunction;
                this.LearningInputs = trainingIn;
                this.LearningOutputs = trainingOut;
      }

    public EANN(double[][] trainingIn, double[] trainingOut, double[][] testingIn, double[] testingOut)
     {
      this.Chronos = new Stopwatch();
      this.Learning_Algorithm = LearningAlgorithmEnum.LevenbergMarquardtLearning;
      this.DefaultActivationFunction = ActivationFunctionEnum.SigmoidFunction;
                this.LearningInputs = trainingIn;
                this.LearningOutputs = trainingOut;
                this.TestingInputs = testingIn;
                this.TestingOutputs = testingOut;
     }

     [Category("Learning Algorithm Parameters")] public LearningAlgorithmEnum Learning_Algorithm {get; set;}

     /// <summary>
     /// The Defaullt is set to Sigmoid Function.
     /// </summary>
     [Category("Learning Algorithm Parameters")] public ActivationFunctionEnum DefaultActivationFunction { get; set; }

     [Category("Learning Algorithm Parameters")] public double[] DefaultActiveFunction_Params { get; set;}

     MonoObjectiveEOALib.EvolutionaryAlgoBase Optimizer;

     public List<MonoObjectiveEOALib.Range> SearchRanges { get; set;}

     private Stopwatch Chronos;
     public long ComputationDuration
         {
             get {if (!Equals(Chronos, null))
              {return Chronos.ElapsedMilliseconds;} 
              else {return 0;}
              }
                  
         }


     //DataSerie1D _BestChart;

     private double _BestScore = double.MaxValue;
     public override double BestScore { get { return _BestScore; }}

     private double  _BestLearningScore = double.NaN;
     public double BestLearningScore { get { return _BestLearningScore;}}

     private double _BestTestingScore = double.NaN;
     public double BestTestingScore { get { return _BestTestingScore; } }

     public override List<double> BestChart { get { return Optimizer.BestChart;} }

     public double[] BestSolution {get {return Optimizer.BestSolution;}}   

     public DataSerie1D HidenLayerStructure { get; set;}

       NeuralNetworkEngineEO _BestNeuralNetwork;
     public NeuralNetworkEngineEO BestNeuralNetwork
       {get {return _BestNeuralNetwork;}}

            private bool CheckData()
            {
                if (Equals(this.LearningInputs, null)){ return false;}
                if (Equals(this.LearningOutputs, null)) { return false;}
                if (Equals(this.TestingInputs, null)) { return false; }
                if (Equals(this.TestingOutputs, null)) { return false; }
                return true;
            }

            /// <summary>
            /// The data must be standarize before learning.
            /// </summary>
            /// <param name="hidenLayerStructure"></param>
      public void Learn(DataSerie1D hidenLayerStructure )
     {
                // Step 0 : Check data
                if (CheckData() == false) { return; }
                _BestNeuralNetwork = new NeuralNetworkEngineEO();

                // Step 1 : Standerize Data and get Input data;
                //------------------------------------------------

                _BestNeuralNetwork.Learning_Inputs = this.LearningInputs;
                _BestNeuralNetwork.Learning_Outputs = ConvertToJagged(this.LearningOutputs);

                /// Step 2 : set ANN's structure, activation function and params, 
                _BestNeuralNetwork.LayersStruct = GetLayersStruct(hidenLayerStructure, this.LearningInputs[0].Length, 1);

                _BestNeuralNetwork.ActivationFunction = DefaultActivationFunction;
                _BestNeuralNetwork.LearningAlgorithm_Params = DefaultActiveFunction_Params;

                _BestNeuralNetwork.Learn();
                
            }

      public override void Learn()
    {
     if (Equals(HidenLayerStructure,null)) { return;}
     Learn(HidenLayerStructure);
    }
      public override void LearnEO()
       {
                if (!CheckData()) { return;}

                InitializeSearchRanges();

                Optimizer = new MonoObjectiveEOALib.PSOGSA_Optimizer();
                
                Optimizer.Dimensions_D = GetSearchSpaceDimension(this.Learning_Algorithm);
                Optimizer.MaxIterations = this.MaxOptimizationIterations;
                Optimizer.PopulationSize_N = this.PopulationSize;
                Optimizer.SearchRanges = this.SearchRanges;

                Optimizer.ObjectiveFunction += Optimizer_ObjectiveFunction;

                if (Equals(Chronos, null)) { Chronos = new Stopwatch(); }

                Chronos.Reset();
                Chronos.Start();

                Optimizer.Compute();
                
                Chronos.Stop();                                    
            
            }
            NeuralNetworkEngineEO neuralNet;
            double LearningIndexScore, TestingIndexScore;

            private void Optimizer_ObjectiveFunction(double[] positions, ref double fitnessValue)
            {
                neuralNet = new NeuralNetworkEngineEO(LearningInputs, LearningOutputs);
                neuralNet.LearningAlgorithm = this.Learning_Algorithm;

                SetLearningAlgoParams(ref positions);

                neuralNet.Learn();

                double[] computedLearningOutputs = GetArray(neuralNet.Compute(LearningInputs));
                double[] computedTestingOutputs = GetArray(neuralNet.Compute(TestingInputs));

                // Compute statistical params for the objective function:
                LearningIndexScore = Statistics.Compute_RMSE(LearningOutputs, computedLearningOutputs);
                TestingIndexScore = Statistics.Compute_RMSE(TestingOutputs, computedTestingOutputs);

              // Compute correlation R for learning and testing to controle results :
              var Rlern  = Statistics.Compute_CorrelationCoeff_R(LearningOutputs, computedLearningOutputs); 
              var Rtest = Statistics.Compute_CorrelationCoeff_R(TestingOutputs, computedTestingOutputs);  
        
              Console.WriteLine("Index (learn)= {0} | Index (test)= {1} ; Correlation : R (learn) = {2} | R (test) = {3}", LearningIndexScore, TestingIndexScore, Rlern,Rtest);
       
               fitnessValue = Math.Pow(LearningIndexScore,2) + Math.Pow(TestingIndexScore, 2);

                //fitnessValue = ((0.01 * (neuralNet.LayersStruct.Length - 1)) + 1) * fitnessValue;
                              
                if (fitnessValue < BestScore )
                {
                    _BestScore = fitnessValue;
                    _BestNeuralNetwork = neuralNet;
                    _BestLearningScore = LearningIndexScore;
                    _BestTestingScore = TestingIndexScore;
                }

            }


            int HidenLayerCount = 1;
            private void SetLearningAlgoParams(ref double[] positions)
            {
                switch (Learning_Algorithm)
                {
                    case LearningAlgorithmEnum.BackPropagationLearning | LearningAlgorithmEnum.LevenbergMarquardtLearning:

                        neuralNet.ActivationFunction = (ActivationFunctionEnum)Convert.ToInt32(positions[0]);
                        neuralNet.ActiveFunction_Params = new double[] { positions[1] };
                        neuralNet.LearningAlgorithm_Params = new double[] { positions[2], positions[3]};
                        neuralNet.LearningError = positions[4];
                        neuralNet.LearningMaxIterations = Convert.ToInt32(positions[5]);

                        HidenLayerCount = Math.Max((int)positions[6], 1);

                        int[] networkStruct = new int[(HidenLayerCount + 1)];
                        networkStruct[HidenLayerCount] = 1;

                        for (int j = 0; j < HidenLayerCount; j++)
                        {
                            networkStruct[j] = Math.Max((int)positions[(j + 6)], 1);
                        }
                        for (int k = (HidenLayerCount + 6); k < positions.Length; k++)
                        { positions[k] = 0; }

                        neuralNet.LayersStruct = networkStruct;

                        break;

                    case LearningAlgorithmEnum.BayesianLevenbergMarquardtLearning:
                        throw new NotImplementedException();
                        break;
                    default:
                        this.Learning_Algorithm = LearningAlgorithmEnum.LevenbergMarquardtLearning;
                        SetLearningAlgoParams(ref positions);
                        break;
                }
                
            }

            public static double[] GetArray(double[][] dataset)
            {
                if (Equals(dataset, null)) { return null; }
                int count = dataset.Length;
                double[] result = new double[count];
                for (int i = 0; i < count; i++)
                {
                    if (Equals(dataset[i], null)) { return null; }
                    else { result[i] = dataset[i][0]; }
                }
                return result;
            }

            private int _MinLearningIterations=10;
            public int MinLearningIterations
            {
                get { return _MinLearningIterations; }
                set { _MinLearningIterations = Math.Max(1, value); }
            }

            private int _MaxLearningIterations=100;
            public int MaxLearningIterations
            {
                get { return _MaxLearningIterations; }
                set { _MaxLearningIterations = Math.Max(1, value); }
            }

            private int _MinHidenNeuronesCount=1;
            public int MinHidenNeuronesCount
            {
                get { return _MinHidenNeuronesCount;}
                set { _MinHidenNeuronesCount = Math.Max(1, value); }
            }

            private int _MaxHidenNeuronesCount = 1;
            public int MaxHidenNeuronesCount
            {
                get { return _MaxHidenNeuronesCount; }
                set { _MaxHidenNeuronesCount = Math.Max(1, value); }
            }

            private int GetSearchSpaceDimension(LearningAlgorithmEnum LearningAlgo)
            {
               
                switch (LearningAlgo)
                {
                    case LearningAlgorithmEnum.BackPropagationLearning | LearningAlgorithmEnum.LevenbergMarquardtLearning :
                       return 12;
                        break;
                    case LearningAlgorithmEnum.BayesianLevenbergMarquardtLearning:
                        return 13;
                        break;
                    default:
                        return 13;
                        break;
                }                
            }

       private void InitializeSearchRanges()
        {
          if (!Equals(SearchRanges, null)) { return;}
          switch(this.Learning_Algorithm)
               {                  
                    case LearningAlgorithmEnum.BackPropagationLearning | LearningAlgorithmEnum.LevenbergMarquardtLearning :
                        
                    SearchRanges = new List<MonoObjectiveEOALib.Range>
                     {
                    new MonoObjectiveEOALib.Range("Activation Function",1, 1),
                    new MonoObjectiveEOALib.Range("Alpha of Activation Function", 2, 2),
                    new MonoObjectiveEOALib.Range("Learning rate", 0.1, 0.1),
                    new MonoObjectiveEOALib.Range("Momentum/Ajustement", 10,12),
                    new MonoObjectiveEOALib.Range("Learning Err", 0.001, 0.01),
                    new MonoObjectiveEOALib.Range("Max Iteration (Kmax)", MinLearningIterations, MaxLearningIterations),
                    new MonoObjectiveEOALib.Range("Hiden Layer Number", 1, 5),
                    new MonoObjectiveEOALib.Range("Layer 1 Nodes count", MinHidenNeuronesCount, MaxHidenNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 2 Nodes count", MinHidenNeuronesCount, MaxHidenNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 3 Nodes count", MinHidenNeuronesCount, MaxHidenNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 4 Nodes count", MinHidenNeuronesCount, MaxHidenNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 5 Nodes count", MinHidenNeuronesCount, MaxHidenNeuronesCount)
                     };

                     break;
                    
                    case LearningAlgorithmEnum.BayesianLevenbergMarquardtLearning:
                        throw new NotImplementedException();
                        break;
                    default:
                        this.Learning_Algorithm = LearningAlgorithmEnum.LevenbergMarquardtLearning;
                        InitializeSearchRanges();
                        break;

                }
      }

    public override double[] Compute(double[][] inputs)
     {
        if (Equals(inputs, null)) { return null;}
        if (Equals(BestNeuralNetwork, null)){ return null; }
        return  GetArray(BestNeuralNetwork.Compute(inputs));
     }

    public static double[][] ConvertToJagged(double[] vector)
            {
                if (Equals(vector, null)) { return null;}

                double[][] matrix = new double[vector.Length][];

                for (int i =0; i < vector.Length; i++)
                {
                    matrix[i] = new double[] {vector[i]};
                }

                return matrix;
            }

    public int[] GetLayersStruct(DataSerie1D hidenLayersStructure, int inputsCount, int ouputsCount)
            {
                int iCount = 2;
                int[] result = null;

                if ((object.Equals(hidenLayersStructure, null)) || (object.Equals(hidenLayersStructure.Data, null)))
                {
                    result = new int[iCount];
                    result[0] = inputsCount;
                    result[1] = ouputsCount;
                }
                else
                {
                    iCount = hidenLayersStructure.Data.Count + 1;

                    result = new int[iCount];

                    for (int i = 0; i < (iCount - 1); i++)
                    {
                        result[i] = (Int32)Math.Round(hidenLayersStructure.Data[i].X_Value, 0);
                    }

                    result[(iCount - 1)] = ouputsCount;

                }
                return result;
            }




        }

    }
}