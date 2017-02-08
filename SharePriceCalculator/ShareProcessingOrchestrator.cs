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

        public void Run()
        {
            // stdin stuff liberated from the internet... it wants to be free
            string stdin = null;
            if (Console.IsInputRedirected)
            {
                using (Stream stream = Console.OpenStandardInput())
                {
                    byte[] buffer = new byte[1000];  // Use whatever size you want
                    StringBuilder builder = new StringBuilder();
                    int read = -1;
                    while (true)
                    {
                        var gotInput = new AutoResetEvent(false);
                        var inputThread = new Thread(() =>
                        {
                            try
                            {
                                read = stream.Read(buffer, 0, buffer.Length);
                                gotInput.Set();
                            }
                            catch (ThreadAbortException)
                            {
                                Thread.ResetAbort();
                            }
                        })
                        {
                            IsBackground = true
                        };

                        inputThread.Start();

                        // Timeout expired?
                        if (!gotInput.WaitOne(100))
                        {
                            inputThread.Abort();
                            break;
                        }

                        // End of stream?
                        if (read == 0)
                        {
                            stdin = builder.ToString();
                            break;
                        }

                        // Got data
                        builder.Append(Console.InputEncoding.GetString(buffer, 0, read));
                    }
                }
            }
            
            var result = _inputReaderService.ReadInput(stdin);

            var stdOut = _outputRendererService.GenerateOutput(result.MarketPrice, result.ShareRecords, result.EmployeeBonuses, result.SaleRecords);

            Console.WriteLine(stdOut);            
        }
    }
}
