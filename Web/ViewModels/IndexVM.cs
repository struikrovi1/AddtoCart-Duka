using Entities;

namespace Web.ViewModels
{
    public class IndexVM
    {

        public List<Product>? FeaturedProducts { get; set; }
        public List<Product>? SliderProducts { get; set; }

        public List<Product>? SaleProducts { get; set; }
    
}
}
