using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Project_Lily.Models;
using DB.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Project_Lily.ViewModels
{
    public partial class ProductionViewModel : ObservableObject
    {
        // 3개 생산라인의 상태를 배열로 관리
        public ProductionLineStatus[] ProductionLines { get; set; } = new ProductionLineStatus[3];

        [ObservableProperty]
        private ProductionItem selectedProductionItem;

        [ObservableProperty]
        private int quantity = 0;

        [ObservableProperty]
        private string countryProperty;

        [ObservableProperty]
        private TimeSpan expirationTime;

        [ObservableProperty]
        private bool isExpired;

        [ObservableProperty]
        private bool isProduced;

        [ObservableProperty]
        private DateTime productionCompleteTime;



        public ObservableCollection<ProductionItem> ProductionItems { get; set; } = new();

        public ProductionViewModel()
        {
            ProductionItemDB.CreateDatabaseAndTable();

            // 생산라인 상태 초기화
            for (int i = 0; i < 3; i++)
            {
                ProductionLines[i] = new ProductionLineStatus();
            }
            InitializeItems();
        }

        protected virtual void InitializeItems()
        {
            // 기본 구현 (테스트용)
            ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Theranos.png", ProductionName = "암철석", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTime = TimeSpan.FromSeconds(5), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Theranos.png", ProductionName = "진토금", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTime = TimeSpan.FromSeconds(10), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Theranos.png", ProductionName = "석기정", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTime = TimeSpan.FromSeconds(15), Quantity = 0 });
        }



        [RelayCommand]
        private void Production()
        {
            if (ProductionLines[0].ItemSelected && !ProductionLines[0].Started && ProductionItems[0].Quantity < 1)
            {
                ProductionLines[0].ItemSelected = false;
                _ = StartProduction(0, ProductionItems[0]); // _= 는 비동기로 처리가능
            }

            if (ProductionLines[1].ItemSelected && !ProductionLines[1].Started && ProductionItems[1].Quantity < 1)
            {
                ProductionLines[1].ItemSelected = false;
                _ = StartProduction(1, ProductionItems[1]);
            }

            if (ProductionLines[2].ItemSelected && !ProductionLines[2].Started && ProductionItems[2].Quantity < 1)
            {
                ProductionLines[2].ItemSelected = false;
                _ = StartProduction(2, ProductionItems[2]);
            }
        }

        private async Task StartProduction(int lineIndex, ProductionItem item)
        {
            item.IsExpired = false;
            int totalTime = (int)item.ProductionTime.TotalSeconds;

            ProductionLines[lineIndex].Started = true;

            for (int i = 0; i < totalTime; i++)
            {
                await Task.Delay(1000);
                int progress = (i + 1) * 100 / totalTime;
                int remainingTime = totalTime - (i + 1);

                ProductionLines[lineIndex].Progress = progress;
                ProductionLines[lineIndex].RemainingTime = remainingTime;
            }

            ProductionItems[lineIndex].Quantity++;
            item.ProductionCompleteTime = DateTime.Now;


            // 리스트 내 재정렬 (예:ProductionTimer 빠른 순으로 정렬)
            var sorted = ProductionItems.OrderBy(sorting => sorting.ProductionTime).ToList();
            ProductionItems.Clear();
            foreach (var sorting in sorted)
                ProductionItems.Add(sorting);

            // DB에 저장
            ProductionItemDB.InsertProduction(item.ProductionName, DateTime.Now, item.Quantity);



            // 생산 완료시 초기화
            ProductionLines[lineIndex].Started = false;
            ProductionLines[lineIndex].Progress = 0;
            ProductionLines[lineIndex].RemainingTime = 0;

            StartExpirationTimer(item, lineIndex);
        }

        private async void StartExpirationTimer(ProductionItem item, int lineIndex)
        {

            var originalExpirationTime = item.ExpirationTime;
            item.IsExpired = false;

            while (item.ExpirationTime > TimeSpan.Zero && !item.IsExpired)
            {
                await Task.Delay(1000);
                item.ExpirationTime = item.ExpirationTime.Subtract(TimeSpan.FromSeconds(1));
            }

            item.IsExpired = true;
            if (item.IsExpired == true)
            {
                ProductionItems[lineIndex].Quantity--;
            }

            ProductionItemDB.DeleteProduction(item.ProductionName);
            item.ExpirationTime = originalExpirationTime;

        }



        // 생산라인별 선택 상태 접근을 위한 헬퍼 프로퍼티들 (XAML 바인딩용)
        public bool Production1ItemSelected
        {
            get => ProductionLines[0].ItemSelected;
            set => ProductionLines[0].ItemSelected = value;
        }

        public bool Production2ItemSelected
        {
            get => ProductionLines[1].ItemSelected;
            set => ProductionLines[1].ItemSelected = value;
        }

        public bool Production3ItemSelected
        {
            get => ProductionLines[2].ItemSelected;
            set => ProductionLines[2].ItemSelected = value;
        }

        // 나머지 프로퍼티들도 동일하게
        public bool Production1Started => ProductionLines[0].Started;
        public int Production1Progress => ProductionLines[0].Progress;
        public int Production1RemainingTime => ProductionLines[0].RemainingTime;

        public bool Production2Started => ProductionLines[1].Started;
        public int Production2Progress => ProductionLines[1].Progress;
        public int Production2RemainingTime => ProductionLines[1].RemainingTime;

        public bool Production3Started => ProductionLines[2].Started;
        public int Production3Progress => ProductionLines[2].Progress;
        public int Production3RemainingTime => ProductionLines[2].RemainingTime;
        
    }
}
