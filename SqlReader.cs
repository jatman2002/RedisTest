using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisTest
{
    internal class SqlReader
    {

        public static SqlReader sqlReader = new SqlReader();

        private string CONNECTION_STRING =
            "Data Source=JATCOMPUTER;" +
            "Initial Catalog=redis_test;" +
            "User ID=sa;" +
            "Password=password;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True;";

        private SqlReader()
        {
        }

        public List<Employee> readFromDB()
        {

            List<Employee> employeeList = new List<Employee>();
            
            using(SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("✅ Connection successful!");

                    string query = "SELECT * FROM Employees";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("\nEmployees:");
                        Console.WriteLine("-----------");

                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string position = reader.GetString(2);
                            int age = reader.GetInt32(3);

                            Employee employee = new Employee() { Id=id, Name=name, Age=age, Position=position};
                            employeeList.Add(employee);

                            Console.WriteLine($"{id}. {name} - {position} - {age}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }

            return employeeList;
        }
    }
}
