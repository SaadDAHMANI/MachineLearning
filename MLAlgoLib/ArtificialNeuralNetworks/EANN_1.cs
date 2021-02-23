using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
//using Accord.Neuro;
//using Accord.Neuro.Learning;
using IOOperations;

using MonoObjectiveEOALib;

namespace MLAlgoLib
{

namespace ArtificialNeuralNetwork
{

public class EANN_1
{
    
            //---------------------Optimizers-------------------------
            GSA_Optimizer GsaOptim ;
           RGA_Optimizer GaOptim;
           GWO_Optimizer GwoOptim;
                
            OptimizationAlogrithmEnum optimAlgo = OptimizationAlogrithmEnum.GA_Optimizer;
            public OptimizationAlogrithmEnum OptimizationAlogrithm
            {
                get { return optimAlgo; }
                set { optimAlgo = value; }
            }
                      

            public Stopwatch Chronos = new Stopwatch();
           long mComputationDuration = 0;
            public long ComputationDuration
            {
                get { return mComputationDuration; }
            }
            //----------------------------------------------

            private LearningAlgorithmEnum mLearning_Algorithm = LearningAlgorithmEnum.BackPropagationLearning;
            public LearningAlgorithmEnum Learning_Algorithm
            {
                get { return mLearning_Algorithm; }
                set { mLearning_Algorithm = value; }
            }

            DataSerieTD mObs_TrainingInputs;
            /// <summary>
            /// Observed Training Inputs.
            /// </summary>
            public DataSerieTD Obs_TrainingInputs
            {
                get { return mObs_TrainingInputs; }
                set { mObs_TrainingInputs = value; }
            }

            DataSerie1D mObs_TrainingOutputs;
           /// <summary>
           /// Observed Training Outputs.
           /// </summary>
            public DataSerie1D Obs_TrainingOutputs
            {
                get { return mObs_TrainingOutputs; }
                set { mObs_TrainingOutputs = value; }
            }

            DataSerie1D mComputed_TrainingOutputs;
            /// <summary>
            /// Computed Training outputs by the Neural Network. 
            /// </summary>
            public DataSerie1D Computed_TrainingOutputs
            { get { return mComputed_TrainingOutputs; } }

            DataSerieTD mObs_TestingInputs;
            /// <summary>
            /// Observed testing inputs.
            /// </summary>
            public DataSerieTD Obs_TestingInputs
            {
                get { return mObs_TestingInputs; }
                set {
                    mObs_TestingInputs = value;
                    mObs_Testing_Inputs = DataSerieTD.Convert(value);
                }
            }

            DataSerie1D mObs_TestingOutputs;
            public DataSerie1D Obs_TestingOutputs
            {
                get { return mObs_TestingOutputs;}
                set 
                {
                    mObs_TestingOutputs = value;
                    mObs_Testing_Outputs = value.GetArray1D();
                }
            }

           // DataSerieTD mTestingOutputs;
            public DataSerie1D ComputedTestingOutputs
            {
                get { return DataSerie1D.Convert(Computed_Testing_Outputs); }
            }

            int mMaxIterations = 100;
             public int MaxIterations
            {
                get { return mMaxIterations; }
                set { mMaxIterations = value; }
            }

            int ANN_Input_Count=1;
            public int ANN_Inputs_Count
            { get { return ANN_Input_Count;}}

            int ANN_Output_Count = 1;
            public int ANN_Outputs_Count
            { get { return ANN_Output_Count; } }

            int mPopulationSize = 30;
            public int PopulationSize
            {
                get { return mPopulationSize; }
                set { mPopulationSize = value; }
            }

            private double[][] mObs_Training_Inputs;
            double[][] Obs_Training_Inputs
            {
                get { return mObs_Training_Inputs; }
                //set { mTraining_Inputs = value; }
            }

            double[][] mObs_Training_Outputs;
            double[][] Obs_Training_Outputs
            {
                get { return mObs_Training_Outputs; }
                //set { mTraining_Outputs = value; }
            }

            double[][] mComputed_Training_Outputs;
            double [][] Computed_Training_Outputs
            { get { return mComputed_Training_Outputs;} }

            double[][] mObs_Testing_Inputs;
            double[][] Obs_Testing_Inputs
            {
                get { return mObs_Testing_Inputs; }
                //set { mTraining_Inputs = value; }
            }

            double[] mObs_Testing_Outputs;
            double[] Obs_Testing_Outputs
            {
                get { return mObs_Testing_Outputs; }
                //set { mTraining_Inputs = value; }
            }


