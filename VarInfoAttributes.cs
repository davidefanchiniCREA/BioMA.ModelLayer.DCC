using System;
using System.Data;

namespace CRA.ModelLayer.DCC
{
	/// <summary>
	/// Summary description for VarInfoAttributes.
	/// </summary>
	internal class VarInfoAttributes
	{
		internal VarInfoAttributes(string d)
		{
			ParseString(d);
		}

		internal VarInfoAttributes(DataRow d)
		{
			ParseDataRow(d);
		}

		private string _description = "";
		private string _name = "";
		private double _maxValue = double.NaN;
		private double _minValue = double.NaN;
		private double _defaultValue = double.NaN;
		private string _units = "";
		private string _type = "double";
        private string _URL = "";

		#region properties
		internal double maxValue
		{
			get
			{
				return _maxValue;
			}
			set
			{
				_maxValue = value;
			}
		}
		internal double minValue
		{
			get
			{
				return _minValue;
			}
			set
			{
				_minValue = value;
			}
		}		
		internal double defaultValue
		{
			get
			{
				return _defaultValue;
			}
			set
			{
				_defaultValue = value;
			}
		}

		internal string description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}
		internal string units
		{
			get
			{
				return _units;
			}
			set
			{
				_units = value;
			}
		}
		internal string name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
		internal string type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}
        internal string URL
        {
            get
            {
                return _URL;
            }
            set
            {
                _URL = value;
            }
        }
		#endregion

		private void ParseString(string d)
		{
			try
			{
				//find tab characters 7 times
				int pos = 0;
				_name = d.Substring(pos, d.IndexOf((char)9, pos + 1) - pos);
				pos = d.IndexOf((char)9, pos + 1) + 1;
				_minValue = double.Parse(d.Substring(pos, d.IndexOf((char)9, pos + 1) - pos));
				pos = d.IndexOf((char)9, pos + 1) + 1;
				_maxValue = double.Parse(d.Substring(pos, d.IndexOf((char)9, pos + 1) - pos));
				pos = d.IndexOf((char)9, pos + 1) + 1;
				_defaultValue = double.Parse(d.Substring(pos, d.IndexOf((char)9, pos + 1) - pos));
				pos = d.IndexOf((char)9, pos + 1) + 1;
				_units = d.Substring(pos, d.IndexOf((char)9, pos + 1) - pos);
				pos = d.IndexOf((char)9, pos + 1) + 1;
				_type = d.Substring(pos, d.IndexOf((char)9, pos + 1) - pos);
                pos = d.IndexOf((char)9, pos + 1) + 1;
                _description = d.Substring(pos, d.IndexOf((char)9, pos + 1) - pos);
                pos = d.IndexOf((char)9, pos + 1) + 1;
				_URL = d.Substring(pos, d.Length - pos);
			}
			catch (Exception err)
			{
				throw new Exception("Either the file is not tab separated, or it is not consistent in one or more record (missing values)", err);

			}
		}

		private void ParseDataRow(DataRow d)
		{
			try
			{
				_name = d["Name"].ToString();
				_minValue = double.Parse(d["MinValue"].ToString());
				_maxValue = double.Parse(d["MaxValue"].ToString());
				_defaultValue = double.Parse(d["DefaultValue"].ToString());
				_type = d["Type"].ToString();
				_units = d["Units"].ToString();
				_description = d["Description"].ToString();
                _URL = d["URL"].ToString();
			}
			catch (Exception err)
			{
				throw new Exception("The file is not consistent in one or more record (missing values", err);
			}
		}
	}
}
