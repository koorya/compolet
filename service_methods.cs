using System;
using System.Threading;
using System.Windows.Forms;

using OMRON.Compolet.CIPCompolet64;

using NetMQ;
using NetMQ.Sockets;

using System.Text;

namespace common_compolet_pure
{
    partial class Form1
    {		

		
        private object RemoveBrackets(string val)
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

        private string GetValueOfVariables(object val)
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
						valStr += this.GetValueString(a) + ",";
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
							valStr += this.GetValueString(valArray.GetValue(i, j)) + ",";
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
								valStr += this.GetValueString(valArray.GetValue(i, j, z)) + ",";
							}
							valStr = valStr.TrimEnd(',');
							valStr += "]";
						}
					}
				}
			}
			else
			{
				valStr = this.GetValueString(val);
			}
			return valStr;
		}
        private string GetValueString(object val)
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