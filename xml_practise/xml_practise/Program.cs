using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace xml_practise
{
    class Program
    {
        static void Process<T>(IEnumerable<T> input)
        {
            Console.WriteLine("\n\t-----------------------\n");
            foreach (var item in input)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
        static void Process(XDocument doc)
        {
            var people = from x in doc.Root.Descendants("person") select x;

            foreach (var item in people)
            {
                Console.WriteLine(item);
            }
        }
        static XDocument LoadXML()
        {
            return XDocument.Load("https://users.nik.uni-obuda.hu/siposm/db/workers.xml");
        }
        static void Main(string[] args)
        {
            XDocument xdoc = LoadXML();
            //Process(xdoc);

            //0. feladat
            //írjuk ki minden ember nevét
            var taks0 = from x in xdoc.Root.Descendants("person")
                        select x.Element("name").Value;
            Process(taks0);

            //1. feladat
            //Kérdezzük le csak a Tamásokat
            var task1 = from x in xdoc.Root.Descendants("person")
                        where x.Element("name").Value.ToUpper().Contains("Tamás")
                        select x.Element("name").Value;
            Process(task1);

            //2. feladat
            //kérjük le a polihisztorokat (email és név)
            var task2 = from x in xdoc.Root.Descendants("person")
                        where x.Element("rank").Value.Contains("polihisztor")
                        select new
                        {
                            _EMAIL = x.Element("email").Value,
                            _NAME = x.Element("name").Value
                        };
            Process(task2);
            //3. feladat
            //számoljuk meg, hányan dolgoznak egyes intézetekben

            var task3 = from x in xdoc.Root.Descendants("person")
                        group x by x.Element("dept").Value into g
                        select new
                        {
                            Letszam = g.Count(),
                            Intezet = g.Key
                        };
            Process(task3);
            /*
            //4. feladat
            //számoljuk meg (egy teljsesen új lekérdezéssel) hogy csak az AII-ben hányan

            var task4 = from x in xdoc.Root.Descendants("person")
                        where x.Element("debt").Value.Equals("Alkalmazott Informatikai Intézet")
                        group x by x.Element("debt").Value into g
                        select new
                        {
                            Letszam = g.Count(),
                            Intezet = g.Key
                        };
            Process(task4);
            */

            //5. feladat
            //új alkalmazott hozzáadása
            XElement uj = new XElement("person",
                new XAttribute("STATUS", "---VIP----"),
                new XElement("name", "Ultron"),
                new XElement("email", "Ultron"),
                new XElement("dept", "Ultron"),
                new XElement("rank", "Ultron"),
                new XElement("phone", "Ultron"),
                new XElement("room", "Ultron"));

            xdoc.Root.Add(uj);
            xdoc.Save("output.xml");
                        
        }
    }
}
