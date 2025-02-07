using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApiWpfClassLibrary.Dto
{
    public class InputData
    {
        private string _name;
        public string CityName { get => _name; set => _name = value.ToLower(); }
    }
}
