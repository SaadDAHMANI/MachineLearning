/*
 * Created by SharpDevelop.
 * User: Saad
 * Date: 01/11/2015
 * Time: 12:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace IOOperations
{
    /// <summary>
    /// DataItem1D is (Title, x) item.
    /// </summary>
    [Serializable]
     public class DataItem1D
	{
        public DataItem1D()
        { }
        public DataItem1D(string title, double x)
        {
            if (title == string.Empty) { mTitle = "/"; }
            else {
				mTitle = title;}
          
            mX_Value = x;
        }

        string mTitle="x";
        public string Title
        {
            get { return mTitle; }
            set { 
                if (value ==string .Empty )
                { mTitle = "/"; }
                else
                {mTitle = value; }
                 }
        }

        double mX_Value;
        public double X_Value
        {
            get { return mX_Value; }
            set { mX_Value = value; }
        }

        public override string ToString()
        {
            return string.Format("{0}; {1}", this.mTitle, this.mX_Value);
        }
    }
			
		
	}

