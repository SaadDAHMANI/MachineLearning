//---------------------------------------------------------------------- 
// Required packages :
// dotnet add package Accord.MachineLearning --version 3.8.2-alpha
// dotnet add package Accord.MachineLearning.GPL --version 3.8.2-alpha
// 
// For genetic algorithm:
// dotnet add package Accord.Genetic --version 3.8.0 
//----------------------------------------------------------------------
// Written by : Saad Dahmani (s.dahmani@univ-bouira.dz; sd.dahmani2000@gmail.com)
//**********************************************************************

using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Collections.Generic;
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
using MonoObjectiveEOALib;

namespace ConsoleApp
{
    class Program
    {       
         static string fileName_DST;
         static DataSerieTD DataSet;
         static DataFormater df;
        static string fileName = String.Empty;

        static void Main(string[] args)
        {

            Console.WriteLine("Hello SVR & ANN!");

            // //string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\DataSet_Ex.csv";
            // //string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\DataSet_Exemple.csv";

            //string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\DataSet_ExempleSinX.csv";

            // //QC_Sidi_Aissa SSL :
            //string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\QC_Sidi_Aissa.csv";
            //string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\QC_Sidi_Aissa_Standards.csv";

            // //Beni-Bahdel_Dame_3Q :
            // //string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\Beni-Bahdel_Dame_3Q.csv";

            // //Station_Ain_El_Assel_Dataset_Pf(Q) :
            // //string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\Station_Ain_El_Assel_Dataset_Pf(Q).csv";
            //string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\Station_Ain_El_Assel_Dataset_Pf(Q)_Standard.csv";
            // string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\Station_Ain_El_Assel_Dataset_1_Standard.csv";

            ////QC_Sidi_Aissa SSL :
            //string file = @"C:\SSL\QC_Sidi_Aissa.csv";
            // string file = @"C:\Users\SD\Documents\Dataset_ANN_SVR\QC_Sidi_Aissa_Standards.csv";

            
            string file = string.Empty;

            while (file ==string.Empty || System.IO.File.Exists(file)==false)
            {
                Console.WriteLine("Saisir le nom de fichier des données (dans le dossier : C:\\SSL) (n'oublier pas de standardiser les données)");
                fileName = Console.ReadLine();
                file = string.Format("C:\\SSL\\{0}.csv", fileName.Trim());
            }   
             
            LoadData(file);
    
            df = new DataFormater(DataSet);
            df.TrainingPourcentage = 70;

            df.Format(1,0);

            // Console.WriteLine("LearnIn = {0}, LearnOut = {1}, TestIn = {2}, TestOut = {3}",df.TrainingInput.Length, df.TrainingOutput.Length, df.TestingInput.Length, df.TestingOutput.Length );

            //if (!Equals(df.TrainingInput, null)) { Console.WriteLine("Training = {0}", df.TrainingInput.Length); }

            // // Luanch EOSVR with EOAlgo params.   
            int n=2;

            int kmax=2;
                                   
            Console.WriteLine("Saisir la taille de la population de recherche < N > (nombre entier > 1):");
            
            if (!int.TryParse(Console.ReadLine(), out n)){n=2;}         
                        
            Console.WriteLine("Saisir le nombre d'itérations < Kmax > (nombre entier > 0):");

            if (!int.TryParse(Console.ReadLine(), out kmax)){kmax=2;}

            Console.WriteLine("Computation by : N = {0}, Kmax={1}",n, kmax); 
                    
            Console.WriteLine("Voulez-vous lancer le EO-SVR ?(if YES , type y; n if NO):");

            var ans = Console.ReadLine();
            
            if (ans=="y" || ans =="yes") {
                                         
             LaunchEOSVR(n,kmax);

            Console.WriteLine("_______________________________________________________________________");

             } 

            Console.WriteLine("Voulez-vous lancer le EO-ANN ?(if YES , type y; n if NO):");

             ans = Console.ReadLine();
            
            if (ans=="y" || ans =="yes") {
                Console.WriteLine("Choisir l'algorithme d'apprentissage (0 : Backpropagation, 1 : Levenberg-Marquardt)");
                
                int learnAlgo = int.Parse(Console.ReadLine());
                if (learnAlgo ==0 || learnAlgo ==1)
                {
                    LaunchANNEO(df, n, kmax, learnAlgo);
                }
                               
              //LaunchANN(df.TrainingInput, df.TrainingOutput);
            }

            Console.WriteLine("Taper f pour fermer");
            string cmd = Console.ReadLine();
            while (cmd != "f")
            {
                Console.WriteLine("Taper f pour fermer");
                cmd = Console.ReadLine();
            }


        }

