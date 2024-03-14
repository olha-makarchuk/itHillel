using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Task1_2
{
    public class Academy_Group
    {

        private List<Student> students;
        private int count;
        private string fileName = "students.xml";

        public Academy_Group()
        {
            students = new List<Student>();
            count = 0;
        }

        public void Add(Student student)
        {
            students.Add(student);
            count++;
        }

        public void Remove(string name)
        {
            Student studentToRemove = students.FirstOrDefault(s => s.Surname == name);

            if(studentToRemove != null)
            {
                students.Remove(studentToRemove);
                count--;
            }
        }

        public void Edit(string surname, Student editStudent)
        {
            Student studentToEdit = students.FirstOrDefault(s=> s.Surname == surname);
       
            if(studentToEdit != null)
            {
                studentToEdit.Name = editStudent.Name;
                studentToEdit.Surname = editStudent.Surname;
                studentToEdit.Age = editStudent.Age;
                studentToEdit.Phone = editStudent.Phone;
                studentToEdit.Average = editStudent.Average;
                studentToEdit.NumberOfGroup = editStudent.NumberOfGroup;
            }
        }

        public void Print()
        {
            foreach (var student in students)
            {
                student.Print();
            }
        }
        public void Save()
        {
            XElement rootElement = new XElement("Students");

            foreach (var student in students)
            {
                XElement studentElement = new XElement("Student",
                    new XElement("Name", student.Name),
                    new XElement("Surname", student.Surname),
                    new XElement("Age", student.Age),
                    new XElement("Phone", student.Phone),
                    new XElement("Average", student.Average),
                    new XElement("NumberOfGroup", student.NumberOfGroup)
                );

                rootElement.Add(studentElement);
            }
            rootElement.Save(fileName);
        }

        public List<Student> Load()
        {
            List<Student> students = new List<Student>();

            XElement rootElement = XElement.Load(fileName);


            foreach (var studentElement in rootElement.Elements("Student"))
            {
                string name = studentElement.Element("Name").Value;
                string surname = studentElement.Element("Surname").Value;
                int age = int.Parse(studentElement.Element("Age").Value);
                string phone = studentElement.Element("Phone").Value;
                string a = studentElement.Element("Average").Value.ToString();
                double average = Convert.ToDouble(studentElement.Element("Average").Value, CultureInfo.InvariantCulture);
                int numberOfGroup = int.Parse(studentElement.Element("NumberOfGroup").Value);

                students.Add(new Student(name, surname, age, phone, average, numberOfGroup));
            }
            return students;
        }

        public List<Student> Search(Func<Student, bool> criteria)
        {
            return students.Where(criteria).ToList();
        }
    }
}
