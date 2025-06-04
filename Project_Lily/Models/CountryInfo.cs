using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;  // ObservableObject 클래스 사용

namespace Project_Lily.Models
{
    public partial class CountryInfo : ObservableObject
    {
        [ObservableProperty]
        private string countryProperty;

        [ObservableProperty]
        private int countryName;
    }
}
