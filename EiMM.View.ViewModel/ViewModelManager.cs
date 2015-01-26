using Crosscutting;
using EiMM.ViewModel.ViewModel;
using Ninject;

namespace EiMM.ViewModel
{
    public class ViewModelManager
    {

        #region Fields

        private static readonly ViewModelManager _viewModelManager = new ViewModelManager();

        #endregion

        #region Singleton

        private ViewModelManager()
        {

        }

        public static ViewModelManager Instance
        {
            get { return _viewModelManager; }
        }

        #endregion

        public EimmControlViewModel EimmControlViewModel
        {
            get { return KernelManager.Instance.GetKernel().Get<EimmControlViewModel>(); }
        }
         
    }
}