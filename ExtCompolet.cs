using System;
using System.Windows.Forms;
using OMRON.Compolet.CIPCompolet64;
using System.Collections.Generic;

using System.IO;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace common_compolet_pure
{
	[Serializable] 
    public partial class ExtCompolet : OMRON.Compolet.CIPCompolet64.CommonCompolet
    {

		public string plc_name;
		public List<plcvariable> plc_var_list;
        public ExtCompolet (System.ComponentModel.IContainer cont) : base(cont)
        {
			plc_var_list = new List<plcvariable>();
        }
        public ExtCompolet (System.ComponentModel.IContainer cont, FileStream fs) : base(cont)
        {
			this.deserialize(fs);
        }
        public ExtCompolet (System.ComponentModel.IContainer cont, ExtComp_serial deser) : base(cont)
        {
			this.deserialize(deser);
        }
        public object WriteVar(string name, object value)
        {
            try
            {
                // write
                object val = value.ToString();
                if(this.GetVariableInfo(name).Type == VariableType.STRUCT)
                {
                    val = this.ObjectToByteArray(val);
                }
                this.WriteVariable(name, val);

                // read
                return this.readFromPlc(name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "this.Text", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

		public object readFromPlc(string varname)
		{
			try
			{
				object obj = this.ReadVariable(varname);
				if (obj == null)
				{
					throw new NotSupportedException();
				}

				VariableInfo info = this.GetVariableInfo(varname);

                return obj;
			}
			catch (Exception ex)
			{
                System.Console.WriteLine("exept");
				MessageBox.Show(ex.Message, "this.Text", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
			}
		}

        public byte[] ObjectToByteArray(object obj)
		{
			if(obj is Array)
			{
				Array arr = obj as Array;
				Byte[] bin = new Byte[arr.Length];
				for(int i = 0 ; i < bin.Length ; i++)
				{
					bin[i] = Convert.ToByte(arr.GetValue(i));
				}
				return bin;
			}
			else
			{
				return new Byte[1]{ Convert.ToByte(obj) };
			}
		}

        public static string GetValueOfVariables(object val)
		{
			string valStr = string.Empty;
			if (val.GetType().IsArray)
			{
				Array valArray = val as Array;
				if (valArray.Rank == 1)
				{
					valStr += "[";
					foreach (object a in valArray)
					{
						valStr += ExtCompolet.GetValueString(a) + ",";
					}
					valStr = valStr.TrimEnd(',');
					valStr += "]";
				}
				else if (valArray.Rank == 2)
				{
					for (int i = 0; i <= valArray.GetUpperBound(0); i++)
					{
						valStr += "[";
						for (int j = 0; j <= valArray.GetUpperBound(1); j++)
						{
							valStr += ExtCompolet.GetValueString(valArray.GetValue(i, j)) + ",";
						}
						valStr = valStr.TrimEnd(',');
						valStr += "]";
					}
				}
				else if (valArray.Rank == 3)
				{
					for (int i = 0; i <= valArray.GetUpperBound(0); i++)
					{
						for (int j = 0; j <= valArray.GetUpperBound(1); j++)
						{
							valStr += "[";
							for (int z = 0; z <= valArray.GetUpperBound(2); z++)
							{
								valStr += ExtCompolet.GetValueString(valArray.GetValue(i, j, z)) + ",";
							}
							valStr = valStr.TrimEnd(',');
							valStr += "]";
						}
					}
				}
			}
			else
			{
				valStr = ExtCompolet.GetValueString(val);
			}
			return valStr;
		}
		static private object RemoveBrackets(string val)
		{
			object obj = string.Empty;
			if (val.IndexOf("[") >= 0)
			{
				string str = val.Trim('[', ']');
				str = str.Replace("][", ",");
				obj = str.Split(',');
			}
			else
			{
				obj = val;
			}
			return obj;
		}


        static private string GetValueString(object val)
		{
			if (val is float || val is double)
			{
				return string.Format("{0:R}", val);
			}
			else
			{
				return val.ToString();
			}
		}
    }

}