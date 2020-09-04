using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OMRON.Compolet.CIPCompolet64;

namespace common_compolet_pure
{
    public partial class Form1 : Form
    {
        private ExtCompolet commonCompolet1;
        private List<plcvariable> my_plc_var;
        private ListBox var_list;
        public Form1()
        {
            InitializeComponent();
        }

    }
}
