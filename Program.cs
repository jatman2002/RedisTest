using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace RedisTest
{
    class Program
    {
        public static Task Main(string[] args)
        {
            bool updateOptionToggle = false;

            int choice = 0;
            while (choice != 5)
            {

                Console.WriteLine("1: Read from Redis");
                Console.WriteLine("2: Write to Redis");
                Console.WriteLine("3: Delete from Redis");
                Console.WriteLine("4: Update Redis entry");
                Console.WriteLine("5: Quit");

                Console.WriteLine("Enter a function: ");
                choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1)
                {
                    String employeeListRaw = RedisConnector.redis.readFromRedis("employees");
                    List<Employee> employeeList = employeeListRaw == null ? new List<Employee>() : JsonConvert.DeserializeObject<List<Employee>>(employeeListRaw);

                    foreach (Employee employee in employeeList)
                    {
                        Console.WriteLine(employee.ToString());
                    }
                }
                else if (choice == 2)
                {
                    List<Employee> employeeList = SqlReader.sqlReader.readFromDB();
                    String raw = JsonConvert.SerializeObject(employeeList);
                    RedisConnector.redis.writeToRedis("employees", raw);
                }
                else if (choice == 3)
                {
                    RedisConnector.redis.deleteFromRedis("employees");
                }
                else if (choice == 4)
                {
                    String test1 = "[{\"Id\":1,\"Name\":\"Alice Johnson\",\"Age\":27,\"Position\":\"Manager\"}]";

                    String test2 = "[{\"Id\":1,\"Name\":\"Alice Johnson\",\"Age\":27,\"Position\":\"Manager\"},{\"Id\":2,\"Name\":\"Bob Smith\",\"Age\":21,\"Position\":\"Developer\"},{\"Id\":3,\"Name\":\"John Smith\",\"Age\":55,\"Position\":\"Senior Developer\"}]";


                    RedisConnector.redis.writeToRedis("employees", updateOptionToggle ? test1 : test2);

                    updateOptionToggle = !updateOptionToggle;
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
            return Task.CompletedTask;
        }
    }
}
