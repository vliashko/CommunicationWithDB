using BenchmarkDotNet.Attributes;
using CommunicationWithDB.EF;
using CommunicationWithDB.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunicationWithDB.Tests
{
    [MemoryDiagnoser]
    public class BenchmarkEF
    {
        private UniversityContext _context;

        [Params(20000)]
        public int EntityCount { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _context.Dispose();
        }

        [Benchmark]
        public List<Student> GetAll_EF_WithTracking()
        {
            _context = new UniversityContext();

            var students = _context.Student.ToList();

            return students;
        }

        [Benchmark]
        public List<Student> GetAll_EF_WithNoTracking()
        {
            _context = new UniversityContext();

            var students = _context.Student.AsNoTracking().ToList();

            return students;
        }
    }
}
