using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using SharePriceCalculator.DependencyResolver;

namespace SharePriceCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap();            
        }

        private static void Bootstrap()
        {
            var kernel = new StandardKernel(new Bindings());

            var modules = new List<INinjectModule>
            {              
                new ServiceBindings()
            };

            kernel.Load(modules);

            var orchestrator = kernel.Get<IShareProcessingOrchestrator>();

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

            orchestrator.Run(stdin);
        }
    }
}
