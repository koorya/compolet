
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using OMRON.Compolet.CIPCompolet64;

namespace common_compolet_pure
{
    // Это класс, который хранит имя переменной плк, 
    // ссылку на объект, который отвечает за подключение к плк
    // пользуясь методами этого объекта производит чтение и запись переменной.
	[Serializable] 
    public class plcvariable
	{
		public IPLCConnect plc_conection; // ссылка на объект для связи с плк
		public string name { get; set; } //имя переменной как оно обозначено в плк

        private object plc_val; // прочитанное из плк значение  
        public plcvariable() // конструктор
        {

        }
        public plcvariable(IPLCConnect plc_conn, string name) // конструктор для создания переменной внутри кода. Он не нужен, я считаю.
        {
            this.name = name;
            this.plc_conection = plc_conn;
        }

		[JsonIgnore] // значение не сохраняется никуда, может быть прочитано только из плк
        public object Plc_value //Свойство со значением переменной. Можно записывать в плк - это долгая процедура, можно читать ранее прочитанное.
        {
            get 
            {
                return plc_val;
            }

            set
            {
				plc_val = plc_conection.WriteVar(name, value);
            }
        }

        //метод для синхронизации значения в объекте со значением в плк
        //когда метод вызывается, происходит отправка запроса в плк и по результату обновляется значение 
        //приватной переменной plc_value
        public void readFromPlc(){
			plc_val = plc_conection.readFromPlc(name);
        }
        
    }
}

