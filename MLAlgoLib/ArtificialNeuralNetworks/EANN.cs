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



     private List<MonoObjectiveEOALib.Range> _SearchRanges;

     public List<MonoObjectiveEOALib.Range> SearchRanges { get { return _SearchRanges;} }


     private Stopwatch Chronos;
     public long ComputationDuration
         {
             get {if (!Equals(Chronos, null))
              {return Chronos.ElapsedMilliseconds;} 
              else {return 0;}
              }
                  
         }


     DataSerie1D _BestChart; 
          
     public DataSerie1D BestChart {get {return _BestChart;}}

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
        if (CheckData()) { return;}
                Initialize();



     }
            private int _MinLearningIterations=1;
            public int MinLearningIterations
            {
                get { return _MinLearningIterations; }
                set { _MinLearningIterations = Math.Max(1, value); }
            }

            private int _MaxLearningIterations=1;
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




            private void Initialize()
      {

          switch(this.Learning_Algorithm)

               {
                    case LearningAlgorithmEnum.BackPropagationLearning:
                        
                    _SearchRanges = new List<MonoObjectiveEOALib.Range>
                    {
                    new MonoObjectiveEOALib.Range("Activation Function",0.8, 2.4),
                    new MonoObjectiveEOALib.Range("Alpha of Activation Function", 0.2, 5),
                    new MonoObjectiveEOALib.Range("Learning rate", 0.1, 1),
                    new MonoObjectiveEOALib.Range("Learning Err", 0.01, 1),
                    new MonoObjectiveEOALib.Range("Max Iteration (Kmax)", MinLearningIterations, MaxLearningIterations),
                    new MonoObjectiveEOALib.Range("Hiden Layer Number", 1, 5),
                    new MonoObjectiveEOALib.Range("Layer 1 Nodes count", MinHidenNeuronesCount, MaxHidenNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 2 Nodes count", MinHidenNeuronesCount, MaxHidenNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 3 Nodes count", MinHidenNeuronesCount, MaxHidenNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 4 Nodes count", MinHidenNeuronesCount, MaxHidenNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 5 Nodes count", MinHidenNeuronesCount, MaxHidenNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 6 Nodes count", MinHidenNeuronesCount, MaxHidenNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 7 Nodes count", MinHidenNeuronesCount, MaxHidenNeuronesCount)
                     };

                        break;
                    case LearningAlgorithmEnum.LevenbergMarquardtLearning:
                        throw new NotImplementedException();
                        break;
                    case LearningAlgorithmEnum.BayesianLevenbergMarquardtLearning:
                        throw new NotImplementedException();
                        break;
                    default:
                        this.Learning_Algorithm = LearningAlgorithmEnum.LevenbergMarquardtLearning;
                        Initialize();
                        break;

                }
      }

    public override void Compute(double[][] inputs)
     {
        
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