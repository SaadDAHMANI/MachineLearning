using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Accord.Neuro;
using Accord.Neuro.Learning;
using IOOperations;

namespace MLAlgoLib
{
namespace ArtificialNeuralNetwork
{

        [Serializable]
        public class NeuralNetworkEngineEO
        {

            /// <summary>
            /// The Activation Function.
            /// </summary>
            [Category("Activation Function Parameters")] public ActivationFunctionEnum ActivationFunction { get; set; }

            /// <summary>
            /// Alpha parameter for activation function (Sigmoid, Bipolar sigmoid, ...etc.)
            /// </summary>
            [Category("Activation Function Parameters")] public double[] ActiveFunction_Params { get; set;}
                       
            [Category("Learning Algorithm Parameters")] public LearningAlgorithmEnum LearningAlgorithm { get; set; }
                         
            int IterationCounter = 0;
            public int FinalIterationsCount
            {
                get { return IterationCounter; }
            }

            int MaxIteration = 3;
            public int MaxIterationCount
            {
                get { return MaxIteration; }
                set { MaxIteration = value; }
            }

            double FinalTeachingErr = double.NaN;
            public double FinalTeachingError
            {
                get { return FinalTeachingErr; }
            }

            double mTeachingError = 0.01;
            public double TeachingError
            {
                get { return mTeachingError; }
                set { mTeachingError = value; }
            }
          
            public int ANN_InputsCount
            {
                get { 
                    if (Equals(mTraining_Inputs, null)) { return -1; }
                    else { return mTraining_Inputs[0].Length;}
                  
                }
             }

            public int ANN_OuputsCount
            {
                get { 
                    if (Equals(mTraining_Outputs, null)) { return -1; }
                    else { return mTraining_Outputs[0].Length;}
                   
                }
                
            }

            double[][] mTraining_Inputs;
            public double[][] Training_Inputs
            {
                get { return mTraining_Inputs; }
                set { mTraining_Inputs = value; }
            }

            double[][] mTraining_Outputs;
            public double[][] Training_Outputs
            {
                get { return mTraining_Outputs; }
                set { mTraining_Outputs = value; }
            }

            int[] networkStruct = null;
            public int[] LayersStruct
            {
                get { return networkStruct; }
                set { networkStruct = value; }
            }
            ActivationNetwork Network = null;

            private List<double> BestChart;
            public List<double> Best_Chart
            { get { return BestChart; } }

            private double[] BestWeights;
            public double[] Best_Weights
            { get { return BestWeights; } }

