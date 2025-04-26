using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PetProjectC_NeuroWeb.Services.ParserService.Interface;
using PetProjectC_NeuroWeb.Services.TimeService;
using System.Data;

namespace PetProjectC_NeuroWeb.Services.TimeService.Ports
{
    public class TimeService  
    {   
        readonly List<Timer> _timers = new List<Timer>();
        readonly IEnumerable<IParserService> _parserServices;
        public TimeService(IEnumerable<IParserService> parserServices)
        {
            _parserServices = parserServices;
           foreach (var parserService in _parserServices)
           {
                //Timer categoriesTimer = new Timer(new TimerCallback(parserService.AddCategoryInDB), null, 0, 60480000);
                //Timer productsTimer = new Timer(new TimerCallback(parserService.AddProductsInDB), null, 0, 60480000);

                _timers.Add(new Timer(new TimerCallback(parserService.AddCategoryInDB), null, 0, 60480000));
                _timers.Add(new Timer(new TimerCallback(parserService.AddProductsInDB), null, 0, 60480000));
           }
        }     
        
        public void Dispose() //очистка в 
        {

        }
    }
}
