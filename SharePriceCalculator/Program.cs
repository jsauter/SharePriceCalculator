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

            orchestrator.Run();
        }
    }
}
