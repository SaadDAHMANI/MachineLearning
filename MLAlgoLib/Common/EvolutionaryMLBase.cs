using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using IOOperations;

namespace MLAlgoLib
{
public abstract class EvolutionaryMLBase
{
   public EvolutionaryMLBase() { }
  public EvolutionaryMLBase(double[][] trainingIn, double[] trainingOut)
  {
    LearningInputs=trainingIn;
    LearningOutputs=trainingOut;
  }
    public EvolutionaryMLBase(double[][] trainingIn, double[] trainingOut, double[][] testingIn, double[] testingOut)
    {
    LearningInputs=trainingIn;
    LearningOutputs=trainingOut;
    TestingInputs=testingIn;
    TestingOutputs=testingOut;
    }
    
     public double[][] LearningInputs {get; set;}
     public double[] LearningOutputs {get; set;}
     public double[][] TestingInputs {get; set;}
     public double[] TestingOutputs{get; set;}
     private double[] _Computed_LearningOutputs;
     public double[] Computed_LearningOutputs {get {return _Computed_LearningOutputs;}}
     
     private double[] _Computed_TestingOutputs;
     public double[] Computed_TestingOutputs{get {return _Computed_TestingOutputs;}}

     private int _MaxOptimizationIterations;
     public int MaxOptimizationIterations
        { get {return _MaxOptimizationIterations; }
       set { _MaxOptimizationIterations = Math.Max(0, value);}
     }
     
     private int _PopulationSize;
     public int PopulationSize 
     { get {return _PopulationSize;}
       set {_PopulationSize =Math.Max(2, value);}
     }
     public abstract void LearnEO();
     public abstract void Learn();
     public abstract double[] Compute(double[][] inputs);

     public virtual double BestScore { get;}
     public virtual List<double> BestChart { get; }


}
}