namespace Task1_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Academy_Group group = new Academy_Group();

            group.Add(new Student("Alice", "Johnson", 21, "+380456789123", 90.1, 1));
            group.Add(new Student("Jane", "Smith", 22, "+380987654321", 78.3, 2));
            group.Add(new Student("John", "Doe", 20, "+380123456789", 85.5, 3));

            Console.WriteLine("Initial group:");
            group.Print();

            group.Remove("Smith");
            Console.WriteLine("\nGroup after removing 'Smith':");
            group.Print();

            Student updatedStudent = new Student ("John", "Smith", 21, "+380555666777", 88.2, 3 );
            group.Edit("Doe", updatedStudent);
            Console.WriteLine("\nGroup after editing 'Doe' to 'Black':");
            group.Print();

            group.Save("students.xml");
            group.Load("students.xml");

            Console.WriteLine("\nGroup after loading from file:");
            group.Print();

            Console.WriteLine("\nStudents in group 1:");
            List<Student> studentsInGroupA = group.Search(s => s.NumberOfGroup == 1);
            foreach (var student in studentsInGroupA)
            {
                student.Print();
            }

            Console.WriteLine("\nStudents who are 21 years old:");
            List<Student> studentsByAge = group.Search(s => s.Age == 21);
            foreach (var student in studentsByAge)
            {
                student.Print();
            }
        }
    }
}
