using System.ComponentModel.DataAnnotations;

namespace CommunicationWithDB.EF.Models
{
    public class Department
    {
        public int Id { get; set; }

        [StringLength(90)]
        public string Name { get; set; }
    }
}
