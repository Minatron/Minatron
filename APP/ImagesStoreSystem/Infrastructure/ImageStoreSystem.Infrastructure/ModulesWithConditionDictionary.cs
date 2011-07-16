using System;
using System.Collections.Generic;

namespace ImageStoreSystem.Infrastructure
{
	public class ModulesWithConditionDictionary
	{
		List<PairOfModuleAndCondition> _modulesWithCondition = new List<PairOfModuleAndCondition>();

		public void AddModule(IModuleWithCondition module, Func<bool> condition = null)
		{
			if (module == null) return;
			if (condition == null) condition = () => true;
			_modulesWithCondition.Add(new PairOfModuleAndCondition(module, condition));
		}

		public void InitModulesWithTrueCondition()
		{
			for (int i = 0; i < _modulesWithCondition.Count; ++i)
			{
				var item = _modulesWithCondition[i];
				if (item.Condition())
				{
					item.Module.ConditionHappened();
					_modulesWithCondition.Remove(item);
					--i;
				}
			}
		}
	}
}
