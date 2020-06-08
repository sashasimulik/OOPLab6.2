using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace OOPLab6._2
{
    public interface Iworker
    {
        public abstract void AveragePatients(List<WorkingDay> Days);
        public abstract void MaxLoad(List<WorkingDay> Days);

    }

    public abstract class Doctor

    {

        public string Name { get; set; }

        public string Speciality { get; set; }
        public abstract void AveragePatients(List<WorkingDay> Days);
        public abstract void MaxLoad(List<WorkingDay> Days);
    }

    public class WorkingDay : Doctor, Iworker

    {

        public string Date { get; set; }

        public string PatientsCount { get; set; }

        public string StartHour { get; set; }

        public override void AveragePatients(List<WorkingDay> Days)

        {

            Console.Clear();

            var Group = from AVpatients in Days

                        group AVpatients by AVpatients.Name into g

                        select new

                        {

                            Name = g.Key,

                            Count = g.Count(),

                            Works = from p in g select p

                        };

            foreach (var group in Group)

            {

                float ptns = 0;

                foreach (var gr in group.Works)

                {

                    ptns += Convert.ToInt32(gr.PatientsCount);

                }

                Console.WriteLine($"{group.Name} : {ptns / group.Count} patients");

                Console.WriteLine();

            }

        }

        public override void MaxLoad(List<WorkingDay> Days)

        {

            Console.Clear();

            var Group = from MALhours in Days

                        group MALhours by MALhours.Date into g

                        select new

                        {

                            Name = g.Key,

                            Count = g.Count(),

                            Works = from p in g select p

                        };

            foreach (var group in Group)

            {

                float ptns = 0;

                foreach (var gr in group.Works)

                {

                    ptns += Convert.ToInt32(gr.PatientsCount);

                }

                Console.WriteLine($"{group.Name} : {ptns} patients");

                Console.WriteLine();

            }

        }


    }

    public class Program

    {

        private static string FileName = "Data.json";

        private static string FilePath = @"Data.json";

        static void Main(string[] args)

        {

            while (true)

            {

                Console.WriteLine("_____________________________________________________");

                Console.WriteLine("   Hot key   │            Function                 |");

                Console.WriteLine("_____________________________________________________");

                Console.WriteLine("      A      │          Add new day                |");

                Console.WriteLine("_____________________________________________________");

                Console.WriteLine("      C      │          Change day                 |");

                Console.WriteLine("_____________________________________________________");

                Console.WriteLine("      D      │          Delete day                 |");

                Console.WriteLine("_____________________________________________________");

                Console.WriteLine("      T      │        Show all days                |");

                Console.WriteLine("_____________________________________________________");

                Console.WriteLine("      H      │      Average number of patients     |  ");

                Console.WriteLine("_____________________________________________________");

                Console.WriteLine("      M      │        Number of days with max load | ");

                Console.WriteLine("_____________________________________________________");

                Console.WriteLine("      P      │     Days with wrong time            | ");

                Console.WriteLine("_____________________________________________________");

                Console.WriteLine("    Space    │         Clear console               | ");

                Console.WriteLine("_____________________________________________________");

                Console.WriteLine("     Esc     │          Exit program               | ");

                Console.WriteLine("_____________________________________________________");

                if (!File.Exists(FileName))

                {

                    File.Create(FileName).Close();

                }

                var Days = JsonConvert.DeserializeObject<List<WorkingDay>>(File.ReadAllText(FilePath));

                WorkingDay Wd = new WorkingDay();

                switch (Console.ReadKey().Key)

                {

                    case ConsoleKey.A:

                        if (Days == null)

                        {

                            Days = new List<WorkingDay>();

                            Days.Add(CreateNewDay());

                        }

                        else

                        {

                            Days.Add(CreateNewDay());

                        }

                        break;

                    case ConsoleKey.C:

                        ChangeData(Days);

                        break;

                    case ConsoleKey.D:

                        DelteDay(Days);

                        break;

                    case ConsoleKey.T:

                        ShowAll(Days);

                        break;

                    case ConsoleKey.Escape:

                        Environment.Exit(0);

                        break;

                    case ConsoleKey.H:

                        Wd.AveragePatients(Days);

                        break;

                    case ConsoleKey.P:

                        HoursOnProject(Days);

                        break;

                    case ConsoleKey.M:

                        Wd.MaxLoad(Days);

                        break;

                    case ConsoleKey.Spacebar:

                        Console.Clear();

                        break;

                }

                string serialize = JsonConvert.SerializeObject(Days, Formatting.Indented);

                if (serialize.Count() > 1)

                {

                    if (!File.Exists(FileName))

                    {

                        File.Create(FileName).Close();

                    }

                    File.WriteAllText(FilePath, serialize, Encoding.UTF8);

                }

            }

        }

        public static WorkingDay CreateNewDay()

        {

            Console.Clear();

            WorkingDay Day = new WorkingDay();

            Console.WriteLine("Enter name of doctor");

            Day.Name = Console.ReadLine();

            Console.WriteLine("Enter speciality");

            Day.Speciality = Console.ReadLine();

            Console.WriteLine("Enter date of day like 01.02.2000");

            Day.Date = Console.ReadLine();

            Console.WriteLine("Enter patients count");

            Day.PatientsCount = Console.ReadLine();

            Console.WriteLine("Enter time of starting work");

            Day.StartHour = Console.ReadLine();

            return Day;

        }

        public static void ChangeData(List<WorkingDay> Days)

        {

            Console.WriteLine("Enter date of day that`s you want to change");

            var s = Console.ReadLine();

            WorkingDay day = Days.Find(x => x.Date == s);

            if (day != null)

            {

                Console.WriteLine("Enter value of day that`s you want to change \n1)Name\n2)Speciality\n3)Date like 01.02.2000\n4)Patients count\n5)Start Hour");

                char a = Console.ReadKey().KeyChar;

                Console.WriteLine("Enter new value");

                switch (a)

                {

                    case '1':

                        day.Name = Console.ReadLine();

                        break;

                    case '2':

                        day.Speciality = Console.ReadLine();

                        break;

                    case '3':

                        day.Date = Console.ReadLine();

                        break;

                    case '4':

                        day.PatientsCount = Console.ReadLine();

                        break;

                    case '5':

                        day.StartHour = Console.ReadLine();

                        break;

                }

            }

        }


        public static void DelteDay(List<WorkingDay> Days)

        {

            if (Days != null)

            {

                Console.WriteLine("Enter date of day that`s you want to delete");

                var s = Console.ReadLine();

                Days.RemoveAll(x => x.Date == s);



            }

        }

        public static void ShowAll(List<WorkingDay> Days)

        {

            Console.Clear();

            Console.WriteLine("____________________________________________________________________");

            Console.WriteLine("     Name    │  Speciality  │   Date   │ Patients Count │ Start Hour");

            Console.WriteLine("____________________________________________________________________");

            for (int i = 0; i < Days.Count; i++)

            {

                Console.WriteLine("{0,10} {1, 10} {2, 15} {3, 10} {4, 13}", Days[i].Name, Days[i].Speciality, Days[i].Date, Days[i].PatientsCount, Days[i].StartHour);

                Console.WriteLine("____________________________________________________________________");

            }

            Console.WriteLine("____________________________________________________________________");

        }

        public static int HoursOnProject(List<WorkingDay> Days)

        {

            Console.Clear();

            var Group = from hours in Days

                        group hours by hours.StartHour into g

                        select new

                        {

                            Name = g.Key,

                            Count = g.Count(),

                            Works = from p in g select p

                        };

            int fulltime = 0;

            foreach (var group in Group)
            {

                foreach (var gr in group.Works)
                {

                    if (Convert.ToInt32(group.Name) > 14)
                    {
                        Console.WriteLine($"{group.Name} : {group.Count} patient was observed after 2pm");
                    }
                }


                Console.WriteLine();

            }

            return fulltime;

        }

        public static void AveragePatients(List<WorkingDay> Days)

        {

            Console.Clear();

            var Group = from AVpatients in Days

                        group AVpatients by AVpatients.Name into g

                        select new

                        {

                            Name = g.Key,

                            Count = g.Count(),

                            Works = from p in g select p

                        };

            foreach (var group in Group)

            {

                float ptns = 0;

                foreach (var gr in group.Works)

                {

                    ptns += Convert.ToInt32(gr.PatientsCount);

                }

                Console.WriteLine($"{group.Name} : {ptns / group.Count} patients");

                Console.WriteLine();

            }

        }

        public static void MaxLoad(List<WorkingDay> Days)

        {

            Console.Clear();

            var Group = from MALhours in Days

                        group MALhours by MALhours.Date into g

                        select new

                        {

                            Name = g.Key,

                            Count = g.Count(),

                            Works = from p in g select p

                        };

            foreach (var group in Group)

            {

                float ptns = 0;

                foreach (var gr in group.Works)

                {

                    ptns += Convert.ToInt32(gr.PatientsCount);

                }

                Console.WriteLine($"{group.Name} : {ptns} patients");

                Console.WriteLine();

            }

        }

    }

}
