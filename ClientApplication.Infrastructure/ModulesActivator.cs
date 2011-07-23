using System.Collections.Generic;

namespace Band.Client.Infrastructure
{
    public interface IActivatable
    {
        bool Activated {get;set;}
    }

    public class ModulesActivator
    {
        readonly HashSet<IActivatable> _collection = new HashSet<IActivatable>();
        bool _activated = false;
       

        public bool Activated 
         {
             get { return _activated; }
             set 
             {
                 _activated = value;
                 foreach (IActivatable item in _collection) 
                     if (Activated != item.Activated) item.Activated = Activated; 
             }
         }

        public void Register(IActivatable item)
        {
            _collection.Add(item);
            if (Activated != item.Activated) item.Activated = Activated;
        }
    }
}
