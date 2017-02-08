using Ninject.Modules;
using SharePriceCalculator.Services;

namespace SharePriceCalculator.DependencyResolver
{
    public class ServiceBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IInputReaderService>().To<InputReaderService>();
            Bind<IOutputRendererService>().To<OutputRendererService>();
            Bind<IRecordFactory>().To<RecordFactory>();
        }
    }
}