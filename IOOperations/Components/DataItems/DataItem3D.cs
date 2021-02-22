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
    /// DataItem3D is (Title, x, y, z) item.
    /// </summary> 
    [Serializable]
    public class DataItem3D
    {
        public DataItem3D()
        { }
        public DataItem3D(string title, double x, double y, double z)
        {
            if (title == string.Empty) { mTitle = "/"; }
            else { mTitle = title; }
            mX_Value = x;
            mY_Value = y;
            mZ_Value = z;
        }

         string mTitle="/";
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

        double mZ_Value;
        public double Z_Value
        {
            get { return mZ_Value; }
            set { mZ_Value = value; }
        }
    }
}
