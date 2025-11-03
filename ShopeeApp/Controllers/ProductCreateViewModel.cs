
namespace ShopeeApp.Controllers
{
    public class ProductCreateViewModel
    {
        internal decimal Price;

        public IEnumerable<object> Images { get; internal set; }
        public string Description { get; internal set; }
        public int CategoryId { get; internal set; }
        public string Name { get; internal set; }
    }
}