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


            this.Controls.Add(new PLCForm(plc_conn[0]));

        }

    }
}

