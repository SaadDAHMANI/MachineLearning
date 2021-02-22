using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace IOOperations
{
	[Serializable]
	public 	class DataSerieTD
	{
		public  DataSerieTD()
		{
			if (object.Equals(mData, null))
			{
				mData = new List<DataItemTD>();
			}
		}

		List<DataItemTD> mData;
		public List<DataItemTD> Data
		{
			get { return mData; }
			set { mData = value; }
		}

		string mName = "DS-T";
		public string Name
		{

			get { return mName; }
			set
			{
				if (value == string.Empty)
				{ mName = "/"; }
				else
				{
					mName = value;
				}
			}
		}

		string mDescription = "/";
		public string Description
		{
			get { return mDescription; }
			set
			{
				if (value == string.Empty)
				{ mDescription = "/"; }
				else
				{ mDescription = value; }
			}
		}

		string mTitle = "DS-T";
		public string Title
		{
			get { return mTitle; }
			set
			{
				if (value == string.Empty)
				{ mTitle = "/"; }
				else
				{
					mTitle = value;
				}
			}
		}

		public int GetRowsCount()
		{			
				if (Equals(mData,null)) { return 0; }
				else { return mData.Count; }			
		}

		public int GetColumnsCount()
		{			
					if (Equals(mData, null)) { return 0; }
				else if (mData.Count <1) { return 0; }
				else { return mData[0].List.Count();}		
		}

		List<string> mTitles;
		public List <string> Titles
		{
			get { return mTitles; }
			set { mTitles = value; }

		}

		public void Add(string title, params double[] list)
		{
			mData.Add(new DataItemTD(title, list));
		}

		public double [] Min
		{
			get
			{
				if (Equals(Data, null)) { return null; }
				if (Data.Count==0) { return null; }
				
				int jCount = mData[0].List.Count();
				double[] minValues = new double[jCount];
				double minValue = double.MaxValue;

				for (int j=0; j < jCount; j++ )
				{
					foreach(DataItemTD ditem in mData)
					{
						if (minValue > ditem.List[j]) { minValue = ditem.List[j]; }
					}
					minValues[j] = minValue;
				}
				return minValues;
			}
		}

		public double [] Max
		{
			get
			{
				if (Equals(Data, null)) { return null; }
				if (Data.Count == 0) { return null; }

				int jCount = mData[0].List.Count();
				double[] mxnValues = new double[jCount];
				double maxValue = double.MinValue ;

				for (int j = 0; j < jCount; j++)
				{
					foreach (DataItemTD ditem in mData)
					{
						if (maxValue < ditem.List[j]) { maxValue = ditem.List[j]; }
					}
					mxnValues[j] = maxValue;
				}
				return mxnValues;
			}
		}

		public override string ToString()
		{
			StringBuilder strb = new StringBuilder();
			strb.AppendLine(Name);
			strb.AppendLine(Description);
			strb.Append(Title).Append("; ");
			foreach (string titl in Titles)
			{ strb.Append(titl).Append("; "); }
			
			strb.AppendLine();

			foreach (DataItemTD itm in Data)
            {
				strb.Append(itm.Title).Append("; ");
				foreach (double value in itm.List)
				{ strb.Append(value).Append("; "); }

				strb.AppendLine();
            }

			return strb.ToString();
		}

		public DataSerie1D ConvertToDS1()
        {
            if (object.Equals (mData, null)) { return null;}
            DataSerie1D ds1 = new DataSerie1D();
            foreach (DataItemTD itm in this.Data)
            {
                ds1.Add(itm.Title, itm.List[0]);
            }
            return ds1;
        }
        
        public double [][] GetArray()
        {
            double[][] result = null;
            if (object.Equals(Data, null)) { return null;}
            int iCount = this.Data.Count;
            int jCount = 0;
            result = new double[iCount][];
            for (int i=0; i<iCount; i++)
            {
                jCount = Data[i].List.Length;
                double[] vector = new double[jCount];
                for(int j=0;j<jCount;j++)
                {
                    vector[j] = Data[i].List[j];
                }
                result[i] = vector;
            }

            return result;
        }

		public double[] GetColumn(int index)
        {						
			if (index <0)
			{return null;}

			if (Equals(Data, null)){ return null;}
						
			int colCount = GetColumnsCount();
			
			if (index >= colCount) { throw new Exception(string.Format("The dataset columns count is < {0}", index));}

			int rowCount = GetRowsCount();

			if (colCount < 1) { return null;}
			if (rowCount < 1) { return null; }

			double[] result = new double[rowCount];

			if ( index < colCount && index >=0 )
            {
			
				for (int i=0; i < rowCount;i++)
                {
					result[i] = this.Data[i].List[index];
                }
            }
			return result; 
        }

		public List<double[]> GetColumns(params int[] columns)
		{
			if (Equals(columns, null)) { return null; }
			if (Equals(Data, null)) { return null; }

			int colCount = GetColumnsCount();
			if (colCount < 1) { return null; }

			if (columns.Min() < 0 || columns.Max() >= colCount)
			{ return null; }

			int rowCount = GetRowsCount();

			if (rowCount < 1) { return null; }

			List<double[]> result = new List<double[]>();

			double[] colmn;
			int i;

			foreach (int colIndex in columns)
            {
				colmn = new double[rowCount];
				i = 0;

				foreach(DataItemTD itm in Data)
                {
					colmn[i] = itm.List[colIndex];
					i += 1;
                }
				result.Add(colmn);
			}

			return result;

		}


		public double [][] GetDataOfColumns(params int[] columns)
		{
			if (Equals(columns, null)) { return null; }
			if (Equals(Data, null)) { return null; }

			int colCount = GetColumnsCount();
			if (colCount < 1) { return null; }

			if (columns.Min() < 0 || columns.Max() >= colCount)
			{ return null; }

			int rowCount = GetRowsCount();

			if (rowCount < 1) { return null; }

			double[][] result = new double[rowCount][];

			double[] row;
			int i = 0;

			foreach (DataItemTD itm in Data)
			 {
				row = new double[columns.Count()];
				
				for (int j=0;j < columns.Count(); j++)
                {
					row[j] = itm.List[columns[j]];
                }
				result[i] = row;
				i += 1;
			 }

			return result;

		}



		public static double[][] Convert(DataSerieTD ds)
        {
            if (Equals(ds, null)) { return null; }

            int iCount = ds.Data.Count;
            if (iCount < 1) { return null; }

            int itmCount = ds.Data[0].List.Count();

            double[][] result = new double[iCount][];
            try
            {
                for (int i = 0; i < iCount; i++)
                {
                    double[] dblList = new double[itmCount];

                    for (int j = 0; j < itmCount; j++)
                    {
                        dblList[j] = ds.Data[i].List[j];

                    }
                    result[i] = dblList;
                }
            }
            catch (Exception ex) { throw ex; }

            return result;
        }
        
    }
}
