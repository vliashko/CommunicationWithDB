using System.ComponentModel.DataAnnotations;

namespace CommunicationWithDB.EF.Models
{
    public class Student
    {
        public int Id { get; set; }

        public int Course { get; set; }

        [StringLength(90)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
