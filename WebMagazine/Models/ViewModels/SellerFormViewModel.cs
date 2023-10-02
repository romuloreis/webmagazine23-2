namespace WebMagazine.Models.ViewModels
{
    /*Model especial para criar 
     a view do formulário de vendedor*/
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public List<Department> Departments { get; set; }
    }
}
