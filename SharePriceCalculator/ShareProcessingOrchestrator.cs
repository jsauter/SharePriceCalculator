using System;
using System.IO;
using System.Text;
using System.Threading;
using SharePriceCalculator.Services;

namespace SharePriceCalculator
{
    public class ShareProcessingOrchestrator : IShareProcessingOrchestrator
    {
        private readonly IInputReaderService _inputReaderService;
        private readonly IOutputRendererService _outputRendererService;

        public ShareProcessingOrchestrator(IInputReaderService inputReaderService, IOutputRendererService outputRendererService)
        {
            _inputReaderService = inputReaderService;
            _outputRendererService = outputRendererService;
        }

        public void Run(string input)
        {
            var result = _inputReaderService.ReadInput(input);

            var stdOut = _outputRendererService.GenerateOutput(result.MarketPrice, result.ShareRecords, result.EmployeeBonuses, result.SaleRecords);

            Console.WriteLine(stdOut);            
        }
    }
}
