﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.elements
{
    class Day//хромосома
    {
        private int lessons;//количество занятий в этот день
        private int window;//количество окон в этот день
        private int maxLessonsTeacher;//максимальное число пар, проводимых одним преподавателем
        private int merge;//число одинаковых пар, идущих подряд
        private int same;//число одинаковых пар
        private int others;//количество разных пар в этот день
        private int areas;//количество разных кабинетов в этот день
        private bool sameAreas;//если существуют пары merge, то проводятся ли они в одном и том же кабинете? false - нет, true - да

        public string name; //имя дня
        public bool[] matrix = { false, false, false, false, false, false };//расписание занятий в этот день, false - окно/нет занятий, true - занятие стоит
        public Lesson[] matrixL = new Lesson[6];//статичный массив, в котором хранится подробная информация о парах в этот день (хромосомы)
        public double mark = 0.0;//текущая оценка дня (оценка особи текущей популяции)
       
    }


}
