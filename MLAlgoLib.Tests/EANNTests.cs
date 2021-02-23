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
   
    }
}
