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

        private GroupBox groupBoxConnection;
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
        private System.ComponentModel.IContainer components = null;



        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
            
            plc_conn = new List<ExtCompolet>();

            FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate);
//            plc_conn.Add(new ExtCompolet(this.components, "{\"plc_name\":\"def name\",\"LocalPort\":3,\"PeerAddress\":\"172.16.201.14\",\"var_name_list\":[{\"name\":\"int_var\"},{\"name\":\"bool_var\"},{\"name\":\"word_var\"}]}"));
            
            ValueTask<List<ExtComp_serial>> _deser = JsonSerializer.DeserializeAsync<List<ExtComp_serial>>(fs);

            while(_deser.IsCompleted);

            foreach (ExtComp_serial deser in _deser.Result)
            {
                ExtCompolet plc = new ExtCompolet(this.components, deser);
                plc_conn.Add(plc);
            }


            #region form content

            this.groupBoxConnection = new GroupBox();
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


            this.groupBoxConnection.Controls.Add(this.btnWriteVariable);
            this.groupBoxConnection.Controls.Add(this.btnReadVariable);
            this.groupBoxConnection.Controls.Add(this.btnReadAllVariables);
            this.groupBoxConnection.Controls.Add(this.txtValue);
            this.groupBoxConnection.Controls.Add(this.labelValue);
            this.groupBoxConnection.Controls.Add(this.txtVariableName);
            this.groupBoxConnection.Controls.Add(this.labelName);
            this.groupBoxConnection.Controls.Add(this.var_list);
            this.groupBoxConnection.Controls.Add(this.value_list);



            this.groupBoxConnection.Controls.Add(this.txtIPAddress);
            this.groupBoxConnection.Controls.Add(this.labelIPAddress);
            this.groupBoxConnection.Controls.Add(this.labelPortNo);
            this.groupBoxConnection.Controls.Add(this.numPortNo);
            this.groupBoxConnection.Location = new System.Drawing.Point(8, 0);
            this.groupBoxConnection.Name = "groupBoxConnection";
            this.groupBoxConnection.Size = new System.Drawing.Size(652, 208);
            this.groupBoxConnection.TabIndex = 0;
            this.groupBoxConnection.TabStop = false;
            this.groupBoxConnection.Text = "Connection";

            
            this.Controls.Add(this.chkActive);
            
            this.Controls.Add(this.groupBoxConnection);



            this.labelIPAddress.Location = new System.Drawing.Point(32, 121);
            this.labelIPAddress.Name = "labelIPAddress";
            this.labelIPAddress.Size = new System.Drawing.Size(64, 18);
            this.labelIPAddress.TabIndex = 7;
            this.labelIPAddress.Text = "IP Address:";

            this.txtIPAddress.Location = new System.Drawing.Point(112, 121);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(176, 20);
            this.txtIPAddress.TabIndex = 8;
            this.txtIPAddress.Text = "172.16.201.14";//"192.168.250.1";
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
            3,
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
            this.chkActive.Location = new System.Drawing.Point(16, 217);
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
            foreach(plcvariable a in this.plc_conn[0].plc_var_list)
            {
                this.var_list.Items.Add(a.name);
                var_list_not_empty = true;
            }
            if(var_list_not_empty)
                this.var_list.SetSelected(0, true);

            this.value_list.Location = new System.Drawing.Point(503, 26);
            
            //plc_conn[0].deserialize()
        //    plc_conn[0].serialize();
            //serialize_var_list(plc_conn[0].plc_var_list);



            #endregion

            

        }

        void var_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCountry = var_list.SelectedItem.ToString();
            this.txtVariableName.Text = selectedCountry;
        }
		private void btnReadVariable_Click(object sender, System.EventArgs e)
		{
            this.plc_conn[0].plc_var_list[var_list.SelectedIndex].readFromPlc();
            object obj = this.plc_conn[0].plc_var_list[var_list.SelectedIndex].Plc_value;
            if (obj == null)
            {
                throw new NotSupportedException();
            }
            else
            {
                string str = this.GetValueOfVariables(obj);
                this.txtValue.Text = str;
            }
		}
		private void btnReadAllVariables_Click(object sender, System.EventArgs e)
		{
            this.txtValue.Text = "";
            value_list.Items.Clear();
            foreach(ExtCompolet plc in plc_conn)
            {
                foreach(plcvariable vr in plc.plc_var_list)
                {
                    vr.readFromPlc();
                    object obj = vr.Plc_value;
                    if (obj == null)
                    {
                        throw new NotSupportedException();
                    }
                    else
                    {
                        string str = this.GetValueOfVariables(obj);
                       // this.txtValue.Text += "; " + str;
                        value_list.Items.Add(str);
                    }
                }
            }

		}



		private void btnWriteVariable_Click(object sender, System.EventArgs e)
		{
            this.plc_conn[0].plc_var_list[var_list.SelectedIndex].Plc_value = this.txtValue.Text;
		}


        private void txtIPAddress_TextChanged(object sender, System.EventArgs e)
		{
			this.plc_conn[0].PeerAddress = this.txtIPAddress.Text;
		}


		private void numPortNo_ValueChanged(object sender, System.EventArgs e)
		{
			this.plc_conn[0].LocalPort = (int)this.numPortNo.Value;
		}

		private void chkActive_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				this.plc_conn[0].Active = this.chkActive.Checked;
				if (this.chkActive.Checked)
				{

					if (!this.plc_conn[0].IsConnected)
					{

						MessageBox.Show("Connection failed !" + System.Environment.NewLine + "Please check PeerAddress.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

						this.plc_conn[0].Active = false;
						this.chkActive.Checked = false;

					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.plc_conn[0].Active = false;
				this.chkActive.Checked = false;
			}
		}



    }
}

