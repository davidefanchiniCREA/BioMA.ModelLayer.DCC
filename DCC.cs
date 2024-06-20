using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using CRA.ModelLayer.Strategy;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using CRA.ModelLayer.Core;

using System.Runtime.CompilerServices;


namespace CRA.ModelLayer.DCC
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
    /// 
   
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSave;
        internal TextBox txtNameSpace;
		private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label label1;
        internal TextBox txtClassName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnAbout;
		private System.Windows.Forms.Button btnExit;
		// TODO DataGrid is no longer supported. Use DataGridView instead. For more details see https://docs.microsoft.com/en-us/dotnet/core/compatibility/winforms#removed-controls
		private System.Windows.Forms.DataGridView dataGrid1;
		internal System.Data.DataSet dataSet1;
		private System.Data.DataTable VarInfoVariables;
		private System.Data.DataColumn dataColumn1;
		private System.Data.DataColumn dataColumn2;
		private System.Data.DataColumn dataColumn3;
		private System.Data.DataColumn dataColumn4;
		private System.Data.DataColumn dataColumn5;
		private System.Data.DataColumn dataColumn6;
		private System.Data.DataColumn dataColumn7;		
		private System.Windows.Forms.Button btnSaveXML;
        private DataColumn dataColumn8;
        private Label label3;
        internal TextBox txtURL;
        private Label label4;
        internal TextBox txtDescription;
        private DataTable dataTable1;
        private DataColumn dataColumn9;
        private DataColumn dataColumn10;
        private DataColumn dataColumn11;
        private DataColumn dataColumn12;
        internal ComboBox cmbLanguage;
        private FolderBrowserDialog folderBrowserDialog1;
        private ToolTip toolTip1;
        private IContainer components;
        private Label label5;
        internal Label lblDomainClassToBeExtended;
        internal Label lblHiddenAssemblyName;
        private Button buttonLoadParamClassFromStrategy;
        private Button buttonLoadDomainClass;
        private RadioButton radioButton1;
        internal RadioButton radioButton2;
        private GroupBox groupBox1;
        private Button buttonExtendDomainClass;
        private Button btnDeveloper;
	    internal StrategyClassLoader frd;

        private const string devdataFile = "AuthorSettings.xml";
        private DataSet developerData = new DataSet();

		public Form1()
		{
            // Check if first time
            if (File.Exists("Lutil.lsf"))
            {
                MLLicense lic = new MLLicense();
                lic.ShowDialog();
            }
            
            //
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            radioButton1.Checked = true;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            // TODO DataGrid is no longer supported. Use DataGridView instead. For more details see https://docs.microsoft.com/en-us/dotnet/core/compatibility/winforms#removed-controls
            this.dataGrid1 = new System.Windows.Forms.DataGridView();
            this.VarInfoVariables = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();            
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataColumn10 = new System.Data.DataColumn();
            this.dataColumn11 = new System.Data.DataColumn();
            this.dataColumn12 = new System.Data.DataColumn();
            this.btnSaveXML = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.lblDomainClassToBeExtended = new System.Windows.Forms.Label();
            this.lblHiddenAssemblyName = new System.Windows.Forms.Label();
            this.buttonLoadParamClassFromStrategy = new System.Windows.Forms.Button();
            this.buttonLoadDomainClass = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonExtendDomainClass = new System.Windows.Forms.Button();
            this.btnDeveloper = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VarInfoVariables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpen.Location = new System.Drawing.Point(8, 16);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(104, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "&open";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSave.Location = new System.Drawing.Point(105, 384);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 24);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "&generate code";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Location = new System.Drawing.Point(332, 96);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(201, 20);
            this.txtNameSpace.TabIndex = 3;
            this.txtNameSpace.Text = "NameSpace";
            // 
            // lblFileName
            // 
            this.lblFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFileName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblFileName.Location = new System.Drawing.Point(120, 16);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(803, 23);
            this.lblFileName.TabIndex = 4;
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(254, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "name space";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(120, 96);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(128, 20);
            this.txtClassName.TabIndex = 7;
            this.txtClassName.Text = "ClassName";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "domain class name ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAbout.Location = new System.Drawing.Point(858, 353);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(65, 24);
            this.btnAbout.TabIndex = 9;
            this.btnAbout.Text = "&about";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnExit.Location = new System.Drawing.Point(858, 384);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(67, 24);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "&exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dataGrid1
            // 
            //this.dataGrid1.AlternatingBackColor = System.Drawing.Color.GhostWhite;
            this.dataGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid1.BackColor = System.Drawing.Color.GhostWhite;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.Lavender;
            this.dataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            //this.dataGrid1.CaptionBackColor = System.Drawing.Color.RoyalBlue;
            //this.dataGrid1.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            //this.dataGrid1.CaptionForeColor = System.Drawing.Color.White;
            this.dataGrid1.DataMember = "";
            this.dataGrid1.DataSource = this.VarInfoVariables;
            //this.dataGrid1.FlatMode = true;
            this.dataGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.dataGrid1.ForeColor = System.Drawing.Color.MidnightBlue;
            //this.dataGrid1.GridLineColor = System.Drawing.Color.RoyalBlue;
            //this.dataGrid1.HeaderBackColor = System.Drawing.Color.MidnightBlue;
            //this.dataGrid1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            //this.dataGrid1.HeaderForeColor = System.Drawing.Color.Lavender;
            //this.dataGrid1.LinkColor = System.Drawing.Color.Teal;
            this.dataGrid1.Location = new System.Drawing.Point(8, 126);
            this.dataGrid1.Name = "dataGrid1";
            //this.dataGrid1.ParentRowsBackColor = System.Drawing.Color.Lavender;
            //this.dataGrid1.ParentRowsForeColor = System.Drawing.Color.MidnightBlue;
            //this.dataGrid1.PreferredColumnWidth = 80;
            //this.dataGrid1.SelectionBackColor = System.Drawing.Color.Teal;
            //this.dataGrid1.SelectionForeColor = System.Drawing.Color.PaleGreen;
            this.dataGrid1.Size = new System.Drawing.Size(920, 211);
            this.dataGrid1.TabIndex = 11;
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            //this.dataGrid1.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.dataGrid1_ControlAdded);
            this.dataGrid1.EditingControlShowing += DataGrid1_EditingControlShowing;
            // 
            // VarInfoVariables
            // 
            this.VarInfoVariables.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8});
            this.VarInfoVariables.TableName = "VarInfoVariables";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "Name";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "MinValue";
            this.dataColumn2.DataType = typeof(double);
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "MaxValue";
            this.dataColumn3.DataType = typeof(double);
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "DefaultValue";
            this.dataColumn4.DataType = typeof(double);
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "Units";
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "Type";
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "Description";
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "URL";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.VarInfoVariables,
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn9,
            this.dataColumn10,
            this.dataColumn11,
            this.dataColumn12});
            this.dataTable1.TableName = "Description";
            // 
            // dataColumn9
            // 
            this.dataColumn9.ColumnName = "DomainClassNameSpace";
            // 
            // dataColumn10
            // 
            this.dataColumn10.ColumnName = "DomainTypeName";
            // 
            // dataColumn11
            // 
            this.dataColumn11.ColumnName = "DomainTypeURL";
            // 
            // dataColumn12
            // 
            this.dataColumn12.ColumnName = "DomainTypeDescription";
            // 
            // btnSaveXML
            // 
            this.btnSaveXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveXML.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSaveXML.Location = new System.Drawing.Point(8, 384);
            this.btnSaveXML.Name = "btnSaveXML";
            this.btnSaveXML.Size = new System.Drawing.Size(91, 24);
            this.btnSaveXML.TabIndex = 12;
            this.btnSaveXML.Text = "save &XML";
            this.btnSaveXML.Click += new System.EventHandler(this.btnSaveXML_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(539, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "URL";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtURL
            // 
            this.txtURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtURL.Location = new System.Drawing.Point(580, 95);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(343, 20);
            this.txtURL.TabIndex = 14;
            this.txtURL.Text = "http://";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 26);
            this.label4.TabIndex = 15;
            this.label4.Text = "domain class description";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(119, 49);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(414, 38);
            this.txtDescription.TabIndex = 16;
            this.txtDescription.Text = "Domain class description";
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Items.AddRange(new object[] {
            "C#"});
            this.cmbLanguage.Location = new System.Drawing.Point(202, 387);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(46, 21);
            this.cmbLanguage.TabIndex = 17;
            this.cmbLanguage.Text = "C#";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(543, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 43);
            this.label5.TabIndex = 23;
            this.label5.Text = "domain class loaded/to be extended";
            // 
            // lblDomainClassToBeExtended
            // 
            this.lblDomainClassToBeExtended.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDomainClassToBeExtended.BackColor = System.Drawing.SystemColors.Window;
            this.lblDomainClassToBeExtended.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDomainClassToBeExtended.Location = new System.Drawing.Point(616, 49);
            this.lblDomainClassToBeExtended.Name = "lblDomainClassToBeExtended";
            this.lblDomainClassToBeExtended.Size = new System.Drawing.Size(307, 38);
            this.lblDomainClassToBeExtended.TabIndex = 24;
            // 
            // lblHiddenAssemblyName
            // 
            this.lblHiddenAssemblyName.AutoSize = true;
            this.lblHiddenAssemblyName.Location = new System.Drawing.Point(564, 0);
            this.lblHiddenAssemblyName.Name = "lblHiddenAssemblyName";
            this.lblHiddenAssemblyName.Size = new System.Drawing.Size(0, 13);
            this.lblHiddenAssemblyName.TabIndex = 25;
            this.lblHiddenAssemblyName.Visible = false;
            // 
            // buttonLoadParamClassFromStrategy
            // 
            this.buttonLoadParamClassFromStrategy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadParamClassFromStrategy.Enabled = false;
            this.buttonLoadParamClassFromStrategy.Location = new System.Drawing.Point(424, 356);
            this.buttonLoadParamClassFromStrategy.Name = "buttonLoadParamClassFromStrategy";
            this.buttonLoadParamClassFromStrategy.Size = new System.Drawing.Size(169, 22);
            this.buttonLoadParamClassFromStrategy.TabIndex = 26;
            this.buttonLoadParamClassFromStrategy.Text = "Load parameters from strategy";
            this.buttonLoadParamClassFromStrategy.UseVisualStyleBackColor = true;
            this.buttonLoadParamClassFromStrategy.Visible = false;
            this.buttonLoadParamClassFromStrategy.Click += new System.EventHandler(this.buttonLoadParamClassFromStrategy_Click);
            // 
            // buttonLoadDomainClass
            // 
            this.buttonLoadDomainClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadDomainClass.Location = new System.Drawing.Point(424, 384);
            this.buttonLoadDomainClass.Name = "buttonLoadDomainClass";
            this.buttonLoadDomainClass.Size = new System.Drawing.Size(169, 24);
            this.buttonLoadDomainClass.TabIndex = 27;
            this.buttonLoadDomainClass.Text = "Load domain class from library";
            this.buttonLoadDomainClass.UseVisualStyleBackColor = true;
            this.buttonLoadDomainClass.Click += new System.EventHandler(this.buttonLoadDomainClass_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(25, 16);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(112, 17);
            this.radioButton1.TabIndex = 28;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Domain class        ";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(171, 16);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(126, 17);
            this.radioButton2.TabIndex = 29;
            this.radioButton2.Text = "Parameters class       ";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(8, 340);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 39);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Code generation options";
            // 
            // buttonExtendDomainClass
            // 
            this.buttonExtendDomainClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExtendDomainClass.Location = new System.Drawing.Point(615, 384);
            this.buttonExtendDomainClass.Name = "buttonExtendDomainClass";
            this.buttonExtendDomainClass.Size = new System.Drawing.Size(234, 24);
            this.buttonExtendDomainClass.TabIndex = 30;
            this.buttonExtendDomainClass.Text = "Load domain class from library and extend it";
            this.buttonExtendDomainClass.UseVisualStyleBackColor = true;
            this.buttonExtendDomainClass.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDeveloper
            // 
            this.btnDeveloper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeveloper.Location = new System.Drawing.Point(257, 385);
            this.btnDeveloper.Name = "btnDeveloper";
            this.btnDeveloper.Size = new System.Drawing.Size(91, 24);
            this.btnDeveloper.TabIndex = 31;
            this.btnDeveloper.Text = "developer";
            this.btnDeveloper.UseVisualStyleBackColor = true;
            this.btnDeveloper.Click += new System.EventHandler(this.btnDeveloper_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(937, 420);
            this.Controls.Add(this.btnDeveloper);
            this.Controls.Add(this.buttonExtendDomainClass);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonLoadDomainClass);
            this.Controls.Add(this.lblHiddenAssemblyName);
            this.Controls.Add(this.lblDomainClassToBeExtended);
            this.Controls.Add(this.buttonLoadParamClassFromStrategy);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbLanguage);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSaveXML);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtClassName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.txtNameSpace);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnOpen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(794, 302);
            this.Name = "Form1";
            this.Text = "Domain Classes Coder";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VarInfoVariables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}


        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
		static void Main() 
		{
			Application.EnableVisualStyles();
            Form1 fr = new Form1();
		    Application.Run(fr);
		}
        

	    private void btnOpen_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.Filter = "Tab separated (*.txt), XML with schema (*.xml)|*.txt; *.xml";
            //#LE:T23
            //openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblFileName.Text = openFileDialog1.FileName;
                if (lblFileName.Text.ToLower().EndsWith("txt"))
                {
                    LoadData(lblFileName.Text);
                }
                else
                {
                    LoadDataXML(lblFileName.Text);
                }
            }
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if (dataSet1.Tables["VarInfoVariables"].Rows.Count>0)
			{
				if (txtNameSpace.Text == "")
				{
					MessageBox.Show("No name space specified!", "Domain Classes Coder");
				}
				else if (txtClassName.Text == "")
				{
					MessageBox.Show("No class name specified!", "Domain Classes Coder");
				}
                else if (CreateParametersClass)
                {
                    bool _save = true;
                    if ((string)dataSet1.Tables["VarInfoVariables"].Rows[0]["Type"] != "string")
                    {
                        MessageBox.Show("The type of the first record must be string (parameter key) to generate code of a parameter class", "Domain Classes Coder");
                        _save = false;
                    }
                    for (int i = 0; i < dataSet1.Tables["VarInfoVariables"].Rows.Count; i++)
                    {
                        if (dataSet1.Tables["VarInfoVariables"].Rows[i]["Type"] is System.DBNull)
                        {
                            MessageBox.Show("One or more of the types is null!",
                                            "Domain Classes Coder");
                            _save = false;
                        }
                        else
                        {
                            string _type = (string) dataSet1.Tables["VarInfoVariables"].Rows[i]["Type"];
                            _type = _type.Trim();
                            if (_type != "string" &&
                                !_type.Contains("Dictionary<string") &&
                                !_type.Contains("Dictionary<double") &&
                                !_type.Contains("Dictionary<int") &&  //this should be redundant
                                !_type.Contains("double") &&
                                !_type.Contains("List<double") &&
                                !_type.Contains("List<int") &&
                                !_type.Contains("int")) // Include test for List<double> and List<int>, allowed
                            {
                                MessageBox.Show("The type " + _type + " is not allowed in parameter classes!",
                                                "Domain Classes Coder");
                                _save = false;
                            }
                        }
                    }
		            if (_save)
		            {
                        try
                        {
                            SaveData();
                        } catch (DCCException exc)
                        {
                            MessageBox.Show(exc.Message,
                                                "Domain Classes Coder");
                        }
		            }
                }
				else
				{
					SaveData();
				}
			}
			else
			{
				MessageBox.Show("Records to export must be > 0!", "Domain Classes Coder");
			}
		}
		private void LoadDataXML(string _fileName)
		{
			try 
			{
				dataSet1.Clear();
				dataSet1.ReadXml(_fileName);
                txtNameSpace.Text = dataSet1.Tables["Description"].Rows[0][0].ToString();
                txtClassName.Text = dataSet1.Tables["Description"].Rows[0][1].ToString();
                txtURL.Text = dataSet1.Tables["Description"].Rows[0][2].ToString();
			    txtDescription.Text = dataSet1.Tables["Description"].Rows[0][3].ToString();
			}
			catch (Exception e) 
			{
				// Let the user know what went wrong.
				throw new Exception("Could not read the XML file", e);
			}

		}

		private void LoadData(string _fileName)
		{
            try 
            {
                 //Create an instance of StreamReader to read from a file.
				StreamReader sr = new StreamReader(_fileName); 
				{
					string line;
					int lineNumber = 0;
					// Read and display lines from the file until the end of 
					// the file is reached.
						while ((line = sr.ReadLine()) != null) 
						{
							if (lineNumber>0) // Skip header row
							{
								VarInfoAttributes v = new VarInfoAttributes(line.ToString());
								DataTable workTable = dataSet1.Tables["VarInfoVariables"];
								DataRow workRow = workTable.NewRow();
								workRow["Name"] = v.name;
								workRow["MinValue"] = v.minValue;
								workRow["Maxvalue"] = v.maxValue;
								workRow["DefaultValue"] = v.defaultValue;
								workRow["Units"] = v.units;
								workRow["Type"] = v.type;
								workRow["Description"] = v.description;
                                workRow["URL"] = v.URL;
								workTable.Rows.Add(workRow);
							}
							lineNumber++;
						}
				}
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                throw new Exception("An error occurred while trying to read the tab-separated file", e);
            }
		}

		private void SaveData()
		{
            //language specific settings
		    string _extension;
		    CodeDomProvider gen;
		    if (cmbLanguage.Text == "VB")
		    {
		        _extension = ".vb";
                gen = new VBCodeProvider();
		    }
		    else 
		    {
                _extension = ".cs";
                gen = new CSharpCodeProvider();
		    }

		    string _par = String.Empty;
            if (CreateParametersClass)
            {
                _par = "_Parameters";
            }
		    string _fileNameDomainClass = txtClassName.Text + _par + _extension;
            string _fileNameVarInfoClass = _fileNameDomainClass.Substring(0, _fileNameDomainClass.Length - 3) + "VarInfo" + _extension;

            List<CodeCommentStatement> listComments = new List<CodeCommentStatement>();
            // Blank line
            CodeCommentStatement comment = new CodeCommentStatement(new CodeComment(String.Empty, true));
            listComments.Add(comment);
            // Reference to the file which is origin of this class  
            comment = new CodeCommentStatement(new CodeComment("This class was created from file " + openFileDialog1.FileName, true));
            listComments.Add(comment);
            // Reference to the tools used to create it
            comment = new CodeCommentStatement(new CodeComment("The tool used was: DCC - Domain Class Coder, http://components.biomamodelling.org/, DCC", true));
            listComments.Add(comment);
            comment = new CodeCommentStatement(new CodeComment(DateTime.Now.ToString(), true));
            listComments.Add(comment);
            // Retrive developer data
            if (File.Exists(devdataFile))
            {
                try
                {
                    developerData.ReadXml(devdataFile);
                    string lastname = developerData.Tables[0].Rows[0][0].ToString();
                    string email = developerData.Tables[0].Rows[0][1].ToString();
                    string institution = developerData.Tables[0].Rows[0][2].ToString();
                    string URL = developerData.Tables[0].Rows[0][3].ToString();
                    comment = new CodeCommentStatement(new CodeComment(lastname, true));
                    listComments.Add(comment);
                    comment = new CodeCommentStatement(new CodeComment(email, true));
                    listComments.Add(comment);
                    comment = new CodeCommentStatement(new CodeComment(institution, true));
                    listComments.Add(comment);
                    comment = new CodeCommentStatement(new CodeComment(URL, true));
                    listComments.Add(comment);
                }
                catch
                {
                    throw new Exception("The file AuthorSettings.xml is corrupted; author data will not be saved in the header of the files generated.");
                }
            }
            

            //local
            DataTable dt = dataSet1.Tables["VarInfoVariables"];

            //Create graph for Domain class            
		    BuildDomainClass bdc = new BuildDomainClass();
            CodeCompileUnit compileUnit = bdc.WriteDomainClass(this,
                                                               dataSet1,
                                                               listComments);
            
		    folderBrowserDialog1.ShowNewFolderButton = true;
		    string _dialogDescription = "Select folder to save files:\t\n" +
		                                _fileNameDomainClass + "\t\n";
            
            
            //Create graph for VarInfo class if parameter class option unchecked
                BuildVarInfo bvi = new BuildVarInfo();
                CodeCompileUnit compileUnit2 = bvi.WriteVarInfo(this,
                                                                dataSet1,
                                                                listComments);
                if (!CreateParametersClass)
            {
                _dialogDescription += _fileNameVarInfoClass;
            }
		    folderBrowserDialog1.Description = _dialogDescription;
                
            
		    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
		    {
		        //Add path to classes
		        _fileNameDomainClass = folderBrowserDialog1.SelectedPath + "\\" + _fileNameDomainClass;
		        _fileNameVarInfoClass = folderBrowserDialog1.SelectedPath + "\\" + _fileNameVarInfoClass;

		        //Code generation options
		        CodeGeneratorOptions cgo = new CodeGeneratorOptions();
		        cgo.BracingStyle = "C";

		        // Create a TextWriter to a StreamWriter to an output file.
		        IndentedTextWriter tw = new IndentedTextWriter(new StreamWriter(_fileNameDomainClass, false), "    ");
		        // Generate source code using the code generator.
		        gen.GenerateCodeFromCompileUnit(compileUnit, tw, cgo);
		        // Close the output file.
		        tw.Close();

                if (!CreateParametersClass)
                {
		            // Create a TextWriter to a StreamWriter to an output file.
		            IndentedTextWriter tw2 = new IndentedTextWriter(new StreamWriter(_fileNameVarInfoClass, false), "    ");
		            // Generate source code using the code generator.
		            gen.GenerateCodeFromCompileUnit(compileUnit2, tw2, cgo);
		            // Close the output file
		            tw2.Close();
		        }

		        //end save
		        string msgSave = "Code files saved!\t\n\t\nFiles:\t\n" +
		                         _fileNameDomainClass;
                if (!CreateParametersClass)
                {
                    msgSave += "\t\n" + _fileNameVarInfoClass;
                }
                MessageBox.Show(msgSave, "Domain Classes Coder");
		    }
		}

		private void btnAbout_Click(object sender, System.EventArgs e)
		{
            AboutBox f = new AboutBox();
			f.ShowDialog();
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void btnSaveXML_Click(object sender, System.EventArgs e)
		{
            try
            {
                dataSet1.DataSetName = txtClassName.Text;
                //
                //TODO:check if rows.count >0, then add, otherwise write on row[0]
                //
                if (dataSet1.Tables["Description"].Rows.Count > 0)
                {
                    dataSet1.Tables["Description"].Rows[0]["DomainTypeDescription"] = txtDescription.Text;
                    dataSet1.Tables["Description"].Rows[0]["DomainTypeURL"] = txtURL.Text;
                    dataSet1.Tables["Description"].Rows[0]["DomainClassNameSpace"] = txtNameSpace.Text;
                    dataSet1.Tables["Description"].Rows[0]["DomainTypeName"] = txtClassName.Text;
                }
                else
                {
                    DataRow dr = dataSet1.Tables["Description"].NewRow();
                    dr["DomainTypeDescription"] = txtDescription.Text;
                    dr["DomainTypeURL"] = txtURL.Text;
                    dr["DomainClassNameSpace"] = txtNameSpace.Text;
                    dr["DomainTypeName"] = txtClassName.Text;
                    dataSet1.Tables["Description"].Rows.Add(dr);
                }
                //saveFileDialog1.Filter = "XML files (*.xml) | *.xml";
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileNameDomainClassXml = folderBrowserDialog1.SelectedPath + "\\" + txtNameSpace.Text +
                        "_" + txtClassName.Text + ".xml";
                    dataSet1.WriteXml(fileNameDomainClassXml, System.Data.XmlWriteMode.WriteSchema);
                    MessageBox.Show("Class description " + fileNameDomainClassXml + " saved!");
                }
            }
            catch (Exception err)
            {
                throw new Exception("Could not save file!", err);
            }
		}

        private void Form1_Load(object sender, EventArgs e)
        {
            //// Create the ToolTip and associate with the Form container.

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 50;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(txtClassName, "Name of the class which will be generated.");
            toolTip1.SetToolTip(txtDescription, "Description of the domain type.");
            toolTip1.SetToolTip(txtNameSpace, "Name space of the component where the domain class will be implemented.");
            toolTip1.SetToolTip(txtURL, "Web link to a definition of the domain type.");
            toolTip1.SetToolTip(dataGrid1, "Properties of the domain classes.");
            toolTip1.SetToolTip(btnOpen, "Load txt or XML file with properties of the domain class.");
            toolTip1.SetToolTip(btnSave, "Save source code of value and VarInfo classes.");
            toolTip1.SetToolTip(btnSaveXML, "Save XML file (re-loadable) with properties of the domain class.");
          
         
        
        }

        private void chkParametersClass_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void chkUseDoaminClass_CheckedChanged(object sender, EventArgs e)
        {
            string _FormTitle = String.Empty;
            if (LoadParametersClassFromStrategyDll)
            {
                if (LoadDomainClassFromInterfacesDllAndExtendIt)
                {
                    _FormTitle = "Load assembly with IDomainClass realizations"; 
                }
                else
                {
                    _FormTitle = "Load assembly with IStrategy realizations";
                }
                openFileDialog1.Title = _FormTitle;
                openFileDialog1.Filter = "Assembly (*.dll)|*.dll";
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    frd = new StrategyClassLoader(this);

                    string fname = openFileDialog1.FileName;
                    lblHiddenAssemblyName.Text = fname;
                    frd.lblDomainClass.Text = fname;
                    Assembly a = Assembly.LoadFrom(fname);
                    Type[] types = a.GetTypes();
                    foreach (Type t in types)
                    {
                        try
                        {
                           if (t.IsClass && t.IsPublic)
                            {
                                if (LoadDomainClassFromInterfacesDllAndExtendIt || LoadDomainClassFromInterfacesDll)
                                {
                                    if (a.CreateInstance(t.ToString()) is IVarInfoClass)
                                    {
                                        frd.comboBox1.Items.Add(t.ToString());
                                    }
                                }
                                else
                                {
                                if (a.CreateInstance(t.ToString()) is IStrategy)
                                    {
                                        frd.comboBox1.Items.Add(t.ToString());
                                    }
                                }
                            }
                        }
                        catch 
                        {
                        }
                        frd.Show();
                    }
                }
            }
        }

       

        private void ChkLoadDomainClass_CheckedChanged(object sender, EventArgs e)
        {
            if (LoadDomainClassFromInterfacesDll)
            {
                CreateParametersClass = false;
                LoadParametersClassFromStrategyDll = true;
                OpenLoadDll();
                
            }
            else
            {
                CreateParametersClass = false;
                LoadParametersClassFromStrategyDll = false;
                
                lblDomainClassToBeExtended.Text = String.Empty;
            }
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGrid1.CurrentCell == null)
            {
                typeColumnSelected = false;
            }
            else
            {
                typeColumnSelected = VarInfoVariables.Columns[dataGrid1.CurrentCell.ColumnIndex].ColumnName.Equals("Type");
            }
        }

        private bool typeColumnSelected;

        private void DataGrid1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl tb)
            {
                tb.KeyDown -= Control_KeyDown;
                tb.KeyDown += Control_KeyDown;
            }
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (typeColumnSelected)
            {
                TypeSelection typeSelection = new TypeSelection();
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
                
                typeSelection.TypeTextBox = (TextBox)sender;
                typeSelection.ShowDialog(this);
            }
        }

        private void OpenLoadDll() {

            string _FormTitle = String.Empty;

            if (LoadDomainClassFromInterfacesDllAndExtendIt || LoadDomainClassFromInterfacesDll)
            {
                _FormTitle = "Load assembly with IDomainClass realizations";
            }
            else
            {
                _FormTitle = "Load assembly with IStrategy realizations";
            }
          
            openFileDialog1.Title = _FormTitle;
            openFileDialog1.Filter = "Assembly (*.dll)|*.dll";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                frd = new StrategyClassLoader(this);

                string fname = openFileDialog1.FileName;
                lblHiddenAssemblyName.Text = fname;
                frd.lblDomainClass.Text = fname;
                Assembly a = Assembly.LoadFrom(fname);
                Type[] types = a.GetTypes();
                foreach (Type t in types)
                {
                    try
                    {
                        if (t.IsClass && t.IsPublic)
                        {
                            if (LoadDomainClassFromInterfacesDllAndExtendIt || LoadDomainClassFromInterfacesDll)
                            {
                                if (a.CreateInstance(t.ToString()) is IVarInfoClass)
                                {
                                    frd.comboBox1.Items.Add(t.ToString());
                                }
                            }
                            else
                            {
                                if (a.CreateInstance(t.ToString()) is IStrategy || a.CreateInstance(t.ToString()) is CRA.ModelLayer.Strategy.IStrategy)
                                {
                                    frd.comboBox1.Items.Add(t.ToString());
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    
                }
                frd.Show();
            }
            
        }

        private void buttonLoadParamClassFromStrategy_Click(object sender, EventArgs e)
        {
            LoadDomainClassFromInterfacesDllAndExtendIt = false;
            LoadParametersClassFromStrategyDll = true;
            LoadDomainClassFromInterfacesDll = false;
            OpenLoadDll();
        }

        private void buttonLoadDomainClass_Click(object sender, EventArgs e)
        {
            LoadDomainClassFromInterfacesDllAndExtendIt = false;
            LoadDomainClassFromInterfacesDll = true;
            LoadParametersClassFromStrategyDll = false;
            OpenLoadDll();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton1.Checked)//parameters class
            {
                buttonLoadParamClassFromStrategy.Enabled = true;
                buttonLoadParamClassFromStrategy.Visible = true;
                buttonLoadDomainClass.Enabled = false;
                buttonLoadDomainClass.Visible = false;
                buttonExtendDomainClass.Enabled = false;
                buttonExtendDomainClass.Visible = false;               
                CreateParametersClass = true;
                LoadDomainClassFromInterfacesDllAndExtendIt = false;
            }
            else //domain class
            {
                buttonLoadParamClassFromStrategy.Enabled = false;
                buttonLoadParamClassFromStrategy.Visible = false;
                buttonLoadDomainClass.Enabled = true;
                buttonLoadDomainClass.Visible = true;
                buttonExtendDomainClass.Enabled = true;
                buttonExtendDomainClass.Visible = true;
                CreateParametersClass = false;
                LoadDomainClassFromInterfacesDllAndExtendIt = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        internal bool CreateParametersClass = false;
        internal bool LoadParametersClassFromStrategyDll = false;
        internal bool LoadDomainClassFromInterfacesDll = false;
        internal bool LoadDomainClassFromInterfacesDllAndExtendIt = false;

        private void button1_Click(object sender, EventArgs e)
        {
            CreateParametersClass = false;            
            LoadDomainClassFromInterfacesDll = false;
            LoadDomainClassFromInterfacesDllAndExtendIt = true;
            LoadParametersClassFromStrategyDll = false;
            OpenLoadDll();
            
        }

        private void btnDeveloper_Click(object sender, EventArgs e)
        {
            DeveloperData devdata = new DeveloperData();
            devdata.ShowDialog();
        }

      
	}
}
