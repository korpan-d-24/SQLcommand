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


            }

            Console.WriteLine("Pres any button to continue");
            Console.ReadLine();
        }
    }
}
