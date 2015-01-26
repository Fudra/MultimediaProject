
using Ninject;

namespace Crosscutting
{
    public class KernelManager
    {
        #region Fields
        private static readonly KernelManager Kernel = new KernelManager();
        private readonly StandardKernel _standardKernel;
        #endregion

        #region Singleton

        public static KernelManager Instance
        {
            get { return Kernel; }
        }

        private KernelManager()
        {
            _standardKernel = new StandardKernel();
            _standardKernel.Load("EiMM.*.dll");
        }

        #endregion

        public IKernel GetKernel()
        {
            return _standardKernel;
        }
    }
}
