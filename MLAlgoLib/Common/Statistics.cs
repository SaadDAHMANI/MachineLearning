using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using IOOperations;

namespace MLAlgoLib
{
    public class Statistics
    {
        public Statistics()
        { }
      
       public Statistics(double[] observed_Serie, double[] predicted_Serie)
        {
            ObservedSerie = DataSerie1D.Convert(observed_Serie);
            PredictedSerie = DataSerie1D.Convert(predicted_Serie);
        }
      
        public Statistics(DataSerie1D observed_Serie, DataSerie1D predicted_Serie)
        {
            this.ObservedSerie = observed_Serie;
            this.PredictedSerie = predicted_Serie;
        }

        private DataSerie1D mObservedSerie = null;
        public DataSerie1D ObservedSerie
        {
            get { return mObservedSerie; }
            set { mObservedSerie = value; }
        }

        private DataSerie1D mPredictedSerie = null;
        public DataSerie1D PredictedSerie
        {
            get { return mPredictedSerie;}
            set { mPredictedSerie = value;}

        }

        public void Refresh()
        {
            mMAE = -1;
            mRMSE = -1;
            mR = -1;
            mR2 = -1;
            mNash = -1;
            md = -1;
        }
        
        public string Indexes
        { get
            {              
                return AgreementIndex.ToString() + " ; " + MAE.ToString() + " ; " + Nash.ToString() + " ; " + R.ToString() + " ; " + R2.ToString() + " ; " + RMSE.ToString();
            } 
        }
       

        double mMAE = -1;
        public double MAE
        {
            get
            { if (mMAE==-1) { mMAE = Compute_MAE(); }
                return mMAE;
               
            }
        }

        double mRMSE = -1;
        public double RMSE
        {
            get
            { if (mRMSE==-1)
                {
                    mRMSE = Compute_RMSE();
                }
                return mRMSE;
            }
        }

        double mR = -1;
        public double R
        {
            get
            {
                if (mR == -1)
                {
                    mR =Compute_CorrelationCoeff_R ();
                }
                return mR;
            }
        }

        double mR2 = -1;
        public double R2
        {
            get
            {
                if (mR2 == -1)
                {
                    mR2 = Compute_DeterminationCoeff_R2();
                }
                return mR2;
            }
        }

        double mNash = -1;
        public double Nash
        {
            get
            {
                if (mNash == -1)
                {
                    mNash = Compute_Nash_Sutcliffe_Efficiency();
                }
                return mNash;
            }
        }

        double md = -1;
        public double AgreementIndex
        {
            get
            {
                if (md == -1)
                {
                    md = Compute_Agreement_Index();
                }
                return md;
            }
        }

        /// <summary>
        /// Computation of Root Mean Square Error (RMSE) of Two data series.
        /// </summary>
        /// <returns>RMSE Value</returns>
        public double Compute_RMSE()
        {
            return Compute_RMSE(mObservedSerie,mPredictedSerie );
        }

        /// <summary>
        /// Computation of Mean Absolute Error (MAE) of Two data series.
        /// </summary>
        /// <returns>MAE Value</returns>
        public double Compute_MAE()
        {
            return Compute_MAE(mObservedSerie, mPredictedSerie);
        }

        /// <summary>
        /// Computation of Correlation Coefficient (R) of Two Series.
        /// </summary>
        /// <returns>R Value</returns>
        public double Compute_CorrelationCoeff_R()
        {
            return Compute_CorrelationCoeff_R(mObservedSerie, mPredictedSerie);
        }

        /// <summary>
        /// Computation of Determination Coefficient (R2=R^2) of Two Series.
        /// </summary>
        /// <returns>R2 Value</returns>
        public double Compute_DeterminationCoeff_R2()
        {
            return Compute_DeterminationCoeff_R2(mObservedSerie, mPredictedSerie);
        }

        /// <summary>
        /// Nash Sutcliffe Efficiency "Nash Criterion, Nash and Sutcliffe (1970)" computation of Two series.
        /// </summary>
        /// <returns>Nash Value</returns>
        public double Compute_Nash_Sutcliffe_Efficiency()
        {
            return Compute_Nash_Sutcliffe_Efficiency(mObservedSerie, mPredictedSerie);
        }

        /// <summary>
        /// Agreement Index (Willmot Index, Willmot (1981) computation of Two Data Series.
        /// </summary>
        /// <returns>d Value</returns>
        public double Compute_Agreement_Index()
        {
            return Compute_Agreement_Index(mObservedSerie, mPredictedSerie);
        }
                              
