using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Project_Lily.Models;
using DB.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Windows;

namespace Project_Lily.ViewModels
{
    public partial class ProductionViewModel : ObservableObject
    {
        

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

        [ObservableProperty]
        private int lineNumber;



        public ObservableCollection<ProductionItem> ProductionItems { get; set; } = new();
        public ObservableCollection<ProductionItem> ProducedItems { get; set; } = new();   // 생산된 리스트용 (스크롤뷰)
        public ObservableCollection<ProductionItem> ProducingItems { get; set; } = new();   // 생산중인 리스트용

        public int ProducingCount => ProducingItems.Count;
        public int ProducingMax => 6;
        public int ProducedCount => ProducedItems.Count;
        public int ProducedMax => 6;

        public string ProducingCountText => $"{ProducingCount} / {ProducingMax}"; // 생산중인 아이템 개수 텍스트
        public string ProducedCountText => $"{ProducedCount} / {ProducedMax}"; // 생산중인 아이템 개수 텍스트



        public ProductionViewModel()
        {
            ProductionItemDB.CreateDatabaseAndTable();

            // 생산라인 상태 초기화
            /*
            for (int i = 0; i < 12; i++)
            {
                ProductionItems[i] = new ProductionLineStatus();
            }
            */
            InitializeItems();


            ProducingItems.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(ProducingCount));
                OnPropertyChanged(nameof(ProducingCountText));
            };

