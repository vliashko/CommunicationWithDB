using BenchmarkDotNet.Attributes;
using CommunicationWithDB.Common;
using CommunicationWithDB.EF.Models;
using System.Data.SqlClient;

namespace CommunicationWithDB.Tests
{
    [MemoryDiagnoser]
    public class BenchmarkAdo
    {
        private SqlConnection _sqlConnection;
        private string _connString;

        [Params(20000)]
        public int EntityCount { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var dbConfiguration = new DbConfiguration();

            _connString = dbConfiguration.GetConnectionString("connString");
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _sqlConnection.Dispose();
        }

        [Benchmark]
        public List<Student> GetAll_ADO()
        {
            _sqlConnection = new SqlConnection(_connString);
            _sqlConnection.Open();

            var sqlCommandForRead = _sqlConnection.CreateCommand();
            sqlCommandForRead.CommandText = "SELECT Id, Name, Course, BirthDate FROM Student";
            SqlDataReader reader = sqlCommandForRead.ExecuteReader();

            var students = new List<Student>();

            while (reader.Read())
            {
                students.Add(
                    new Student()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Course = reader.GetInt32(2),
                        BirthDate = reader.GetDateTime(3)
                    });
            }

            reader.Close();
            _sqlConnection.Close();

            return students;
        }
    }
}
