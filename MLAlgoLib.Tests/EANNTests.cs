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
          Assert.Equal(_tst.Learning_Algorithm, 0);
        }
    }
}
