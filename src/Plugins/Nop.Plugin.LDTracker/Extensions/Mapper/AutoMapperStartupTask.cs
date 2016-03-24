using Nop.Core.Infrastructure;

namespace Nop.Plugin.LDTracker.Extensions.Mapper
{
    /// <summary>
    /// Startup class for AutoMapper
    /// </summary>
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            AutoMapperConfiguration.Init();
        }
        
        public int Order
        {
            get { return 0; }
        }
    }
}