            public void LuanchLearning()
            {
                try
                {
                    //if (object.Equals(networkStruct, null)) { networkStruct = GetLayersStruct(LayersStruct); }

                    if (Equals(networkStruct, null)) { return; }
                    if (ANN_InputsCount==-1 | ANN_OuputsCount==-1) { return; }

                    // create neural network
                    // Network = new ActivationNetwork(new SigmoidFunction(1),mInputsCount, networkStruct);
                    //Network = new ActivationNetwork(new BipolarSigmoidFunction(2), mInputsCount, networkStruct);
                    //2  :  two inputs in the network
                    // 2  : two neurons in the first layer
                    // 1  : one neuron in the second layer

                    switch (ActivationFunction)
                    {
                        case ActivationFunctionEnum.LinearFunction:
                            Network = new ActivationNetwork(new LinearFunction(ActiveFunction_Alpha), ANN_InputsCount, networkStruct);
                            break;

                        case ActivationFunctionEnum.SigmoidFunction:
                            Network = new ActivationNetwork(new SigmoidFunction(ActiveFunction_Alpha), ANN_InputsCount, networkStruct);
                            break;
                        case ActivationFunctionEnum.BipolarSigmoidFunction:
                            Network = new ActivationNetwork(new BipolarSigmoidFunction(ActiveFunction_Alpha), ANN_InputsCount, networkStruct);
                            break;

                        default:
                            Network = new ActivationNetwork(new SigmoidFunction(ActiveFunction_Alpha), ANN_InputsCount, networkStruct);
                            break;

                    }

                    // create teacher
                    ISupervisedLearning teacher = null;

                    //LevenbergMarquardtLearning teacher = new LevenbergMarquardtLearning(Network);
                    //BackPropagationLearning teacher = new BackPropagationLearning(Network);
                    // EvolutionaryLearning teacher = new EvolutionaryLearning(Network, 25);

                    switch (LearningAlgorithm)
                    {
                        case LearningAlgorithmEnum.BackPropagationLearning:
                            teacher = new BackPropagationLearning(Network);
                            var teacherBP = (BackPropagationLearning)teacher;
                            teacherBP.LearningRate = this.LearningRate;
                            teacher = teacherBP; 
                            break;

                        case LearningAlgorithmEnum.LevenbergMarquardtLearning:
                            teacher = new LevenbergMarquardtLearning(Network);
                            var teacherLM = (LevenbergMarquardtLearning)teacher;
                            teacherLM.LearningRate=this.LearningRate;
                            teacher = teacherLM;
                            break;

                        case LearningAlgorithmEnum.EvolutionaryLearningGA:
                            teacher = new EvolutionaryLearning(Network, EOA_PopulationSize) ;

                            break;
                        case LearningAlgorithmEnum.RGA_Learning:
                            throw new NotImplementedException();
                            // teacher = new RGA_Learning(Network, EOA_PopulationSize, RGA_MutationPhrequency);
                            break;
                        case LearningAlgorithmEnum.GSA_Learning:
                            throw new NotImplementedException();
                           // teacher = new GSA_Learning(Network, EOA_PopulationSize, MaxIteration, GSA_Go, GSA_Alpha);
                            break;
                        case LearningAlgorithmEnum.GWO_Learning:
                            throw new NotImplementedException();
                           // teacher = new GWO_Learning(Network, EOA_PopulationSize, MaxIteration, GWO_Version, IGWO_uParameter);
                            break;

                        case LearningAlgorithmEnum.HPSOGWO_Learning:
                            throw new NotImplementedException();
                            //teacher = new HPSOGWO_Learning(Network, EOA_PopulationSize, MaxIteration, HPSOGWO_C1, HPSOGWO_C2, HPSOGWO_C3);
                            break;

                        case LearningAlgorithmEnum.mHPSOGWO_Learning:
                            throw new NotImplementedException();
                            //teacher = new HPSOGWO_Learning(Network, EOA_PopulationSize, MaxIteration, HPSOGWO_C1, HPSOGWO_C2, HPSOGWO_C3);
                            break;
                        case LearningAlgorithmEnum.PSOGSA_Learning:
                            teacher = new PSOGSA_Learning(Network, EOA_PopulationSize, MaxIteration,PSOGSA_Go,PSOGSA_Alpha,PSOGSA_C1,PSOGSA_C2);
                            break;
                            
                    }

                    bool needToStop = false;
                    IterationCounter = 0;
                    double error = double.NaN;

                    // loop
                    while (!needToStop)
                    {
                        // run epoch of learning procedure
                        error = teacher.RunEpoch(mTraining_Inputs, mTraining_Outputs);

                        IterationCounter += 1;

                        // check error value to see if we need to stop
                        // ...
                        //Console.WriteLine(error);

                        if (error <= mTeachingError || IterationCounter >= MaxIteration)
                        {
                            needToStop = true;
                        }

                    }

                    FinalTeachingErr = error;
                    //----------------------------------
                    switch (LearningAlgorithm)
                    {
                        case LearningAlgorithmEnum.GSA_Learning:
                            throw new NotImplementedException();

                            //GSA_Learning gsaL = (GSA_Learning)teacher;
                            //this.BestChart = gsaL.Best_Chart;
                            //this.BestWeights = gsaL.BestSolution;

                            //Set best weights parameters to the network:
                            //SetBestWeightsToTheNetwork();
                            break;

                        case LearningAlgorithmEnum.HPSOGWO_Learning:
                            throw new NotImplementedException();

                            //HPSOGWO_Learning hpgwoL = (HPSOGWO_Learning)teacher;
                            //this.BestChart = hpgwoL.Best_Chart;
                            //this.BestWeights = hpgwoL.BestSolution;

                            //Set best weights parameters to the network:
                            //SetBestWeightsToTheNetwork();
                            break;

                        case LearningAlgorithmEnum.mHPSOGWO_Learning:
                            throw new NotImplementedException();
                            
                            //HPSOGWO_Learning hpsgwoL = (HPSOGWO_Learning)teacher;
                            //this.BestChart = hpsgwoL.Best_Chart;
                            //this.BestWeights = hpsgwoL.BestSolution;

                            //Set best weights parameters to the network:
                            //SetBestWeightsToTheNetwork();
                            break;

                        case LearningAlgorithmEnum.GWO_Learning:
                            throw new NotImplementedException();
                            
                            //GWO_Learning gwoL = (GWO_Learning)teacher;
                            //this.BestChart = gwoL.Best_Chart;
                            //this.BestWeights = gwoL.BestSolution;

                            //Set best weights parameters to the network:
                            //SetBestWeightsToTheNetwork();
                            break;

                        case LearningAlgorithmEnum.RGA_Learning:
                            throw new NotImplementedException();
                            //RGA_Learning rgaL = (RGA_Learning)teacher;
                            //this.BestChart = rgaL.Best_Chart;
                            //this.BestWeights = rgaL.BestSolution;

                            //Set best weights parameters to the network: 
                            //SetBestWeightsToTheNetwork();
                            break;

                        case LearningAlgorithmEnum.PSOGSA_Learning:
                            PSOGSA_Learning psogsaL = (PSOGSA_Learning)teacher;
                            BestChart = psogsaL.Best_Chart;
                            BestWeights = psogsaL.BestSolution;

                            //Set best weights parameters to the network: 
                            SetBestWeightsToTheNetwork();
                            break;
                    }

                }
                catch (Exception ex)
                { throw ex; }

            }
            
