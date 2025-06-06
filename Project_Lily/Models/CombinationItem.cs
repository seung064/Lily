using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;  // ObservableObject 클래스 사용

namespace Project_Lily.Models
{
    public partial class CombinationItem : ObservableObject
    {

        [ObservableProperty]
        private string combinationImagePath;


        [ObservableProperty]
        private string combinationName;

        [ObservableProperty]
        private int combinationQuantity;

        [ObservableProperty]
        private bool combinationSelected;

        [ObservableProperty]
        private int combinationLineNumber;


    }
}