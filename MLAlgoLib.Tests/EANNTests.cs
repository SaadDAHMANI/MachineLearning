using System;
using Xunit;
using MLAlgoLib.ArtificialNeuralNetwork;

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
            Assert.Equal(null, EANN.ConvertToJagged(vectorIn));

        }


    }
    }