            double[][] mComputed_Testing_Outputs;
            double[][] Computed_Testing_Outputs
            {
                get { return mComputed_Testing_Outputs; }
                //set { mTraining_Outputs = value; }
            }
            
            DataSerie1D mLearningErr_Result;
            public DataSerie1D LearningErr_Result
            {
                get { return mLearningErr_Result; }
                set { mLearningErr_Result = value; }
            }

            public DataSerie1D LearningIteration_Result;
            public DataSerie1D Best_Chart;

            NeuralNetworkEngineEO BestNeuralNetwork;
            public NeuralNetworkEngineEO Best_NeuralNetwork
            { get { return BestNeuralNetwork; } }

            DataSerie1D mBest_Solution;
            public   DataSerie1D Best_Solution
            {
                get { return mBest_Solution; }
                set { mBest_Solution = value; }
            }
            double BestComputedErr = double.MaxValue;           

            public Dictionary<string, double> Couple_Solution_Fitness=null;

           [Category("ANNs Parameters")] public int MinNeuronesCount { get; set; } = 1;
            [Category("ANNs Parameters")] public int MaxNeuronesCount { get; set; } = 15;
            [Category("Learning Parameters")] public int MinLearningIterations { get; set; } = 40;
            [Category("Learning Parameters")] public int MaxLearningIterations { get; set; } = 70;

            [Category("GSA Parameters")] public double GSA_Alpha { get; set; } = 20;
            [Category("GSA Parameters")] public double GSA_G0 { get; set; } = 100;

            [Category("HPSOGWO Parameters")] public double HPSOGWO_C1 { get; set; } = 0.5;
            [Category("HPSOGWO Parameters")] public double HPSOGWO_C2 { get; set; } = 0.5;
            [Category("HPSOGWO Parameters")] public double HPSOGWO_C3 { get; set; } = 0.5;

            //[Category("GWO Parameters")] public GWOVersionEnum GWOVersion { get; set; } = GWOVersionEnum.StandardGWO;
            //[Category("GWO Parameters")] public double IGWO_uParameter { get; set; } = 1.1;


