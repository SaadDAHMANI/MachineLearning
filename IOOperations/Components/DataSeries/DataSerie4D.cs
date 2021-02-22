/*
 * Created by SharpDevelop.
 * User: Saad
 * Date: 01/11/2015
 * Time: 12:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOOperations
{
	/// <summary>
	/// Description of DataSerie5D.
	/// </summary>
	[Serializable]
	public class DataSerie4D
	{
		public DataSerie4D()
		{
			if (object.Equals(mData, null))
			{
				mData = new List<DataItem4D>();
			}
		}

		string mName = "DS-4";
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


		string mTitle = "DS-4";
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

		string mA_Title = "A";
		public string A_Title
		{
			get { return mA_Title; }
			set
			{
				if (value == string.Empty)
				{ mA_Title = "/"; }
				else { mA_Title = value; }
			}
		}

		string mB_Title = "B";
		public string B_Title
		{
			get { return mB_Title; }
			set
			{
				if (value == string.Empty)
				{ mB_Title = "/"; }
				else
				{
					mB_Title = value;
				}
			}
		}

		string mC_Title = "C";
		public string C_Title
		{
			get { return mC_Title; }
			set
			{
				if (value == string.Empty)
				{ mC_Title = "/"; }
				else
				{
					mC_Title = value;
				}
			}
		}

		string mD_Title = "D";
		public string D_Title
		{
			get { return mD_Title; }
			set
			{
				if (value == string.Empty)
				{ mD_Title = "/"; }
				else
				{
					mD_Title = value;
				}
			}
		}

		
		List<DataItem4D> mData;
		public List<DataItem4D> Data
		{
			get { return mData; }
			set { mData = value; }
		}


		public void Add(string title, double aValue, double bValue, double cValue, double dValue)
		{

			mData.Add(new DataItem4D(title, aValue, bValue, cValue, dValue));
		}

		public override string ToString()
		{
			return mName;
		}

	}
}
