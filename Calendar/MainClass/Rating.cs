using Calendar.elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    class Rating
    {
        private Day[] days = new Day[6];
        private int maxLessons;
        private List<UnicLesson> unicLessons;

        public Rating(Day[] days, int maxLessons, List<UnicLesson> unicLessons)
        {
            for (int i = 0; i < 6; i++)
            {
                this.days[i] = new Day(days[i]);
            }
            this.maxLessons = maxLessons;
            this.unicLessons = unicLessons;

            //оценка c понедельника по субботу, где this.mark - это оценка одного дня
            for (int i = 0; i < 6; i++)
            {
                this.days[i].mark = NextDay(days[i]);
            }

            //days = InputMarks();
            //итоговая оценка

        }

        public  double GetXromMarks(int index)
        {
            double mark = days[index].mark;

            return mark;
        }

        //Метод для расчета оценки одного дня, строится на основе штрафов за каждый критерий исследования генов в хромосоме
        private double NextDay(Day day)
        {
            double totalMarkDay = 0.0;//общая оценка отдного дня по всем критериям

            //оценка общего числа пар
            totalMarkDay += MaxLessonsMark(day);//штрафы за количество пар
            totalMarkDay += WindowMarks(day);//штрафы за наличие окон между парами
            totalMarkDay += SameMarks(day);//штрафы за однообразие пар
            totalMarkDay += SameTeachersMarks(day);//штрафы за сверхурочные работы преподавателей в эти дни
            totalMarkDay += FizkultPrivatMarks(day);//давайте поставим физкультуру последней парой
            totalMarkDay += CountLessonMarks(day);//штрафы за неравномерную распределнность предметов в расписании

            return totalMarkDay;
        }

        //Метод, возвращающий оценку по критерию количества пар в день на основе их общего количества
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
                    if (count == 6 && maxLessons > 34) mark += ((40.0 / (double)maxLessons) * 10.0);

                }
                else
                {
                    if (count <= 5) mark += 0.0;//при крайне высоком числе предметов на неделе - 5 наилучший результат
                    if (count == 6) mark += ((100.0 / (double)maxLessons) * 10.0); //градация оценки при 29 = 34,5 | 30 =  33,3 |  31 =  32,2 |  32 = 31,25 |  33 = 30,3 ....
                }
            }


            return mark;
        }

        //Метод, возвращающий оценку по критерию наличия окон в текущем дне, чем больше окон между парами - тем выше штраф
        private double WindowMarks(Day day)
        {
            double mark = 0.0;

            int count = 0;//счетчик пар
            int lastIndex = Array.LastIndexOf(day.matrix, true);//позиция последней пары
            int window = 0;//счетчик окон

            for (int i = 0; i < 6; i++)
            {
                if (day.matrix[i])//если пара есть
                {

                    if (i != 0 && !day.matrix[i - 1] && count != 0)//[если пара не первая] и [предыдущей пары не было] и [раннее уже встречались пары]
                    {
                        window++;
                    }

                    count++;
                }
                else//если пары нет
                {
                    if (i != 0 && !day.matrix[i - 1] && count != 0 && lastIndex > i)//[если пара не первая] и [предыдущей пары тоже не было] и [пары раннее уже встречались] и [последняя пара еще не началась]
                    {
                        window++;
                    }
                }
            }

            if (window == 4) mark = 3000.0;
            if (window == 3) mark = 1500.0;
            if (window == 2) mark = 1000.0;
            if (window == 1) mark = 300.0;
            if (window == 0) mark = 0.0;

            return mark;
        }

        //Метод, возвращающий штрафы за одинаковые пары, не стоящие рядом друг с другом
        private double SameMarks(Day day)
        {
            double mark = 0.0;

            string[] lessons = { "", "", "", "", "", "" };
            for (int i = 0; i < 6; i++)
            {
                if (day.matrixL[i] != null) lessons[i] = day.matrixL[i].lesson;
            }
            List<KeyValuePair<int, string>> odinary = new List<KeyValuePair<int, string>>();//объявим словарь, в котором ключи могут повторяться
            //занесем все уникальные названия уроков и число их повторений в словарь
            foreach (string val in lessons.Distinct())
            {
                odinary.Add(new KeyValuePair<int, string>(lessons.Where(x => x == val).Count(), val));
            }
            //пройдемся по полученным результатам и выясним какие предметы повторяются и стоят ли они рядом?
            foreach (KeyValuePair<int, string> keyValue in odinary)
            {
                if (keyValue.Value != "" && keyValue.Key > 1)//если поле не пустое и число повторений более одного:
                {
                    if (keyValue.Key > 3) mark += 2500.0;//если встречаются 4 одинаковых предмета в один день
                    if (keyValue.Key == 3)//если встречается 3 одинаковых предмета в этот день, проверим стоят ли они подряд
                    {
                        int index = Array.IndexOf(lessons, keyValue.Value); //индекс первого вхождения рассматриваемого предмета
                        int same = 0;//последовательно идущие предметы         0
                        int unSame = 0;//число разрывов между ними             0
                        //если предметов = 3, то гарантированно первое вхождение от 1 о 4
                        for (int i = index; i < 6; i++)
                        {
                            if (lessons[i] == keyValue.Value) same++; else if (keyValue.Key != same) unSame++;
                        }

                        if (unSame == 2 || unSame == 3) mark += 752.0;//ужасная ситуация, пары разрознены в шахматном порядке
                        if (unSame == 1) mark += 150.0;//разрозненность минимальна
                        if (unSame == 0) mark += 0.0;
                    }

                    if (keyValue.Key == 2)//если встречается 2 одинаковых предмета в этот день, проверим стоят ли они подряд
                    {
                        int index = Array.IndexOf(lessons, keyValue.Value); //индекс первого вхождения рассматриваемого предмета
                        int same = 0;//последовательно идущие предметы         0
                        int unSame = 0;//число разрывов между ними             0
                        //если предметов = 2, то гарантированно первое вхождение от 1 до 5
                        for (int i = index; i < 6; i++)
                        {
                            if (lessons[i] == keyValue.Value) same++; else if (keyValue.Key != same) unSame++;
                        }

                        if (unSame >= 1) mark += 653.1;//2 пары должны всегда стоять рядом
                        if (unSame == 0) mark += 0.0;
                    }

                    if (keyValue.Key == 1) mark += 0.0;
                }
            }

            return mark;
        }

        //Метод, возвращающий штрафы за преподавание пар одним и тем же профессором более 4х пар подряд в день.
        private double SameTeachersMarks(Day day)
        {
            double mark = 0.0;

            string[] professor = { "", "", "", "", "", "" };
            for (int i = 0; i < 6; i++)
            {
                if (day.matrixL[i] != null) professor[i] = day.matrixL[i].teacher;
            }

            List<KeyValuePair<int, string>> odinary = new List<KeyValuePair<int, string>>();//объявим словарь, в котором ключи могут повторяться
            //занесем все уникальные фио учителей и число их повторений в словарь
            foreach (string val in professor.Distinct())
            {
                odinary.Add(new KeyValuePair<int, string>(professor.Where(x => x == val).Count(), val));
            }

            foreach (KeyValuePair<int, string> keyValue in odinary)
            {
                if (keyValue.Value != "" && keyValue.Key > 1)//если поле не пустое и число повторений более одного:
                {
                    if (keyValue.Key == 6) mark += 2500.0;//6 пар подряд - крайне много и недопустимо для одного человека
                    if (keyValue.Key == 5) mark += 500.0;//5 пар - нежелательно для одного учителя, но допустимо
                    if (keyValue.Key <= 4) mark += 0;

                }
            }

            return mark;
        }

        //Метод, возвращающий штраф, если физкультура не стоит последней парой
        private double FizkultPrivatMarks(Day day)
        {
            double mark = 0.0;

            string[] lessons = { "", "", "", "", "", "" };
            bool checker = false;//обнаружена ли физкультура
            string Fizkult = "";//физ-ра в бд
            for (int i = 0; i < 6; i++)
            {
                if (day.matrixL[i] != null)
                {
                    lessons[i] = day.matrixL[i].lesson;
                    if (lessons[i].IndexOf("Физкультура") > -1)
                    {
                        checker = true;
                        Fizkult = lessons[i];
                    }
                }
            }

            if (checker)//если физкультура была обнаружена среди пар, то:
            {
                int indexFiz = Array.LastIndexOf(lessons, Fizkult);//индекс последнего вхождения физкультуры
                int index = 0;//индекс предмета, отличного от физкультуры, стоящего последним и не являющимся окном, либо индекс физ-ры, если она стоит последней
                for (int i = 0; i < 6; i++)
                {
                    if (lessons[i] != "") index = i;
                }

                if (indexFiz >= index) mark += 0.0; else mark += 997.63;//если физкультура найдена и стоит последней то отлично!, если нет, то штрафуем
            }

            return mark;
        }

        //метод возвращающий штрафы за неравномерное распределение предметов в течение недели
        private double CountLessonMarks(Day day)
        {
            double mark = 0.0;

            string[] lessons = { "", "", "", "", "", "" };
            for (int i = 0; i < 6; i++)
            {
                if (day.matrixL[i] != null) lessons[i] = day.matrixL[i].lesson;
            }
            List<KeyValuePair<int, string>> odinary = new List<KeyValuePair<int, string>>();//объявим словарь, в котором ключи могут повторяться
            //занесем все уникальные названия уроков и число их повторений в словарь
            foreach (string val in lessons.Distinct())
            {
                odinary.Add(new KeyValuePair<int, string>(lessons.Where(x => x == val).Count(), val));
            }
            //пройдемся по полученным результатам и выясним, равномерно ли распределенны предметы в этот день по отношению к расписанию?
            foreach (KeyValuePair<int, string> keyValue in odinary)
            {
                if (keyValue.Value != "")//если поле не пустое
                {
                    if (keyValue.Key > 3) mark += 2500.0;//если встречаются 4 одинаковых предметов в один день, нас это ни в коем случае не устраивает
                    if (keyValue.Key == 3)//если встретилось 3 предмета в один день
                    {
                        //посмотрим, сколько раз этот предмет проводится в неделю
                        bool check = true; //пока чекер активен - исследуем массив на похожее значение
                        int i = 0;//индекс первого предмета среди уникальных
                        while (check && i < unicLessons.Count)
                        {
                            if (keyValue.Value == unicLessons[i].lesson)
                            {
                                check = false;//выходим из цикла, если найдено совпадение
                                if (unicLessons[i].time >= 6) mark += 0.0;//предмет может проводиться 3 раза в один день, если их общее число в неделю привышает 6
                                if (unicLessons[i].time == 5) mark += 52.0;//проведение предмета при таком числе 3 раза возможно
                                if (unicLessons[i].time == 4) mark += 351.78;//проведение предмета при таком числе 3 раза крайне не желательно
                                if (unicLessons[i].time == 3) mark += 0.0;//проведение предмета при таком числе 3 раза является неплохим вариантом
                            }
                            i++;
                        }
                    }
                    if (keyValue.Key == 2)//если встретилось 2 предмета в один день
                    {
                        //посмотрим, сколько раз этот предмет проводится в неделю
                        bool check = true; //пока чекер активен - исследуем массив на похожее значение
                        int i = 0;//индекс первого предмета среди уникальных
                        while (check && i < unicLessons.Count)
                        {
                            if (keyValue.Value == unicLessons[i].lesson)
                            {
                                check = false;//выходим из цикла, если найдено совпадение
                                if (unicLessons[i].time >= 3) mark += 0.0;//проведение предмета при 2 раза при 3х всего - хороший вариант
                                if (unicLessons[i].time == 2) mark += 57.0;//проведение предмета при таком числе 2 раза не критично, но и не является необходимым
                            }
                            i++;
                        }
                    }
                    if (keyValue.Key == 1)//если встретилось 1 предмета в один день
                    {
                        //посмотрим, сколько раз этот предмет проводится в неделю
                        bool check = true; //пока чекер активен - исследуем массив на похожее значение
                        int i = 0;//индекс первого предмета среди уникальных
                        while (check && i < unicLessons.Count)
                        {
                            if (keyValue.Value == unicLessons[i].lesson)
                            {
                                check = false;//выходим из цикла, если найдено совпадение
                                if (unicLessons[i].time >= 6) mark += 970.2;//слишком растратно проводить предмет 1 раз при высокой нагрузке
                                if (unicLessons[i].time == 5) mark += 552.21;//1 раз слишком мало при таком числе раз
                                if (unicLessons[i].time == 4) mark += 251.78;//нежелательно проводить предмет 1 раз при 4х
                                if (unicLessons[i].time == 3) mark += 174.6;//проводить предмет при таком числе раз не слишком целесообразно
                                if (unicLessons[i].time <= 2) mark += 0;//проводить предмет 1 раз из 2х или 1ого - лучший вариант
                            }
                            i++;
                        }
                    }
                }
            }
            return mark;


        }

        //Метод, возвращающий особь с посчитанными оценками хромосом, для замены содержимого ссылки в конструкторе класса
        public Day[] InputMarks()
        {
            return days;
        }

        //Подсчет итоговой оценки для особи
        public double TotalMark()
        {
            double total = 0.0;
            for (int i = 0; i < 6; i++)
            {
                total += days[i].mark;
            }
           
            // total += CommonMarksDays(); //хотелось бы добавить метод о распределенности предметов в расписании

            return total;
        }
    }
}
