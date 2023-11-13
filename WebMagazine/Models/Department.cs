using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebMagazine.Models
{
    public class Department
    {

        public int Id { get; set; }
        [Display(Name="Department Name")]
        [Required]
        public string Name { get; set; }
        public string Abbreviation { get; set; } 

        /*Define uma relação com vendedores (Seller)*/
        ICollection <Seller> Sellers { get; set; } =
            new List<Seller>();

        public double TotalSales(DateTime initial,
            DateTime final){
            return Sellers.Sum(seller => 
                seller.TotalSales(initial, final));
        }


    }
}
