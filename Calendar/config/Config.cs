namespace Calendar
{
    class Config
    {
        public string connect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Slava\source\repos\Calendar\Calendar\calendar.mdf;Integrated Security=True";
       // public string connect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dex25\Desktop\calendar\Calendar\calendar.mdf;Integrated Security=True";
        public int numGenerations = 100000;  //максимальное число итераций (количество рассматриваемых популяций)
        public int stop = 0;     // остановка алгоритма, если в течении указанного числа не было улучшений среди особей; при 0 или меньше - алгоритм работает до предела
    }
}
