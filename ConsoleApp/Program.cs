//---------------------------------------------------------------------- 
// Required packages :
// dotnet add package Accord.MachineLearning --version 3.8.2-alpha
// dotnet add package Accord.MachineLearning.GPL --version 3.8.2-alpha
// 
// For genetic algorithm:
// dotnet add package Accord.Genetic --version 3.8.0 
//----------------------------------------------------------------------
// Written by : Saad Dahmani (s.dahmani@univ-bouira.dz)
//**********************************************************************

using System;
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
using MLAlgoLib;
using MLAlgoLib.SupportVectorRegression;
using MLAlgoLib.ArtificialNeuralNetwork;

namespace ConsoleApp
{
    class Program
    {       

         static string fileName_DST;
         static DataSerieTD DataSet;
         static DataFormater df;   

        static void Main(string[] args)
        {

            Console.WriteLine("Hello ANN!");



            //Console.WriteLine("Hello SVR!");

            string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\Test_Wail.csv";  

            // //string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\DataSet_Ex.csv";
            // //string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\DataSet_Exemple.csv";

            //string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\DataSet_ExempleSinX.csv";
            
            // //QC_Sidi_Aissa SSL :
            // string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\QC_Sidi_Aissa.csv";
            // //string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\QC_Sidi_Aissa_Clustre_1.csv";

                        
            // //Beni-Bahdel_Dame_3Q :
            // //string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\Beni-Bahdel_Dame_3Q.csv";

            // //Station_Ain_El_Assel_Dataset_Pf(Q) :
            // //string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\Station_Ain_El_Assel_Dataset_Pf(Q).csv";
                        
            LoadData(file);

            df = new DataFormater(DataSet);
            df.TrainingPourcentage = 70;

            df.Format(2,0,1);

            // Console.WriteLine("LearnIn = {0}, LearnOut = {1}, TestIn = {2}, TestOut = {3}",df.TrainingInput.Length, df.TrainingOutput.Length, df.TestingInput.Length, df.TestingOutput.Length );


            //if (!Equals(df.TrainingInput, null)) { Console.WriteLine("Training = {0}", df.TrainingInput.Length); }

            // // Luanch EOSVR with EOAlgo params.   
            int n=2;
            int kmax=2;

            LaunchEOSVR(n,kmax);

            Console.WriteLine("___________________________________________________");

            //LaunchANN(df.TrainingInput, df.TrainingOutput);

            LaunchANNEO(df, n, kmax);

        }

        static void LaunchEOSVR( int popSize, int iterMax)
        {
            if (Equals(DataSet, null) || Equals(df, null)){return;}
            EOSVR eo_svr = new EOSVR(df.TrainingInput, df.TrainingOutput, df.TestingInput, df.TestingOutput);
            
            eo_svr.PopulationSize=popSize;
            eo_svr.MaxIterations=iterMax;

            eo_svr.Sigma_Kernel=1.336687063023768; //1.1; //1.336687063023768
            eo_svr.Param_Complexity = 56.8121658157614; // 100; //56.8121658157614
            eo_svr.Param_Epsilon = 0.001; // 0.001
            eo_svr.Param_Tolerance = 0.001; //0.001

            // Console.WriteLine("---------> SVR : ");
            eo_svr.Learn();    
            
                      
            //Console.WriteLine("---------> EO-SVR : ");
            //eo_svr.LearnEO();

            Console.WriteLine("Best score = {0}", eo_svr.BestScore);
           
         // Console.WriteLine("Best soltion : SigmaKernel= {0}; Complexity_C= {1}; Tolerance= {2}; Epsilon= {3}", eo_svr.BestSolution[0],eo_svr.BestSolution[1],eo_svr.BestSolution[2],eo_svr.BestSolution[3]);
  
            Console.WriteLine("Best learning index = {0} ; Best testing index = {1}", eo_svr.BestLearningScore, eo_svr.BestTestingScore);

            double[][] xy = new double[][] 
            {
                new double [] { 0.8, 0.12 },
               new double []{0.16, 0.10},

                new double [] {0.24, 0.26 },
               new double []{0.35, 0.10}
            };

            var z = eo_svr.Compute(xy);

            if (Equals(z, null)) { return; }
            foreach (double value in z)
            {
                Console.WriteLine("z = {0}", Math.Round( value,3));
            }
        }

