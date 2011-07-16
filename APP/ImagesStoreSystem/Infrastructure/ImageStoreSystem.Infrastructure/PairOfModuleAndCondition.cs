using System;

namespace ImageStoreSystem.Infrastructure
{
	public class PairOfModuleAndCondition
	{
		public IModuleWithCondition Module { get; protected set; }
		public Func<bool> Condition { get; protected set; }

		public PairOfModuleAndCondition(IModuleWithCondition module, Func<bool> condition)
		{
			Module = module;
			Condition = condition;
		}
	}
}
