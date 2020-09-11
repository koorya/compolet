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
    // Список из именно этих объектов сериализуется в файл и десериализуется из него
    // Когда нужно достать из файла данные о плк и переменных, мы достаем этот объект
    // а затем уже из него получаем данные для создания объекта связи с плк
    public class ExtComp_serial
    {
        // название плк
        public string plc_name { get; set; }
        
        // номер порта в sysmac geteway, на котором находится плк 
        public int LocalPort { get; set; }
        
        // ip адрес манипулятора
        public string PeerAddress { get; set; }

        // список переменных, которые заданы для этого контроллера как сетевые
        public List<plcvariable> var_name_list { get; set; }

    }
    
    partial class ExtCompolet 
    {
        // метод, который упаковывает этот объект в тот, который будет сериализоваться в файл.
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

        // этот метод принимает промежуточный объект и по нему заполняет поля основного объекта
        public void deserialize(ExtComp_serial deser)
        {

            this.Active = false;
            this.ConnectionType = OMRON.Compolet.CIPCompolet64.ConnectionType.UCMM;
            this.LocalPort = deser.LocalPort;
            this.PeerAddress = deser.PeerAddress;//"192.168.250.1";
            this.ReceiveTimeLimit = ((long)(750));
            this.RoutePath = "2%172.16.201.14\\1%0";//"2%192.168.250.1\\1%0"; // в нашем контексте не используется
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