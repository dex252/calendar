using Calendar.elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    internal class Rating
    {
        private Day[] days;
        private int maxLessons;

        public Rating(Day[] days, int maxLessons)
        {
            this.days = days;
            this.maxLessons = maxLessons;

            //оценка c понедельника по субботу, где this.mark - это оценка одного дня
            for (int i = 0; i < 6; i++)
            {
                days[i].mark = NextDay(days[i]);
            }

            days = InputMarks();
            //итоговая оценка

        }

        private double NextDay(Day day)
        {
            double totalMarkDay = 0.0;//общая оценка отдного дня по всем критериям

            //оценка общего числа пар
            totalMarkDay += MaxLessonsMark(day);//прибавить критерий количества пар

            return totalMarkDay;
        }

        private double MaxLessonsMark(Day day)
        {
            double mark = 0.0;

            //считаем число пар в этот день без учета названий предмета, преподавателей и аудиторий
            int count = 0;//количество пар
            for (int i = 0; i < 6; i++)
            {
                if (day.matrix[i])
                {
                    count++;
                }
            }
            if (maxLessons <= 18)
            {
                if (day.name == "Суббота")
                {
                    if (count == 0) mark += 0.0;// лучше освободить субботу при низкой нагрузке
                    if (count > 0 && count <= 3) mark += ((500.0 / (double)maxLessons) * 10.0);//зачем эти 1 или 2 или 3 пары из 18 ? в этом нет смысла
                    if (count == 4) mark += ((700.0 / (double)maxLessons) * 10.0);
                    if (count == 5) mark += ((800.0 / (double)maxLessons) * 10.0);
                    if (count == 6) mark += ((900.0 / (double)maxLessons) * 10.0);
                }
                else
                {
                    if (count <= 3) mark += 0.0;//при столь малом числе предметов на неделе - 3 оптимальный результат
                    if (count == 4) mark += ((100.0 / (double)maxLessons) * 10.0);//чем меньше общее число пар, тем хуже(выше) оценка при 18 оценка: 55,5
                    if (count == 5) mark += ((200.0 / (double)maxLessons) * 10.0);//111,1
                    if (count == 6) mark += ((500.0 / (double)maxLessons) * 10.0);//277,7
                }


            }

            if (maxLessons > 18 && maxLessons <= 23)
            {
                if (day.name == "Суббота")
                {
                    if (count == 0) mark += 0.0;// лучше освободить субботу при средней нагрузке
                    if (count > 0 && count <= 2) mark += ((700.0 / (double)maxLessons) * 10.0);//зачем эти 1 или 2  пары ? в этом нет смысла
                    if (count == 3) mark += ((100.0 / (double)maxLessons) * 10.0);
                    if (count == 4) mark += ((250.0 / (double)maxLessons) * 10.0);
                    if (count == 5) mark += ((550.0 / (double)maxLessons) * 10.0);
                    if (count == 6) mark += ((800.0 / (double)maxLessons) * 10.0);

                }
                else
                {
                    if (count <= 4) mark += 0.0;//при среднем числе предметов на неделе - 4 оптимальный результат
                    if (count == 5) mark += ((150.0 / (double)maxLessons) * 10.0); //градация оценки при 19 = 78,9 | 20 =  75 |  21 =  71,4 |  22 = 68,1  |  23 = 65,2
                    if (count == 6) mark += ((300.0 / (double)maxLessons) * 10.0); //градация оценки при 19 = 157,8 | 20 =  150 |  21 =  142,8 |  22 = 136,3  |  23 = 130,4
                }
            }

            if (maxLessons > 23 && maxLessons <= 28)
            {
                if (day.name == "Суббота")
                {
                    if (count <= 3) mark += 0.0;// при большом числе предметов стоит уделить субботе меньшее их число
                    if (count == 4) mark += ((70.0 / (double)maxLessons) * 10.0);
                    if (count == 5) mark += ((110.0 / (double)maxLessons) * 10.0);
                    if (count == 6) mark += ((800.0 / (double)maxLessons) * 10.0);

                }
                else
                {
                    if (count <= 4) mark += 0.0;//при большом числе предметов на неделе - 4 наилучший результат
                    if (count == 5) mark += ((60.0 / (double)maxLessons) * 10.0); //градация оценки при 24 = 25 | 25 =  24   |  26 =  23 |  27 = 22,2 |  28 = 21,4
                    if (count == 6) mark += ((250.0 / (double)maxLessons) * 10.0); //градация оценки при 24 = 104,1 | 25 =  100 |  26 =  96,1 |  27 = 92,5 |  28 = 89,2
                }
            }

            if (maxLessons > 28)
            {
                if (day.name == "Суббота")
                {
                    if (count <= 4) mark += 0.0;// при крайне высокой нагрузке для субботы подойдет почти любое число предметов
                    if (count == 5) mark += ((100.0 / (double)maxLessons) * 10.0);
                    if (count == 6 && maxLessons < 34)
                    {
                        mark += ((250.0 / (double)maxLessons) * 10.0);
                    }
                    if (count == 6 && maxLessons > 34) mark+= ((40.0 / (double)maxLessons) * 10.0);

                }
                else
                {
                    if (count <= 5) mark += 0.0;//при крайне высоком числе предметов на неделе - 5 наилучший результат
                    if (count == 6) mark += ((100.0 / (double)maxLessons) * 10.0); //градация оценки при 29 = 34,5 | 30 =  33,3 |  31 =  32,2 |  32 = 31,25 |  33 = 30,3 ....
                }
            }


            return mark;
        }

        public Day[] InputMarks()
        {
            return days;
        }

        public double TotalMark()
        {
            double total = 0.0;
            for (int i = 0; i < 6; i++)
            {
                total += days[i].mark;
            }
            return total;
        }
    }
}
