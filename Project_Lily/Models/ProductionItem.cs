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
        private TimeSpan productionTimer; // 생산 시간

        [ObservableProperty]
        private TimeSpan expirationTimer = TimeSpan.Zero; // 유통기한

        [ObservableProperty]
        private TimeSpan ramainingTime; // 남은 시간

        [ObservableProperty]
        private int quantity; // 생산 아이템 수량

        [ObservableProperty]
        private bool isExpired = false;
    }
}
