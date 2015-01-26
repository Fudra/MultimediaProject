using EiMM.ViewModel.Model.Impl;
using EiMM.ViewModel.Model.Interface;
using EiMM.ViewModel.ViewModel;
using Ninject.Modules;

namespace EiMM.ViewModel
{
    public class ViewModelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<EimmControlViewModel>().ToSelf().InSingletonScope();

            // Settings
            Bind<ISetting>().To<Setting>().InSingletonScope();
        }
    }
}