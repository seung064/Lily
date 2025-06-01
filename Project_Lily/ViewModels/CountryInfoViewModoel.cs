using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Project_Lily.Commands;
using Project_Lily.Models;
using DB.Models; // 네임스페이스와 일치 (끌어오기)
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;  // ObservableObject 클래스 사용
using CommunityToolkit.Mvvm.Input;
using System.Windows; // RelayCommand 클래스 사용

namespace Project_Lily.ViewModels
{
    public partial class CountryInfoViewModoel : ObservableObject
    {


        public ObservableCollection<CountryInfo> CountryInfos { get; set; } = new();
        public CountryInfoViewModoel() // 생성 아이템
        {
            CountryInfos.Add(new CountryInfo { CountryProperty = "거대한 산맥과 광활한 평야 지형\n" +
                                                  "자원이 풍부하지만 환경은 거칠고 건조함\n" +
                                                  "지진 및 지열 활동이 활발함"
            });
        }
    }
}
