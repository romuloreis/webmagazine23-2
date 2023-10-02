namespace WebMagazine.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; } 

        /*Define uma relação com vendedores (Seller)*/
        ICollection <Seller> Sellers { get; set; } =
            new List<Seller>();
    }
}
