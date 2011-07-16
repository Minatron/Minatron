using System;

namespace WPF.Patterns.ViewModel
{
    public abstract class UpdatableViewModelBase<Tmodel> : ViewModelBase, IUpdatebleViewModel 
        where Tmodel : class
    {
        protected Tmodel m_model;

        protected virtual void OnRefreshView() { OnPropertyChanged("Model"); }        
        protected virtual void OnUpdateModelData() { }        
        protected virtual void OnChangeModel(Tmodel newmodel) { }        

        public void UpdateModel()
        {
            if (m_model != null)
            {
                OnUpdateModelData();
                OnRefreshView();
            }
        }

        public void ClearModel()
        {
            if (m_model != null)
            {
                OnChangeModel(null);
                m_model = null;                
                OnRefreshView();
            }
        }


        public void UpdateModel(object model)
        {
            Model = (model as Tmodel);            
        }

        public Tmodel Model
        {
            get
            {
                return m_model;
            }
            set 
            {
                if (value == null) ClearModel();  
                else if (object.ReferenceEquals(m_model, value)) UpdateModel(); 
                else
                {
                    OnChangeModel(value);
                    m_model = value;
                    UpdateModel();
                } 
            }
        }
        protected override void OnDispose()
        {            
            m_model = default(Tmodel);
            base.OnDispose();
        }
        
    }
}
