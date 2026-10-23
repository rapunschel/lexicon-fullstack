using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using Microsoft.VisualBasic;
namespace LinqOvningar
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Class { get; set; } = "";
        public int Points { get; set; }

        public override string ToString()
        {
            return $"{Name} {Age} år. Klass: {Class}. Poäng: {Points}";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // Exempeldata
            List<Student> students = new List<Student>
            {
                new Student { Id = 1, Name = "Anna", Age = 19, Class = "A", Points = 85 },
                new Student { Id = 2, Name = "Bertil", Age = 22, Class = "B", Points = 72 },
                new Student { Id = 3, Name = "Cecilia", Age = 20, Class = "A", Points = 91 },
                new Student { Id = 4, Name = "David", Age = 24, Class = "C", Points = 64 },
                new Student { Id = 5, Name = "Eva", Age = 19, Class = "B", Points = 85 },
                new Student { Id = 6, Name = "Felix", Age = 21, Class = "A", Points = 77 },
                new Student { Id = 7, Name = "Gustav", Age = 23, Class = "C", Points = 58 },
                new Student { Id = 8, Name = "Hanna", Age = 20, Class = "B", Points = 95 }
            };

            Console.WriteLine("LINQ-ÖVNINGAR MED STUDENTER\n");

            System.Console.WriteLine("Where");
            PrintTable([.. students.Where(s => s.Age > 20)], "Hämta alla studenter som är äldre än 20.");
            PrintTable([.. students.Where(s => s.Points > 80)], "Hämta alla studenter som har mer än 80 poäng");


            System.Console.WriteLine(new string('-', 71));

            System.Console.WriteLine("Select");
            PrintNames(students.Select(s => s.Name), "Visa bara namnen på alla studenter.");

            System.Console.WriteLine("Visa namn och poäng i en ny anonym typ.");
            var result = students.Select(s => new { s.Name, s.Points });
            foreach (var item in result)
            {
                Console.WriteLine(item.ToString());
            }
            System.Console.WriteLine(new string('-', 71));
            System.Console.WriteLine("OrderBy");
            PrintTable(students.OrderByDescending(s => s.Age).ToList(), "Sortera studenter efter ålder.");
            PrintTable(students.OrderByDescending(s => s.Points).ToList(), "Sortera studenter efter poäng, högst först");


            System.Console.WriteLine(new string('-', 71));
            System.Console.WriteLine("Count");
            System.Console.WriteLine($"Räkna hur många studenter som finns. {students.Count()}");
            System.Console.WriteLine($"Räkna hur många som går i klass A {students.Count(s => s.Class == "A")}");


            System.Console.WriteLine(new string('-', 71));
            System.Console.WriteLine("Take");
            PrintTable(students.Take(3).ToList(), "Ta ut de tre första studenterna.");
            PrintTable(students.OrderByDescending(s => s.Points).Take(2).ToList(), "Ta ut de två bästa poängen efter sortering.");

            System.Console.WriteLine(new string('-', 71));
            System.Console.WriteLine("Distinct");
            System.Console.WriteLine("Visa alla unika klassnamn.");
            System.Console.WriteLine(string.Join(", ", students.Select(s => s.Class).Distinct()));
            System.Console.WriteLine("Visa alla unika åldrar.");
            System.Console.WriteLine(string.Join(", ", students.Select(s => s.Age).Distinct()));

            System.Console.WriteLine(new string('-', 71));
            System.Console.WriteLine("Any / All");
            System.Console.WriteLine($"Finns det någon student med 100 poäng? {students.Any(s => s.Points == 100)}");
            System.Console.WriteLine($"Har alla studenter minst 50 poäng? {students.All(s => s.Points >= 50)}");

            System.Console.WriteLine(new string('-', 71));
            System.Console.WriteLine("FirstOrDefault / Last / Single");
            System.Console.WriteLine($"Hämta första studenten i klass B. {students.FirstOrDefault(s => s.Class == "B")}");
            System.Console.WriteLine($"Hämta sista studenten i listan. {students.LastOrDefault()}");
            System.Console.WriteLine($"Hämta studenten med ett visst Id (3). {students.Single(s => s.Id == 3)}");

            System.Console.WriteLine(new string('-', 71));
            System.Console.WriteLine("GroupBy");
            System.Console.WriteLine("Gruppera studenter per klass & Skriv ut varje klass och namnen i gruppen..");
            var byClass = students.GroupBy(s => s.Class);
            foreach (var c in byClass)
            {
                PrintTable(c, $"Class {c.Key}");
            }



            System.Console.WriteLine(new string('-', 71));
            System.Console.WriteLine("Sum / Average / Min / Max");
            System.Console.WriteLine($"Sum: {students.Sum(s => s.Points)}");
            System.Console.WriteLine($"Average: {students.Average(s => s.Points)}");
            System.Console.WriteLine($"Lägsta: {students.Min(s => s.Points)} | Högsta: {students.Max(s => s.Points)}");



        }

        static void PrintNames(IEnumerable<string> names, string task)
        {
            const int offSet = -10;
            StringBuilder sb = new();
            sb.Append(task + "\n");
            sb.Append(new string('=', 10));
            sb.Append("\n");
            foreach (String n in names)
            {
                sb.Append($"{n,offSet}\n");
            }
            System.Console.WriteLine(sb.ToString());
        }

        static void PrintTable(IEnumerable<Student> students, string task)
        {
            const int offSet = -10;
            StringBuilder sb = new();
            sb.Append(task + "\n");
            sb.Append($"{"Name",-10} {"Age",-10} {"Class",-10} {"Points",-10}\n");
            sb.Append(new string('=', 40));
            sb.Append("\n");
            foreach (Student s in students)
            {
                sb.Append($"{s.Name,offSet} {s.Age,offSet} {s.Class,offSet} {s.Points}\n");
            }
            System.Console.WriteLine(sb.ToString());
        }
    }


}