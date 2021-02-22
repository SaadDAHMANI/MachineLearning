/*
 * Created by SharpDevelop.
 * User: Saad
 * Date: 01/11/2015
 * Time: 12:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace IOOperations
{
    /// <summary>
    /// Description of DataSerie1D.
    /// </summary>
    [Serializable]
    public class DataSerie1D
    {      
		public  DataSerie1D()
		{
			if (object.Equals(mData, null))
			{
				mData = new List<DataItem1D>();
			}
		}
          string mName="DS-1";
          public string Name
          {
              get { return mName; }
              set {
                  if (value == string.Empty)
                  { mName = "/"; }
                  else 
                  {
                  mName = value; }
              }
          }

        public string FileName;

          string mDescription="/";
          public string Description
          {
              get { return mDescription; }
              set 
              {
                  if  (value ==string .Empty )
              { mDescription = "/"; }
              else
              { mDescription = value;}
          }
          }

          List<DataItem1D> mData;
          public List<DataItem1D> Data
          {
              get { return mData; }
              set { mData = value; }
          }

          string mTitle="DS1";
          public string Title
          {
              get { return mTitle; }
              set {
                  if (value == string.Empty)
                  { mTitle = "/"; }
                  else
                  {
                      mTitle = value;
                  }
              }
          }

          string mX_Title="X";
          public string X_Title
          {
              get { return mX_Title; }
              set {
                  if (value == string.Empty)
                  { mX_Title = "X"; }
                  else 
                  {mX_Title = value;}
                   }
          }

          public void Add(string title, double xValue)
          {
             
              mData.Add(new DataItem1D(title, xValue));
          }

			[Category ("Statistics"), Description ("The Min Value.")] public double Min
		{ get
			{
				double minValue = double.MaxValue;
				foreach (DataItem1D itm1 in mData)
					{
					if (itm1.X_Value < minValue)
					{ minValue = itm1.X_Value; }
					}
				return minValue;
			}

		}

			[Category("Statistics"), Description("The Max Value.")] public double Max{
			get
			{
				double minValue = double.MinValue;
				foreach (DataItem1D itm1 in mData)
				{
					if (itm1.X_Value > minValue)
					{ minValue = itm1.X_Value; }
				}
				return minValue;
			}

		}

			[Category("Statistics"), Description("The Average Value.")] public double Mean
		{
			get
			{
				double sumValue = Sum;
				int iCount = Count ;
				if (Count >0)
				{
					sumValue = (sumValue / iCount);
					return Math.Round( sumValue,4);
				}
				else
				{ return double.NaN; }

				
			}

		}

			[Category("Statistics"), Description("The Count Value.")] public int Count
		{
			get
			{
				if (object.Equals(mData, null))
				{ return 0; }
				else
				{ return mData.Count; }
				
			}

		}
	
			[Category("Statistics"), Description("The Algebric Sum Value.")] public double Sum
		{
			get
			{
				double sumValue = 0;
				foreach (DataItem1D itm1 in mData)
				{
					sumValue += itm1.X_Value;
				}
				return Math.Round(sumValue, 4);
			}

		}

		public DataSerie1D Aggregate(int countStep )
		{
			DataSerie1D result_ds = null;
			if (countStep <1) { return null; }
			if (Equals(mData, null)) { return null; }
			if (Count <1) { return null; }
						
			result_ds = new DataSerie1D();
			int count = Count;
			double summ = 0;

			if (count <= countStep)
			{
				for (int i = 0; i < count; i++)
				{
					summ += mData[i].X_Value;
				}
				result_ds.Add("1", summ);
			}
			else
			{
				summ = 0;
				int rest = 0;
				int aggregCount = 1;

				for (int i = 0; i < count; i++)
				{
					summ += mData[i].X_Value;

					aggregCount = Math.DivRem((i + 1), countStep, out rest);

					if (rest == 0)
					{
						result_ds.Add(aggregCount.ToString(), summ);
						summ = 0;
					}
				}

				summ = 0;
				aggregCount = Math.DivRem(count, countStep, out rest);

				for (int i = (count -rest ); i < count; i++)
				{
					summ += mData[i].X_Value;
				}

				result_ds.Add((aggregCount+1).ToString(), summ);
			}
			return result_ds;
		}


		public override string ToString()
		{
           
			return mName ;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format">"int" int values</param>
        /// <returns></returns>
        public string Data2String(string format)
        {
            string result = "";
            if (format=="int")
            {
                for (int i=0;i<Count;i++)
                {
                    result += Math.Round(Data[i].X_Value).ToString ()+ " ; ";
                }
            }
            else
            {
                for (int i = 0; i < Count; i++)
                {
                    result += Data[i].X_Value.ToString ()+ " ; ";
                }
            }
            return result;
        }

        public static DataSerie1D Convert(double[][] source)
        {
            if (Equals (source, null)) { return null;}
             DataSerie1D result = new DataSerie1D();
            int iCount = source.GetLength(0);
           if (iCount >0)
            {
               for (int i=0; i<iCount; i++)
                    {
                        result.Add(i.ToString(), source[i][0]);
                    }
               }
            return result;
        }
        
        public static DataSerie1D Convert(double[] source)
        {
            if (Equals(source, null)) { return null; }
            DataSerie1D result = new DataSerie1D();
            int iCount = source.GetLength(0);
           if (iCount > 0)
            {             
              for (int i = 0; i < iCount; i++)
                {
                        result.Add(i.ToString(), source[i]);
                }               
            }
            return result;
        }

        public double[][] GetArray()
        {
            double[][] result = null;
            if (object.Equals(Data, null)) { return null; }
            int iCount = this.Data.Count;
            result = new double[iCount][];
            for (int i = 0; i < iCount; i++)
            {
               double[] vector = new double[1];
               vector[0] = Data[i].X_Value;
               result[i] = vector;
            }
            return result;
        }

        public double[] GetArray1D()
        {
            if (object.Equals(Data, null)) { return null; }
            int iCount = this.Data.Count;
            double[] result = new double[iCount];
            for (int i = 0; i < iCount; i++)
            {               
                result[i] = Data[i].X_Value;
            }
            return result;
        }

        private static double[][] Convert(DataSerie1D ds1)
        {

            int iCount = ds1.Data.Count;

            double[][] result = new double[iCount][];

            for (int i = 0; i < iCount; i++)
            {
                result[i] = new double[] { ds1.Data[i].X_Value };
            }

            return result;
        }
        /// <summary>
        /// Ascendant sorting.
        /// </summary>
        public void Sort()
        {
            if (object.Equals(Data, null)) { return; }
            Data.Sort(delegate(DataItem1D item1, DataItem1D item2)
            {
                if (item1.X_Value == item2.X_Value) { return 0; }
                else if(item1.X_Value < item2.X_Value) { return -1; }
                else { return 1; }
            });
        }
        /// <summary>
        /// Descendant sorting.
        /// </summary>
        public void SortReverse()
        {
            if (object.Equals(Data, null)) { return; }
            Data.Sort(delegate (DataItem1D item1, DataItem1D item2)
            {
                if (item1.X_Value == item2.X_Value) { return 0; }
                else if (item1.X_Value < item2.X_Value) { return 1; }
                else { return -1; }
            });
        }
      
    }
}
