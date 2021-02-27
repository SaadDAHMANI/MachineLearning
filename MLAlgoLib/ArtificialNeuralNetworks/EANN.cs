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

    }
    [Category("Learning Algorithm Parameters")] public LearningAlgorithmEnum Learning_Algorithm {get; set;}

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

    public void Learn(DataSerie1D hidenLayerStructure )
     {
      
                if (CheckData() == false) { return                                                                                 _è}
                _BestNeuralNetwork = new NeuralNetworkEngineEO();

                // Step 1 : Standerize Data and get Input data;


                _BestNeuralNetwork.Training_Inputs = this.LearningInputs;
                _BestNeuralNetwork.Training_Outputs = ConvertToJagged(this.LearningOutputs);
                _BestNeuralNetwork.LayersStruct = GetLayersStruct(hidenLayerStructure, this.LearningInputs[0].Length, 1);
     
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

            private int[] GetLayersStruct(DataSerie1D ds1, int inputsCount, int ouputsCount)
            {
                int iCount = 2;
                int[] result = null;

                if ((object.Equals(ds1, null)) || (object.Equals(ds1.Data, null)))
                {
                    result = new int[iCount];
                    result[0] = inputsCount;
                    result[1] = ouputsCount;
                }
                else
                {
                    iCount = ds1.Data.Count + 1;

                    result = new int[iCount];

                    for (int i = 0; i < (iCount - 1); i++)
                    {
                        result[i] = (Int32)Math.Round(ds1.Data[i].X_Value, 0);
                    }

                    result[(iCount - 1)] = ouputsCount;

                }
                return result;
            }




        }

    }
}