        /// <summary>
        /// Computation of Root Mean Square Error (RMSE) of Two data series.
        /// </summary>
        /// <param name="O_Serie">Observed data series</param>
        /// <param name="P_Serie">Predicted data series</param>
        /// <returns>RMSE Value</returns>
        public static double  Compute_RMSE(DataSerie1D O_Serie, DataSerie1D P_Serie)
        {
            double rmse = double .NaN;
            if (CheckDataSeries(O_Serie, P_Serie) == false) { return rmse; }
            try
            {
                double sum = 0;
                int N = O_Serie.Count;
                for (int i = 0; i < N; i++)
                {
                    sum += Math.Pow((O_Serie.Data[i].X_Value - P_Serie.Data[i].X_Value), 2);
                }
                rmse = sum / N;
                rmse = Math.Sqrt(rmse);
            }
            catch(Exception ex) { rmse = double.NaN;  throw ex;}
            return rmse;
        }

/// <summary>
        /// Computation of Root Mean Square Error (RMSE) of Two data series.
        /// </summary>
        /// <param name="O_Serie">Observed data series</param>
        /// <param name="P_Serie">Predicted data series</param>
        /// <returns>RMSE Value</returns>
        public static double  Compute_RMSE(double[] O_Serie, double[] P_Serie)
        {
            double rmse = double .NaN;
            if (CheckDataSeries(O_Serie, P_Serie) == false) { return rmse; }
            try
            {
                double sum = 0;
                int N = O_Serie.Length;
                for (int i = 0; i < N; i++)
                {
                    sum += Math.Pow((O_Serie[i] - P_Serie[i]), 2);
                }
                rmse = sum / N;
                rmse = Math.Sqrt(rmse);
            }
            catch(Exception ex) { rmse = double.NaN; throw ex;}
            return rmse;
        }

  
       
       
        /// <summary>
        /// Computation of Mean Absolute Error (MAE) of Two data series.
        /// </summary>
        /// <param name="O_Serie">Observed data series</param>
        /// <param name="P_Serie">Predicted data series</param>
        /// <returns>MAE Value</returns>
        public static double Compute_MAE(DataSerie1D O_Serie, DataSerie1D P_Serie)
        {
            double mae = double.NaN;
            if (CheckDataSeries(O_Serie, P_Serie) == false) { return mae; }
            try
            {
                double sum = 0;
                int N = O_Serie.Count;
                for (int i = 0; i < N; i++)
                {
                    sum += Math.Abs((O_Serie.Data[i].X_Value - P_Serie.Data[i].X_Value));
                }
                mae = sum / N;
             }

            catch (Exception ex) {mae = double.NaN;  throw ex; }
            return mae;
        }

        
        /// <summary>
        /// Computation of Mean Absolute Error (MAE) of Two data series.
        /// </summary>
        /// <param name="O_Serie">Observed data series</param>
        /// <param name="P_Serie">Predicted data series</param>
        /// <returns>MAE Value</returns>
        public static double Compute_MAE(double[] O_Serie, double[] P_Serie)
        {
            double mae = double.NaN;
            if (CheckDataSeries(O_Serie, P_Serie) == false) { return mae; }
            try
            {
                double sum = 0;
                int N = Math.Min(O_Serie.Length, P_Serie.Length);
                for (int i = 0; i < N; i++)
                {
                    sum += Math.Abs(O_Serie[i] - P_Serie[i]);
                }
                mae = sum / N;
             }

            catch (Exception ex) {mae = double.NaN;  throw ex; }
            return mae;
        }

        /// <summary>
        /// Computation of Correlation Coefficient (R) of Two Series.
        /// </summary>
        /// <param name="O_Serie">Observed data series</param>
        /// <param name="P_Serie">Predicted data series</param>
        /// <returns>R Value</returns>
        public static double Compute_CorrelationCoeff_R(DataSerie1D O_Serie, DataSerie1D P_Serie)
        {
            double rValue = double.NaN;
            if (CheckDataSeries(O_Serie, P_Serie) == false) { return rValue ; }
            try
            {
                double sum1 = 0;
                double sum2 = 0;
                double sum3 = 0;

                int N = O_Serie.Count;
                double oMean = O_Serie.Mean;
                double pMean = P_Serie.Mean;

                for (int i = 0; i < N; i++)
                {
                    sum1 += ((O_Serie.Data[i].X_Value - oMean )*(P_Serie.Data[i].X_Value -pMean));
                    sum2 += Math.Pow((O_Serie.Data[i].X_Value - oMean), 2);
                    sum3 += Math.Pow((P_Serie.Data[i].X_Value - pMean), 2);
                }
                if (sum2==0 || sum3==0)
                {
                    if (sum1>=0)
                    { return double.PositiveInfinity; }
                    else
                    { return double.NegativeInfinity; }

                }
                else
                {
                    rValue = sum1 / (Math.Sqrt((sum2 * sum3)));
                }
                }

            catch (Exception ex) { rValue  = double.NaN;}
            return rValue ;
        }

            
        /// <summary>
        /// Computation of Correlation Coefficient (R) of Two Series.
        /// </summary>
        /// <param name="O_Serie">Observed data series</param>
        /// <param name="P_Serie">Predicted data series</param>
        /// <returns>R Value</returns>
        public static double Compute_CorrelationCoeff_R(double[] O_Serie, double[] P_Serie)
        {
            double rValue = double.NaN;
            if (CheckDataSeries(O_Serie, P_Serie)== false) { return rValue ; }
           
           
            try
            {
                double sum1 = 0;
                double sum2 = 0;
                double sum3 = 0;

                int N = Math.Min(O_Serie.Length, P_Serie.Length);
                double oMean = O_Serie.Average();
                double pMean = P_Serie.Average();

                for (int i = 0; i < N; i++)
                {
                    sum1 += ((O_Serie[i] - oMean )*(P_Serie[i] -pMean));
                    sum2 += Math.Pow((O_Serie[i] - oMean), 2);
                    sum3 += Math.Pow((P_Serie[i] - pMean), 2);
                }
                if (sum2==0 || sum3==0)
                {
                    if (sum1>=0)
                    { return double.PositiveInfinity; }
                    else
                    { return double.NegativeInfinity; }

                }
                else
                {
                    rValue = sum1 / (Math.Sqrt((sum2 * sum3)));
                }
                }

            catch (Exception ex) { rValue  = double.NaN;}
            return rValue ;
        }


