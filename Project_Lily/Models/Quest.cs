using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;  // ObservableObject 클래스 사용


namespace Project_Lily.Models
{
    public partial class Quest : ObservableObject
    {
        [ObservableProperty]
        private bool isQuestActive;

        [ObservableProperty]
        private string questTitle = "새로운 퀘스트";

        [ObservableProperty]
        private ObservableCollection<CombinationItem> questItems;

        [ObservableProperty]
        private string questName;
        
        [ObservableProperty]
        private bool questCompleted;

        [ObservableProperty]
        private TimeSpan questTime;

    }
}
