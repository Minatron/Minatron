using Microsoft.Practices.Composite.Modularity;

namespace ImageStoreSystem.Infrastructure
{
	public interface IModuleWithCondition : IModule
	{
		void ConditionHappened();
	}
}
