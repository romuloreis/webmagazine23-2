using System.ComponentModel.DataAnnotations;
using WebMagazine.Controllers;

namespace WebMagazine.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage="Insira um nome maior")]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name="E-mail")]
        public string Email { get; set; }
        [Required]
        [Display(Name="Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        [DataType(DataType.Currency)]
        public double Salary { get; set; }

        /* Define a relação com Department */
        [Display(Name="Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        /* Define a relação com SalesRecord */
        public ICollection<SalesRecord> Sales { get; set; }
            = new List<SalesRecord>();


        public double TotalSales(DateTime initial,
            DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial 
            && sr.Date <= final).Sum(sr => sr.Price);
        }
    }
}
