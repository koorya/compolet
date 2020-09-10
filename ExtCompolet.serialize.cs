using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using OMRON.Compolet.CIPCompolet64;

using System.IO;
using System.Threading.Tasks;

namespace common_compolet_pure
{
    [Serializable]
    public class ExtComp_serial
    {
        public string plc_name { get; set; }
        public int LocalPort { get; set; }
        public string PeerAddress { get; set; }

        public List<plcvariable> var_name_list { get; set; }

    }
    partial class ExtCompolet 
    {
        public ExtComp_serial convert_to_serial()
        {
            ExtComp_serial ser = new ExtComp_serial();
            ser.plc_name = this.plc_name;
            ser.PeerAddress = this.PeerAddress;
            ser.LocalPort = this.LocalPort;

            ser.var_name_list = new List<plcvariable>();
            foreach (plcvariable v in this.plc_var_list)
            {
                ser.var_name_list.Add(v);
            }
            
            return ser;
        }

        public void deserialize(FileStream fs)
        {

            ValueTask<ExtComp_serial> _deser = JsonSerializer.DeserializeAsync<ExtComp_serial>(fs);

            while(_deser.IsCompleted);

            ExtComp_serial deser = _deser.Result;
            Console.WriteLine(deser.plc_name);
            Console.WriteLine(deser.PeerAddress);
            Console.WriteLine(deser.LocalPort);

            this.Active = false;
            this.ConnectionType = OMRON.Compolet.CIPCompolet64.ConnectionType.UCMM;
            this.LocalPort = deser.LocalPort;
            this.PeerAddress = deser.PeerAddress;//"192.168.250.1";
            this.ReceiveTimeLimit = ((long)(750));
            this.RoutePath = "2%172.16.201.14\\1%0";//"2%192.168.250.1\\1%0";
            this.UseRoutePath = false;
            this.plc_name = deser.plc_name;

            foreach(plcvariable v in deser.var_name_list)
            {
                v.plc_conection = this;
                Console.WriteLine(v.name);
            }
            this.plc_var_list = deser.var_name_list;

        }
        public void deserialize(ExtComp_serial deser)
        {

            Console.WriteLine(deser.plc_name);
            Console.WriteLine(deser.PeerAddress);
            Console.WriteLine(deser.LocalPort);

            this.Active = false;
            this.ConnectionType = OMRON.Compolet.CIPCompolet64.ConnectionType.UCMM;
            this.LocalPort = deser.LocalPort;
            this.PeerAddress = deser.PeerAddress;//"192.168.250.1";
            this.ReceiveTimeLimit = ((long)(750));
            this.RoutePath = "2%172.16.201.14\\1%0";//"2%192.168.250.1\\1%0";
            this.UseRoutePath = false;
            this.plc_name = deser.plc_name;

            foreach(plcvariable v in deser.var_name_list)
            {
                v.plc_conection = this;
                Console.WriteLine(v.name);
            }
            this.plc_var_list = deser.var_name_list;

        }
    }
}