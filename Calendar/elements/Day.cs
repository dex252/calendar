using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.elements
{
    class Day//хромосома
    {
        public string name; //имя дня
        public bool[] matrix = { false, false, false, false, false, false };//расписание занятий в этот день, false - окно/нет занятий, true - занятие стоит
        public Lesson[] matrixL = new Lesson[6];//статичный массив, в котором хранится подробная информация о парах в этот день (хромосомы)
        public double mark = 0.0;//текущая оценка дня (оценка особи текущей популяции)
       
        public Day()
        {
            name = "";
            for (int i = 0; i < 6; i++)
            {
                matrixL[i] = new Lesson();
            }
           
        }

        // Copy constructor.
        public Day(Day previousPerson)
        {
            name = previousPerson.name;
            mark = previousPerson.mark;
            matrix = previousPerson.matrix;
            //mainPerson[i] = new Day(main.days[i]);

            for (int i = 0; i < 6; i++)
            {
                matrixL[i] = new Lesson(previousPerson.matrixL[i]);
            }
        }
    }


}