            ProducedItems.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(ProducedCount));
                OnPropertyChanged(nameof(ProducedCountText));
            };
        }

        protected virtual void InitializeItems()
        {
            /*
            // 기본 구현 (테스트용)
            ProductionItems.Add(new ProductionItem { LineNumber = 0, ProductionImagePath = "/Assets/Theranos.png", ProductionName = "암철석", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTime = TimeSpan.FromSeconds(5), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 1, ProductionImagePath = "/Assets/Theranos.png", ProductionName = "진토금", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTime = TimeSpan.FromSeconds(10), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 2, ProductionImagePath = "/Assets/Theranos.png", ProductionName = "석기정", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTime = TimeSpan.FromSeconds(15), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 3, ProductionImagePath = "/Assets/Aquarius.png", ProductionName = "심해석", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTime = TimeSpan.FromSeconds(5), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 4, ProductionImagePath = "/Assets/Aquarius.png", ProductionName = "청류정수", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTime = TimeSpan.FromSeconds(10), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 5, ProductionImagePath = "/Assets/Aquarius.png", ProductionName = "바다수정", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTime = TimeSpan.FromSeconds(15), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 6, ProductionImagePath = "/Assets/Silphy.png", ProductionName = "풍정석", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTime = TimeSpan.FromSeconds(5), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 7, ProductionImagePath = "/Assets/Silphy.png", ProductionName = "회오리 플라스크", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTime = TimeSpan.FromSeconds(10), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 8, ProductionImagePath = "/Assets/Silphy.png", ProductionName = "공명결정", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTime = TimeSpan.FromSeconds(15), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 9, ProductionImagePath = "/Assets/Inferna.png", ProductionName = "화염저왕", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTime = TimeSpan.FromSeconds(5), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 10, ProductionImagePath = "/Assets/Inferna.png", ProductionName = "용석탄", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTime = TimeSpan.FromSeconds(10), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 11, ProductionImagePath = "/Assets/Inferna.png", ProductionName = "발화진주", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTime = TimeSpan.FromSeconds(15), Quantity = 0 });            ProductionItems.Add(new ProductionItem { LineNumber = 0, ProductionImagePath = "/Assets/Theranos.png", ProductionName = "암철석", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTime = TimeSpan.FromSeconds(5), Quantity = 0 });
            */
            // 테스트2
            ProductionItems.Add(new ProductionItem { LineNumber = 0, ProductionImagePath = "/Assets/Theranos.png", ProductionName = "암철석", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(200), ProductionTime = TimeSpan.FromSeconds(60), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 1, ProductionImagePath = "/Assets/Theranos.png", ProductionName = "진토금", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(200), ProductionTime = TimeSpan.FromSeconds(60), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 2, ProductionImagePath = "/Assets/Theranos.png", ProductionName = "석기정", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(60), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 3, ProductionImagePath = "/Assets/Aquarius.png", ProductionName = "심해석", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(60), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 4, ProductionImagePath = "/Assets/Aquarius.png", ProductionName = "청류정수", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(60), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 5, ProductionImagePath = "/Assets/Aquarius.png", ProductionName = "바다수정", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(60), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 6, ProductionImagePath = "/Assets/Silphy.png", ProductionName = "풍정석", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(60), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 7, ProductionImagePath = "/Assets/Silphy.png", ProductionName = "회오리 플라스크", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(60), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 8, ProductionImagePath = "/Assets/Silphy.png", ProductionName = "공명결정", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(60), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 9, ProductionImagePath = "/Assets/Inferna.png", ProductionName = "화염저왕", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(60), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 10, ProductionImagePath = "/Assets/Inferna.png", ProductionName = "용석탄", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(60), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 11, ProductionImagePath = "/Assets/Inferna.png", ProductionName = "발화진주", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(60), Quantity = 0 });

        }



        [RelayCommand]
        private void Production()
        {
            int currentTotal = ProducingItems.Count + ProducedItems.Count;
            if (currentTotal >= 6)
            {
                MessageBox.Show("더 이상 생산 불가");
                // 생산 불가 시 선택된 체크박스 취소
                for (int i = 0; i < ProductionItems.Count; i++)
                {
                    ProductionItems[i].ItemSelected = false;
                }
                return;
            }

            // 시간남으면 생산버튼 비활성 추가

            for (int i = 0; i < 12; i++)  // 배열/리스트 길이만큼 반복
            {
                var item = ProductionItems[i];

                if (ProductionItems[i].ItemSelected && !ProductionItems[i].Started && ProductionItems[i].Quantity < 1)
                {
                    ProductionItems[i].ItemSelected = false;
                    _ = StartProduction(ProductionItems[i]);
                }
            }
        }

        private async Task StartProduction(ProductionItem item)
        {
            item.IsExpired = false;
            item.Started = true;

            int totalTime = (int)item.ProductionTime.TotalSeconds;
            ProducingItems.Add(item);
            for (int i = 0; i < totalTime; i++)
            {
                await Task.Delay(1000);
                item.ProductionProgress = (i + 1) * 100 / totalTime;
                item.RemainingTime = totalTime - (i + 1);
            }

            item.Quantity++;
            item.ProductionCompleteTime = DateTime.Now;


            // 리스트 내 재정렬 (예:Production완료된 순으로 정렬) - 뷰
            ProducingItems.Remove(item);     // 생산 중인 리스트에서 제거
            ProducedItems.Add(item);    // 생산 완료된 리스트에 추가
            var sorted = ProducedItems.OrderBy(x => x.ProductionCompleteTime).ToList();
            ProducedItems.Clear();
            foreach (var sorting in sorted)
                ProducedItems.Add(sorting);
            

            // DB에 저장
            ProductionItemDB.InsertProduction(item.ProductionName, DateTime.Now, item.Quantity);


            // 생산 완료시 초기화


            item.ProductionProgress = 0;
            item.RemainingTime = 0;

            StartExpirationTimer(item);
        }

        // 유통기한 타이머 함수 
        private async void StartExpirationTimer(ProductionItem item)
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
                item.Quantity--;

                if (ProducedItems.Contains(item))
                    ProducedItems.Remove(item);
            }

            ProductionItemDB.DeleteProduction(item.ProductionName);
            item.ExpirationTime = originalExpirationTime;

        }


        /*
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
        */
    }
}
