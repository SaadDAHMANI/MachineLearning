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
    [Category("Learning Algorithm Parameters")] public LearningAlgorithmEnum Learning_Algorithm {get; set;}

            /// <summary>
            /// The Defaullt is set to Sigmoid Function.
            /// </summary>
            [Category("Learning Algorithm Parameters")] public ActivationFunctionEnum DefaultActivationFunction { get; set; }

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

                _BestNeuralNetwork.Training_Inputs = this.LearningInputs;
                _BestNeuralNetwork.Training_Outputs = ConvertToJagged(this.LearningOutputs);

                /// Step 2 : set ANN's structure-
                _BestNeuralNetwork.LayersStruct = GetLayersStruct(hidenLayerStructure, this.LearningInputs[0].Length, 1);
     
                _BestNeuralNetwork.ActivationFunction= 
            }

            public override void Learn()
            {
                if (Equals(HidenLayerStructure,null)) { return;}
                Learn(HidenLayerStructure);
            }
            public override void LearnEO()
     {
        
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