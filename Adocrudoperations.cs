using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Adocrud
{
  
    public class Adocrudoperations()
    {
        static string connectionString = "server=(localdb)\\MSSQLLOCALDB; Initial Catalog = practies_db; Integrated Security = SSPI";
        static SqlConnection con = new SqlConnection(connectionString);
        public void create()
        {
            string Cretatable = "Create table Employee1(EmployeeId int primary key identity(1,1),Name varchar(50),Address varchar(20),Salary decimal(10,2),JoinDate Date,City varchar(20))";
            SqlCommand cmd = new SqlCommand(Cretatable, con);
            con.Open();
            cmd.ExecuteNonQuery();
            Console.WriteLine("successfull table created");
            con.Close();

        }

        public void insert()
        {
            string insert = "insert into Employee1 values('hari','hydrabad',40000,'2003-03-22','shivnagar')";

            SqlCommand cmd = new SqlCommand(insert, con);
            con.Open();
            cmd.ExecuteNonQuery();
            Console.WriteLine("successfull inserted values");
            con.Close();
        }

        public void InsertMultiple(List<Employee> employees)
        {
            con.Open();
            foreach (var emp in employees)
            {
                string insertQuery = "INSERT INTO Employee1 (Name, Address, Salary, JoinDate, City) VALUES (@Name, @Address, @Salary, @JoinDate, @City)";
                SqlCommand cmd = new SqlCommand(insertQuery, con);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Address", emp.Address);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@JoinDate", emp.JoinDate);
                cmd.Parameters.AddWithValue("@City", emp.City);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Successfully inserted values for " + emp.Name);
            }
            con.Close();
        }
        public void joindate()
        {
            string empnames = "select * from Employee1 where JoinDate between '2003-03-22' and '2005-07-22'";
            SqlCommand cmd = new SqlCommand(empnames, con);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"Id: {reader["EmployeeId"]}, Name: {reader["Name"]}, Address: {reader["Address"]}, Salary: {reader["Salary"]}, Joindate: {reader["JoinDate"]}, City: {reader["City"]}");
            }

            con.Close(); 
        }
        public void view()
        {
            string display = "select * from Employee1";
            SqlCommand cmd = new SqlCommand(display, con);
            con.Open();
            SqlDataReader reader=cmd.ExecuteReader();   
         
            while (reader.Read())
            {
                Console.WriteLine($" Id : {reader["EmployeeId"]},Name :{reader["Name"]},Address : {reader["Address"]},Salary : {reader["Salary"]},Joindate: {reader["JoinDate"]},City : {reader["City"]}");
            }
            con.Close();
        }

        public  void Update(string name, int Id)
        {
            string updateData = "update Employee1 set @Name = Name where @Id =  EmployeeId";
            SqlCommand sqlCommand = new(updateData,con);
            con.Open();
            sqlCommand.Parameters.AddWithValue("@Name", name);
            sqlCommand.Parameters.AddWithValue("@Id", Id);
            sqlCommand.ExecuteNonQuery();
            Console.WriteLine("Name update to " + name);
            con.Close();
        }

        public void Delete(int id)
        {
            string deleteData = "delete Employee1 where EmployeeId = @Id";
            SqlCommand sqlCommand = new(deleteData, con);
            con.Open();
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.ExecuteNonQuery();
            Console.WriteLine("Deleted!");
            con.Close();
        }
        static void Main(string[] args)
        {
            Adocrudoperations method= new Adocrudoperations();

            List<Employee> employees = new List<Employee>
            {
                new Employee { Name = "neha", Address = "chennai", Salary = 360000, JoinDate = DateTime.Parse("2004-03-22"), City = "hsr" },
        
            };
            Console.WriteLine("Enter your choice");
            Console.WriteLine("1.Create");
            Console.WriteLine("2.Read");
            Console.WriteLine("3.Update");
            Console.WriteLine("4.Delete");
            int choice = int.Parse(Console.ReadLine());

            switch(choice)
            {
                case 1: method.create();
                        method.insert();
                        method.InsertMultiple(employees);
                    break;
                case 2:Console.WriteLine("view the table");
                    method.view();
                    Console.WriteLine();
                    Console.WriteLine("Employee who are join date between 2005 to 2006");
                    method.joindate();
                    break;
                case 3:Console.WriteLine("Update employee name");
                    Console.WriteLine("Enther the name and id of employee to update");
                    string name= Console.ReadLine();
                    int id = int.Parse(Console.ReadLine());
                    method.Update(name, id);
                    break;

               case 4:Console.WriteLine("Enter the id of employee to delete");
                    int ide= int.Parse(Console.ReadLine());  
                    method.Delete(ide);
                    break;

            }
        }
    }
}
