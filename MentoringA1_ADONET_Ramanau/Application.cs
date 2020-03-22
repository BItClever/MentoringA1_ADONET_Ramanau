using System;
using System.Data.SqlClient;

namespace MentoringA1_ADONET_Ramanau
{
    public class Application
    {
        public void Start()
        {
            var connectionString = "Data Source=EPBYMINW9787;Initial Catalog=Northwind;Integrated Security=True";

            using (var connection = new SqlConnection(connectionString))
            {
                var insertQuery = "INSERT INTO tblUsers VALUES('Ivan2', 66, 'Gomel')";
                var updateQuery = "UPDATE tblUsers2 SET UserName = 'Anya' WHERE Id = 5";
                var deleteQuery = "DELETE FROM tblUsers WHERE Id = 5";

                connection.Open();
                var tran = connection.BeginTransaction();
                try
                {
                    var insertCommand = connection.CreateCommand();
                    insertCommand.CommandText = insertQuery;
                    insertCommand.Transaction = tran;

                    var updateCommand = connection.CreateCommand();
                    updateCommand.CommandText = updateQuery;
                    updateCommand.Transaction = tran;

                    var deleteCommand = connection.CreateCommand();
                    deleteCommand.CommandText = deleteQuery;
                    deleteCommand.Transaction = tran;

                    insertCommand.ExecuteNonQuery();
                    updateCommand.ExecuteNonQuery();
                    deleteCommand.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                }
            }
        }
    }
}
