using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PetProjectC_NeuroWeb.Services.ParserService.Interface;
using PetProjectC_NeuroWeb.Services.ParserService.Ports;
using PetProjectC_NeuroWeb.Services.TimeService;
using System.Data;

namespace PetProjectC_NeuroWeb.Services.TimeService.Ports
{
    public class BackgroundTimeService : BackgroundService
    {
        private readonly ILogger<BackgroundTimeService> _logger;
        readonly List<Timer> _timers = new List<Timer>();
        readonly IEnumerable<IParserService> _parserServices;
        public BackgroundTimeService(ILogger<BackgroundTimeService> logger, IEnumerable<IParserService> parserServices)
        {
            _logger = logger;
            _parserServices = parserServices;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            


            while (!cancellationToken.IsCancellationRequested)
            {
                if (_parserServices.Any() != true)
                {
                    _logger.LogWarning("No one parser services was found");
                    return;
                }

                foreach (var parser in _parserServices)
                {
                    _logger.LogDebug("Парсер работает");
                    await parser.AddCategoryInDB();
                    await parser.AddCategoryInDB();
                    _logger.LogDebug("Парсер закончил свою работу");

                    await Task.Delay(TimeSpan.FromDays(7));
                }
            }
            _logger.LogDebug("Финиш");
        }
    }
}
