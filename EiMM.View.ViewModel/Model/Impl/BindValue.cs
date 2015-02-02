using EiMM.ViewModel.Enums;
using EiMM.ViewModel.Model.Interface;

namespace EiMM.ViewModel.Model.Impl
{
    public class BindValue : IBindValue
    {
        private int _counter;
        private float _value;

        public int Id { get; set; }

        public string Address { get; set; }

        public BindingOscValue OscToValue { get; set; }

        public bool Avg { get; set; }

        public float Value
        {
            get
            {
                var val = Avg ? _value/_counter : _value;
                _value = 0;
                _counter = 0;
                return val;
            }
            set
            {
                _value = Avg ? (_value+value) : value;
                _counter++;
            }
        }
    }
}