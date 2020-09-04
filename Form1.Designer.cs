using System;
using System.Windows.Forms;
using System.Threading;
using OMRON.Compolet.CIPCompolet64;
using System.Collections.Generic;

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
        private CheckBox chkPx;
        private CheckBox chkMQ;

        private Button btnWriteVariable;
        private Button btnReadVariable;
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

            commonCompolet1 = new ExtCompolet(this.components);
            
            this.commonCompolet1.Active = false;
            this.commonCompolet1.ConnectionType = OMRON.Compolet.CIPCompolet64.ConnectionType.UCMM;
            this.commonCompolet1.LocalPort = 3;
            this.commonCompolet1.PeerAddress = "172.16.201.14";//"192.168.250.1";
            this.commonCompolet1.ReceiveTimeLimit = ((long)(750));
            this.commonCompolet1.RoutePath = "2%172.16.201.14\\1%0";//"2%192.168.250.1\\1%0";
            this.commonCompolet1.UseRoutePath = false;
            // 

            this.my_plc_var = new List<plcvariable>();
            this.my_plc_var.Add(new plcvariable(this.commonCompolet1, "int_var"));
            this.my_plc_var.Add(new plcvariable(this.commonCompolet1, "bool_var"));
            this.my_plc_var.Add(new plcvariable(this.commonCompolet1, "word_var"));



            #region form content

            this.groupBoxConnection = new GroupBox();
            this.numPortNo = new NumericUpDown();
            this.labelPortNo = new Label();
            this.labelIPAddress = new Label();
            this.txtIPAddress = new TextBox();
            this.chkActive = new CheckBox();
            this.chkMQ = new CheckBox();
            
            this.btnWriteVariable = new Button();
            this.btnReadVariable = new Button();
            this.txtValue = new TextBox();
            this.labelValue = new Label();
            this.txtVariableName = new TextBox();
            this.labelName = new Label();

            this.var_list = new ListBox();


            this.groupBoxConnection.Controls.Add(this.btnWriteVariable);
            this.groupBoxConnection.Controls.Add(this.btnReadVariable);
            this.groupBoxConnection.Controls.Add(this.txtValue);
            this.groupBoxConnection.Controls.Add(this.labelValue);
            this.groupBoxConnection.Controls.Add(this.txtVariableName);
            this.groupBoxConnection.Controls.Add(this.labelName);
            this.groupBoxConnection.Controls.Add(this.var_list);



            this.groupBoxConnection.Controls.Add(this.txtIPAddress);
            this.groupBoxConnection.Controls.Add(this.labelIPAddress);
            this.groupBoxConnection.Controls.Add(this.labelPortNo);
            this.groupBoxConnection.Controls.Add(this.numPortNo);
            this.groupBoxConnection.Location = new System.Drawing.Point(8, 0);
            this.groupBoxConnection.Name = "groupBoxConnection";
            this.groupBoxConnection.Size = new System.Drawing.Size(512, 208);
            this.groupBoxConnection.TabIndex = 0;
            this.groupBoxConnection.TabStop = false;
            this.groupBoxConnection.Text = "Connection";

            
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.chkPx);
            this.Controls.Add(this.chkMQ);
            
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


            this.chkMQ.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkMQ.Location = new System.Drawing.Point(200, 350);
            this.chkMQ.Name = "chkMQ";
            this.chkMQ.Size = new System.Drawing.Size(71, 17);
            this.chkMQ.Text = "chkMQ";
           // this.chkMQ.CheckedChanged += null ;//+= new System.EventHandler(this.chkMQ_CheckedChanged);


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
            this.txtVariableName.Text = "glob_jumar.dist";
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
            foreach(plcvariable a in this.my_plc_var)
                this.var_list.Items.Add(a.name);


            #endregion

            

        }

        void var_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCountry = var_list.SelectedItem.ToString();
            this.txtVariableName.Text = selectedCountry;
        }
		private void btnReadVariable_Click(object sender, System.EventArgs e)
		{
            this.my_plc_var[var_list.SelectedIndex].readFromPlc();
            object obj = this.my_plc_var[var_list.SelectedIndex].Plc_value;
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



		private void btnWriteVariable_Click(object sender, System.EventArgs e)
		{
            this.my_plc_var[var_list.SelectedIndex].Plc_value = this.txtValue.Text;
		}


        private void txtIPAddress_TextChanged(object sender, System.EventArgs e)
		{
			this.commonCompolet1.PeerAddress = this.txtIPAddress.Text;
		}


		private void numPortNo_ValueChanged(object sender, System.EventArgs e)
		{
			this.commonCompolet1.LocalPort = (int)this.numPortNo.Value;
		}

		private void chkActive_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				this.commonCompolet1.Active = this.chkActive.Checked;
				if (this.chkActive.Checked)
				{

					if (!this.commonCompolet1.IsConnected)
					{

						MessageBox.Show("Connection failed !" + System.Environment.NewLine + "Please check PeerAddress.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

						this.commonCompolet1.Active = false;
						this.chkActive.Checked = false;

					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.commonCompolet1.Active = false;
				this.chkActive.Checked = false;
			}
		}



    }
}

