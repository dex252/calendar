using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.elements
{
    class MinDay
    {
        public string name; //имя дня
        public Lesson[] matrixL = new Lesson[6];//статичный массив, в котором хранится подробная информация о парах в этот день (хромосомы)
        public double mark;//текущая оценка дня (оценка особи текущей популяции)

        public MinDay(Day day)
        {
            name = day.name;
            matrixL = day.matrixL;
            mark = day.mark;
        }
    }
}
