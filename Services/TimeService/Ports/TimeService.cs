using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PetProjectC_NeuroWeb.Services.ParserService.Interface;
using PetProjectC_NeuroWeb.Services.TimeService;

namespace PetProjectC_NeuroWeb.Services.TimeService.Ports
{
    public class TimeService  
    {   
        readonly IEnumerable<IParserService> _parserServices;
        public TimeService(IEnumerable<IParserService> parserServices)
        {
           foreach (var parserService in parserServices)
           {
                Timer categoriesTimer = new Timer(new TimerCallback(parserService.AddCategoryInDB), null, 0, 60480000);
                Timer productsTimer = new Timer(new TimerCallback(parserService.AddProductsInDB), null, 0, 60480000);
           }
        }     
        
    }
}
