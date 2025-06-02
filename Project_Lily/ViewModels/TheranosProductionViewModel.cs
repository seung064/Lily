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
using System.Data.SQLite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;

namespace Project_Lily.ViewModels
{
    public partial class TheranosProductionViewModel : ObservableObject
    {

        [ObservableProperty]// 자동으로 public bool ProductionItemSelected { get; set; } 생성됨
        private bool production1ItemSelected; // 생산 아이템 선택 여부 

        [ObservableProperty]
        private bool production1Started;

        [ObservableProperty]
        private int production1Progress;

        [ObservableProperty]
        private int production1RemainingTime;

        [ObservableProperty]
        private bool production2ItemSelected;

        [ObservableProperty]
        private bool production2Started;

        [ObservableProperty]
        private int production2Progress;

        [ObservableProperty]
        private int production2RemainingTime;

        [ObservableProperty]
        private bool production3ItemSelected;

        [ObservableProperty]
        private bool production3Started;

        [ObservableProperty]
        private int production3Progress;

        [ObservableProperty]
        private int production3RemainingTime;

        [ObservableProperty]
        private ProductionItem selectedProductionItem;

        [ObservableProperty]
        private int quantity = 0;

        [ObservableProperty]
        private string countryProperty;

        [ObservableProperty]
        private TimeSpan expirationTimer;
        
        


        public ObservableCollection<ProductionItem> ProductionItems { get; set; } = new(); // 생산 아이템들을 담는 ObservableCollection



        public TheranosProductionViewModel() // 생성 아이템
        {
            ProductionItemDB.CreateDatabaseAndTable(); // DB 생성 코드 먼저 실행        
            ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Theranos.png", ProductionName = "진토금", ProductionProgress = 30, ExpirationTimer = TimeSpan.FromSeconds(600), ProductionTimer = TimeSpan.FromSeconds(30), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Theranos.png", ProductionName = "암철석", ProductionProgress = 30, ExpirationTimer = TimeSpan.FromSeconds(600), ProductionTimer = TimeSpan.FromSeconds(60), Quantity=0 });
            ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Theranos.png", ProductionName = "석기정", ProductionProgress = 30, ExpirationTimer = TimeSpan.FromSeconds(600), ProductionTimer = TimeSpan.FromSeconds(45), Quantity = 0 });
        }



        [RelayCommand]
        private async Task Production() // 생산 버튼 클릭 시 실행되는 메서드
        {
            if (Production1ItemSelected && !Production1Started)
            {
                Production1ItemSelected = false;
                _= StartProduction(1, ProductionItems[0]); // _= 는 비동기로 처리가능
            }

            if (Production2ItemSelected && !Production2Started)
            {
                Production2ItemSelected = false;
                _= StartProduction(2, ProductionItems[1]);
            }       
   
            if (Production3ItemSelected && !Production3Started)
            {
                Production3ItemSelected = false;
                _= StartProduction(3, ProductionItems[2]);
            }

            
        }


        // 생산 메서드 / 생산 라인으로 구분하여 비동기로 처리
        private async Task StartProduction(int lineNumber, ProductionItem item)
        {
            int totalTime = (int)item.ProductionTimer.TotalSeconds; // 총 생산 시간 (초 단위)



            for (int i = 0; i < totalTime; i++)
            {



                await Task.Delay(1000);
                int progress = (i + 1) * 100 / totalTime; // 프로그레스바 진행률
                int remainingTime = totalTime - (i + 1); // 남은 시간

                // await으로 1초마다 업데이트
                switch (lineNumber)
                {
                    case 1:
                        Production1Progress = progress;
                        Production1RemainingTime = remainingTime;
                        break;

                    case 2:
                        Production2Progress = progress;
                        Production2RemainingTime = remainingTime;
                        break;

                    case 3:
                        Production3Progress = progress;
                        Production3RemainingTime = remainingTime;
                        break;
                }
            }


           

            switch (lineNumber)
            {
                case 1: ProductionItems[0].Quantity++;
                    break;
                case 2: ProductionItems[1].Quantity++;
                    break;
                case 3: ProductionItems[2].Quantity++;
                    break;
            }

            ProductionItemDB.InsertProduction(item.ProductionName, DateTime.Now, item.Quantity);

            // 생산 완료시 초기화
            switch (lineNumber)
            {
                case 1:
                    Production1Started = false;
                    Production1Progress = 0;
                    Production1RemainingTime = 0;
                    break;

                case 2:
                    Production2Started = false;
                    Production2Progress = 0;
                    Production2RemainingTime = 0;
                    break;

                case 3:
                    Production3Started = false;
                    Production3Progress = 0;
                    Production3RemainingTime = 0;
                    break;
            }

            OnProductionComplete(lineNumber - 1); // 인덱스

        }

        private void OnProductionComplete(int itemIndex)
        {
            var item = ProductionItems[itemIndex];

            item.Quantity++;
            item.ProductionProgress = 0;

            // 유통기한 시작
            item.ExpirationTimer = TimeSpan.FromSeconds(600);
            item.IsExpired = false;

            StartExpirationTimer(item);
        }


        private async void StartExpirationTimer(ProductionItem item)
        {
            while (item.ExpirationTimer > TimeSpan.Zero && !item.IsExpired)
            {
                /*
                await Task.Delay(1000);

                item.ExpirationTimer = item.ExpirationTimer.Subtract(TimeSpan.FromSeconds(1));
                //item.ExpirationDisplay = $"{item.ExpirationTimer.Minutes:D2}:{item.ExpirationTimer.Seconds:D2}";
                 */
                await Task.Delay(1000); // 1초 대기
                item.ExpirationTimer = item.ExpirationTimer.Subtract(TimeSpan.FromSeconds(1)); // 여기!!
            }

            item.IsExpired = true;
        }
    }
}

