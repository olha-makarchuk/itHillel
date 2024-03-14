using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task1_2
{
    public class Person
    {
        protected string name;
        protected string surname;
        protected int age;
        protected string phone;

        public string Name{get { return name; } set { name = value; }}
        public string Surname {get { return surname; }set { surname = value; }}
        public int Age {get { return age; }set { age = value; }}
        public string Phone{get { return phone; }set { phone = value; }}

        public Person(string name, string surname, int age, string phone)
        {
            this.name = name;
            this.surname = surname;
            this.age = age;
            this.phone = phone;
        }

        public void Print()
        {
            Console.WriteLine($"Name: {Name}, Surname: {Surname}, Age: {Age}, Phone: {Phone}");
        }
    }
}
