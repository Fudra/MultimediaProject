using System;
using EiMM.ViewModel.Enums;

namespace EiMM.ViewModel.Model.Interface
{
    public interface IBindValue
    {
        int Id { get; set; }

        String Address { get; set; }

        BindingOscValue OscToValue { get; set; }

        bool Avg { get; set; }

        float Value { get; set; }
    }
}