        /// <summary>
        /// Computation of Determination Coefficient (R2=R^2) of Two Series.
        /// </summary>
        /// <param name="O_Serie">Observed data series</param>
        /// <param name="P_Serie">Predicted data series</param>
        /// <returns>R2 Value</returns>
        public static double Compute_DeterminationCoeff_R2(DataSerie1D O_Serie, DataSerie1D P_Serie)
        {
            double rValue = Compute_CorrelationCoeff_R(O_Serie, P_Serie);
            if(rValue ==double.NaN ) { return double.NaN; }
            else if (Object.Equals(rValue,double.PositiveInfinity) || object.Equals(rValue,double.NegativeInfinity))
            { return double.PositiveInfinity; }
            else
            {
              return Math.Pow(rValue, 2); 
            }
        }

        /// <summary>
        /// Computation of Determination Coefficient (R2=R^2) of Two Series.
        /// </summary>
        /// <param name="O_Serie">Observed data series</param>
        /// <param name="P_Serie">Predicted data series</param>
        /// <returns>R2 Value</returns>
        public static double Compute_DeterminationCoeff_R2(double[] O_Serie, double[] P_Serie)
        {
            double rValue = Compute_CorrelationCoeff_R(O_Serie, P_Serie);
            if(rValue ==double.NaN ) { return double.NaN; }
            else if (Object.Equals(rValue,double.PositiveInfinity) || object.Equals(rValue,double.NegativeInfinity))
            { return double.PositiveInfinity; }
            else
            {
              return Math.Pow(rValue, 2); 
            }
        }

        /// <summary>
        /// Nash Sutcliffe Efficiency "Nash Criterion, Nash and Sutcliffe (1970)" computation of Two series.
        /// </summary>
        /// <param name="O_Serie">Observed data series</param>
        /// <param name="P_Serie">Predicted data series</param>
        /// <returns>Nash criterion Value</returns>
        public static double Compute_Nash_Sutcliffe_Efficiency(DataSerie1D O_Serie, DataSerie1D P_Serie)
        {
            double nash = double.NaN;
            if (CheckDataSeries(O_Serie, P_Serie) == false) {return double.NaN ;}
            try
            {
                double sum1 = 0;
                double sum2 = 0;
                int N = O_Serie.Count;
                double oMean = O_Serie.Mean;
                for(int i=0;i<N;i++)
                {
                    sum1+=Math.Pow((O_Serie.Data[i].X_Value-P_Serie.Data[i].X_Value),2);
                    sum2+= Math.Pow((O_Serie.Data[i].X_Value -oMean), 2);
                }
                if (sum2 == 0) { return double.NegativeInfinity; }
                else
                {
                    nash = 1 - (sum1 / sum2);
                }
            }
            catch(Exception ex)
            {
                nash = double.NaN;
                throw ex;
            }
             return nash;
        }

