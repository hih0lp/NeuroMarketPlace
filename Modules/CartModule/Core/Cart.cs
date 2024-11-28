using Microsoft.IdentityModel.Tokens;

namespace PetProjectC_NeuroWeb.Modules.CartModule.Core
{
    public class Cart
    {
        public Cart(List<string> productsList)
        {
            _productList = productsList;
        }

        private List<string> _productList = new List<string>();

        
    }
}