        static void LoadDataDST()
        {
            string fileRoot = string.Format("{0}\\DataSource", System.IO.Directory.GetCurrentDirectory());

            string file1 = string.Format("{0}\\{1}", fileRoot, fileName_DST);

            if (System.IO.File.Exists(file1) == false)
            {
                Console.WriteLine("No file [{0}] found ...", file1);
                return;
            }

            CsvFileIO reader1 = new CsvFileIO(file1);
            DataSet = reader1.Read_DST();

            if (Equals(DataSet, null)) { Console.WriteLine("No data set .."); return; }
            if (Equals(DataSet.Data, null)) { Console.WriteLine("No data .."); return; }

            Console.WriteLine(DataSet.ToString());

            var x = DataSet.GetColumn(2);

            foreach (double itm in x)
            { Console.WriteLine(itm); }


            Console.WriteLine("There is {0} records in : {1}", DataSet.GetRowsCount(), DataSet.Name);
        }

        static void LoadData(string file)
        {
            //string fileRoot = string.Format("{0}\\DataSource", System.IO.Directory.GetCurrentDirectory());


            //string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\DataSet_Exemple.csv";    //Console.ReadLine();

            if (System.IO.File.Exists(file) == false)
            {
                Console.WriteLine("No file [{0}] found ...", file);
                return;
            }

            CsvFileIO reader1 = new CsvFileIO(file);
            DataSet = reader1.Read_DST(false, false,true,false);

            if (Equals(DataSet, null)) { Console.WriteLine("No data set .."); return; }
            if (Equals(DataSet.Data, null)) { Console.WriteLine("No data .."); return; }

            Console.WriteLine("There is {0} records in : {1}", DataSet.GetRowsCount(), DataSet.Name);

            //Console.WriteLine(DataSet.ToString());

        }

        static void LaunchANN(double[][] matrix, double[] vector)
        {
            
            //double[] vector = new double[] { 0.2, 0.2, 0.6, 0.9, 0.5};
            //double[][] matrix = new double[][]
            //{
            //    new double[]{0.1, 0.1},
            //    new double[]{0.2, 0.0},
            //    new double[]{0.3, 0.3},
            //    new double[]{0.4, 0.5}, 
            //    new double[]{0.25, 0.25}
            //};

            DataSerie1D annStrct = new DataSerie1D();
            annStrct.Add("HL1", 4);
            annStrct.Add("HL2", 2);

            NeuralNetworkEngineEO ann = new NeuralNetworkEngineEO(matrix, vector, annStrct);
            ann.LearningMaxIterations = 100;
            ann.LearningError = 0.0001;
            ann.LearningAlgorithm = LearningAlgorithmEnum.LevenbergMarquardtLearning;
            ann.LearningAlgorithm_Params = new double[] {0.1, 10};
            ann.ActivationFunction = ActivationFunctionEnum.BipolarSigmoidFunction;
            ann.ActiveFunction_Params = new double[]{2.00};


            ann.Learn();
            //-0.5440211109;-0.2720105555
            var ans =ann.Compute(new double[] {0.28, 0.29}); 

            foreach (var valu in ans)
            {
                Console.WriteLine("ans = {0}", Math.Round(valu,3));
            }

            Console.WriteLine("Final teaching err = {0}; MaxIter ={1}.", ann.FinalTeachingError, ann.FinalIterationsCount);

        }

        static void LaunchANNEO(DataFormater dfr, int n, int kmax)
        {
            EANN annEo = new EANN(dfr.TrainingInput, dfr.TrainingOutput, dfr.TestingInput, dfr.TestingOutput);
            
            annEo.Learning_Algorithm = LearningAlgorithmEnum.LevenbergMarquardtLearning ;
            annEo.MinLearningIterations = 50;
            annEo.MaxLearningIterations = 100;

            annEo.MinHidenNeuronesCount = 1;
            annEo.MaxHidenNeuronesCount = 5;

            annEo.MaxIterations = kmax;           
            annEo.PopulationSize = n;
           
            annEo.LearnEO();

            Console.WriteLine("Best learning scroe = {0} ; Best testing score = {1}", annEo.BestLearningScore, annEo.BestTestingScore);

           foreach (DataItem1D itm in annEo.BestChart.Data)
            {
                Console.WriteLine(itm.Title, itm.X_Value);
            }


            double[][] xy = new double[][]
            {
               new double []{0.8, 0.12},
               new double []{0.16, 0.10},
               new double []{0.24, 0.26},
               new double []{0.35, 0.10}
            };

            var z = annEo.Compute(xy);

            if (Equals(z, null)) { return; }
            foreach (double value in z)
            {
                Console.WriteLine("z = {0}", Math.Round(value, 3));
            }

        }




    }
}
