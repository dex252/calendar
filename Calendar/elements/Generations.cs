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
        public Day[] days; //популяция
        public double mark = 0.0;//итоговая оценка популяции

        public Generations(string name)
        {
            days = new Day[6];
            for (int i = 0; i < 6; i++)
            {
                days[i] = new Day();
            }
            this.name = name;
        }

        public Generations(Generations old)
        {
            name = old.name;
            mark = old.mark;
            days = new Day[6];
            for (int i = 0; i < 6; i++)
            {
                days[i] = new Day(old.days[i]);
            }
        }

        public void Input(Day[] days)
        {
            for (int i = 0; i < 6; i++)
            {
                this.days[i] = new Day(days[i]);
            }
        }

        public string GetName()
        {
            return name;
        }

        public Day[] GetGeneration()
        {
            return days;
        }
    }
}