            /// <summary>
            /// Set the best solution (weights) to the network.
            /// </summary>
            private void SetBestWeightsToTheNetwork()
            {
                if (Equals(Network, null)) { return; }
                if (Equals(BestWeights, null)) { return; }

                //Copy the weights to the network: 
                double[] chromosomeGenes = BestWeights;
                // put best chromosome's value into neural network's weights
                int v = 0;

                for (int i = 0; i < Network.Layers.Length; i++)
                {
                    Layer layer = Network.Layers[i];

                    for (int j = 0; j < layer.Neurons.Length; j++)
                    {
                        ActivationNeuron neuron = layer.Neurons[j] as ActivationNeuron;

                        for (int k = 0; k < neuron.Weights.Length; k++)
                        {
                            neuron.Weights[k] = chromosomeGenes[v++];
                        }
                        neuron.Threshold = chromosomeGenes[v++];
                    }
                }


            }

            public double[] Compute(double[] input)
            {
                double[] result = null;
                if (object.Equals(Network, null) == false)
                {
                    result = Network.Compute(input);

                }
                return result;
            }

            public double[][] Compute(double[][] inputs)
            {

                int iCount = inputs.GetLength(0);
                double[][] allResults = new double[(iCount)][];

                if (object.Equals(Network, null) == false)
                {
                    for (int i = 0; i < iCount; i++)
                    {

                        double[] input = inputs[i];
                        var result = Network.Compute(input);
                        allResults[i] = result;
                    }
                }
                return allResults;
            }

            public bool SaveNeuralNetwork(string fileName)
            {
                bool result = false;
                try
                {
                    if (Equals(Network, null)) { throw new NullReferenceException("No Neural Network existed");}
                    if (File.Exists(fileName) == false) 
                    {
                        Stream stream = new FileStream(fileName, FileMode.CreateNew);
                        stream.Close();
                    }
                    Network.Save(fileName);
                    result = true;
                }
                catch (Exception ex)
                { throw ex; }
                return result;
            }
            public bool LoadNeuralNetwork(string fileName)
            {
                bool result = false;
                try
                {
                    if (File.Exists (fileName ))
                    { 
                    FileStream fileStrem = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    IFormatter formatter = new BinaryFormatter();
                    Network = (ActivationNetwork)formatter.Deserialize(fileStrem);
                    result = true;
                    }
                }
                catch (Exception ex) { throw ex; }
                return result;
            }
            
            public bool Save(string fileName)
            {
                bool result = false;
                try
                { 
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                formatter.Serialize(stream, this);
                stream.Close();
                result = true;
                }
                catch (Exception ex) { throw ex; }
                return result;
            }

            public static NeuralNetworkEngineEO Load(string fileName)
            {
                NeuralNetworkEngineEO resultEoNet =null;
                try
                {
                    if (File.Exists(fileName))
                    {
                        FileStream fileStrem = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                        IFormatter formatter = new BinaryFormatter();
                        resultEoNet = (NeuralNetworkEngineEO)formatter.Deserialize(fileStrem);
                    }

                }
                catch(Exception ex){ throw ex; }
                return resultEoNet;
            }
            /// <summary>
            /// The value determines speed of learning. Default value is 0.1. 
            /// </summary>
            [Category("Shared BP & LM Parameters")] public double LearningRate { get; set; } = 0.1;

            [Category("EOAs Shared Parameters")] public int EOA_PopulationSize { get; set; } = 15;
           
            #region GSA_Params
            [Category("GSA_Parameters")] public double GSA_Alpha { get; set; } = 20;                    
            [Category("GSA_Parameters")] public double GSA_Go { get; set; } = 100;
             #endregion

            #region HPSOGWO_Params
                       
            [Category("HPSOGWO_Parameters")] public double HPSOGWO_C1 { get; set; } = 0.5;           
            [Category("HPSOGWO_Parameters")] public double HPSOGWO_C2 { get; set; } = 0.5;
            [Category("HPSOGWO_Parameters")] public double HPSOGWO_C3 { get; set; } = 0.5;
           
            #endregion

            #region RGA_Params
            [Category("RGA_Parameters")] public float RGA_MutationPhrequency { get; set; } = 0.05f;
            #endregion

            #region GWO_Params
            //[Category("GWO_Parameters")] public GWOVersionEnum GWO_Version { get; set; }=GWOVersionEnum.StandardGWO;
            //[Category("GWO_Parameters")] public double IGWO_uParameter { get; set; } = 1.1;
            #endregion

            #region PSOGSA_Params
            [Category("PSOGSA_Parameters")] public double PSOGSA_C1 { get; set; } = 0.5;
            [Category("PSOGSA_Parameters")] public double PSOGSA_C2 { get; set; } = 1.5;
            [Category("PSOGSA_Parameters")] public double PSOGSA_Alpha { get; set; } = 23;
            [Category("PSOGSA_Parameters")] public double PSOGSA_Go { get; set; } = 1;
            #endregion

        }



}
}