using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using OMRON.Compolet.CIPCompolet64;

namespace common_compolet_pure
{
    [Serializable]
    public class ExtComp_serial
    {
        public string plc_name { get; set; }
        public int LocalPort { get; set; }
        public string PeerAddress { get; set; }

        public List<plcvariable> var_name_list { get; set; }

/*
            this.plc_conn[0].Active = false;
            this.plc_conn[0].ConnectionType = OMRON.Compolet.CIPCompolet64.ConnectionType.UCMM;
            this.plc_conn[0].LocalPort = 3;
            this.plc_conn[0].PeerAddress = "172.16.201.14";//"192.168.250.1";
            this.plc_conn[0].ReceiveTimeLimit = ((long)(750));
            this.plc_conn[0].RoutePath = "2%172.16.201.14\\1%0";//"2%192.168.250.1\\1%0";
            this.plc_conn[0].UseRoutePath = false;
            */ 

    }
    partial class ExtCompolet 
    {
        public string serialize()
        {
            ExtComp_serial ser = new ExtComp_serial();
            ser.plc_name = "def name";
            ser.PeerAddress = this.PeerAddress;
            ser.LocalPort = this.LocalPort;

            ser.var_name_list = new List<plcvariable>();
            foreach (plcvariable v in this.plc_var_list)
            {
                ser.var_name_list.Add(v);
            }
            string serial_str = JsonSerializer.Serialize<ExtComp_serial>(ser);
            Console.WriteLine(serial_str);
            return serial_str;
        }

        public void deserialize(string serial_str)
        {
            ExtComp_serial deser = JsonSerializer.Deserialize<ExtComp_serial>(serial_str);
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
                v.extCompolet = this;
                Console.WriteLine(v.name);
            }
            this.plc_var_list = deser.var_name_list;

        }
    }
}