        /// <summary>
        /// Nash Sutcliffe Efficiency "Nash Criterion, Nash and Sutcliffe (1970)" computation of Two series.
        /// </summary>
        /// <param name="O_Serie">Observed data series</param>
        /// <param name="P_Serie">Predicted data series</param>
        /// <returns>Nash criterion Value</returns>
        public static double Compute_Nash_Sutcliffe_Efficiency(double[] O_Serie, double[] P_Serie)
        {
            double nash = double.NaN;
            if (CheckDataSeries(O_Serie, P_Serie) == false) {return double.NaN ;}
            try
            {
                double sum1 = 0;
                double sum2 = 0;
                int N = O_Serie.Length;
                double oMean = O_Serie.Average();
                for(int i=0;i<N;i++)
                {
                    sum1+=Math.Pow((O_Serie[i]-P_Serie[i]),2);
                    sum2+= Math.Pow((O_Serie[i] -oMean), 2);
                }
                if (sum2 == 0) { return double.NegativeInfinity; }
                else
                {
                    nash = 1 - (sum1 / sum2);
                }
            }
            catch(Exception ex)
            {
                nash = double.NaN;
                throw ex;
            }
             return nash;
        }


        /// <summary>
        /// Agreement Index (Willmot Index, Willmot (1981) computation of Two Data Series.
        /// </summary>
        /// <param name="O_Serie">Observed data series</param>
        /// <param name="P_Serie">Predicted data series</param>
        /// <returns>d Value</returns>
        public static double Compute_Agreement_Index(DataSerie1D O_Serie, DataSerie1D P_Serie)
        {
            double dValue = double.NaN;
            if (CheckDataSeries(O_Serie, P_Serie) == false) { return double.NaN; }

            try
            {
                double sum1 = 0;
                double sum2 = 0;
                int N = Math.Min(O_Serie.Count, P_Serie.Count);
                double oMean = O_Serie.Mean;
                for (int i = 0; i < N; i++)
                {
                    sum1 += Math.Pow((O_Serie.Data[i].X_Value - P_Serie.Data[i].X_Value),2);
                    sum2 += Math.Pow((Math.Abs((O_Serie.Data[i].X_Value-oMean))+Math.Abs((P_Serie.Data[i].X_Value-oMean))), 2);
                }
                if (sum2==0) { return double.NegativeInfinity; }
                else
                {
                    dValue = 1 - (sum1 / sum2);
                }
                
                }
            catch (Exception ex)
            {
                dValue = double.NaN;
                throw ex;
            }

            return dValue;
        }

        /// <summary>
        /// Agreement Index (Willmot Index, Willmot (1981) computation of Two Data Series.
        /// </summary>
        /// <param name="O_Serie">Observed data series</param>
        /// <param name="P_Serie">Predicted data series</param>
        /// <returns>d Value</returns>
        public static double Compute_Agreement_Index(double[] O_Serie, double[] P_Serie)
        {
            double dValue = double.NaN;
            if (CheckDataSeries(O_Serie, P_Serie) == false) { return double.NaN; }

            try
            {
                double sum1 = 0;
                double sum2 = 0;
                int N = Math.Min(O_Serie.Length, P_Serie.Length);
                double oMean = O_Serie.Average();
                for (int i = 0; i < N; i++)
                {
                    sum1 += Math.Pow((O_Serie[i] - P_Serie[i]),2);
                    sum2 += Math.Pow((Math.Abs((O_Serie[i]-oMean))+Math.Abs((P_Serie[i]-oMean))), 2);
                }
                if (sum2==0) { return double.NegativeInfinity; }
                else
                {
                    dValue = 1 - (sum1 / sum2);
                }
                
                }
            catch (Exception ex)
            {
                dValue = double.NaN;
                throw ex;
            }

            return dValue;
        }    

      private static bool CheckDataSeries(DataSerie1D O_Serie, DataSerie1D P_Serie)
        {
            bool result = true ;
            if (object.Equals(O_Serie, null) || object.Equals(O_Serie.Data, null)) { return false ; }
            if (object.Equals(P_Serie, null) || object.Equals(P_Serie.Data, null)) { return false ; }
            if (O_Serie.Count != P_Serie.Count) { return false ; }
            if (O_Serie.Count<1) { return false; }
            return result;
        }

        private static bool CheckDataSeries(double[] O_Serie, double[] P_Serie)
        {
            bool result = true ;
            if (object.Equals(O_Serie, null) || O_Serie.Length<1) { return false ; }
            if (object.Equals(P_Serie, null) || P_Serie.Length<1) { return false ; }
            if (O_Serie.Length != P_Serie.Length) { return false ; }
            return result;
        }

        public override string ToString()
        {
            return string.Format("MAE={0} | RMSE= {1} | R={2} | R2={3} | Nash={4}.", MAE, RMSE,R, R2, Nash);
        }

       public string ToString(int decimals)
        {
            int d= Math.Max(0, decimals);
            d= Math.Min(d, 12);
            return string.Format("MAE={0} | RMSE= {1} | R={2} | R2={3} | Nash={4}.", Math.Round(MAE, d), Math.Round(RMSE,d),Math.Round(R,d), Math.Round(R2,d), Math.Round(Nash,d));
        }
       

    }
}
