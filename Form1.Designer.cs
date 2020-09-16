using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;

namespace common_compolet_pure
{
	partial class Form1
	{

		private System.ComponentModel.IContainer components = null; 
		private List<ExtCompolet> plc_conn; // Список объектов для подключения к плк через CIP
		private Dictionary<string, PLCForm> plc_form_dict; // Список гуишных разделов для управления каждым плк.
		private TreeView plc_tree; 
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
			plc_form_dict = new Dictionary<string, PLCForm>();
			foreach(ExtCompolet plc in plc_conn)
			{
				PLCForm plc_form = new PLCForm(plc); // создали сам фрейм
				plc_form_dict.Add(plc_form.plc_conn.plc_name, plc_form); // добавили его в список фреймов, чтоб потом была возможность их все перебрать
				this.Controls.Add(plc_form); // добавили этот фрейм на форму (окошко)
				plc_form.Visible = false; // сделали форму невидимой, чтоб они не перекрывались.
			}



			plc_tree = new TreeView();
			this.Controls.Add(plc_tree);
			plc_tree.Size = new System.Drawing.Size(150, 200);
			plc_tree.Location = new System.Drawing.Point(10, 5);

			// В качестве обработчика задана лямбда функция, которая в случае, если элемент с нужным именем в коллекции есть длает все невидимыми, а его делает видимым. 
			plc_tree.AfterSelect += (a, b) =>  
			{ 
				if( plc_form_dict.ContainsKey(plc_tree.SelectedNode.Text)) 
				{
					foreach(KeyValuePair<string, PLCForm> plc_form in plc_form_dict)
					{
						plc_form.Value.Visible = false;
					}
					plc_form_dict[plc_tree.SelectedNode.Text].Visible = true; 
				}
			};
			// заполняем дерево узлами с именами плк и их ип адресами в качестве поднодов
			foreach (ExtCompolet plc in plc_conn)
			{
				TreeNode node = new TreeNode();
				node.Text = plc.plc_name;
				node.Nodes.Add(new TreeNode());
				node.Nodes[0].Text = plc.PeerAddress;
				plc_tree.Nodes.Add(node);
			}
		}

	}
}

