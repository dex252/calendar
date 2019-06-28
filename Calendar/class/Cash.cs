using Calendar.elements;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{

    internal class Cash
    {
        private SqlConnection sqlConnection;
        public List<Day> days;
        public List<Lesson> lessons;
        private string[] Months = { "September", "October", "November", "December", "January" };

        public Cash(SqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;

            days = new List<Day>();
            lessons = new List<Lesson>();

            DayFromDatabase();//заполнение кеша из базы данных
            LessonFromDatabase();//заполнение кеша из базы данных
        }

        private void DayFromDatabase()
        {
            SqlCommand command = sqlConnection.CreateCommand();

            foreach (string month in Months)
            {
                command.CommandText = "select day, num_day from days where day='" + month + "' order by num_day;";

                try
                {
                    SqlDataReader sqlReader = null;
                    try
                    {
                        sqlReader = command.ExecuteReader();

                        while (sqlReader.Read())
                        {
                            Day day = new Day();
                            day.name = (string)sqlReader[0];
                            day.num = (int)sqlReader[1];
                            days.Add(day);
                            day = null;
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
                            Lesson lesson = new Lesson();
                            lesson.id = (int)sqlReader[0];
                            lesson.teacher = (string)sqlReader[1];
                            lesson.lesson = (string)sqlReader[2];
                            lesson.time = (int)sqlReader[3];
                            lessons.Add(lesson);
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


    }
}
