using Calendar.elements;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{

    class Cash
    {
        private int maxDay;//общее число дней в семестре
        private string[] Week = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };
        private Random rand = new Random();
        private string group;// - тут хранится номер группы
        private Stack<Lesson> lessons;//стек, в котором хранятся все пары для распределения в days
        private List<string> area;//номера кабинетов
        private string[] timers = { "", "", "", "", "", "" };//расписание звонков
        //private Generator generator;//здесь проводятся скрещивания и мутации, выводятся итоговые поколения
        private Generator generator;

        public int maxStack;//общее число предметов на неделе
        public SqlConnection sqlConnection;
        public List<UnicLesson> unicLessons; //уникальные уроки, т.е. соответствие преподаватель-предмет
        public double firstmark;
        public Day[] days; //дни - особь
        public FormWriter render;//отображение на форме
        public List<Generations> generations; //здесь хранятся все итоговые поколения (ВАЖНЕЙШИЙ)

        public Cash(SqlConnection sqlConnection, Calendar main, Config config)
        {
            this.sqlConnection = sqlConnection;
            render = new FormWriter(main, this);

            unicLessons = new List<UnicLesson>();
            area = new List<string>();
            lessons = new Stack<Lesson>();
            generations = new List<Generations>();
            days = new Day[6];
            //заполнение кеша из базы данных
            DayAndGroupsFromDatabase();//читаем число дней и названия групп
            AreasFromDatabase();//заполнение списка кабинетов
            TimersFromDatabase();//чтение расписания звонков
            LessonFromDatabase();//заполнение уникальных уроков в бд
            DaysStart();//заполнение дней именами недели
            StackStart();//заполнение стека парами из расчета их количества в unicLessons
            FirstPopulation();//формирование начальной популяции
            generator = new Generator(this);

            //generator.GetPopulations(1000);//выведение новых поколений, аргумент - число популяций
            generator.GetPopulations(config.numGenerations);

            render.Timers(timers, group);//отрисовка времени проведения занятий и номера группы
            render.GetList(generations);//заполнение листа поколениями

        }

        private void FirstPopulation()
        {
            List<int> freeSpace = new List<int>();//список свободных пар на неделе (6 дней * 6 пар = 36)
            List<int> weekly = new List<int>();
            int x = 0;
            for (int i = 0; i < 36; i++)
            {
                freeSpace.Add(i);
                weekly.Add(x);
                x++;
                if (x > 5) x = 0;
            }

            //распределение всех пар из стека по дням, то есть распределение хромосом в особи
            maxStack = lessons.Count;

            for (int i = 0; i < maxStack; i++)
            {
                Lesson freeLesson = lessons.Pop();
                //boundLesson.area =   // вставить номер кабинета

                int index = rand.Next(freeSpace.Count);//index в свободных днях для проведения пар
                int index2 = weekly.ElementAt(index);//по какому счету проводится пара от 0 до 5
                int dday = freeSpace.ElementAt(index);//значение свободного дня от 1 до 36
                freeSpace.RemoveAt(index);//удаляем из списка свободных дней тот, который уже взяли
                weekly.RemoveAt(index);
                int curDay = 0;
                if (dday >= 0 && dday < 6)
                {
                    curDay = 0;
                }
                else if (dday >= 6 && dday < 12)
                {
                    curDay = 1;
                }
                else if (dday >= 12 && dday < 18)
                {
                    curDay = 2;
                }
                else if (dday >= 18 && dday < 24)
                {
                    curDay = 3;
                }
                else if (dday >= 24 && dday < 30)
                {
                    curDay = 4;
                }
                else
                {
                    curDay = 5;
                }

                days[curDay].matrix[index2] = true;
                days[curDay].matrixL[index2] = freeLesson;
            }

            //проводим оценку начальной особи по хромосомам
            Rating ratio = new Rating(days, maxStack, unicLessons);

            //заносим начальную популяцию в generations
            Generations generic = new Generations("популяция #0");
            
            generic.mark = ratio.TotalMark();//считаем общую оценку особи
            generic.Input(days);
            generations.Add(new Generations(generic));
            firstmark = generic.mark;
            generic = null;
            //generator = new Generator(days, maxStack, unicLessons, generations, firstmark);//вносим первую основную особь в генератор

           

        }

        private void StackStart()
        {
            for (int i = 0; i < unicLessons.Count; i++)
            {
                for (int j = 0; j < unicLessons[i].time; j++)
                {
                    Lesson lesson = new Lesson();
                    lesson.lesson = unicLessons[i].lesson;
                    lesson.teacher = unicLessons[i].teacher;
                    lesson.area = area[rand.Next(area.Count)];

                    lessons.Push(lesson);
                    lesson = null;
                }
            }
        }

        private void DaysStart()
        {
            int i = 0;
            foreach (string week in Week)
            {
                Day day = new Day();
                day.name = week;
                days[i] = day;
                i++;
            }

        }

        private void DayAndGroupsFromDatabase()
        {
            SqlCommand command = sqlConnection.CreateCommand();

            command.CommandText = "select count (id), (select name from groups) from days";

            try
            {
                SqlDataReader sqlReader = null;
                try
                {
                    sqlReader = command.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        maxDay = (int)sqlReader[0];
                        group = sqlReader[1].ToString();
                    }

                    if (sqlReader != null)
                    {
                        sqlReader.Close();
                    }

                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("Что то пошло не так при считывании записей.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                finally
                {
                    if (sqlReader != null)
                    {
                        sqlReader.Close();
                    }
                }
            }

            catch
            {
                System.Windows.Forms.MessageBox.Show("Что то пошло не так.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

        }

        private void LessonFromDatabase()
        {
            SqlCommand command = sqlConnection.CreateCommand();

            command.CommandText = "select id, (SELECT name from teachers where id=work.id_teacher),(SELECT name from lessons where id=work.id_lesson),  (SELECT time from lessons where id=work.id_lesson) from work";

            try
            {
                SqlDataReader sqlReader = null;
                try
                {
                    sqlReader = command.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        UnicLesson lesson = new UnicLesson();
                        lesson.id = (int)sqlReader[0];
                        lesson.teacher = (string)sqlReader[1];
                        lesson.lesson = (string)sqlReader[2];
                        lesson.time = (int)sqlReader[3] * 6 / maxDay;//здесь заполняется число пар для этого предмета в неделю
                        double m = (int)sqlReader[3] * 6 % maxDay / (double)maxDay;
                        if (m > 0.7) lesson.time += 1;
                        unicLessons.Add(lesson);

                        lesson = null;
                    }

                    if (sqlReader != null)
                    {
                        sqlReader.Close();
                    }

                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("Что то пошло не так при считывании записей.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                finally
                {
                    if (sqlReader != null)
                    {
                        sqlReader.Close();
                    }
                }
            }

            catch
            {
                System.Windows.Forms.MessageBox.Show("Что то пошло не так.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }


        }

        private void AreasFromDatabase()

        {
            SqlCommand command = sqlConnection.CreateCommand();

            command.CommandText = "select name from area";

            try
            {
                SqlDataReader sqlReader = null;
                try
                {
                    sqlReader = command.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        string areas = sqlReader[0].ToString();
                        area.Add(areas);
                        areas = null;
                    }

                    if (sqlReader != null)
                    {
                        sqlReader.Close();
                    }

                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("Что то пошло не так при считывании записей.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                finally
                {
                    if (sqlReader != null)
                    {
                        sqlReader.Close();
                    }
                }
            }

            catch
            {
                System.Windows.Forms.MessageBox.Show("Что то пошло не так.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void TimersFromDatabase()
        {
            {
                SqlCommand command = sqlConnection.CreateCommand();

                command.CommandText = "select time from timers order by number_lesson";

                try
                {
                    SqlDataReader sqlReader = null;
                    try
                    {
                        sqlReader = command.ExecuteReader();
                        int i = 0;
                        while (sqlReader.Read())
                        {
                            string timer = sqlReader[0].ToString();
                            timers[i] = timer;
                            timer = null;
                            i++;
                        }

                        if (sqlReader != null)
                        {
                            sqlReader.Close();
                        }

                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("Что то пошло не так при считывании записей.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (sqlReader != null)
                        {
                            sqlReader.Close();
                        }
                    }
                }

                catch
                {
                    System.Windows.Forms.MessageBox.Show("Что то пошло не так.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }

    }
}
