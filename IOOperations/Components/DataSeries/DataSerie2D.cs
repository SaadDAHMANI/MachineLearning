/*
 * Created by SharpDevelop.
 * User: Saad
 * Date: 01/11/2015
 * Time: 12:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOOperations
{
	/// <summary>
	/// Description of DataSerie2D.
	/// </summary>
	[Serializable]
	public class DataSerie2D
    {
		public  DataSerie2D()
		{
			if (object.Equals(mData, null))
			{
				mData = new List<DataItem2D>();
			}
		}

		public DataSerie2D( string name)
		{
			mName = name;
			if (object.Equals(mData, null))
			{
				mData = new List<DataItem2D>();
			}
		}


		string mName = "DS-2";
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

        public string FileName;
        
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

       
        string mTitle = "DS-2";
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

        string mX_Title = "X";
        public string X_Title
        {
            get { return mX_Title; }
            set
            {
                if (value == string.Empty)
                { mX_Title = "X"; }
                else
                { mX_Title = value; }
            }
        }

        string mY_Title="Y";
        public string Y_Title
        {
            get { return mY_Title; }
            set {
                if (value == string.Empty)
                { mY_Title = "Y"; }
                else
                {
                    mY_Title = value;
                }
            }
        }

        List<DataItem2D> mData;
        public List<DataItem2D> Data
        {
            get { return mData; }
            set { mData = value; }
        }

		public int Count
		{ get {
				if (Equals(mData,null)==false )
				{ return mData.Count(); }
				else
				{ return 0; }
								
			}
		}

		[Category("Statistics"), Description("The Max Value of [Xi].")]
		public double Max_X
		{
			get
			{
				double minValue = double.MinValue;
				foreach (DataItem2D itm1 in mData)
				{
					if (itm1.X_Value > minValue)
					{
						minValue = itm1.X_Value;
					}
				}
				return minValue;
			}
		}

		[Category("Statistics"), Description("The Max Value of [Yi].")]
		public double Max_Y
		{
			get
			{
				double minValue = double.MinValue;
				foreach (DataItem2D itm1 in mData)
				{
					if (itm1.Y_Value > minValue)
					{
						minValue = itm1.Y_Value;
					}
				}
				return minValue;
			}
		}

		[Category("Statistics"), Description("The Min Value of [Xi].")]
		public double Min_X
		{
			get
			{
				double minValue = double.MaxValue;
				foreach (DataItem2D itm1 in mData)
				{
					if (itm1.X_Value < minValue)
					{ minValue = itm1.X_Value; }
				}
				return minValue;
			}

		}

		[Category("Statistics"), Description("The Min Value of [Yi].")]
		public double Min_Y
		{
			get
			{
				double minValue = double.MaxValue;
				foreach (DataItem2D itm1 in mData)
				{
					if (itm1.Y_Value < minValue)
					{ minValue = itm1.Y_Value; }
				}
				return minValue;
			}

		}


		public void Add(string title, double xValue, double yValue)
        {
          
            mData.Add(new DataItem2D(title, xValue, yValue));
        }

		public void Add(DataItem2D dItem)
		{

			mData.Add(dItem);
		}

		public override string ToString()
		{
			return mName;
		}
	}
	
}
