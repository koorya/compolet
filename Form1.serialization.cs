using System;
using System.Windows.Forms;
using System.Threading;
using OMRON.Compolet.CIPCompolet64;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace common_compolet_pure
{
    partial class Form1
    {
        private void serialize_var_list(List<plcvariable> vl)
        {
            foreach(plcvariable a in  vl)
                Console.WriteLine(JsonSerializer.Serialize<plcvariable>(a));

        }               
    }
}