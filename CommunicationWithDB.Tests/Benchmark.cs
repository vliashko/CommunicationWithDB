using BenchmarkDotNet.Attributes;
using CommunicationWithDB.Common;
using CommunicationWithDB.EF;
using CommunicationWithDB.EF.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace CommunicationWithDB.Tests
{
    [MemoryDiagnoser]
    public class Benchmark
    {
        private UniversityContext _context;
        private SqlConnection _connectionDapper;
        private SqlConnection _connectionAdo;

        [Params(100, 5000, 20000)]
        public int EntityCount { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var dbConfiguration = new DbConfiguration();

            _context = new UniversityContext();
            _connectionDapper = new SqlConnection(dbConfiguration.GetConnectionString("connString"));
            _connectionAdo = new SqlConnection(dbConfiguration.GetConnectionString("connString"));
            _connectionAdo.Open();

            var list = new List<Student>();

            for (int i = 0; i < EntityCount; i++)
            {
                list.Add(
                    new Student() 
                    { 
                        Name = $"TestStudent {i}", 
                        Course = i, 
                        BirthDate = new DateTime(2022, 1, 1) 
                    });
            }

            _context.Student.AddRange(list);
            _context.SaveChanges();
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _connectionAdo.Execute("TRUNCATE TABLE Student");
            _context.Dispose();
            _connectionAdo.Close();
            _connectionAdo.Dispose();
            _connectionDapper.Dispose();
        }

        [Benchmark]
        public List<Student> GetAll_EF_WithTracking()
        {
            var students = _context.Student.ToList();

            return students;
        }

        [Benchmark]
        public List<Student> GetAll_EF_WithNoTracking()
        {
            var students = _context.Student.AsNoTracking().ToList();

            return students;
        }

        [Benchmark]
        public List<Student> GetAll_Dapper()
        {
            var students = _connectionDapper.Query<Student>("SELECT * FROM Student").ToList();

            return students;
        }

        [Benchmark]
        public List<Student> GetAll_ADO()
        {
            var sqlCommandForRead = _connectionAdo.CreateCommand();
            sqlCommandForRead.CommandText = "SELECT * FROM Student";
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

            return students;
        }
    }
}
