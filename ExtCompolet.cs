using System;
using System.Windows.Forms;
using OMRON.Compolet.CIPCompolet64;

namespace common_compolet_pure
{
    public class ExtCompolet : OMRON.Compolet.CIPCompolet64.CommonCompolet
    {

        public ExtCompolet (System.ComponentModel.IContainer cont) : base(cont)
        {

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
    }

}