using BenchmarkDotNet.Attributes;
using CommunicationWithDB.Common;
using CommunicationWithDB.Dapper;
using Dapper;
using System.Data.SqlClient;

namespace CommunicationWithDB.Tests
{
    [MemoryDiagnoser]
    public class BenchmarkDapper
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
        public List<Student> GetAll_Dapper()
        {
            _sqlConnection = new SqlConnection(_connString);

            var students = _sqlConnection.Query<Student>("SELECT Id, Name, Course, BirthDate FROM Student").ToList();

            return students;
        }
    }
}
