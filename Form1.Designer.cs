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

        private ComboBox plcName; //выпадающий список для выбора плк
        private List<ExtCompolet> plc_conn; // Список объектов для подключения к плк через CIP
        private List<PLCForm> plc_form_list; // Список гуишных разделов для управления каждым плк.
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
            
            plc_conn = new List<ExtCompolet>(); // список объектов для связи с плк через CIP

            FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate); // поток для чтения конфгов плк из файла
            
            ValueTask<List<ExtComp_serial>> _deser = JsonSerializer.DeserializeAsync<List<ExtComp_serial>>(fs); // десериализация промежуточных объектов, 
            // по которым будут создаваться обычные объекты для общения с плк

            while(_deser.IsCompleted);//если прочиталось с успехом, можно продолжать. Потенциально здесь ошибка и проблема.

            // по каждому из прочитанных промежуточных объектов создаем нормальный объект для связи с плк
            foreach (ExtComp_serial deser in _deser.Result)
            {
                ExtCompolet plc = new ExtCompolet(this.components, deser);
                // добавляем этот объект в список объектов для связи с плк
                plc_conn.Add(plc);
            }

            #region serialization to file
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
            #endregion

            // список фреймов с полями для управления конкретным плк
            plc_form_list = new List<PLCForm>();
            foreach(ExtCompolet plc in plc_conn)
            {
                PLCForm plc_form = new PLCForm(plc); // создали сам фрейм
                plc_form_list.Add(plc_form); // добавили его в список фреймов, чтоб потом была возможность их все перебрать
                this.Controls.Add(plc_form); // добавили этот фрейм на форму (окошко)
                plc_form.Visible = false; // сделали форму невидимой, чтоб они не перекрывались.
            }

            this.plcName = new ComboBox(); // поле для выбора активного фрейма в окне
            this.Controls.Add(plcName); // добавляем это поле на форму
            this.plcName.Location = new System.Drawing.Point(10, 220);
            this.plcName.Name = "plcName";
            this.plcName.SelectedIndexChanged += this.plcName_ValueChanged; // устанавливаем обработчик события изменения выбранного индекса

            // заполняем этот выпадающий список именами плк, проходя по списку объектов для связи с плк
            foreach (ExtCompolet plc in plc_conn)
            {
                this.plcName.Items.Add(plc.plc_name);
            }
            this.plcName.SelectedIndex = 0; // выбираем первый пункт в списке, чтобы после у нас отобразился один фрейм
            plcName_ValueChanged(null, null); // имитируем событие смены индекса, чтобы у нас отобразился первый фрейм


        }

        // обработчик события при выборе текущего плк
		private void plcName_ValueChanged(object sender, System.EventArgs e)
		{
            // проходим по списку фреймов и гасим все, у которых номер 
            // не совпадает с выбранным пунктом. А выббранный наоборот делаем видимым
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

