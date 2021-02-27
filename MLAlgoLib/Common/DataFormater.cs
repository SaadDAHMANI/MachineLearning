using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using IOOperations;

namespace MLAlgoLib
{
    public class DataFormater
    {
        public DataFormater(DataSerieTD dataset)
        {
            DataSet = dataset;
        }


        public DataSerieTD DataSet { get; set; }

        private double _TrainingPourcentage;
        public double TrainingPourcentage
        { get { return _TrainingPourcentage; } set { _TrainingPourcentage = Math.Max(0, Math.Min(value, 100)); } }

        public double TestingPourcentage
        { get { return 100 - _TrainingPourcentage; } }

        private double[][] _TrainingInput;
        public double[][] TrainingInput
        { get { return _TrainingInput; } }

        private double[] _TrainingOutput;
        public double[] TrainingOutput
        { get { return _TrainingOutput;}}

        private double[][] _TestingInput;
        public double[][] TestingInput
        { get { return _TestingInput; } }

        private double[] _TestingOutput;
        public double[] TestingOutput
        { get { return _TestingOutput; } }


        public void Format(int targetColumnIndex, params int[] modelInputColumns)
        {
             if(_TrainingPourcentage<=0){ return;}
            if(Equals(DataSet,null)){return;}
            if(Equals(DataSet.Data, null)){return;}
            
         int colCount = DataSet.GetColumnsCount();
         int rowCount = DataSet.GetRowsCount();
         if (colCount < 1 || rowCount < 2) {return;}

         int trainRowCount = Convert.ToInt32(((TrainingPourcentage * rowCount) / 100));

            double[] targetCol = DataSet.GetColumn(targetColumnIndex);
            double[][] dataCols = DataSet.GetDataOfColumns(modelInputColumns); 

            if (Equals(targetCol, null)) { return; }
            if (Equals(dataCols, null)) { return; }

            _TrainingInput = dataCols.Take(trainRowCount).ToArray();
            _TestingInput = dataCols.TakeLast((rowCount - trainRowCount)).ToArray();

            _TrainingOutput = targetCol.Take(trainRowCount).ToArray();
            _TestingOutput = targetCol.TakeLast((rowCount - trainRowCount)).ToArray();
                          
 }
        public static double[][] ConvertToJagged(double[] vector)
        {
            if (Equals(vector, null)) { return null; }

            double[][] matrix = new double[vector.Length][];

            for (int i = 0; i < vector.Length; i++)
            {
                matrix[i] = new double[] { vector[i] };
            }

            return matrix;
        }


    }
}

