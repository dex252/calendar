﻿using Calendar.elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    internal class Generator
    {
        private Cash main;
        private Random rand = new Random();
        private List<UnicLesson> unicLessons;
        private List<Generations> generations;

        public Generator(Cash main)
        {
            this.main = main;
        }

        public void GetPopulations(int NumGenerations)
        {
            //---------объявления-------------
            Day[] mainPerson = new Day[6]; // основная особь, в начале необходимо скопировать в неё экземпляр из первого поколения
            double mainMark;// оценка основной особи, с которой происходит сравнение при отборе

            //---------присваивания-------------
            for (int i = 0; i < 6; i++)
            {
                mainPerson[i] = new Day(main.days[i]);
            }

            mainMark = main.firstmark;
            unicLessons = main.unicLessons;
            generations = main.generations;



            //вырастим numPopulations поколений
            for (int j = 0; j < NumGenerations; j++)
            {
                List<Day[]> population = new List<Day[]>();//популяция из нескольких особей
                string[] names = new string[15];
                double[] marks = new double[15];

                //возьмем основную особь и составим 15 вариантов её скрещивания по заранее составленной схеме
                for (int i = 0; i < 15; i++)
                {
                    Day[] person = new Day[6];

                    person = GetNewGeneration(mainPerson, i);

                    Rating ratio = new Rating(person, main.maxStack, unicLessons);

                    names[i] = "популяция #" + (j + 1) + "  особь под номером #" + ((j * 15) + (i + 1));
                    marks[i] = ratio.TotalMark();//считаем общую оценку особи

                    for (int k = 0; k < 6; k++)
                    {
                        person[k].mark = ratio.GetXromMarks(k);
                    }

                    population.Add(person);
                }

                int index = -1;

                for (int i = 0; i < 15; i++)
                {
                    if (mainMark > marks[i])
                    {
                        mainMark = marks[i];
                        index = i;
                    }
                }

                if (index != -1)//если родительская особь лучше своих поколений, то все поколение бракуется | иначе назначается новая особь, а результат добавляется в список поколений
                {

                    for (int i = 0; i < 6; i++)
                    {
                        mainPerson[i] = new Day(population[index][i]);
                    }

                    Generations generic = new Generations(names[index]);
                    generic.mark = mainMark; ;
                    generic.Input(mainPerson);

                    for (int i = 0; i < 6; i++)
                    {
                        generic.days[i].mark = population[index][i].mark;
                    }
                   
                    generations.Add(new Generations(generic));//добавляем новую особь в список

                }
            }

        }

        private Day[] GetNewGeneration(Day[] mainPerson, int num)
        {
            Day[] person = new Day[6];

            for (int i = 0; i < 6; i++)
            {
                person[i] = new Day(mainPerson[i]);
            }

            if (num == 0)
            {
                person = Swap(person, 0, 1);//свап генами между указанными хромосомами
                person = Swap(person, 2, 3);
                person = Swap(person, 4, 5);
            }
            if (num == 1)
            {
                person = Swap(person, 0, 1);
                person = Swap(person, 2, 4);
                person = Swap(person, 3, 5);
            }
            if (num == 2)
            {
                person = Swap(person, 0, 1);
                person = Swap(person, 2, 5);
                person = Swap(person, 3, 4);
            }
            if (num == 3)
            {
                person = Swap(person, 0, 2);
                person = Swap(person, 1, 3);
                person = Swap(person, 4, 5);
            }
            if (num == 4)
            {
                person = Swap(person, 0, 2);
                person = Swap(person, 1, 4);
                person = Swap(person, 3, 5);
            }
            if (num == 5)
            {
                person = Swap(person, 0, 2);
                person = Swap(person, 1, 5);
                person = Swap(person, 3, 4);
            }
            if (num == 6)
            {
                person = Swap(person, 0, 3);
                person = Swap(person, 1, 2);
                person = Swap(person, 4, 5);
            }
            if (num == 7)
            {
                person = Swap(person, 0, 3);
                person = Swap(person, 1, 4);
                person = Swap(person, 2, 5);
            }
            if (num == 8)
            {
                person = Swap(person, 0, 3);
                person = Swap(person, 1, 5);
                person = Swap(person, 2, 4);
            }
            if (num == 9)
            {
                person = Swap(person, 0, 4);
                person = Swap(person, 1, 2);
                person = Swap(person, 3, 5);
            }
            if (num == 10)
            {
                person = Swap(person, 0, 4);
                person = Swap(person, 1, 3);
                person = Swap(person, 2, 5);
            }
            if (num == 11)
            {
                person = Swap(person, 0, 4);
                person = Swap(person, 1, 5);
                person = Swap(person, 2, 3);
            }
            if (num == 12)
            {
                person = Swap(person, 0, 5);
                person = Swap(person, 1, 2);
                person = Swap(person, 3, 4);
            }
            if (num == 13)
            {
                person = Swap(person, 0, 5);
                person = Swap(person, 1, 3);
                person = Swap(person, 2, 4);
            }
            if (num == 14)
            {
                person = Swap(person, 0, 5);
                person = Swap(person, 1, 4);
                person = Swap(person, 2, 3);
            }

            return person;
        }

        private Day[] Swap(Day[] day, int leftIndex, int rightIndex)//меняем местами гены у указанных дней
        {
            Day[] person = new Day[6];
            Day left = new Day(day[leftIndex]);
            Day right = new Day(day[rightIndex]);

            int cross = rand.Next(1, 10);//число обменов генами между двумя хромосомами

            for (int i = 0; i < cross; i++)
            {
                int gen1 = rand.Next(6);//выбор первого гена для обмена
                int gen2 = rand.Next(6);//выбор второго гена для обмена

                Lesson remember = new Lesson(left.matrixL[gen1]);//запоминаем ген из первой хромосомы
                bool remember_n = left.matrix[gen1];//запоминаем инфо о первом гене первой хромосомы

                left.matrixL[gen1] = new Lesson(right.matrixL[gen2]);//обмен геном и информацией
                left.matrix[gen1] = right.matrix[gen2];

                right.matrixL[gen2] = new Lesson(remember);//записываем во вторую хромосому сохраненную информацию
                right.matrix[gen2] = remember_n;
            }

            //после обмена генами перезаписываем информацию в исходную особь

            day[leftIndex] = new Day(left);
            day[rightIndex] = new Day(right);

            for (int i = 0; i < 6; i++)
            {
                person[i] = new Day(day[i]);
            }
            return person;
        }

    }
}
