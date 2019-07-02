using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.elements
{
    internal class MinDay
    {
        public string name; //имя дня
        public Lesson[] matrixL = new Lesson[6];//статичный массив, в котором хранится подробная информация о парах в этот день (хромосомы)
        public double mark;//текущая оценка дня (оценка особи текущей популяции)

        public MinDay()
        {
            name = "";
            mark = 0.0;
            for (int i = 0; i < 6; i++)
            {
                matrixL[i] = new Lesson();
            }
        }

        public MinDay(Day day)
        {
            name = day.name;
            for (int i = 0; i < 6; i++)
            {
                matrixL[i] = new Lesson(day.matrixL[i]);
            }
            mark = day.mark;
        }

        public MinDay(MinDay old)
        {
            name = old.name;
            mark = old.mark;
            for (int i = 0; i < 6; i++)
            {
                matrixL[i] = new Lesson(old.matrixL[i]);
            }
        }
    }
}
