using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.elements
{
    class Generations
    {
        string name; //имя поколения
        List<Day> days; //популяция

        public Generations(string name, List<Day> days)
        {
            this.days = new List<Day>();
            this.days = days;
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public List<Day> GetGeneration()
        {
            return days;
        }
    }
}
