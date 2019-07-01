using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.elements
{
    class Generations
    {
        public string name; //имя поколения
        public MinDay[] days; //популяция
        public double mark = 0.0;//итговая оценка популяции

        public Generations(string name)
        {
            days = new MinDay[6];
            this.name = name;
        }

        public void Input(Day[] days)
        {

            for (int i = 0; i < 6; i++)
            {
                this.days[i] = new MinDay(days[i]);
            }

        }

        public string GetName()
        {
            return name;
        }

        public MinDay[] GetGeneration()
        {
            return days;
        }
    }
}
