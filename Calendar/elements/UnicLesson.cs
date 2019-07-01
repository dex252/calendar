namespace Calendar.elements
{
    class UnicLesson
    {
        public int id; //уникальный id, так как один предмет могут вести более одного учителя теоретически. Ориентир на id - таблица work
        public string teacher;
        public string lesson;
        public int time;//число пар в неделю
    }
}
