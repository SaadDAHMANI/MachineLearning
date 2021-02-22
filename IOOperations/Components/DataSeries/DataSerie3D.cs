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
    /// Description of DataSerie3D.
    /// </summary>
    [Serializable]
    public class DataSerie3D
    {
		public  DataSerie3D()
		{
			if (object.Equals(mData, null))
			{
				mData = new List<DataItem3D>();
			}
		}
		
		string mName = "DS-3";
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


        string mTitle = "DS-3";
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

        string mY_Title = "Y";
        public string Y_Title
        {
            get { return mY_Title; }
            set
            {
                if (value == string.Empty)
                { mY_Title = "Y"; }
                else
                {
                    mY_Title = value;
                }
            }
        }

           
         string mZ_Title="Z";
           public string Z_Title
           {
               get { return mZ_Title; }
               set {
                   if (value ==string .Empty )
                   { mZ_Title = "Z"; }
                   else
                   { mZ_Title = value; }
                   }
           }

           List<DataItem3D> mData;
           public List<DataItem3D> Data
           {
               get { return mData; }
               set { mData = value; }
           }

           public void Add(string title, double xValue, double yValue, double zValue)
           {
               
               mData.Add(new DataItem3D(title, xValue, yValue, zValue));
           }

		public override string ToString()
		{
			return mName;
		}

	}

}
