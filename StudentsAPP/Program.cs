using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace StudentsAPP
{
    class Program
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["StudentDB"].ConnectionString;

        private static SqlConnection sqlConnection = null;

        static void Main(string[] args)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            Console.WriteLine("StudentsApp");

            SqlDataReader sqlDataReader = null;

            string command = string.Empty;

            while(true)
            {
                try
                {

                Console.Write("> ");
                command = Console.ReadLine();
                #region Exit
                if (command.ToLower().Equals("exit"))
                {
                    if(sqlConnection.State == ConnectionState.Open)
                    {
                        sqlConnection.Close();
                    }
                    if(sqlDataReader != null)
                    {
                        sqlDataReader.Close();
                    }
                    break;
                }
                #endregion

                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

                // SELECT * FROM [Students] WHERE Id=1

                switch(command.Split(' ')[0].ToLower())
                {
                    case "select":
                        sqlDataReader = sqlCommand.ExecuteReader();
                        while(sqlDataReader.Read())
                        {
                            Console.WriteLine($"{sqlDataReader["Id"]} {sqlDataReader["FullName"]}" +
                                $"{sqlDataReader["Birthday"]} {sqlDataReader["University"]} {sqlDataReader["Group_Num"]}" +
                                $"{sqlDataReader["Course"]} {sqlDataReader["Avarge_score"]}");

                            Console.WriteLine(new string('-', 30));
                        }
                        if (sqlDataReader != null)
                        {
                            sqlDataReader.Close();
                        }
                        break;
                    case "insert":
                        Console.WriteLine($"Added: {sqlCommand.ExecuteNonQuery()} string(s)");
                        break;
                    case "update":
                        Console.WriteLine($"Changet: {sqlCommand.ExecuteNonQuery()} string(s)");
                        break;
                    case "delete":
                        Console.WriteLine($"Deleted: {sqlCommand.ExecuteNonQuery()} string(s)");
                        break;
                    default:
                        Console.WriteLine($"Commant {command} isn`t correct!");
                        break;
                }

                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Mistake {ex.Message}");
                }
            }

            Console.WriteLine("Pres any button to continue");
            Console.ReadLine();
        }
    }
}
