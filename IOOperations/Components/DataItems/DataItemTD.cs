using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOOperations
{
	[Serializable]
	public class DataItemTD
	{
		string mTitle = "/";
		public string Title
		{
			get { return mTitle; }
			set
			{
				if (value == string.Empty)
				{ mTitle = "/"; }
				else { mTitle = value; }
			}
		}

		public DataItemTD(string title, params double [] list)
		{
			if (title == string.Empty)

			{ mTitle = "/"; }

			else
			{
				mTitle = title;
			}
			mList = list;
					}

		double[] mList;
		public double[] List
		{
			get { return mList; }
			set { mList = value; }
		}

	}
}
