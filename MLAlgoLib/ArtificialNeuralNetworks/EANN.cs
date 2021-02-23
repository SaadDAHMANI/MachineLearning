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


     public Stopwatch Chronos = new Stopwatch();
         long mComputationDuration = 0;
         public long ComputationDuration
         {
             get { return mComputationDuration; }
         }

}

}
}