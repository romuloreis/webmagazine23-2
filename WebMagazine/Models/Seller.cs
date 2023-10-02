namespace WebMagazine.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double Salary { get; set; }

        /* Define a relação com Department */
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        /* Define a relação com SalesRecord */
        ICollection<SalesRecord> Sales { get; set; } 
            = new List<SalesRecord>();
    }
}
