namespace WebMagazine.Models.ViewModels
{
    /*Model especial para criar 
     a view do formulário de vendedor*/
    public class SalesRecordFormViewModel
    {
        public SalesRecord SalesRecord { get; set; }
        public List<Seller> Sellers { get; set; }
    }
}
