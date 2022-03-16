using Entities;

namespace Web.ViewModels
{
    public class CartVM
    {
        public  List<Product> CartItems { get; set; }   

        public List<int> ProIds { get; set; }
    }
}
