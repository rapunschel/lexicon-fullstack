using System;
using System.Collections.Generic;
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
            PrintTable([.. students.Where(s => s.Age >= 20)], "Hämta alla studenter som är äldre än 20.");
            PrintTable([.. students.Where(s => s.Points >= 80)], "Hämta alla studenter som har mer än 80 poäng");

            System.Console.WriteLine("Select");
            PrintNames(students.Select(s => s.Name), "Visa bara namnen på alla studenter.");

            System.Console.WriteLine("Visa namn och poäng i en ny anonym typ.");
            var result = students.Select(s => new { s.Name, s.Points });
            foreach (var item in result)
            {
                Console.WriteLine(item.ToString());
            }
            System.Console.WriteLine();
            System.Console.WriteLine("OrderBy");
            PrintTable(students.OrderByDescending(s => s.Age).ToList(), "Sortera studenter efter ålder.");
            PrintTable(students.OrderByDescending(s => s.Points).ToList(), "Sortera studenter efter poäng, högst först");



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

        static void PrintTable(List<Student> students, string task)
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