            private void Initialize()
            {
                List<MonoObjectiveEOALib.Range> intervales;

                switch (this.Learning_Algorithm)
                {
                    case LearningAlgorithmEnum.BackPropagationLearning :
                        intervales = new List<MonoObjectiveEOALib.Range>
                    {
                    new MonoObjectiveEOALib.Range("Activation Function",0.8, 2.4),
                    new MonoObjectiveEOALib.Range("Alpha of Activation Function", 0.2, 5),
                    new MonoObjectiveEOALib.Range("Learning rate", 0.1, 1),
                    new MonoObjectiveEOALib.Range("Learning Err", 0.01, 1),
                    new MonoObjectiveEOALib.Range("Max Iteration (Kmax)", MinLearningIterations , MaxLearningIterations),
                    new MonoObjectiveEOALib.Range("Hiden Layer Number", 1, 5),
                    new MonoObjectiveEOALib.Range("Layer 1 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 2 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 3 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 4 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 5 Nodes count", MinNeuronesCount, MaxNeuronesCount)
                    //new MonoObjectiveEOALib.Range("Layer 6 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    //new MonoObjectiveEOALib.Range("Layer 7 Nodes count", MinNeuronesCount, MaxNeuronesCount)
                     };
                        break;
                        
                    case LearningAlgorithmEnum.LevenbergMarquardtLearning:
                     intervales = new List<MonoObjectiveEOALib.Range>
                    {
                    new MonoObjectiveEOALib.Range("Activation Function",0.8, 2.4),
                    new MonoObjectiveEOALib.Range("Alpha of Activation Function", 0.2, 5),
                    new MonoObjectiveEOALib.Range("Learning rate", 0.1, 1),
                    new MonoObjectiveEOALib.Range("Learning Err", 0.01, 1),
                    new MonoObjectiveEOALib.Range("Max Iteration (Kmax)", MinLearningIterations , MaxLearningIterations),
                    new MonoObjectiveEOALib.Range("Hiden Layer Number", 1, 5),
                    new MonoObjectiveEOALib.Range("Layer 1 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 2 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 3 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 4 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 5 Nodes count", MinNeuronesCount, MaxNeuronesCount)
                    //new MonoObjectiveEOALib.Range("Layer 6 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    //new MonoObjectiveEOALib.Range("Layer 7 Nodes count", MinNeuronesCount, MaxNeuronesCount)
                     };
                       break;

                    case LearningAlgorithmEnum.EvolutionaryLearningGA:
                        intervales = new List<MonoObjectiveEOALib.Range>
                    {
                    new MonoObjectiveEOALib.Range("Activation Function",0.8, 2.4),
                    new MonoObjectiveEOALib.Range("Alpha of Activation Function", 0.2, 5),
                    new MonoObjectiveEOALib.Range("Population size", 30, 120),
                    new MonoObjectiveEOALib.Range("Learning Err", 0.01, 1),
                    new MonoObjectiveEOALib.Range("Max Iteration (Kmax)", MinLearningIterations , MaxLearningIterations),
                    new MonoObjectiveEOALib.Range("Hiden Layer Number", 1, 5),
                    new MonoObjectiveEOALib.Range("Layer 1 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 2 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 3 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 4 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 5 Nodes count", MinNeuronesCount, MaxNeuronesCount)
                    //new MonoObjectiveEOALib.Range("Layer 6 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    //new MonoObjectiveEOALib.Range("Layer 7 Nodes count", MinNeuronesCount, MaxNeuronesCount)
                        };
                        break;

                    case LearningAlgorithmEnum.HPSOGWO_Learning:
                        intervales = new List<MonoObjectiveEOALib.Range>
                    {
                    new MonoObjectiveEOALib.Range("Activation Function",0.8, 2.4),
                    new MonoObjectiveEOALib.Range("Alpha of Activation Function", 0.2, 5),
                    new MonoObjectiveEOALib.Range("Population size", 30, 120),
                    new MonoObjectiveEOALib.Range("C1", 0.05, 5),
                    new MonoObjectiveEOALib.Range("C2", 0.05, 5),
                    new MonoObjectiveEOALib.Range("C3", 0.05, 5),
                    new MonoObjectiveEOALib.Range("Learning Err", 0.01, 1),
                    new MonoObjectiveEOALib.Range("Max Iteration (Kmax)", MinLearningIterations , MaxLearningIterations),
                    new MonoObjectiveEOALib.Range("Hiden Layer Number", 1, 4),
                    new MonoObjectiveEOALib.Range("Layer 1 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 2 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 3 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 4 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    new MonoObjectiveEOALib.Range("Layer 5 Nodes count", MinNeuronesCount, MaxNeuronesCount)
                    //new MonoObjectiveEOALib.Range("Layer 6 Nodes count", MinNeuronesCount, MaxNeuronesCount),
                    //new MonoObjectiveEOALib.Range("Layer 7 Nodes count", MinNeuronesCount, MaxNeuronesCount)
                        };
                        break;

                    default:
                        throw new NotImplementedException();                       
                }

                //gsaOpim.Intervalles.Add(new MonoObjectiveEOALib.Range("Additional Params (Population zize for GA", 5, 20));
          
                if (optimAlgo ==OptimizationAlogrithmEnum.GSA_Optimizer)
                {
                    throw new NotImplementedException();
                    //GsaOptim = new GSA_Optimizer();
                   // GsaOptim.Alpha = GSA_Alpha;
                    //GsaOptim.G0 = GSA_G0;
                   // GsaOptim.D_Dimensions = GetSearchSpaceDimension(Learning_Algorithm);
                    //GsaOptim.MaxIterations = MaxIterations ;
                    //GsaOptim.N_Agents = PopulationSize;
                    //GsaOptim.OptimizationType = RmosEngine.OptimizationTypeEnum.Minimization;
                    //GsaOptim.ElitistCheck = RmosEngine.ElitistCheckEnum.Equation21;
                                      
                    // Set intervalles :
                    //GsaOptim.Intervalles = intervales;
 }
                else if (optimAlgo == OptimizationAlogrithmEnum.GA_Optimizer)
                {
                     throw new NotImplementedException();

                   // GaOptim = new GAOptimizer();
                   // GaOptim.GenomesLenght = GetSearchSpaceDimension(this.Learning_Algorithm);
                   // GaOptim.InitialPopulation = this.PopulationSize;
                   // GaOptim.MaxIteration = this.MaxIterations; ;
                   // GaOptim.PopulationLimit = this.PopulationSize;
                   // GaOptim.DeathFitnessLimit = 10 ^ -6;
                   // GaOptim.Save_2_BestCrossover = true;
                   // GaOptim.Intervalles = intervales;
                    //GaOptim.MutationFrequency = 0.2f;
                    
                    
                }
                else if(optimAlgo == OptimizationAlogrithmEnum.HPSOGWO_Optimizer)
                {
                     throw new NotImplementedException();

                    //HpsoGwoOptim = new HybridPSOGWOptimizer();
                    //HpsoGwoOptim.C_1 = HPSOGWO_C1;
                    //HpsoGwoOptim.C_2 = HPSOGWO_C2;
                    //HpsoGwoOptim.C_3 = HPSOGWO_C3;
                   // HpsoGwoOptim.Dimensions = GetSearchSpaceDimension(this.Learning_Algorithm);
                   // HpsoGwoOptim.OptimizationType = RmosEngine.OptimizationTypeEnum.Minimization;
                    //HpsoGwoOptim.WolvesCount = PopulationSize;
                   // HpsoGwoOptim.Intervalles = intervales;
                    //HpsoGwoOptim.IterationsCount = MaxIterations;
                   
                }
                else if (optimAlgo == OptimizationAlogrithmEnum.GWO__Optimizer)
                {
                     throw new NotImplementedException();
                   // GwoOptim = new GWOptimizer();
                   // GwoOptim.GWOVersion = GWOVersion;
                   // GwoOptim.IGWO_uParameter = IGWO_uParameter;
                    //GwoOptim.Dimensions= GetSearchSpaceDimension(this.Learning_Algorithm);
                    //GwoOptim.OptimizationType = RmosEngine.OptimizationTypeEnum.Minimization;
                   // GwoOptim.WolvesCount = PopulationSize;
                    //GwoOptim.Intervalles = intervales;
                   // GwoOptim.IterationsCount = MaxIterations;

                }
                //Initialize results series :
                LearningErr_Result = new DataSerie1D();
                LearningIteration_Result = new DataSerie1D();
                Best_Chart = new DataSerie1D();
                Best_Solution = new DataSerie1D();
                
            }

            private int GetSearchSpaceDimension(LearningAlgorithmEnum LearningAlgo)
            {
                int dimension = 13;
                switch (LearningAlgo)
                {
                    case LearningAlgorithmEnum.BackPropagationLearning :
                        dimension = 10;
                        break;
                    case LearningAlgorithmEnum.LevenbergMarquardtLearning:
                        dimension = 10;
                        break;
                    case LearningAlgorithmEnum.EvolutionaryLearningGA:
                        dimension = 10;
                        break;
                    case LearningAlgorithmEnum.HPSOGWO_Learning | LearningAlgorithmEnum.mHPSOGWO_Learning:
                        dimension = 13;
                        break;
                    default:
                        dimension = 13;
                        break;
                 }
                return dimension;
            }

            public void Compute()
            {
                try
                {                 // Step 1 : Standerize Data and get Input data;
                    StandardizeData(Obs_TrainingInputs);
                    StandardizeData(Obs_TrainingOutputs);
                    StandardizeData(mObs_TestingInputs);

                    mObs_Training_Inputs = DataSerieTD.Convert(Obs_TrainingInputs);
                    mObs_Training_Outputs = Obs_TrainingOutputs.GetArray();

                    ANN_Input_Count = mObs_Training_Inputs[0].Length;
                    ANN_Output_Count = mObs_Training_Outputs[0].Length;
                    //---------------Starting :----------------------------
                    Chronos.Start();
                    // Step 2 : Initialize optimizer :
                    Initialize();
                    //------------Optimization------------------
                    switch (optimAlgo)
                    {
                        case OptimizationAlogrithmEnum.GA_Optimizer:
                          throw new NotImplementedException();
                            break;
                        case OptimizationAlogrithmEnum.GSA_Optimizer:
                            throw new NotImplementedException();
                            break;
                        case OptimizationAlogrithmEnum.GWO__Optimizer:
                           throw new NotImplementedException();
                            break;
                        case OptimizationAlogrithmEnum.HPSOGWO_Optimizer:
                           throw new NotImplementedException();

                            // Set Optimization function :
                            //HpsoGwoOptim.ObjectiveFunctionComputation += HpsoGwoOptim_ObjectiveFunctionComputation;
                            //Computation:
                            //HpsoGwoOptim.LuanchComputation();
                            //----------------------------------------------------------------------
                            //CopySolution(HpsoGwoOptim.BestSolution);

                            //for (int j = 0; j < HpsoGwoOptim.BestChart.Count; j++)
                           // {
                           //     Best_Chart.Add(j.ToString(), HpsoGwoOptim.BestChart[j]);
                           // }
                           
                            break;
                    }

                    Chronos.Stop();
                    mComputationDuration = Chronos.ElapsedMilliseconds;

                    //--Compute Training and Testing Outputs :
                    if (Equals(BestNeuralNetwork, null)) {return;}
                    if (Equals(this.Obs_Training_Inputs, null)) {return;}
                    if (Equals(this.mObs_Testing_Inputs, null)) { return; }

                    mComputed_TrainingOutputs = DataSerie1D.Convert(BestNeuralNetwork.Compute(this.Obs_Training_Inputs));
                    mComputed_Testing_Outputs = BestNeuralNetwork.Compute(this.mObs_Testing_Inputs);
                    
                     //-----------------------------

                }
                catch (Exception ex) { throw ex; }
            }
            private void CopySolution(double[] dSource)
            {
                if (object.Equals(dSource, null)) { return; }
                if (object.Equals(this.Best_Solution, null)) { this.Best_Solution = new DataSerie1D(); }

                int value = (int)Math.Round(dSource[0]);
                Best_Solution.Add(string.Format("Activation Function : {0}", (ActivationFunctionEnum)value), value);
                Best_Solution.Add("Alpha of Activation Function", dSource[1]);
               switch (Learning_Algorithm)
                {
                    case (LearningAlgorithmEnum.BackPropagationLearning | LearningAlgorithmEnum.LevenbergMarquardtLearning):
                        Best_Solution.Add("Learning Rate", dSource[2]);
                        Best_Solution.Add("Learning Err", dSource[3]);
                        Best_Solution.Add("Max Iterations", Math.Round(dSource[4]));
                        value = (int)Math.Round(dSource[5]);
                        Best_Solution.Add("Hiden Layers Count", value);

                        for (int i = 6; i < (value + 5); i++)
                        {
                            Best_Solution.Add(string.Format("Layer {0} Nodes count", (i - 5).ToString()), Math.Round(dSource[i]));
                        }
                        break;

                    case LearningAlgorithmEnum.EvolutionaryLearningGA:

                        Best_Solution.Add("Population size", dSource[2]);
                        Best_Solution.Add("Learning Err", dSource[3]);
                        Best_Solution.Add("Max Iterations", Math.Round(dSource[4]));
                        value = (int)Math.Round(dSource[5]);
                        Best_Solution.Add("Hiden Layers Count", value);

                        for (int i = 6; i < (value + 5); i++)
                        {
                            Best_Solution.Add(string.Format("Layer {0} Nodes count", (i - 5).ToString()), Math.Round(dSource[i]));
                        }
                        break;

                    case (LearningAlgorithmEnum.HPSOGWO_Learning | LearningAlgorithmEnum.mHPSOGWO_Learning):
                        Best_Solution.Add("Population size", dSource[2]);
                        Best_Solution.Add("C1", dSource[3]);
                        Best_Solution.Add("C2", dSource[4]);
                        Best_Solution.Add("C3", dSource[5]);
                        Best_Solution.Add("Learning Err", dSource[6]);
                        Best_Solution.Add("Max Iterations", Math.Round(dSource[7]));
                        value = (int)Math.Round(dSource[8]);
                        Best_Solution.Add("Hiden Layers Count", value);

                        for (int i = 9; i < (value + 8); i++)
                        {
                            Best_Solution.Add(string.Format("Layer {0} Nodes count", (i - 8).ToString()), Math.Round(dSource[i]));
                        }
                        break;
                }
           
            }

            private void CopySolution(List<double> dSource)
            {
                if (object.Equals(dSource, null)) { return; }
                if (object.Equals(this.Best_Solution, null)) { this.Best_Solution = new DataSerie1D(); }

                int value = (int)Math.Round(dSource[0]);
                Best_Solution.Add(string.Format("Activation Function : {0}", (ActivationFunctionEnum)value), value);
                Best_Solution.Add("Alpha of Activation Function", dSource[1]);
                switch (Learning_Algorithm)
                {
                    case (LearningAlgorithmEnum.BackPropagationLearning | LearningAlgorithmEnum.LevenbergMarquardtLearning):
                        Best_Solution.Add("Learning Rate", dSource[2]);
                        Best_Solution.Add("Learning Err", dSource[3]);
                        Best_Solution.Add("Max Iterations", Math.Round(dSource[4]));
                        value = (int)Math.Round(dSource[5]);
                        Best_Solution.Add("Hiden Layers Count", value);

                        for (int i = 6; i < (value + 5); i++)
                        {
                            Best_Solution.Add(string.Format("Layer {0} Nodes count", (i - 5).ToString()), Math.Round(dSource[i]));
                        }
                        break;

                    case LearningAlgorithmEnum.EvolutionaryLearningGA:

                        Best_Solution.Add("Population size", dSource[2]);
                        Best_Solution.Add("Learning Err", dSource[3]);
                        Best_Solution.Add("Max Iterations", Math.Round(dSource[4]));
                        value = (int)Math.Round(dSource[5]);
                        Best_Solution.Add("Hiden Layers Count", value);

                        for (int i = 6; i < (value + 5); i++)
                        {
                            Best_Solution.Add(string.Format("Layer {0} Nodes count", (i - 5).ToString()), Math.Round(dSource[i]));
                        }
                        break;

                    case (LearningAlgorithmEnum.HPSOGWO_Learning | LearningAlgorithmEnum.mHPSOGWO_Learning):
                        Best_Solution.Add("Population size", dSource[2]);
                        Best_Solution.Add("C1", dSource[3]);
                        Best_Solution.Add("C2", dSource[4]);
                        Best_Solution.Add("C3", dSource[5]);
                        Best_Solution.Add("Learning Err", dSource[6]);
                        Best_Solution.Add("Max Iterations", Math.Round(dSource[7]));
                        value = (int)Math.Round(dSource[8]);
                        Best_Solution.Add("Hiden Layers Count", value);

                        for (int i = 9; i < (value + 8); i++)
                        {
                            Best_Solution.Add(string.Format("Layer {0} Nodes count", (i - 8).ToString()), Math.Round(dSource[i]));
                        }
                        break;
                }
            }                

            private void SetLearningAlgoParams(ref double[] positions)
            {
                try
                { 
                switch (Learning_Algorithm)
                {
                    case LearningAlgorithmEnum.BackPropagationLearning:

                        AnnEo.ActivationFunction = (ActivationFunctionEnum)(int)positions[0];
                        AnnEo.ActiveFunction_Alpha = positions[1];

                        AnnEo.LearningRate = positions[2];

                        AnnEo.TeachingError = positions[3];
                        AnnEo.MaxIterationCount = (int)positions[4];

                        hidenLayerCount =Math.Max((int)positions[5],1);

                        if (hidenLayerCount > 0)
                        {                            
                            int[] networkStruct = new int[(hidenLayerCount + 1)];
                            networkStruct[hidenLayerCount] = 1;

                            for (int j = 0; j < hidenLayerCount; j++)
                            {
                                networkStruct[j] = Math.Max((int)positions[(j + 6)],1);
                            }
                            for (int k = (hidenLayerCount + 6); k < positions.Length; k++)
                            { positions[k] = 0; }

                            AnnEo.LayersStruct = networkStruct;
                        }
                        break;

                    case LearningAlgorithmEnum.LevenbergMarquardtLearning:

                        AnnEo.ActivationFunction = (ActivationFunctionEnum)(int)positions[0];
                        AnnEo.ActiveFunction_Alpha = positions[1];

                        AnnEo.LearningRate = positions[2];

                        AnnEo.TeachingError = positions[3];
                        AnnEo.MaxIterationCount = (int)positions[4];

                        hidenLayerCount = Math.Max((int)positions[5],1);

                        if (hidenLayerCount > 0)
                        {
                            int[] networkStruct = new int[(hidenLayerCount + 1)];
                            networkStruct[hidenLayerCount] = 1;

                            for (int j = 0; j < hidenLayerCount; j++)
                            {
                                networkStruct[j] =Math.Max((int)positions[(j + 6)],1);
                            }
                            for (int k = (hidenLayerCount + 6); k < positions.Length; k++)
                            { positions[k] = 0; }

                            AnnEo.LayersStruct = networkStruct;
                        }
                        break;

                    case LearningAlgorithmEnum.EvolutionaryLearningGA:
                        AnnEo.ActivationFunction = (ActivationFunctionEnum)(int)positions[0];
                        AnnEo.ActiveFunction_Alpha = positions[1];

                        AnnEo.EOA_PopulationSize = (int)positions[2];

                        AnnEo.TeachingError = positions[3];
                        AnnEo.MaxIterationCount = (int)positions[4];

                        hidenLayerCount = (int)positions[5];

                        if (hidenLayerCount > 0)
                        {
                            int[] networkStruct = new int[(hidenLayerCount + 1)];
                            networkStruct[hidenLayerCount] = 1;

                            for (int j = 0; j < hidenLayerCount; j++)
                            {
                                networkStruct[j] = (int)positions[(j + 6)];
                            }
                            for (int k = (hidenLayerCount + 6); k < positions.Length; k++)
                            { positions[k] = 0; }

                            AnnEo.LayersStruct = networkStruct;
                        }
                         break;
                    case LearningAlgorithmEnum.HPSOGWO_Learning:
                        AnnEo.ActivationFunction = (ActivationFunctionEnum)(int)positions[0];
                        AnnEo.ActiveFunction_Alpha = positions[1];

                        AnnEo.EOA_PopulationSize = (int)positions[2];
                        AnnEo.HPSOGWO_C1 = positions[3];
                        AnnEo.HPSOGWO_C2 = positions[4];
                        AnnEo.HPSOGWO_C3 = positions[5];

                        AnnEo.TeachingError = positions[6];
                        AnnEo.MaxIterationCount = (int)positions[7];

                        hidenLayerCount = (int)positions[8];

                        if (hidenLayerCount > 0)
                        {
                            int[] networkStruct = new int[(hidenLayerCount + 1)];
                            networkStruct[hidenLayerCount] = 1;

                            for (int j = 0; j < hidenLayerCount; j++)
                            {
                                networkStruct[j] = (int)positions[(j + 9)];
                            }
                            for (int k = (hidenLayerCount + 9); k < positions.Length; k++)
                            { positions[k] = 0; }

                            AnnEo.LayersStruct = networkStruct;
                        }
                        break;
                    default:
                            throw new NotImplementedException() ;
                        break;
                }
                 }
                catch (Exception ex) {throw ex;}
            }

            private void HpsoGwoOptim_ObjectiveFunctionComputation(ref double[] positions, ref double fitnessValue)
            {
                fitnessValue = 0;

                AnnEo = new NeuralNetworkEngineEO();
                AnnEo.LearningAlgorithm = Learning_Algorithm;
                //AnnEo.InputsCount = Training_Input_Count; 
                AnnEo.Training_Inputs = this.Obs_Training_Inputs;
                AnnEo.Training_Outputs = this.Obs_Training_Outputs;

                SetLearningAlgoParams(ref positions);

                AnnEo.LuanchLearning();

                // Compute testing error (for fitness computing)
                var testingOut = GetFirstColumn(AnnEo.Compute(this.Obs_Testing_Inputs));

                double testingFitness = -2;
                if (!Equals(testingOut, null))
                {
                    testingFitness = Statistics.Compute_DeterminationCoeff_R2(testingOut, mObs_Testing_Outputs);
                    if (Double.IsInfinity(testingFitness)) { testingFitness = -2; }
                }

                //-----------------------------------------------------------------------------------------
                Debug.Print("Testing R2 = {0}", testingFitness);
                //fitnessValue = AnnEo.FinalTeachingError;

                fitnessValue = ((0.01 * (AnnEo.LayersStruct.Length - 1)) + 1) * (AnnEo.FinalTeachingError + Math.Abs(1 - testingFitness));


                //Objectve function without penality:
                //fitnessValue = AnnEo.FinalTeachingError;

                //Objectve function with penality :
                //fitnessValue = ((0.01*(AnnEo.LayersStruct.Length - 1))+1)* AnnEo.FinalTeachingError ;

                if (fitnessValue < BestComputedErr) 
                {
                    BestComputedErr = fitnessValue;
                    BestNeuralNetwork = AnnEo;
                    //mComputed_TrainingOutputs = DataSerie1D.Convert(AnnEo.Compute(this.Obs_Training_Inputs));
                    //mComputed_Testing_Outputs  = AnnEo.Compute(mObs_Testing_Inputs);
                }
            }

            NeuralNetworkEngineEO AnnEo;
            int hidenLayerCount=0;
            DataSerie1D LayersStruct;

           public static double[] GetFirstColumn(double[][] dataset)
            {
                if (Equals(dataset, null)) { return null;}
                int count = dataset.Length;
                double[] result = new double[count];
                for(int i=0; i<count; i++)
                {
                    if (Equals(dataset[i], null)) { return null;}
                    else { result[i] = dataset[i][0]; }
                    
                }
                return result;
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
           
            public void StandardizeData(DataSerieTD dseries, ActivationFunctionEnum activeFunction)
            {
                if (Equals(dseries, null)) { return; }
                if (Equals(dseries.Data, null)) { return; }

                bool doStand = false;

                switch (activeFunction)
                {
                    case ActivationFunctionEnum.SigmoidFunction:

                        foreach (DataItemTD ditem in dseries.Data)
                        {
                            foreach (double value in ditem.List)
                            {
                                if ((value < 0) || (value > 1))
                                {
                                    doStand = true;
                                    break;
                                }
                            }
                            if (doStand) { break; }
                        }

                        if (doStand)
                        {
                            double[] mins = dseries.Min;
                            double[] maxs = dseries.Max;

                            int jCount = mins.Count();

                            foreach (DataItemTD ditem in dseries.Data)
                            {
                                for (int j = 0; j < jCount; j++)
                                {
                                    ditem.List[j] = (ditem.List[j] - mins[j]) / (maxs[j] - mins[j]);
                                }
                            }
                        }

                        break;
                    case ActivationFunctionEnum.BipolarSigmoidFunction:
                        break;
                    case ActivationFunctionEnum.LinearFunction:
                        break;
                }
            }

            /// <summary>
            /// Standardize Data between [0, 1]. 
            /// </summary>
            /// <param name="dseries"></param>
            public void StandardizeData(DataSerieTD dseries)
            {
                if (Equals(dseries, null)) { return; }
                if (Equals(dseries.Data, null)) { return; }

                bool doStand = false;

                foreach (DataItemTD ditem in dseries.Data)
                        {
                            foreach (double value in ditem.List)
                            {
                                if ((value < 0) || (value > 1))
                                {
                                    doStand = true;
                                    break;
                                }
                            }
                            if (doStand) { break; }
                        }

                        if (doStand)
                        {
                            double[] mins = dseries.Min;
                            double[] maxs = dseries.Max;

                            int jCount = mins.Count();

                            foreach (DataItemTD ditem in dseries.Data)
                            {
                                for (int j = 0; j < jCount; j++)
                                {
                                    ditem.List[j] = (ditem.List[j] - mins[j]) / (maxs[j] - mins[j]);
                                }
                            }
                        }
                                             
                }
            
            public void StandardizeData(DataSerie1D dseries, ActivationFunctionEnum activeFunction)
            {
                if (Equals(dseries, null)) { return; }
                if (Equals(dseries.Data, null)) { return; }

                bool doStand = false;

                switch (activeFunction)
                {
                    case ActivationFunctionEnum.SigmoidFunction:

                        foreach (DataItem1D ditem in dseries.Data)
                        {
                            if ((ditem.X_Value < 0) || (ditem.X_Value > 1))
                            {
                                doStand = true;
                                break;
                            }
                            if (doStand) { break; }
                        }

                        if (doStand)
                        {
                            double mins = dseries.Min;
                            double maxs = dseries.Max-mins;

                            foreach (DataItem1D ditem in dseries.Data)
                            {
                                ditem.X_Value = (ditem.X_Value - mins) / maxs;
                            }
                        }

                        break;
                    case ActivationFunctionEnum.BipolarSigmoidFunction:
                        break;
                    case ActivationFunctionEnum.LinearFunction:
                        break;
                }
            }
            
            /// <summary>
            /// Standardize Data between [0, 1]. 
            /// </summary>
            /// <param name="dseries"></param>
            public void StandardizeData(DataSerie1D dseries)
            {
                if (Equals(dseries, null)) { return; }
                if (Equals(dseries.Data, null)) { return; }

                   bool doStand = false;
                   foreach (DataItem1D ditem in dseries.Data)
                        {
                            if ((ditem.X_Value < 0) || (ditem.X_Value > 1))
                            {
                                doStand = true;
                                break;
                            }
                            if (doStand) { break; }
                        }

                   if (doStand)
                        {
                            double mins = dseries.Min;
                            double maxs = dseries.Max - mins;

                            foreach (DataItem1D ditem in dseries.Data)
                            {
                                ditem.X_Value = (ditem.X_Value - mins) / maxs;
                            }
                        }
                                            
                }

            private int[] GetLayersStruct(DataSerie1D ds1)
            {
                int iCount = 2;
                int[] result = null;

                if ((object.Equals(ds1, null)) || (object.Equals(ds1.Data, null)))
                {
                    result = new int[iCount];
                    result[0] = this.ANN_Inputs_Count ;
                    result[1] = this.ANN_Outputs_Count ;
                }
                else
                {
                    iCount = ds1.Data.Count + 1;

                    result = new int[iCount];

                    for (int i = 0; i < (iCount - 1); i++)
                    {
                        result[i] = (Int32)Math.Round(ds1.Data[i].X_Value, 0);
                    }

                    result[(iCount - 1)] = this.ANN_Outputs_Count;
                }
                return result;
            }

        
        }        
    
}

}
