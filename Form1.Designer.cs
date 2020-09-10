using System;
using System.Windows.Forms;
using System.Threading;
using OMRON.Compolet.CIPCompolet64;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using System.IO;
using System.Threading.Tasks;

namespace common_compolet_pure
{
    partial class Form1
    {

        private System.ComponentModel.IContainer components = null;

        private ComboBox plcName;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
            
            plc_conn = new List<ExtCompolet>();

            FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate);
            
            ValueTask<List<ExtComp_serial>> _deser = JsonSerializer.DeserializeAsync<List<ExtComp_serial>>(fs);

            while(_deser.IsCompleted);

            foreach (ExtComp_serial deser in _deser.Result)
            {
                ExtCompolet plc = new ExtCompolet(this.components, deser);
                plc_conn.Add(plc);
            }

            List<ExtComp_serial> ser_list = new List<ExtComp_serial>();
            foreach (ExtCompolet plc in plc_conn)
            {
                ExtComp_serial ser = plc.convert_to_serial();
                ser_list.Add(ser);
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            using (FileStream _fs = new FileStream("user1.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.SerializeAsync<List<ExtComp_serial>>(_fs, ser_list, options);
                Console.WriteLine("Data has been saved to file");
            }

            plc_form_list = new List<PLCForm>();
            foreach(ExtCompolet plc in plc_conn)
            {
                PLCForm plc_form = new PLCForm(plc);
                plc_form_list.Add(plc_form);
                this.Controls.Add(plc_form);
                plc_form.Visible = false;
            }

            this.plcName = new ComboBox();
            this.Controls.Add(plcName);


            this.plcName.Location = new System.Drawing.Point(10, 220);


            this.plcName.Name = "plcName";
        //    this.plcName.Size = new System.Drawing.Size(176, 20);
        //    this.plcName.TabIndex = 0;

            this.plcName.SelectedIndexChanged += this.plcName_ValueChanged;

            foreach (ExtCompolet plc in plc_conn)
            {
                this.plcName.Items.Add(plc.plc_name);
            }
            this.plcName.SelectedIndex = 0;
            plcName_ValueChanged(null, null);


        }

		private void plcName_ValueChanged(object sender, System.EventArgs e)
		{
            int i = 0;
            foreach(PLCForm plc_form in plc_form_list)
            {
                if(i == (int)this.plcName.SelectedIndex)
    			    plc_form.Visible = true;
                else
                    plc_form.Visible = false;
                i++;
            }
		}
    }
}

