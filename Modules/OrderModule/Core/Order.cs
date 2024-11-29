using Microsoft.IdentityModel.Tokens;

namespace PetProjectC_NeuroWeb.Modules.CartModule.Core
{
    public class Order
    {
        public Order(List<string> productsList)
        {
            _productList = productsList;
        }

        private List<string> _productList = new List<string>();

        
    }
}
