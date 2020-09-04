
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using OMRON.Compolet.CIPCompolet64;

namespace common_compolet_pure
{
	[Serializable] 
    public class plcvariable
	{
		private ExtCompolet extCompolet;
		public string name { get; set; } //имя переменной как оно обозначено в плк

        private object plc_val; // прочитанное значение, которое приведено к нрмальному виду. 
                                //Потом, думаю, чисто этот класс не будет использоваться, будем от него наследоаться
                                //А там уже под кажды тип переменных будет свое свойство
		public plcvariable(ExtCompolet comp, string name)
		{
			this.extCompolet = comp;
			this.name = name;
		}
		[JsonIgnore]
        public object Plc_value //собственно свойство. Можно записывать в плк - это долгая процедура, можно читать ранее прочитанное.
        {
            get 
            {
                return plc_val;
            }

            set
            {
                //тут нужно описать запись в плк. Должен быть какой-то объект "ПЛК", у него должны быть методы для записи переменных
				plc_val = extCompolet.WriteVar(name, value);
            }
        }

        //метод для синхронизации значения в объекте со значением в плк
        //когда метод вызывается, происходит отправка запроса в плк и по результату обновляется значение 
        //приватной переменной plc_value
        public void readFromPlc(){
			plc_val = extCompolet.readFromPlc(name);
        }
        
    }
}

