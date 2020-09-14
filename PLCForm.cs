using System;
using System.Windows.Forms;

namespace common_compolet_pure
{
    // Гуишный контейнер для управления подключением к плк и его переменными
    // наследуется от групбокса - объединяющего гуишного контейнера для других гуишных компонентов
    public class PLCForm : GroupBox
    {
        private NumericUpDown numPortNo;
        private Label labelPortNo;
        private Label labelIPAddress;
        private TextBox txtIPAddress;
        private CheckBox chkActive;

        private Button btnWriteVariable;
        private Button btnReadVariable;
        private Button btnReadAllVariables;
        private TextBox txtValue;
        private Label labelValue;
        private TextBox txtVariableName;
        private Label labelName;
        public ExtCompolet plc_conn;
        private ListBox var_list;
        private ListBox value_list;

        // конструктор, принимающий в качестве параметра объект связи с плк
        public PLCForm(ExtCompolet plc_conn) : base()
        {
            this.plc_conn = plc_conn;

            // тут расставляем на объекте гуишки управления, наполняем списки переменных по входному объекту связи с плк
            #region form content

            this.numPortNo = new NumericUpDown();
            this.labelPortNo = new Label();
            this.labelIPAddress = new Label();
            this.txtIPAddress = new TextBox();
            this.chkActive = new CheckBox();
            
            this.btnWriteVariable = new Button();
            this.btnReadVariable = new Button();
            this.btnReadAllVariables = new Button();
            this.txtValue = new TextBox();
            this.labelValue = new Label();
            this.txtVariableName = new TextBox();
            this.labelName = new Label();

            this.var_list = new ListBox();
            this.value_list = new ListBox();


            this.Controls.Add(this.btnWriteVariable);
            this.Controls.Add(this.btnReadVariable);
            this.Controls.Add(this.btnReadAllVariables);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.txtVariableName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.var_list);
            this.Controls.Add(this.value_list);



            this.Controls.Add(this.txtIPAddress);
            this.Controls.Add(this.labelIPAddress);
            this.Controls.Add(this.labelPortNo);
            this.Controls.Add(this.numPortNo);
            this.Location = new System.Drawing.Point(8, 0);
            this.Name = "groupBoxConnection";
            this.Size = new System.Drawing.Size(652, 208);
            this.TabIndex = 0;
            this.TabStop = false;
            this.Text = "Connection";

            
            this.Controls.Add(this.chkActive);
            


            this.labelIPAddress.Location = new System.Drawing.Point(32, 121);
            this.labelIPAddress.Name = "labelIPAddress";
            this.labelIPAddress.Size = new System.Drawing.Size(64, 18);
            this.labelIPAddress.TabIndex = 7;
            this.labelIPAddress.Text = "IP Address:";

            this.txtIPAddress.Location = new System.Drawing.Point(112, 121);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(176, 20);
            this.txtIPAddress.TabIndex = 8;
            this.txtIPAddress.Text = this.plc_conn.PeerAddress;//"172.16.201.14";//"192.168.250.1";
            this.txtIPAddress.TextChanged += new System.EventHandler(this.txtIPAddress_TextChanged);
            // 



            this.labelPortNo.Location = new System.Drawing.Point(32, 104);
            this.labelPortNo.Name = "labelPortNo";
            this.labelPortNo.Size = new System.Drawing.Size(64, 17);
            this.labelPortNo.TabIndex = 5;
            this.labelPortNo.Text = "Port No";
            // 
            // numPortNo
            // 
            this.numPortNo.Location = new System.Drawing.Point(112, 100);
            this.numPortNo.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numPortNo.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numPortNo.Name = "numPortNo";
            this.numPortNo.Size = new System.Drawing.Size(176, 20);
            this.numPortNo.TabIndex = 0;
            this.numPortNo.Value = new decimal(new int[] {
            this.plc_conn.LocalPort,
            0,
            0,
            0});
            this.numPortNo.ValueChanged += new System.EventHandler(this.numPortNo_ValueChanged);
            this.numPortNo.DragLeave += new System.EventHandler(this.numPortNo_ValueChanged);
            this.numPortNo.Leave += new System.EventHandler(this.numPortNo_ValueChanged);


            // 
            // chkActive
            // 
            this.chkActive.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkActive.Location = new System.Drawing.Point(16, 180);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(71, 17);
            this.chkActive.TabIndex = 1;
            this.chkActive.Text = "Active";
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);



            // 
            // labelName
            // 
            this.labelName.Location = new System.Drawing.Point(8, 17);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(40, 18);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name";
            // 
            // txtVariableName
            // 
            this.txtVariableName.Location = new System.Drawing.Point(64, 17);
            this.txtVariableName.Name = "txtVariableName";
            this.txtVariableName.Size = new System.Drawing.Size(93, 20);
            this.txtVariableName.TabIndex = 1;
            this.txtVariableName.Text = "";
            // 
            // labelValue
            // 
            this.labelValue.Location = new System.Drawing.Point(8, 43);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(40, 18);
            this.labelValue.TabIndex = 2;
            this.labelValue.Text = "Value";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(64, 43);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(93, 20);
            this.txtValue.TabIndex = 3;
            this.txtValue.Text = "0";
            // 
            // btnReadVariable
            // 
            this.btnReadVariable.Location = new System.Drawing.Point(173, 46);
            this.btnReadVariable.Name = "btnReadVariable";
            this.btnReadVariable.Size = new System.Drawing.Size(64, 26);
            this.btnReadVariable.TabIndex = 5;
            this.btnReadVariable.Text = "Read";
            this.btnReadVariable.Click += new System.EventHandler(this.btnReadVariable_Click);

