using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Project_Lily.Models
{
    public partial class ProductionLineStatus : ObservableObject
    {
        [ObservableProperty]
        private bool itemSelected;

        [ObservableProperty]
        private bool started;

        [ObservableProperty]
        private int progress;

        [ObservableProperty]
        private int remainingTime;

        [ObservableProperty]
        private int quantity;

        [ObservableProperty]
        private DateTime productionCompleteTime;
    }
}