        static void LaunchEOSVR( int popSize, int iterMax)
        {
            if (Equals(DataSet, null) || Equals(df, null)){return;}
            EOSVR eo_svr = new EOSVR(df.TrainingInput, df.TrainingOutput, df.TestingInput, df.TestingOutput);
            
            eo_svr.PopulationSize=popSize;
            eo_svr.MaxIterations=iterMax;

            eo_svr.Sigma_Kernel=1.1; //1.1; //1.336687063023768
            eo_svr.Param_Complexity = 100; // 100; //56.8121658157614
            eo_svr.Param_Epsilon = 0.001; // 0.001
            eo_svr.Param_Tolerance = 0.001;// 0.001
            
            // Console.WriteLine("---------> SVR : ");
            //eo_svr.Learn(); 
                      
            Console.WriteLine("---------> EO-SVR : ");

            eo_svr.LearnEO();

            Console.WriteLine("Best score = {0}", eo_svr.BestScore);
            if (!Equals(eo_svr.BestSolution, null)) {
            Console.WriteLine("Best solution : Sigma of Gaussian = {0}; Complexity = {1}; Tolerance = {2}; Epsilon = {3}.", eo_svr.BestSolution[0], eo_svr.BestSolution[1], eo_svr.BestSolution[2], eo_svr.BestSolution[3]);
            }
           // Console.WriteLine("Best soltion : SigmaKernel= {0}; Complexity_C= {1}; Tolerance= {2}; Epsilon= {3}", eo_svr.BestSolution[0],eo_svr.BestSolution[1],eo_svr.BestSolution[2],eo_svr.BestSolution[3]);
  
            Console.WriteLine("EO-SVR :->Best learning index = {0} ; EO-SVR :-> Best testing index = {1}", eo_svr.BestLearningScore, eo_svr.BestTestingScore);

            if (!Equals(eo_svr.BestChart, null))
            {
                foreach (var valu in eo_svr.BestChart)
                {
                    Console.WriteLine(valu);
                }
            }
            


            SaveResults(eo_svr,string.Format("C:\\SSL\\Results_SVR_{0}.txt", fileName.Trim()));

            //double[][] xy = new double[][] 
            //{
            //   new double []{ 0.8},
            //   new double []{0.16},
            //   new double []{0.24},
            //   new double []{0.35},
            //   new double []{0.25}
            //};

            //var z = eo_svr.Compute(xy);

            //if (Equals(z, null)) { return; }
            //foreach (double value in z)
            //{
            //    Console.WriteLine("z = {0}", Math.Round( value,3));
            //}
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

        static void LaunchANNEO(DataFormater dfr, int n, int kmax, int learnAlgo)
        {
            int minHLNeurones = 1;
            int maxHLNeurones= 15;
            int kminL = 40;
            int kmaxL = 200;

            if (learnAlgo == 0)
            {
                kminL = 10000;
                kmaxL = 21000;
            }

            var ranges = new List<MonoObjectiveEOALib.Range>
                     {
                    new MonoObjectiveEOALib.Range("Activation Function",0.9, 2.1),
                    new MonoObjectiveEOALib.Range("Alpha of Activation Function", 0.1, 10),
                    new MonoObjectiveEOALib.Range("Learning rate", 0.05, 0.1),
                    new MonoObjectiveEOALib.Range("Momentum/Ajustement", 10, 12),
                    new MonoObjectiveEOALib.Range("Learning Err", 0.001, 0.01),
                    new MonoObjectiveEOALib.Range("Max Iteration (Kmax)", kminL, kmaxL),
                    new MonoObjectiveEOALib.Range("Hiden Layer Number", 1, 5),
                    new MonoObjectiveEOALib.Range("Layer 1 Nodes count",minHLNeurones, maxHLNeurones),
                    new MonoObjectiveEOALib.Range("Layer 2 Nodes count", minHLNeurones, maxHLNeurones),
                    new MonoObjectiveEOALib.Range("Layer 3 Nodes count", minHLNeurones, maxHLNeurones),
                    new MonoObjectiveEOALib.Range("Layer 4 Nodes count",  minHLNeurones, maxHLNeurones),
                    new MonoObjectiveEOALib.Range("Layer 5 Nodes count", minHLNeurones, maxHLNeurones),
                    new MonoObjectiveEOALib.Range("Layer 6 Nodes count", minHLNeurones, maxHLNeurones)
                     };

            EANN annEo = new EANN(dfr.TrainingInput, dfr.TrainingOutput, dfr.TestingInput, dfr.TestingOutput);

            annEo.Learning_Algorithm = (LearningAlgorithmEnum)learnAlgo;// LearningAlgorithmEnum.LevenbergMarquardtLearning;

            annEo.SearchRanges = ranges;

            annEo.MaxOptimizationIterations= kmax;           
            annEo.PopulationSize = n;

            annEo.LearnEO();
           
            Console.WriteLine("EO-ANN :-> Best learning scroe = {0} ; EO-ANN :-> Best testing score = {1}", annEo.BestLearningScore, annEo.BestTestingScore);

            foreach (var itm in annEo.BestChart)
            {
                Console.WriteLine(itm);
            }

             SaveResults(annEo, string.Format("C:\\SSL\\Results_ANN_{0}.txt", fileName.Trim()));

            //double[][] xy = new double[][]
            // {
            //   new double []{ 0.8},
            //   new double []{0.16},
            //   new double []{0.24},
            //   new double []{0.35},
            //   new double []{0.25}
            // };

            //double[][] xy = new double[][]
            //{
            //   new double []{0.8, 0.12},
            //   new double []{0.16, 0.10},
            //   new double []{0.24, 0.26},
            //   new double []{0.35, 0.10},
            //   new double []{0.25, 0.12}
            //};

            //var z = annEo.Compute(xy);

            //if (Equals(z, null)) { return; }
            //foreach (double value in z)
            //{
            //    Console.WriteLine("z = {0}", Math.Round(value, 3));
            //}

        }

        static void SaveResults(EOSVR eo_svr, string filePath)
        {
            if (Equals(eo_svr, null)){return;}
            if(Equals(eo_svr.BestChart, null)){return;}
            if(Equals(eo_svr.BestSolution, null)){return;}

           using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
           {
               using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
               {
                   System.Text.StringBuilder strb = new System.Text.StringBuilder();
                   
                   strb.AppendLine("Best_Chart;");
                   
                   for (int i=0; i < eo_svr.BestChart.Count(); i++)
                   {
                       strb.Append(eo_svr.BestChart[i].ToString()).AppendLine(";");
                   }

                   strb.AppendLine("Best Learning score;");
                   strb.Append(eo_svr.BestLearningScore).AppendLine(";"); 

                   strb.AppendLine("Best Testing Score;"); 
                   strb.Append(eo_svr.BestTestingScore).AppendLine(";"); 

                   strb.AppendLine("Best solution;");
                   
                   for (int i=0; i< eo_svr.BestSolution.Length; i++)
                   {
                       strb.Append(eo_svr.BestSolution[i]).AppendLine(";");
                   }
                   
                   sw.Write(strb.ToString());
                   sw.Flush();
                   sw.Close();
                   fs.Close(); 
               }
           }
        }    
        static void SaveResults(EANN eo_ann, string filePath)
        {
            if(Equals(eo_ann, null)){return;}
            if(Equals(eo_ann.BestChart, null)){return;}
            if(Equals(eo_ann, null)){return;}

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
           {
               using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
               {

                    System.Text.StringBuilder strb = new System.Text.StringBuilder();
                   
                   strb.AppendLine("Best_Chart;");
                   
                   for (int i=0; i < eo_ann.BestChart.Count; i++)
                   {
                       strb.Append(eo_ann.BestChart[i]).AppendLine(";");
                   }

                   strb.AppendLine("Best Learning score;");
                   strb.Append(eo_ann.BestLearningScore).AppendLine(";"); 

                   strb.AppendLine("Best Testing Score;"); 
                   strb.Append(eo_ann.BestTestingScore).AppendLine(";"); 

                   strb.AppendLine("Best solution;");
                   
                   for (int i=0; i< eo_ann.BestSolution.Length; i++)
                   {
                       strb.Append(eo_ann.BestSolution[i]).AppendLine(";");
                   }
                   
                   sw.Write(strb.ToString());
                   sw.Flush();
                   sw.Close();
                   fs.Close();        
                       
            
               }
               }
            bool save = eo_ann.BestNeuralNetwork.SaveNeuralNetwork(string.Format("C:\\SSL\\The_Best_ANN_{0}.ann", fileName.Trim()));
            Console.WriteLine("Save the best ANN : {0}", save);
        }
    }
}