            this.btnReadAllVariables.Location = new System.Drawing.Point(373, 126);
            this.btnReadAllVariables.Name = "btnReadAllVariables";
            this.btnReadAllVariables.Size = new System.Drawing.Size(64, 26);
            this.btnReadAllVariables.TabIndex = 5;
            this.btnReadAllVariables.Text = "Read All";
            this.btnReadAllVariables.Click += new System.EventHandler(this.btnReadAllVariables_Click);

            // 
            // btnWriteVariable
            // 
            this.btnWriteVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWriteVariable.Location = new System.Drawing.Point(239, 46);
            this.btnWriteVariable.Name = "btnWriteVariable";
            this.btnWriteVariable.Size = new System.Drawing.Size(64, 26);
            this.btnWriteVariable.TabIndex = 6;
            this.btnWriteVariable.Text = "Write";
            this.btnWriteVariable.Click += new System.EventHandler(this.btnWriteVariable_Click);

            this.var_list.SelectedIndexChanged += var_list_SelectedIndexChanged;

            this.var_list.Location = new System.Drawing.Point(373, 26);
            bool var_list_not_empty = false;
            foreach(plcvariable a in this.plc_conn.plc_var_list)
            {
                this.var_list.Items.Add(a.name);
                var_list_not_empty = true;
            }
            if(var_list_not_empty)
                this.var_list.SetSelected(0, true);

            this.value_list.Location = new System.Drawing.Point(503, 26);
            


            #endregion

        }

        // обработчик события при выборе переменной из списка
        // Выделенное имя просто вписываем в дополнительное поле
        void var_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCountry = var_list.SelectedItem.ToString();
            this.txtVariableName.Text = selectedCountry;
        }

        // обработчик события при нажатии кнопки "прочитать переменную"
		private void btnReadVariable_Click(object sender, System.EventArgs e)
		{
            // находим нужную переменную по номеру в списке и вызывем у нее метод "прочитать"
            this.plc_conn.plc_var_list[var_list.SelectedIndex].readFromPlc();
            // полученное значение мы преобразуем в строку и помещаем в поле на форме
            object obj = this.plc_conn.plc_var_list[var_list.SelectedIndex].Plc_value;
            if (obj == null)
            {
                throw new NotSupportedException();
            }
            else
            {
                string str = ExtCompolet.GetValueOfVariables(obj);
                this.txtValue.Text = str;
            }
		}
        // Обработчик события кнопки "прочитать все"
        // происходит чтение всех переменных по очереди и заполнение значениями лист-бокса со значениями
		private void btnReadAllVariables_Click(object sender, System.EventArgs e)
		{
            this.txtValue.Text = "";
            value_list.Items.Clear();
            if(plc_conn.Active == true)
            {
                foreach(plcvariable vr in plc_conn.plc_var_list)
                {
                    vr.readFromPlc();
                    object obj = vr.Plc_value;
                    if (obj == null)
                    {
                        throw new NotSupportedException();
                    }
                    else
                    {
                        string str = ExtCompolet.GetValueOfVariables(obj);
                    // this.txtValue.Text += "; " + str;
                        value_list.Items.Add(str);
                    }
                }
            }
        

		}


        // Обработчик нажатия на кнопку для записи одной переменной
		private void btnWriteVariable_Click(object sender, System.EventArgs e)
		{
            this.plc_conn.plc_var_list[var_list.SelectedIndex].Plc_value = this.txtValue.Text;
		}

        // при изменении текста в поле с ip адресом - новый адрес фиксируется в объект связи с плк
        private void txtIPAddress_TextChanged(object sender, System.EventArgs e)
		{
			this.plc_conn.PeerAddress = this.txtIPAddress.Text;
		}

        // при изменении текста в поле с номером порта - новый номер фиксируется в объект связи с плк
		private void numPortNo_ValueChanged(object sender, System.EventArgs e)
		{
			this.plc_conn.LocalPort = (int)this.numPortNo.Value;
		}

        // Здесь происходит подключение и отключение от плк
		private void chkActive_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				this.plc_conn.Active = this.chkActive.Checked; // подключаемся либо отключаемся от плк
				if (this.chkActive.Checked)
				{

					if (!this.plc_conn.IsConnected)// если не получилось подключиться, пишем об этом в сообщении, выключаем подключение и снимаем галку
					{

						MessageBox.Show("Connection failed !" + System.Environment.NewLine + "Please check PeerAddress.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

						this.plc_conn.Active = false;
						this.chkActive.Checked = false;

					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.plc_conn.Active = false;
				this.chkActive.Checked = false;
			}
		}



    }
}
