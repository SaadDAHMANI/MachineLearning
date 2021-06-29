using System;
using Xunit;
using MLAlgoLib.ArtificialNeuralNetwork;
using IOOperations;

namespace MLAlgoLib.Tests
{
    public class EANNTests
    {
        readonly EANN _tst;
        public EANNTests()
        {
            _tst=new EANN();
        } 

        [Fact]
        public void Learning_AlgorithmDefaultValue()
        {
            Assert.Equal(LearningAlgorithmEnum.LevenbergMarquardtLearning, _tst.Learning_Algorithm);
        }
        
        [Fact]
        public void ComputationDurationDefaultOut()
        {
        Assert.Equal(0, _tst.ComputationDuration);
        }

        [Fact]
        public void Convert2JaggedTest()
        {
            double[] vectorIn = new double[] { 1, 2, 3, 4 };
            double[][] matrixOut = new double[][]
            {
                new double[]{1},
                new double[]{2},
                new double[]{3},
                new double[]{4}
            };

            Assert.Equal(matrixOut, EANN.ConvertToJagged(vectorIn));            
        }

        [Fact]
        public void Convert2JaggedTestWhenNull()
        {
            
            double[] vectorIn = new double[] { 1, 2, 3, 4 };
            vectorIn = null;
            //Assert.Equal(null, EANN.ConvertToJagged(vectorIn));

        }


        [Fact]
        public void GetArrayTest1()
        {
            double[] vector = new double[] { 1, 2, 3, 4 };
            double[][] matrix = new double[][]
            {
                new double[]{1, 0},
                new double[]{2, 0},
                new double[]{3, 0},
                new double[]{4, 0}
            };

            Assert.Equal(vector, EANN.GetArray(matrix));
        }

        [Fact]
        public void GetArrayTest2()
        {
            double[] vector = new double[] { 0, 0, 0, 0, 0 };
            double[][] matrix = new double[][]
            {
                new double[]{0},
                new double[]{0},
                new double[]{0},
                new double[]{0},
                new double[]{0}
            };

            Assert.Equal(vector, EANN.GetArray(matrix));
        }

        [Fact]
        public void GetLayersStructTest1()
        {
            DataSerie1D hidenLayersStruct = new DataSerie1D();
            hidenLayersStruct.Add("HL1", 4);
            hidenLayersStruct.Add("HL2", 5);
            hidenLayersStruct.Add("HL3", 2);
            int inp = 2;
             int[] AnnStruct = new int[] { 4, 5, 2, 1 };
            Assert.Equal(AnnStruct, _tst.GetLayersStruct(hidenLayersStruct, inp, 1));
        }


    }
    }
