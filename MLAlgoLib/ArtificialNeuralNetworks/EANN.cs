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

       NeuralNetworkEngineEO _BestNeuralNetwork;
        public NeuralNetworkEngineEO BestNeuralNetwork
       {get {return _BestNeuralNetwork;}}
    public override void Learn()
     {
        
     }
     public override void LearnEO()
     {
        
     }
      public override void Compute(double[][] inputs)
     {
        
     }





}

}
}