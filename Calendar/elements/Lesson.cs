using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.elements
{
    class Lesson
    {
        public int id; //уникальный id, так как один предмет могут вести более одного учителя теоретически. Ориентир на id
        public string teacher;
        public string lesson;
        public int time;
    }
}
