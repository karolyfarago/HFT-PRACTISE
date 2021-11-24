using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_xml_practise
{
    
    class Program
    {
        static void Process<T>(IEnumerable<T> input)
        {
            Console.WriteLine("\n------------------\n\n");
            foreach (var item in input)
            {
                Console.WriteLine(item);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("---LINQ---");

            #region INTRODUCTION TO LINQ
            List<int> list = new List<int>();
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                list.Add(r.Next(99));
            }
            IEnumerable<int> filteredList = list.FindAll(x=>x %2 ==0);
            int maxItem = filteredList.Max(x=>x);

            
            //-------------

            var stud1 = new Student() { Name = "Test person1" };
            //stud1.Name;
            var atud2 = new
            {
                Name = "XYZ person",
                Nationality = "Hungarian",
                Age = 20
            };

            //--------------

            //query syntax
            var evens = from x in list
                        where x%2 ==0
                        select x;

            Process(evens);

            //method syntax
            var evens2=list.Where(x => x % 2 == 0);
            Process(evens2);

            #endregion

            //###################################
            #region GENERATE DATA
            var OrderedNumbers = list.OrderBy(X => X);
            Process(OrderedNumbers);

            List<Student> students = new List<Student>();
            students.Add(new Student() { Name = "ASD1" });
            students.Add(new Student() { Name = "ASD2" });
            students.Add(new Student() { Name = "ASD3" });
            students.Add(new Student() { Name = "Klaudia" });
            students.Add(new Student() { Name = "Kettyós Klaudia" });

            var orederStudents = students.OrderBy(x => x.Name);
            Process(orederStudents);

            var nameIs =students.Where(x => x.Name.ToUpper().Contains("3"));
            Process(nameIs);


            #endregion

            #region TASK1
            //1. feladat
            //    adott egy adatbázis Éist-ként, kérdezzük le a Klaudiák számát
            //    ekkora mérettel hozzunk létre tömböt és másoljuk át a Klaudiákat
            //    Kis és nagybetűre figyelve
            int count = students.Count(x => x.Name.ToUpper().Contains("Klaudia"));
            var klaudiak = students.Where(x => x.Name.ToUpper().Contains("Klaudia"));
            Student[] selectedOnes = new Student[count];
            int index = 0;
            foreach (var item in klaudiak)
            {
                selectedOnes[index++] = item;
            }
            Process(selectedOnes);
            #endregion
            //################################################
            #region TASK2
            //2. feladat
            //    hallgatók 20-50 között és nincsenek párkapcsolatban
            //    (ehhez hallgató osztály kiegészítése életkor + párkapcsolati státusz)

            Predicate<int> stausRandomizer = x => { return x == 0; };
            for (int i = 0; i < students.Count; i++)
            {
                students[i].Status = (bool)stausRandomizer?.Invoke(r.Next(2));
                students[i].Age = r.Next(10, 60);
            }
            Process(students);

            var selectedStuds = students.Where(x =>
             {
                 return x.Status == false
                        && (x.Age > 19 && x.Age < 51);
             });
            Process(selectedStuds);

            #endregion
            //#######################################################
            #region TASK3
            //3. feladat
            //    kérjük le azokat akik kapcsolatban vannak, a kapott eredményt rendezzük sorrendbe név alapján
            //    és alakítsuk nagybetűssé a neveket
            var taken = students
                .Where(x => x.Status)
                .OrderBy(x => x.Name)
                .Select(x => x.Name.ToUpper());
            Process(taken);

            #endregion
            //########################################################
            #region TASK4
            //4. feladat
            //    kérjük le a kapcsolatban / nem kapcsolatban léávő hallgatókat
            //    csoportosítva és számoljuk meg, hogy hányan vannak ezekben a csoportokban
            var group1 = students.GroupBy(x => x.Status);

            var group2 = from x in students
                         group x by x.Status into xResult
                         select new
                         {
                             _GROUP = xResult.Key,
                             _COUNT = xResult.Count()
                         }; //alulvonás direkt van használva!!!!
            foreach (var item in group1)
            {
                Console.WriteLine("Csoport: {0} <> darabszám : {1}", item.Key, item.Count());
            }

            foreach (var item in group2)
            {
                Console.WriteLine("Csoport: {0} <> darabszám : {1}", item._GROUP, item._COUNT);
            }

            #endregion
            //####################################################
            #region TASK5
            //5. feladat
            //    hallgatók akik nevében benne van az "e" vagy "E"betű

            //    alakítsuk a nevét nagybetűssé egy új objektum keretein belül
            //    tároljuk el mellé még az életkorát is (más-más tulajdonságokban)

            //    rendezzük életkor szerint
            var e_students = from x in students
                             where x.Name.Contains('e') || x.Name.Contains('E')
                             orderby x.Age
                             select new
                             {
                                 _NAME = x.Name.ToUpper(),
                                 _AGE = x.Age,
                                 _STATUS = x.Status
                             };
            Process(e_students);

            //feladat 5.1
            //    végezzük el ugyanazt a lekérdezést, de csoportosítsunk 
            //    kapcsolatban lévő stázusz szerint
            //    és egyes csoportokat nézzük meg, hogy mennyi az átlagos életkor

            var e_sudents2 = from x in e_students
                            group x by x._STATUS into g
                            select new
                            {
                                _AVG = g.Average(a => a._AGE),
                                _COUNT = g.Count(),
                                _GROUP = g.Key
                            };
            Process(e_sudents2);           


            #endregion




        }
    }
}
