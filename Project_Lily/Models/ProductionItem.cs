using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;  // ObservableObject 클래스 사용



namespace Project_Lily.Models
{
    public partial class ProductionItem : ObservableObject 
    {
        [ObservableProperty] // 자동으로 public 속성 생성 + getter와 setter 생성
        private string productionImagePath; // 생산 아이템 이미지 경로

        [ObservableProperty]
        private string productionName; // 생산 아이템 이름

        [ObservableProperty]
        private int productionProgress; // 생산 진행율

        [ObservableProperty]
        private TimeSpan productionTime; // 생산 시간

        [ObservableProperty]
        private TimeSpan expirationTime; // 유통기한

        [ObservableProperty]
        private TimeSpan ramainingTime; // 남은 시간

        [ObservableProperty]
        private int quantity; // 생산 아이템 수량

        [ObservableProperty]
        private bool isExpired = false;

        [ObservableProperty]
        private bool itemSelected;

        
        [ObservableProperty]
        private bool started;
        
        [ObservableProperty]
        private int progress;
        
        [ObservableProperty]
        private int remainingTime;
        

        // 계산 속성 IsProduced: Quantity > 0이면 true / 읽기전용
        public bool IsProduced => Quantity > 0;

        partial void OnQuantityChanged(int oldValue, int newValue)
        {
            OnPropertyChanged(nameof(IsProduced));
        }


        [ObservableProperty]
        private DateTime productionCompleteTime;

        [ObservableProperty]
        private int lineNumber; // 생산 라인 번호


        /*
        [ObservableProperty]
        private DateTime producedAT;
        */
    }
}
