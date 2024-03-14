using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_2
{
    public class Student : Person
    {
        protected double average;
        protected int numberOfGroup;

        public double Average{ get { return average; } set { average = value; } }
        public int NumberOfGroup { get { return numberOfGroup; } set { numberOfGroup = value; } }
        public Student(string name, string surname, int age, string phone, double average, int numberOfGroup) 
            : base(name, surname, age, phone)
        {
            this.average = average;
            this.numberOfGroup = numberOfGroup;
        }
        public void Print()
        {
            Console.WriteLine($"Name: {Name}, Surname: {Surname}, Age: {Age}, Phone: {Phone}, Average: {Average}, Number of group: {NumberOfGroup}");
        }
    }
}
