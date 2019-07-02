using Calendar.elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    internal class GeneratorOld
    {
        private Day[] main; //основная особь
        private Day[] day;//общий контейнер для особей (при скрещивании)
        private string[] names;//имена особей
        private double[] marks;//оценки особей
        private double MARK;//сохраняемая оценка главной особи
        private Random rand = new Random();
        private int maxLessons;
        private List<Day[]> population = new List<Day[]>();//популяция из нескольких особей
        private List<UnicLesson> unicLessons;
        private List<Generations> generations;
       

        public GeneratorOld(Day[] days, int maxLessons, List<UnicLesson> unicLessons, List<Generations> generations, double mainMark)
        {
            main = new Day[6];
            Array.Copy(days, main, 6);

            MARK = mainMark;

            this.generations = generations;
            this.maxLessons = maxLessons;
            this.unicLessons = unicLessons;
        }

        //начало формирования новых поколений
        public void GetPopulations(int numPopulations)//число популяций
        {
            //вырастим numPopulations поколений
            for (int j = 0; j < numPopulations; j++)
            {
                population.Clear();
                population = new List<Day[]>();
                names = new string[15];
                marks = new double[15];

                //возьмем основную особь и составим 15 вариантов её скрещивания по заранее составленной схеме
                for (int i = 0; i < 15; i++)
                {
                    day = new Day[6];

                    /*здесь ошибка*/
                    GetNewGeneration(main, i);//вызов скрещивания особи для составления нового поколения
                    //Mutations();
                    Rating ratio = new Rating(day, maxLessons, unicLessons);

                    names[i] = "популяция #" + (j + 1) + "  особь под номером #" + ((j * 15) + (i + 1));
                    marks[i] = ratio.TotalMark();//считаем общую оценку особи

                    population.Add(day);

                }

                //после выведения поколения стоит применить мутации к наиболее приспособленным особям, пока опустим
                //ищем наиболее приспособленную особь -- в дальнейшем искать стоит 3 лучшие особи, если среди них нет лучше, чем родитель, то убиваем поколение
                double generalMark = MARK;//считаем общую оценку родительской особи
                int index = -1;//индекс родительской особи

                for (int i = 0; i < 15; i++)
                {
                    if (generalMark > marks[i])
                    {
                        generalMark = marks[i];
                        MARK = generalMark;
                        index = i;
                    }
                }

                if (index != -1)//если родительская особь лучше своих поколений, то все поколение бракуется | иначе назначается новая особь, а результат добавляется в список поколений
                {

                    Day[] main = new Day[6];
                    Array.Copy(population[index], main, 6);
                    this.main = main;
                    population = new List<Day[]>();
                    Generations generic = new Generations(names[index]);
                    generic.mark = generalMark;
                    generic.Input(main);
                    generations.Add(generic);//добавляем новую особь в список

                }
            }


        }

        private void GetNewGeneration(Day[] day1, int num)//в качестве аргумента берется рассматриваемая особь и индекс скрещивания
        {
            day = day1;
            if (num == 0)
            {
                Swap(0, 1);//свап генами между указанными хромосомами
                Swap(2, 3);
                Swap(4, 5);
            }
            if (num == 1)
            {
                Swap(0, 1);
                Swap(2, 4);
                Swap(3, 5);
            }
            if (num == 2)
            {
                Swap(0, 1);
                Swap(2, 5);
                Swap(3, 4);
            }
            if (num == 3)
            {
                Swap(0, 2);
                Swap(1, 3);
                Swap(4, 5);
            }
            if (num == 4)
            {
                Swap(0, 2);
                Swap(1, 4);
                Swap(3, 5);
            }
            if (num == 5)
            {
                Swap(0, 2);
                Swap(1, 5);
                Swap(3, 4);
            }
            if (num == 6)
            {
                Swap(0, 3);
                Swap(1, 2);
                Swap(4, 5);
            }
            if (num == 7)
            {
                Swap(0, 3);
                Swap(1, 4);
                Swap(2, 5);
            }
            if (num == 8)
            {
                Swap(0, 3);
                Swap(1, 5);
                Swap(2, 4);
            }
            if (num == 9)
            {
                Swap(0, 4);
                Swap(1, 2);
                Swap(3, 5);
            }
            if (num == 10)
            {
                Swap(0, 4);
                Swap(1, 3);
                Swap(2, 5);
            }
            if (num == 11)
            {
                Swap(0, 4);
                Swap(1, 5);
                Swap(2, 3);
            }
            if (num == 12)
            {
                Swap(0, 5);
                Swap(1, 2);
                Swap(3, 4);
            }
            if (num == 13)
            {
                Swap(0, 5);
                Swap(1, 3);
                Swap(2, 4);
            }
            if (num == 14)
            {
                Swap(0, 5);
                Swap(1, 4);
                Swap(2, 3);
            }

        }

        private void Swap(int leftIndex, int rightIndex)//меняем местами гены у указанных дней
        {
            Day left = day[leftIndex];
            Day right = day[rightIndex];

            int cross = rand.Next(1, 10);//число обменов генами между двумя хромосомами

            for (int i = 0; i < cross; i++)
            {
                int gen1 = rand.Next(6);//выбор первого гена для обмена
                int gen2 = rand.Next(6);//выбор второго гена для обмена

                Lesson remember = left.matrixL[gen1];//запоминаем ген из первой хромосомы
                bool remember_n = left.matrix[gen1];//запоминаем инфо о первом гене первой хромосомы

                left.matrixL[gen1] = right.matrixL[gen2];//обмен геном и информацией
                left.matrix[gen1] = right.matrix[gen2];

                right.matrixL[gen2] = remember;//записываем во вторую хромосому сохраненную информацию
                right.matrix[gen2] = remember_n;
            }

            //после обмена генами перезаписываем информацию в исходную особь
            /*
             * ошибка по передаче ссылки а не значения
             */

            day[leftIndex] = left;
            day[rightIndex] = right;
        }

        //public void Mutations()
        //{
        //    for (int j = 0; j < 6; j++)
        //    {
        //        Array.Sort(day[j].matrix);
        //        Array.Reverse(day[j].matrix);


        //        string[] lessons = { "", "", "", "", "", "" };
        //        for (int i = 0; i < 6; i++)
        //        {
        //            if (day[j].matrixL[i] != null) lessons[i] = day[j].matrixL[i].lesson;
        //        }
        //        List<KeyValuePair<int, string>> odinary = new List<KeyValuePair<int, string>>();//объявим словарь, в котором ключи могут повторяться
        //                                                                                        //занесем все уникальные названия уроков и число их повторений в словарь
        //        foreach (string val in lessons.Distinct())
        //        {
        //            odinary.Add(new KeyValuePair<int, string>(lessons.Where(x => x == val).Count(), val));
        //        }
        //        //пройдемся по полученным результатам и выясним какие предметы повторяются и стоят ли они рядом?
        //        Stack<string> sub = new Stack<string>();
        //        foreach (KeyValuePair<int, string> keyValue in odinary)
        //        {
        //            if (keyValue.Value != "" && keyValue.Key > 0)//если поле не пустое и число повторений более нуля:
        //            {
        //                for (int i = 0; i < keyValue.Key; i++)
        //                {
        //                    sub.Push(keyValue.Value);//заполняем стек предметами
        //                }
                        
        //            }
        //        }
        //        int k = 0;
        //       while (sub.Count != 0)
        //        {
        //            string subject = "";
        //            subject = sub.Pop();
        //            if (day[j].matrixL == null)
        //            {
        //                day[j].matrixL = new Lesson[6];
        //                day[j].matrixL[k].lesson = subject;
        //            }

        //            for (int i = 0; i < unicLessons.Count; i++)
        //            {
        //                if (subject == unicLessons[i].lesson)
        //                {
        //                    if (day[j].matrixL == null)
        //                    {
        //                        day[j].matrixL = new Lesson[6];
        //                        day[j].matrixL[k].teacher = unicLessons[i].teacher;
        //                    }
                               
        //                }
        //            }
        //            k++;
        //        }


        //    }

           
        //}
    }
}
