using Ninject.Modules;

namespace SharePriceCalculator
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IShareProcessingOrchestrator>().To<ShareProcessingOrchestrator>();
        }
    }
}