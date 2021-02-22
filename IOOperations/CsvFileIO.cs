/*
 * Created by SharpDevelop.
 * User: Saad
 * Date: 01/11/2015
 * Time: 12:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System .IO ;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOOperations
{
	/// <summary>
	/// Description of CsvFileIO.
	/// </summary>
	 public class CsvFileIO
    {
      
           public CsvFileIO(string fileName)
           {
               this.mFileName = fileName;
           }
        string mFileName= string.Empty;
        public string FileName {get{return mFileName;} set {mFileName=value;}}

		public bool Write(DataSerie1D ds)
		{
			bool result = false;
			try
			{

				using (FileStream fs = File.Open(this.mFileName, FileMode.OpenOrCreate))
				{
					using (StreamWriter sw = new StreamWriter(fs))
					{

						sw.WriteLine(ds.Name);
						sw.WriteLine(ds.Description);
						sw.WriteLine(string.Format("{0}; {1}", ds.Title, ds.X_Title));

						if (object.Equals(ds.Data, null))
						{
							return false;
						}

						foreach (DataItem1D itm in ds.Data)
						{
							sw.WriteLine(string.Format("{0}; {1}", itm.Title, itm.X_Value));
						}

						sw.Flush();
						result = true;
					}
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public bool Write(DataSerie2D ds)
           {
               bool result = false;
               try
               {

                   using (FileStream fs = File.Open(this.mFileName, FileMode.OpenOrCreate))
                   {
                       using (StreamWriter sw = new StreamWriter(fs))
                       {
                       
                           sw.WriteLine(ds.Name);
                           sw.WriteLine(ds.Description);
                           sw.WriteLine(string.Format("{0}; {1}; {2}", ds.Title, ds.X_Title, ds.Y_Title));
                           if (object.Equals(ds.Data, null))
                           {
                               return false;
                           }
                           foreach (DataItem2D itm in ds.Data)
                           {
                               sw.WriteLine(string.Format("{0}; {1}; {2}", itm.Title, itm.X_Value, itm.Y_Value));
                           }

                           sw.Flush();
                           result = true;
                       }
                   }

               }
               catch (Exception ex)
               {
                   throw ex;
               }
               return result;
           }

        public bool Write(DataSerie3D ds)
        {
            bool result = false;
            try
            {

                using (FileStream fs = File.Open(this.mFileName, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {

                        sw.WriteLine(ds.Name);
                        sw.WriteLine(ds.Description);
                        sw.WriteLine(string.Format("{0}; {1}; {2}; {3}", ds.Title, ds.X_Title, ds.Y_Title, ds.Z_Title));
                        if (object.Equals(ds.Data, null))
                        {
                            return false;
                        }
                        foreach (DataItem3D itm in ds.Data)
                        {
                            sw.WriteLine(string.Format("{0}; {1}; {2}; {3}", itm.Title, itm.X_Value, itm.Y_Value, itm.Z_Value ));
                        }

                        sw.Flush();
                        result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool Write(DataSerie5D ds5)
        {
            bool result = false;
            try
            {

                using (FileStream fs = File.Open(this.mFileName, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {

                        //sw.WriteLine(ds5.Name);
                        //sw.WriteLine(ds5.Description);
                     //sw.WriteLine(string.Format("{0}; {1}; {2}", ds5.Title, ds5, ds5.Y_Title));
                        if (object.Equals(ds5.Data, null))
                        {
                            return false;
                        }
                        foreach (DataItem5D itm in ds5.Data)
                        {
                            sw.WriteLine(string.Format("{0}; {1}; {2}; {3}; {4}", itm.A_Value, itm.B_Value , itm .C_Value ,itm.D_Value ,itm.E_Value ));
                        }

                        sw.Flush();
                        result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

		public bool Write(DataSerieTD ds)
		{
			bool result = false;
			try
			{
				using (FileStream fs = File.Open(this.mFileName, FileMode.OpenOrCreate))
				{
					using (StreamWriter sw = new StreamWriter(fs))
					{
						sw.WriteLine(ds.Name);

						sw.WriteLine(ds.Description);

						StringBuilder strb = new StringBuilder();

						strb.Append(ds.Title);

						if (Equals(ds.Titles,null)==false )
						{
							foreach (string title in ds.Titles)
							{
								//strb.Append(";").Append(title);                                                                     ")
							}
						}
						else { strb.Append(";"); }

						sw.WriteLine(strb.ToString ());

						if (Equals(ds.Data, null))
						{
							return false;
						}

						strb.Clear();

						foreach (DataItemTD itm in ds.Data)
						{
							strb.Append(itm.Title);
							foreach( double value in itm.List)
							{
								strb.Append(";").Append(value.ToString());
							}
							sw.WriteLine(strb.ToString());
							strb.Clear();
						}
												
						sw.Flush();
						strb.Clear();
						strb = null;
						result = true;
					}
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public bool Write(List<DataSerie1D> ds1List)
           {
               bool result = false;
               try
               {

                   using (FileStream fs = File.Open(this.mFileName, FileMode.OpenOrCreate))
                   {
                       using (StreamWriter sw = new StreamWriter(fs))
                       {
                           if (object.Equals(ds1List, null)) { return false; }
                           
                           int sCount = ds1List.Count;
                           if (sCount < 1) { return false; }
                           StringBuilder strb = new StringBuilder();
                           // Head of file :

                           strb.Append("Name").Append(";");
                           strb.Append("Description").Append(";");
                           if (object.Equals(ds1List[0].Data, null) == false)
                           {
                               if (ds1List[0].Data.Count > 0)
                               {
                                   foreach (DataItem1D item in ds1List[0].Data)
                                   {
                                       strb.Append(item.Title).Append(";");
                                   }

                                   sw.WriteLine(strb.ToString());
                                   strb.Clear();
                               }
                           }

                           foreach (DataSerie1D ds2 in ds1List)
                           {
                               if (object.Equals(ds2.Data, null) == false)
                               {
                                   if (ds2.Data.Count > 0)
                                   {
                                       strb.Append(ds2.Name).Append(";");
                                       strb.Append(ds2.Description).Append(";");
                                       sCount = ds2.Data.Count;
                                       foreach (DataItem1D item in ds2.Data)
                                       {
                                           strb.Append(item.X_Value).Append(";");
                                       }
                                       
                                       sw.WriteLine(strb.ToString());
                                       strb.Clear();
                                   }
                               }
                           }
                           sw.Flush();
                           result = true;
                       }
                   }

               }
               catch (Exception ex)
               {
                   throw ex;
               }
               return result;
           }

		public bool Write(List<DataSerie1D> ds1List, int x)
		{
			bool result = false;
			try
			{

				using (FileStream fs = File.Open(this.mFileName, FileMode.OpenOrCreate))
				{
					using (StreamWriter sw = new StreamWriter(fs))
					{
						if (object.Equals(ds1List, null)) { return false; }

						int sCount = ds1List.Count;
						if (sCount < 1) { return false; }
						//-----------------------------------------
						foreach (DataSerie1D ds1 in ds1List)
						{
							if (object.Equals(ds1.Data, null))
							{
								return false;
							}
						}
						//------------------------------------------

						StringBuilder strb = new StringBuilder();
						// Head of file :
						int iCount = ds1List[0].Data.Count;

						for (int i = 0; i < iCount; i++)
						{
							foreach (DataSerie1D ds1 in ds1List)
							{
								strb.Append(ds1.Data[i].X_Value).Append(";");
							}
							strb.AppendLine();
						}		
							
						sw.WriteLine(strb.ToString());
						strb.Clear();
						sw.Flush();
						result = true;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

        public bool Write(List<DataSerie1D> ds1List, string file)
        {
            bool result = false;
            try
            {

                using (FileStream fs = File.Open(file, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        if (object.Equals(ds1List, null)) { return false; }

                        int sCount = ds1List.Count;
                        if (sCount < 1) { return false; }
                        //-----------------------------------------
                        foreach (DataSerie1D ds1 in ds1List)
                        {
                            if (object.Equals(ds1.Data, null))
                            {
                                return false;
                            }
                        }
                        //------------------------------------------

                        StringBuilder strb = new StringBuilder();
                        // Head of file :
                        int iCount = ds1List[0].Data.Count;

                        for (int i = 0; i < iCount; i++)
                        {
                            foreach (DataSerie1D ds1 in ds1List)
                            {
                                strb.Append(ds1.Data[i].X_Value).Append(";");
                            }
                            strb.AppendLine();
                        }

                        sw.WriteLine(strb.ToString());
                        strb.Clear();
                        sw.Flush();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool Write(List<DataSerie2D> ds2List)
           {
               bool result = false;
               try
               {

                   using (FileStream fs = File.Open(this.mFileName, FileMode.OpenOrCreate))
                   {
                       using (StreamWriter sw = new StreamWriter(fs))
                       {
                           if (object.Equals(ds2List, null)) { return false; }

                           int sCount = ds2List.Count;
                           if (sCount < 1) { return false; }
                           StringBuilder strb = new StringBuilder();
                           // Head of file :

                           strb.Append("Name").Append(";");
                           strb.Append("Description").Append(";");
                           if (object.Equals(ds2List[0].Data, null) == false)
                           {
                               if (ds2List[0].Data.Count > 0)
                               {
                                   foreach (DataItem2D item in ds2List[0].Data)
                                   {
                                       strb.Append(item.Title).Append(";");
                                   }

                                   sw.WriteLine(strb.ToString());
                                   strb.Clear();
                               }
                           }

                           foreach (DataSerie2D ds2 in ds2List)
                           {
                               if (object.Equals(ds2.Data, null) == false)
                               {
                                   if (ds2.Data.Count > 0)
                                   {
                                       strb.Append(ds2.Name).Append(";");
                                       strb.Append(ds2.Description).Append(";");
                                       sCount = ds2.Data.Count;
                                       foreach (DataItem2D item in ds2.Data)
                                       {
                                           strb.Append(item.X_Value).Append(";");
                                       }

                                       sw.WriteLine(strb.ToString());
                                       strb.Clear();
                                   }
                               }
                           }
                           sw.Flush();
                         
                           result = true;
                       }
                   }

               }
               catch (Exception ex)
               {
                   throw ex;
               }
               return result;
           }

       
       [Obsolete("Revoir cette procedure.")]public bool Write(List<DataSerie5D> ds5List)
           {
               bool result = false;
               try
               {

                   using (FileStream fs = File.Open(this.mFileName, FileMode.OpenOrCreate))
                   {
                       using (StreamWriter sw = new StreamWriter(fs))
                       {
                           if (object.Equals(ds5List, null)) { return false; }

                           int sCount = ds5List.Count;
                           if (sCount < 1) { return false; }
                           StringBuilder strb = new StringBuilder();
                           // Head of file :

                           strb.Append("Name").Append(";");
                           strb.Append("Description").Append(";");
                           if (object.Equals(ds5List[0].Data, null) == false)
                           {
                               if (ds5List[0].Data.Count > 0)
                               {
                                   foreach (DataItem5D item in ds5List[0].Data)
                                   {
                                       strb.Append(item.Title).Append(";");
                                   }

                                   sw.WriteLine(strb.ToString());
                                   strb.Clear();
                               }
                           }

                           foreach (DataSerie5D ds5 in ds5List)
                           {
                               if (object.Equals(ds5.Data, null) == false)
                               {
                                   if (ds5.Data.Count > 0)
                                   {
                                       strb.AppendLine(ds5.Name);
                                       strb.AppendLine(ds5.Description);
                                       strb.Append(ds5.A_Title).Append(";");
                                       strb.Append(ds5.B_Title).Append(";");
                                       strb.Append(ds5.C_Title).Append(";");
                                       strb.Append(ds5.D_Title).Append(";");
                                       strb.AppendLine(ds5.E_Title);

                                       foreach (DataItem5D item in ds5.Data)
                                       {
                                           strb.Append(item.A_Value).Append(";");
                                           strb.Append(item.B_Value).Append(";");
                                           strb.Append(item.C_Value).Append(";");
                                           strb.Append(item.D_Value).Append(";");
                                           strb.AppendLine(item.E_Value.ToString ());
                                       }

                                       sw.WriteLine(strb.ToString());
                                       strb.Clear();
                                   }
                               }
                           }
                           sw.Flush();
                          
                           result = true;
                       }
                   }

               }
               catch (Exception ex)
               {
                   throw ex;
               }
               return result;
           }

		public DataSerie1D Read_DS1()
		{
			DataSerie1D ds1 = null;
			
            try
			{
				if (File.Exists(this.mFileName))
				{
					ds1 = new DataSerie1D();

					using (FileStream fs = File.Open(this.mFileName, FileMode.Open))
					{
						using (StreamReader sReader = new StreamReader(fs))
						{
							ds1.Name = sReader.ReadLine().Replace(";","");
							ds1.Description = sReader.ReadLine().Replace(";", "");
							string title = sReader.ReadLine();
							string[] titles = title.Split(';');
							if (titles.Count() == 2)
							{
								ds1.Title = titles[0];
								ds1.X_Title = titles[1];
								
							}
							double xx;
							bool tryResult = true;

							while (sReader.EndOfStream == false)
							{
								title = sReader.ReadLine();
								titles = title.Split(';');
								
								if (titles.Count() == 2)
								{
									//xx = double.Parse(titles[1]);
									xx = double.NaN;
									tryResult =	double.TryParse(titles[1], out xx);
									if (tryResult) {ds1.Add(titles[0], xx); }
									else { ds1.Add("$Err$", Double.NaN); }
									
								}
							}
						}

					}

				}

			}
			catch (Exception ex)
			{
				throw ex;
			}

			return ds1;
		}
		
        
		public DataSerie1D Read_DS1(string thefile)
		{
			DataSerie1D ds1 = null;
			
            try
			{
				if (File.Exists(thefile))
				{
					ds1 = new DataSerie1D();

					using (FileStream fs = File.Open(thefile, FileMode.Open))
					{
						using (StreamReader sReader = new StreamReader(fs))
						{
							ds1.Name = sReader.ReadLine().Replace(";","");
							ds1.Description = sReader.ReadLine().Replace(";", "");
							string title = sReader.ReadLine();
							string[] titles = title.Split(';');
							if (titles.Count() == 2)
							{
								ds1.Title = titles[0];
								ds1.X_Title = titles[1];
								
							}
							double xx;
							bool tryResult = true;

							while (sReader.EndOfStream == false)
							{
								title = sReader.ReadLine();
								titles = title.Split(';');
								
								if (titles.Count() == 2)
								{
									//xx = double.Parse(titles[1]);
									xx = double.NaN;
									tryResult =	double.TryParse(titles[1], out xx);
									if (tryResult) {ds1.Add(titles[0], xx); }
									else { ds1.Add("$Err$", Double.NaN); }
									
								}
							}
						}

					}

				}

			}
			catch (Exception ex)
			{
				throw ex;
			}

			return ds1;
		}
	
		public DataSerie2D Read_DS2()
           {
               DataSerie2D ds2= null ;
               try
               {
                   if (File.Exists(this.mFileName))
                   {
                       ds2 = new DataSerie2D();

                       using (FileStream fs = File.Open(this.mFileName, FileMode.Open))
                       {
                           using (StreamReader sReader = new StreamReader(fs))
                           {
                               ds2.Name = sReader.ReadLine().Replace(";","");
                               ds2.Description = sReader.ReadLine().Replace (";","");
                               string title = sReader.ReadLine();
                               string[] titles = title.Split(';');
                               if (titles.Count() == 3)
                               {
                                   ds2.Title = titles[0];
                                   ds2.X_Title = titles[1];
                                   ds2.Y_Title = titles[2];
                               }
                               double xx, yy;
							bool tryResult_X = true;
							bool tryResult_Y = true;
							xx = 0;
							yy = 0;
							while (sReader.EndOfStream == false)
                               {
                                   title = sReader.ReadLine();
                                   titles = title.Split(';');
                                   if (titles.Count() == 3)
                                   {
								 tryResult_X=double.TryParse(titles[1],out xx);
									tryResult_Y= double.TryParse(titles[2], out yy);

									if (tryResult_X ==false) { ds2.Add(titles[0], double.NaN, yy); }
									else if (tryResult_Y==false ) { ds2.Add(titles[0], xx, double.NaN); }
									else { ds2.Add(titles[0], xx, yy); }
							        }
                               }
                             }
                          
                       }

                   }

               }
               catch (Exception ex)
               {
                   throw ex;
               }

               return ds2;
           }

		public DataSerie3D Read_DS3()
		{
			DataSerie3D ds3 = null;
			try
			{
				if (File.Exists(this.mFileName))
				{
					ds3 = new DataSerie3D();

					using (FileStream fs = File.Open(this.mFileName, FileMode.Open))
					{
						using (StreamReader sReader = new StreamReader(fs))
						{
							ds3.Name = sReader.ReadLine();
							ds3.Description = sReader.ReadLine();
							string title = sReader.ReadLine();
							string[] titles = title.Split(';');
							if (titles.Count() == 3)
							{
								ds3.Title = titles[0];
								ds3.X_Title = titles[1];
								ds3.Y_Title = titles[2];
								ds3.Y_Title = titles[3];
							}
							double xx, yy, zz;
							while (sReader.EndOfStream == false)
							{
								title = sReader.ReadLine();
								titles = title.Split(';');
								if (titles.Count() == 4)
								{
									xx = double.Parse(titles[1]);
									yy = double.Parse(titles[2]);
									zz = double.Parse(titles[3]);
									ds3.Add(titles[0], xx, yy,zz);

								}
							}
						}

					}

				}

			}
			catch (Exception ex)
			{
				throw ex;
			}

			return ds3;
		}

		public DataSerieTD Read_DST()
		{
			DataSerieTD ds = null;
			try
			{
				if (File.Exists(this.mFileName))
				{
					ds = new DataSerieTD();

					using (FileStream fs = File.Open(mFileName, FileMode.Open))
					{
						using (StreamReader sReader = new StreamReader(fs))
						{
							ds.Name = sReader.ReadLine();
							ds.Description = sReader.ReadLine();
							string title = sReader.ReadLine();
							string[] titles = title.Split(';');

							if (titles.Count() > 0)
							{
							 ds.Title = titles[0];

							ds.Titles = new List<string>();

								for (int i=1; i<titles.Count();i++)
								{
									ds.Titles.Add(titles[i]);
								}
							}
														
							while (sReader.EndOfStream == false)
							{
								title = sReader.ReadLine();
								titles = title.Split(';');

								int countinItem = titles.Count();

								double[] listValues = new double[(countinItem-1)];

								for (int j=1;j<countinItem;j++)
								{
									listValues[(j - 1)] = double.Parse(titles[j]);
								}
									
								ds.Add(titles[0], listValues);

							}
						}
                        fs.Close();
                    }
				}					
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return ds;
		}

        public DataSerieTD Read_DST(bool thereIsName, bool thereIsDescription, bool thereIsHeader, bool thereIsKeyColomn)
        {
            if (thereIsName && thereIsDescription && thereIsHeader && thereIsKeyColomn)
            {
                return Read_DST();
            }

            DataSerieTD ds = null;
            try
            {
                if (File.Exists(this.mFileName))
                {
                    ds = new DataSerieTD();

                    using (FileStream fs = File.Open(mFileName, FileMode.Open))
                    {
                        using (StreamReader sReader = new StreamReader(fs))
                        {
                            if(thereIsName)
                            {
                                ds.Name = sReader.ReadLine();
                            }else { ds.Name = "n/n"; }
                                                    

                            if(thereIsDescription)
                            {
                             ds.Description = sReader.ReadLine();
                            }
                            else { ds.Description = "n/n"; }

                            string title;
                            string[] titles;

                            if (thereIsHeader)
                            {
                                title = sReader.ReadLine();
                                titles = title.Split(';');

                                if (titles.Length > 0)
                                {
                                    ds.Title = titles[0];

                                    ds.Titles = new List<string>();

                                    for (int i = 1; i < titles.Length; i++)
                                    {
                                        ds.Titles.Add(titles[i]);
                                    }
                                }
                            }
                                                      
                           if (thereIsKeyColomn)
                            { 
                            while (sReader.EndOfStream == false)
                            {
                                title = sReader.ReadLine();
                                titles = title.Split(';');

                                int countinItem = titles.Length;

                                double[] listValues = new double[(countinItem - 1)];

                                for (int j = 1; j < countinItem; j++)
                                {
                                    listValues[(j - 1)] = double.Parse(titles[j]);
                                }

                                ds.Add(titles[0], listValues);
                            }
                            }
                            else 
                            {
                                int i = 0;

                                while (sReader.EndOfStream == false)
                                {
                                    i += 1;

                                    title = sReader.ReadLine();
                                    titles = title.Split(';');

                                    int countinItem = titles.Length;

                                    double[] listValues = new double[countinItem];

                                    for (int j = 0; j < countinItem; j++)
                                    {
                                        listValues[j] = double.Parse(titles[j]);
                                    }

                                    ds.Add(i.ToString (), listValues);
                                }

                            }
                            fs.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return ds;
        }

        public List<DataSerie1D> Read_DS1List()
           {
               List<DataSerie1D> ds1List = null;

               try
               {
                   if (File.Exists(this.mFileName))
                   {
                       ds1List = new List<DataSerie1D>();

                       using (FileStream fs = File.Open(this.mFileName, FileMode.Open))
                       {
                           using (StreamReader sReader = new StreamReader(fs))
                           {
                               string header = sReader.ReadLine();
                               string[] headers = header.Split(';');
                               //----------data 
                               string xLineValue;
                               string[] xValues;

                               double xValue = 0;

                               while (sReader.EndOfStream == false)
                               {
                                   xLineValue = sReader.ReadLine();

                                   xValues = xLineValue.Split(';');

                                   if (xValues.Count() > 2)
                                   {

                                       DataSerie1D ds1 = new DataSerie1D();
                                       ds1.Name = xValues[0];
                                       ds1.Description = xValues[1];
                                       for (int j = 2; j < (xValues.Count() ); j++)
                                       {
                                           xValue = double.Parse(xValues[j]);
                                           ds1.Add(headers[j], xValue);
                                       }
                                       ds1List.Add(ds1);
                                   }
                                }

                               sReader.Close();
                               int ss = ds1List.Count();
                               header = null;
                               headers = null;
                               xLineValue = null;
                               xValues = null;

                           }
                           }
                   }
               }
               catch (Exception ex)
               {
                   throw ex;
               }

               return ds1List;
           }

       }
}
