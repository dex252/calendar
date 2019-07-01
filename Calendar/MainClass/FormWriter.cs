using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendar.elements;

namespace Calendar
{
    internal class FormWriter
    {
        private Calendar main;
        private Cash cash;

        public FormWriter(Calendar main, Cash cash)
        {
            this.main = main;
            this.cash = cash;
        }

        //отрисовка поколений
        public void GetPopulation(int index)
        {
            if (index >=0)
            {
                Generations cur = cash.generations[index];
                //оценки
                main.mark.Text = cur.mark.ToString();
                main.mark0.Text = cur.days[0].mark.ToString();
                main.mark1.Text = cur.days[1].mark.ToString();
                main.mark2.Text = cur.days[2].mark.ToString();
                main.mark3.Text = cur.days[3].mark.ToString();
                main.mark4.Text = cur.days[4].mark.ToString();
                main.mark5.Text = cur.days[5].mark.ToString();
                //заполнение парами
                //заполнение кабинетов
                //заполнение иен преподователей

                //понедельник
                if (cur.days[0].matrixL[0] != null)
                {
                    main.name00.Text = cur.days[0].matrixL[0].lesson;
                    main.cabinet00.Text = cur.days[0].matrixL[0].area;
                    main.teacher00.Text = cur.days[0].matrixL[0].teacher;
                }
                if (cur.days[0].matrixL[1] != null)
                {
                    main.name01.Text = cur.days[0].matrixL[1].lesson;
                    main.cabinet01.Text = cur.days[0].matrixL[1].area;
                    main.teacher01.Text = cur.days[0].matrixL[1].teacher;
                }
                if (cur.days[0].matrixL[2] != null)
                {
                    main.name02.Text = cur.days[0].matrixL[2].lesson;
                    main.cabinet02.Text = cur.days[0].matrixL[2].area;
                    main.teacher02.Text = cur.days[0].matrixL[2].teacher;
                }
                if (cur.days[0].matrixL[3] != null)
                {
                    main.name03.Text = cur.days[0].matrixL[3].lesson;
                    main.cabinet03.Text = cur.days[0].matrixL[3].area;
                    main.teacher03.Text = cur.days[0].matrixL[3].teacher;
                }
                if (cur.days[0].matrixL[4] != null)
                {
                    main.name04.Text = cur.days[0].matrixL[4].lesson;
                    main.cabinet04.Text = cur.days[0].matrixL[4].area;
                    main.teacher04.Text = cur.days[0].matrixL[4].teacher;
                }
                if (cur.days[0].matrixL[5] != null)
                {
                    main.name05.Text = cur.days[0].matrixL[5].lesson;
                    main.cabinet05.Text = cur.days[0].matrixL[5].area;
                    main.teacher05.Text = cur.days[0].matrixL[5].teacher;
                }

                //вторник
                if (cur.days[1].matrixL[0] != null)
                {
                    main.name10.Text = cur.days[1].matrixL[0].lesson;
                    main.cabinet10.Text = cur.days[1].matrixL[0].area;
                    main.teacher10.Text = cur.days[1].matrixL[0].teacher;
                }
                if (cur.days[1].matrixL[1] != null)
                {
                    main.name11.Text = cur.days[1].matrixL[1].lesson;
                    main.cabinet11.Text = cur.days[1].matrixL[1].area;
                    main.teacher11.Text = cur.days[1].matrixL[1].teacher;
                }
                if (cur.days[1].matrixL[2] != null)
                {
                    main.name12.Text = cur.days[1].matrixL[2].lesson;
                    main.cabinet12.Text = cur.days[1].matrixL[2].area;
                    main.teacher12.Text = cur.days[1].matrixL[2].teacher;
                }
                if (cur.days[1].matrixL[3] != null)
                {
                    main.name13.Text = cur.days[1].matrixL[3].lesson;
                    main.cabinet13.Text = cur.days[1].matrixL[3].area;
                    main.teacher13.Text = cur.days[1].matrixL[3].teacher;
                }
                if (cur.days[1].matrixL[4] != null)
                {
                    main.name14.Text = cur.days[1].matrixL[4].lesson;
                    main.cabinet14.Text = cur.days[1].matrixL[4].area;
                    main.teacher14.Text = cur.days[1].matrixL[4].teacher;
                }
                if (cur.days[1].matrixL[5] != null)
                {
                    main.name15.Text = cur.days[1].matrixL[5].lesson;
                    main.cabinet15.Text = cur.days[1].matrixL[5].area;
                    main.teacher15.Text = cur.days[1].matrixL[5].teacher;
                }

                //среда
                if (cur.days[2].matrixL[0] != null)
                {
                    main.name20.Text = cur.days[2].matrixL[0].lesson;
                    main.cabinet20.Text = cur.days[2].matrixL[0].area;
                    main.teacher20.Text = cur.days[2].matrixL[0].teacher;
                }
                if (cur.days[2].matrixL[1] != null)
                {
                    main.name21.Text = cur.days[2].matrixL[1].lesson;
                    main.cabinet21.Text = cur.days[2].matrixL[1].area;
                    main.teacher21.Text = cur.days[2].matrixL[1].teacher;
                }
                if (cur.days[2].matrixL[2] != null)
                {
                    main.name22.Text = cur.days[2].matrixL[2].lesson;
                    main.cabinet22.Text = cur.days[2].matrixL[2].area;
                    main.teacher22.Text = cur.days[2].matrixL[2].teacher;
                }
                if (cur.days[2].matrixL[3] != null)
                {
                    main.name23.Text = cur.days[2].matrixL[3].lesson;
                    main.cabinet23.Text = cur.days[2].matrixL[3].area;
                    main.teacher23.Text = cur.days[2].matrixL[3].teacher;
                }
                if (cur.days[2].matrixL[4] != null)
                {
                    main.name24.Text = cur.days[2].matrixL[4].lesson;
                    main.cabinet24.Text = cur.days[2].matrixL[4].area;
                    main.teacher24.Text = cur.days[2].matrixL[4].teacher;
                }
                if (cur.days[2].matrixL[5] != null)
                {
                    main.name25.Text = cur.days[2].matrixL[5].lesson;
                    main.cabinet25.Text = cur.days[2].matrixL[5].area;
                    main.teacher25.Text = cur.days[2].matrixL[5].teacher;
                }

                //четверг
                if (cur.days[3].matrixL[0] != null)
                {
                    main.name30.Text = cur.days[3].matrixL[0].lesson;
                    main.cabinet30.Text = cur.days[3].matrixL[0].area;
                    main.teacher30.Text = cur.days[3].matrixL[0].teacher;
                }
                if (cur.days[3].matrixL[1] != null)
                {
                    main.name31.Text = cur.days[3].matrixL[1].lesson;
                    main.cabinet31.Text = cur.days[3].matrixL[1].area;
                    main.teacher31.Text = cur.days[3].matrixL[1].teacher;
                }
                if (cur.days[3].matrixL[2] != null)
                {
                    main.name32.Text = cur.days[3].matrixL[2].lesson;
                    main.cabinet32.Text = cur.days[3].matrixL[2].area;
                    main.teacher32.Text = cur.days[3].matrixL[2].teacher;
                }
                if (cur.days[3].matrixL[3] != null)
                {
                    main.name33.Text = cur.days[3].matrixL[3].lesson;
                    main.cabinet33.Text = cur.days[3].matrixL[3].area;
                    main.teacher33.Text = cur.days[3].matrixL[3].teacher;
                }
                if (cur.days[3].matrixL[4] != null)
                {
                    main.name34.Text = cur.days[3].matrixL[4].lesson;
                    main.cabinet34.Text = cur.days[3].matrixL[4].area;
                    main.teacher34.Text = cur.days[3].matrixL[4].teacher;
                }
                if (cur.days[3].matrixL[5] != null)
                {
                    main.name35.Text = cur.days[3].matrixL[5].lesson;
                    main.cabinet35.Text = cur.days[3].matrixL[5].area;
                    main.teacher35.Text = cur.days[3].matrixL[5].teacher;
                }

                //пятница
                if (cur.days[4].matrixL[0] != null)
                {
                    main.name40.Text = cur.days[4].matrixL[0].lesson;
                    main.cabinet40.Text = cur.days[4].matrixL[0].area;
                    main.teacher40.Text = cur.days[4].matrixL[0].teacher;
                }
                if (cur.days[4].matrixL[1] != null)
                {
                    main.name41.Text = cur.days[4].matrixL[1].lesson;
                    main.cabinet41.Text = cur.days[4].matrixL[1].area;
                    main.teacher41.Text = cur.days[4].matrixL[1].teacher;
                }
                if (cur.days[4].matrixL[2] != null)
                {
                    main.name42.Text = cur.days[4].matrixL[2].lesson;
                    main.cabinet42.Text = cur.days[4].matrixL[2].area;
                    main.teacher42.Text = cur.days[4].matrixL[2].teacher;
                }
                if (cur.days[4].matrixL[3] != null)
                {
                    main.name43.Text = cur.days[4].matrixL[3].lesson;
                    main.cabinet43.Text = cur.days[4].matrixL[3].area;
                    main.teacher43.Text = cur.days[4].matrixL[3].teacher;
                }
                if (cur.days[4].matrixL[4] != null)
                {
                    main.name44.Text = cur.days[4].matrixL[4].lesson;
                    main.cabinet44.Text = cur.days[4].matrixL[4].area;
                    main.teacher44.Text = cur.days[4].matrixL[4].teacher;
                }
                if (cur.days[4].matrixL[5] != null)
                {
                    main.name45.Text = cur.days[4].matrixL[5].lesson;
                    main.cabinet45.Text = cur.days[4].matrixL[5].area;
                    main.teacher45.Text = cur.days[4].matrixL[5].teacher;
                }

                //суббота
                if (cur.days[5].matrixL[0] != null)
                {
                    main.name50.Text = cur.days[5].matrixL[0].lesson;
                    main.cabinet50.Text = cur.days[5].matrixL[0].area;
                    main.teacher50.Text = cur.days[5].matrixL[0].teacher;
                }
                if (cur.days[5].matrixL[1] != null)
                {
                    main.name51.Text = cur.days[5].matrixL[1].lesson;
                    main.cabinet51.Text = cur.days[5].matrixL[1].area;
                    main.teacher51.Text = cur.days[5].matrixL[1].teacher;
                }
                if (cur.days[5].matrixL[2] != null)
                {
                    main.name52.Text = cur.days[5].matrixL[2].lesson;
                    main.cabinet52.Text = cur.days[5].matrixL[2].area;
                    main.teacher52.Text = cur.days[5].matrixL[2].teacher;
                }
                if (cur.days[5].matrixL[3] != null)
                {
                    main.name53.Text = cur.days[5].matrixL[3].lesson;
                    main.cabinet53.Text = cur.days[5].matrixL[3].area;
                    main.teacher53.Text = cur.days[5].matrixL[3].teacher;
                }
                if (cur.days[5].matrixL[4] != null)
                {
                    main.name54.Text = cur.days[5].matrixL[4].lesson;
                    main.cabinet54.Text = cur.days[5].matrixL[4].area;
                    main.teacher54.Text = cur.days[5].matrixL[4].teacher;
                }
                if (cur.days[5].matrixL[5] != null)
                {
                    main.name55.Text = cur.days[5].matrixL[5].lesson;
                    main.cabinet55.Text = cur.days[5].matrixL[5].area;
                    main.teacher55.Text = cur.days[5].matrixL[5].teacher;
                }

            }
        }

        //заполнение листа поколениями
        public void GetList(List<Generations> generations)
        {
            for (int i =0; i<generations.Count; i++)
            {
                main.generationsBox.Items.Add(generations[i].name);
            }
           
        }

        public void Timers(string[] timers, string group)
        {
            main.textBoxTimers1.Text = timers[0];
            main.textBoxTimers2.Text = timers[1];
            main.textBoxTimers3.Text = timers[2];
            main.textBoxTimers4.Text = timers[3];
            main.textBoxTimers5.Text = timers[4];
            main.textBoxTimers6.Text = timers[5];

            main.textBoxTimers7.Text = timers[0];
            main.textBoxTimers8.Text = timers[1];
            main.textBoxTimers9.Text = timers[2];
            main.textBoxTimers10.Text = timers[3];
            main.textBoxTimers11.Text = timers[4];
            main.textBoxTimers12.Text = timers[5];

            main.labelGroup.Text = group;
        }

    }
}
