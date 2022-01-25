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

                if(command.ToLower().Equals("clear"))
                    {
                        Console.Clear();
                        continue;
                    }
                SqlCommand sqlCommand = null;

                    // SELECT * FROM [Students] WHERE Id=1
                    string[] commandArray = command.ToLower().Split(' ');
                
                switch(commandArray[0])
                {
                    case "select":
                        sqlCommand = new SqlCommand(command, sqlConnection);
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
                        sqlCommand = new SqlCommand(command, sqlConnection);
                        Console.WriteLine($"Added: {sqlCommand.ExecuteNonQuery()} string(s)");
                        break;
                    case "update":
                        sqlCommand = new SqlCommand(command, sqlConnection);
                        Console.WriteLine($"Changet: {sqlCommand.ExecuteNonQuery()} string(s)");
                        break;
                    case "delete":
                        sqlCommand = new SqlCommand(command, sqlConnection);
                        Console.WriteLine($"Deleted: {sqlCommand.ExecuteNonQuery()} string(s)");
                        break;
                        case "sortby":
                            sqlCommand = new SqlCommand($"SELECT * FROM [Table] ORDER BY {commandArray[1]} {commandArray[2]}", sqlConnection);
                            sqlDataReader = sqlCommand.ExecuteReader();
                            while (sqlDataReader.Read())
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
