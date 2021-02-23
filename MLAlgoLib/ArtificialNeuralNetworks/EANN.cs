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

public class EANN
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

     public double[][] LearningInputs {get; set;}
     public double[] LearningOutputs {get; set;}
     public double[][] TestingInputs {get; set;}
     public double[] TestingOutputs{get; set;}
     private double[] _Computed_LearningOutputs;
     public double[] Computed_LearningOutputs {get {return _Computed_LearningOutputs;}}
     
     private double[] _Computed_TestingOutputs;
     public double[] Computed_TestingOutputs{get {return _Computed_TestingOutputs;}}

     private int _MaxIterations;
     public int MaxIterations 
     { get {return _MaxIterations;}
       set {_MaxIterations=Math.Max(0, value);}
     }
     
     private int _PopulationSize;
     public int PopulationSize 
     { get {return _PopulationSize;}
       set {_PopulationSize =Math.Max(2, value);}
     }




}

}
}