/*
 * Created by SharpDevelop.
 * User: Saad
 * Date: 01/11/2015
 * Time: 12:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace IOOperations
{
    /// <summary>
    /// DataItem2D is (Title, x, y) item.
    /// </summary>

    [Serializable]
    public class DataItem2D
    {
        public DataItem2D()
        { }
        public DataItem2D(string title, double x, double y)
        {
            if (title == string.Empty)

            { mTitle = "/"; }

            else
            {
                mTitle = title;
            }
            
            mX_Value = x;
            mY_Value = y;

        }

        string mTitle="/";
        public string Title
        {
            get { return mTitle; }
            set { 
                if (value ==string .Empty )
                { mTitle = "/"; }
                else {  mTitle = value; }
               }
        }

        double mX_Value;
        public double X_Value
        {
            get { return mX_Value; }
            set { mX_Value = value; }
        }

        double mY_Value;
        public double Y_Value
        {
            get { return mY_Value; }
            set { mY_Value = value; }
        }
    }
	
}
