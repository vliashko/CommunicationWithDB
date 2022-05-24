namespace CommunicationWithDB.EF.Models
{
    public class Coursework
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int DepartmentId { get; set; }

        public DateTime DeliveryDate { get; set; }
    }
}
