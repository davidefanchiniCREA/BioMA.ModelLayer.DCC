using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using CRA.ModelLayer.Core;
using CRA.ModelLayer.DCC;
using CRA.ModelLayer.Strategy;
//using CRA.ModelLayer.VarInfoConverter;    CRA.Core.Preconditions non usato 

namespace CRA.ModelLayer.DCC
{
    public partial class StrategyClassLoader : Form
    {
        public StrategyClassLoader(Form1 form1)
        {
            InitializeComponent();
            frmDCC = form1;
        }

        private Form1 frmDCC;

        private void button1_Click(object sender, EventArgs e)
        {
            this.comboBox1.Text = String.Empty;
            frmDCC.txtDescription.Text = "Domain class description";
            frmDCC.txtURL.Text = "http://components.biomamodelling.org";
            frmDCC.txtNameSpace.Text = "MyNamespace";
            frmDCC.txtClassName.Text = "MyClass";
            frmDCC.dataSet1.Tables[0].Clear();
            frmDCC.dataSet1.Tables[1].Clear();
            frmDCC.LoadParametersClassFromStrategyDll = false;
            frmDCC.LoadDomainClassFromInterfacesDllAndExtendIt= false;
            frmDCC.LoadDomainClassFromInterfacesDll = false;
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                frmDCC.lblDomainClassToBeExtended.Text = comboBox1.Text;
                string domainClass = comboBox1.Text;
                string domainClass_values = domainClass.Substring(0, domainClass.Length - 7);
                string fname = lblDomainClass.Text;
                Assembly a = Assembly.LoadFile(fname);
                Type test = a.GetType(domainClass); //Loads specific type
                MemberInfo[] minf = test.GetProperties();
                Object g = a.CreateInstance(test.FullName);
                // Load value class
                Type testb = a.GetType(domainClass); // To allow compilation (safe default testb init)
                if (frmDCC.LoadDomainClassFromInterfacesDll || frmDCC.LoadDomainClassFromInterfacesDllAndExtendIt)
                {
                    testb = a.GetType(domainClass_values); // Loads specific type
                    g = a.CreateInstance(testb.FullName);
                }
                // Start loading properties
                frmDCC.dataSet1.Tables["Description"].Clear();
                PropertyInfo pi = g.GetType().GetProperty("Description");
                if (frmDCC.LoadDomainClassFromInterfacesDllAndExtendIt || frmDCC.LoadDomainClassFromInterfacesDll)
                {
                    frmDCC.txtDescription.Text = "Domain class" + test.Name + " " +
                                                 (string)pi.GetValue(g, null);
                }
                else
                {
                    frmDCC.txtDescription.Text = "Parameter class to load from XML for the " + test.Name + " class - " +
                                                 (string)pi.GetValue(g, null);
                }
                pi = g.GetType().GetProperty("URL");
                frmDCC.txtURL.Text = (string)pi.GetValue(g, null);
                frmDCC.txtNameSpace.Text = test.Namespace;
                if (test.Name.EndsWith("VarInfo"))
                {
                    frmDCC.txtClassName.Text = test.Name.Substring(0, test.Name.Length - 7);
                }
                else
                {
                    frmDCC.txtClassName.Text = test.Name;
                }
                DataRow dr2 = frmDCC.dataSet1.Tables["Description"].NewRow();
                dr2["DomainTypeDescription"] = frmDCC.txtDescription.Text;
                dr2["DomainTypeURL"] = frmDCC.txtURL.Text;
                dr2["DomainClassNameSpace"] = frmDCC.txtNameSpace.Text;
                dr2["DomainTypeName"] = frmDCC.txtClassName.Text;
                frmDCC.dataSet1.Tables["Description"].Rows.Add(dr2);
                //int _counter = 0;
                frmDCC.dataSet1.Tables["VarInfoVariables"].Clear();
                DataRow dr;
                
                //strategy class
                if (frmDCC.LoadParametersClassFromStrategyDll)
                {
                    dr = frmDCC.dataSet1.Tables["VarInfoVariables"].NewRow();
                    dr[0] = "ParameterKey";
                    dr[1] = "0";
                    dr[2] = "0";
                    dr[3] = "0";
                    dr[4] = "unitless";
                    dr[5] = "string";
                    dr[6] = "Parameter key";
                    //#LE:T24
                    //dr[7] = "http://components.biomamodelling.org/components/";
                    dr[7] = ConfigurationManager.AppSettings.Get("StrategyParameterURI");
                    frmDCC.dataSet1.Tables["VarInfoVariables"].Rows.Add(dr);

                    /* 3/sept/2012 davide fuma - modifica recupero parametri della strategy: non si recuperano piu' dalle proprieta' della strategy ma chiedendole al ModelingOptionsManager - NEW START */
                    if (g is CRA.ModelLayer.Strategy.IStrategy)
                    {
                        IEnumerable<VarInfo> strategyParameters = ((IStrategy)g).AllPossibleParameters();
                        foreach (VarInfo vv in strategyParameters)
                        {
                            FillVarInfoRowInTable(vv);
                        }
                    }
                    //else if (g is CRA.Core.Preconditions.IStrategy)
                    //{

                    //    //recupero parametri nel vecchio modo
                    //    List<VarInfo> strategyParameters = new List<VarInfo>();
                     
                    //    foreach (MemberInfo mi in minf)
                    //    {
                    //        string varInfoProperty = mi.Name;
                    //        _counter++;
                    //        int pos = varInfoProperty.IndexOf(" ", 0, varInfoProperty.Length);
                    //        varInfoProperty = varInfoProperty.Substring(pos + 1, varInfoProperty.Length - (pos + 1));
                    //        CRA.Core.Preconditions.VarInfo vv = new CRA.Core.Preconditions.VarInfo();

                    //        try
                    //        {
                    //            vv =
                    //                (CRA.Core.Preconditions.VarInfo)
                    //                test.InvokeMember(varInfoProperty, BindingFlags.GetProperty, null, new CRA.Core.Preconditions.VarInfo(), null);

                    //            //avoid duplication of params
                    //            if (strategyParameters.Where(aa => aa.Name.Equals(vv.Name)).Count() == 0)
                    //            {
                    //                strategyParameters.Add(vv.ToVarInfo());
                    //            }
                    //        }
                    //        catch
                    //        {
                    //            // No action
                    //        }

                          
                    //    }
                    //    foreach (VarInfo p in strategyParameters)
                    //    {
                    //        FillVarInfoRowInTable(p);
                    //    }
                    //}
                    /* 3/sept/2012 davide fuma - modifica recupero parametri della strategy: non si recuperano piu' dalle proprieta' della strategy ma chiedendole al ModelingOptionsManager - NEW END */

                    /* 3/sept/2012 davide fuma - modifica recupero parametri della strategy: non si recuperano piu' dalle proprieta' della strategy ma chiedendole al ModelingOptionsManager - OLD START

                    foreach (MemberInfo mi in minf)
                    {
                        string varInfoProperty = mi.Name;
                        _counter++;
                        int pos = varInfoProperty.IndexOf(" ", 0, varInfoProperty.Length);
                        varInfoProperty = varInfoProperty.Substring(pos + 1, varInfoProperty.Length - (pos + 1));
                        VarInfo vv = new VarInfo();

                        try
                        {
                            vv =
                                (VarInfo)
                                test.InvokeMember(varInfoProperty, BindingFlags.GetProperty, null, new VarInfo(), null);
                            //TODO  :write here on dataset
                            dr = frmDCC.dataSet1.Tables["VarInfoVariables"].NewRow();
                            dr[0] = vv.Name;
                            dr[1] = vv.MinValue.ToString();
                            dr[2] = vv.MaxValue.ToString();
                            dr[3] = vv.DefaultValue.ToString();
                            dr[4] = vv.Units;
                            MemberInfo _var;
                            if (frmDCC.ChkExtendDomainClass.Checked || frmDCC.ChkLoadDomainClass.Checked)
                            {
                                _var = testb.GetProperty(vv.Name);
                            }
                            else
                            {
                                _var = test.GetProperty(vv.Name);
                            }
                            string _varNameType = _var.ToString();
                            int _pos = _varNameType.IndexOf(" ");
                            string _varNameOnly = _varNameType.Substring(_pos + 1);
                            // Define types (if array, get dimension from instance)
                            if (_varNameType.Contains("Double "))
                            {
                                dr[5] = "double";
                            }
                            else if (_varNameType.Contains("Double[]"))
                            {
                                double[] pi3 = (double[])g.GetType().GetProperty(_varNameOnly).GetValue(g, null);
                                int _dim = pi3.GetLength(0);
                                dr[5] = "double[" + _dim.ToString() + "]";
                            }
                            else if (_varNameType.Contains("Int32[]"))
                            {
                                int[] pi3 = (int[])g.GetType().GetProperty(_varNameOnly).GetValue(g, null);
                                int _dim = pi3.GetLength(0);
                                dr[5] = "int[" + _dim.ToString() + "]";
                            }
                            else if (_varNameType.Contains("Int32 "))
                            {
                                dr[5] = "int";
                            }
                            else if (_varNameType.Contains("Int[]"))
                            {
                                int[] pi3 = (int[])g.GetType().GetProperty(_varNameOnly).GetValue(g, null);
                                int _dim = pi3.GetLength(0);
                                dr[5] = "int[" + _dim.ToString() + "]";
                            }
                            else if (_varNameType.Contains("Int "))
                            {
                                dr[5] = "int";
                            }
                            else if (_varNameType.Contains("List") && _varNameType.Contains("String"))
                            {
                                dr[5] = "List<string>";
                            }
                            else if (_varNameType.Contains("List") && _varNameType.Contains("Double"))
                            {
                                dr[5] = "List<double>";
                            }
                            else if (_varNameType.Contains("List") && _varNameType.Contains("Int"))
                            {
                                dr[5] = "List<int>";
                            }
                            else if (_varNameType.Contains("Boolean "))
                            {
                                dr[5] = "string";
                            }
                            else if (_varNameType.Contains("Boolean[]"))
                            {
                                bool[] pi3 = (bool[])g.GetType().GetProperty(_varNameOnly).GetValue(g, null);
                                int _dim = pi3.GetLength(0);
                                dr[5] = "bool[" + _dim.ToString() + "]";
                            }
                            else if (_varNameType.Contains("Double[,]"))
                            {
                                double[,] pi3 = (double[,])g.GetType().GetProperty(_varNameOnly).GetValue(g, null);
                                int _dim = pi3.GetLength(0);
                                int _dim2 = pi3.GetLength(1);
                                dr[5] = "double[" + _dim.ToString() + 
                                    "," + _dim2.ToString() + "]";
                            }
                            // Set description
                            dr[6] = vv.Description;
                            // Set URL
                            if (vv.URL == null)
                            {
                                dr[7] = "http://agsys.cra-cin.it/tools/";
                            }
                            else
                            {
                                dr[7] = vv.URL;
                            }
                            frmDCC.dataSet1.Tables["VarInfoVariables"].Rows.Add(dr);
                        }
                        catch
                        {
                            // No action
                        }
                    }   
                        3/sept/2012 davide fuma - modifica recupero parametri della strategy: non si recuperano piu' dalle proprieta' della strategy ma chiedendole al ModelingOptionsManager - OLD END */

                }
                //domain class extension
                else if ( frmDCC.LoadDomainClassFromInterfacesDllAndExtendIt)
                {
                    // Reset for class extension
                    frmDCC.txtNameSpace.Text = "MyNamespace";
                    frmDCC.txtClassName.Text += "Extended";
                    frmDCC.txtDescription.Text = "Extension of " + frmDCC.txtDescription.Text;
                }
                //domain class simple
                else if (frmDCC.LoadDomainClassFromInterfacesDll && !frmDCC.LoadDomainClassFromInterfacesDllAndExtendIt)
                {
                    Type domainClassVarInfoType = test;

                    //only static properties of type VarInfo
                    var varinfosPropetyInfos = domainClassVarInfoType.GetProperties().Where(aa => aa.PropertyType.Equals(typeof(VarInfo)) && aa.GetGetMethod().IsStatic);
                
                    foreach (PropertyInfo vv in varinfosPropetyInfos)
                    {
                        FillVarInfoRowInTable((VarInfo)vv.GetValue(g,null));
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error loading the informations from the strategy:"+ex.Message);
            }
        }

            private void FillVarInfoRowInTable(VarInfo vv){
                        DataRow dr = frmDCC.dataSet1.Tables["VarInfoVariables"].NewRow();
                        dr[0] = vv.Name;
                        dr[1] = vv.MinValue.ToString();
                        dr[2] = vv.MaxValue.ToString();
                        dr[3] = vv.DefaultValue.ToString();
                        dr[4] = vv.Units;
                        int dim = 1;
                        if (vv.CurrentValue is Array) { dim=(vv.CurrentValue as Array).GetLength(0); }
                        if (vv.ValueType == null)
                        {
                            dr[5] = VarInfoValueTypes.GetInstanceForName("Double").Converter.GetTypeNameRepresentation(dim);
                        }
                        else
                        {
                            dr[5] = vv.ValueType.Converter.GetTypeNameRepresentation(dim);
                        }
                        dr[6] = vv.Description;                        
                        
                        //#LE:T24
                        //if (vv.URL == null)
                        if (String.IsNullOrEmpty(vv.URL))
                            {
                                //dr[7] = "http://agsys.cra-cin.it/tools/";
                                dr[7] = ConfigurationManager.AppSettings.Get("VarInfoParameterURI");
                            }
                        else
                            {
                                dr[7] = vv.URL;
                            }
                        frmDCC.dataSet1.Tables["VarInfoVariables"].Rows.Add(dr);
            }
        
    }
}