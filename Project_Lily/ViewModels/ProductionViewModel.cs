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
using System.Windows.Input;
using System.Windows.Threading;
using System.Diagnostics;

namespace Project_Lily.ViewModels
{
    public partial class ProductionViewModel : ObservableObject
    {
        
        //생산
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
        //-----


        // 조합
        [ObservableProperty]
        private ProductionItem selectedProducedItem;

        [ObservableProperty]
        private int combinationItemQuantity = 0;

        [ObservableProperty]
        private int combinationLineNumber;

        [ObservableProperty]
        private CombinationItem selectedCombinationItem;
        //-----


        //퀘스트
        [ObservableProperty]
        private CombinationItem questItem;

        [ObservableProperty]
        private string questTitle = "새로운 퀘스트";

        [ObservableProperty]
        private bool isQuestActive;

        [ObservableProperty]
        private string questName;

        [ObservableProperty]
        private bool questCompleted;

        private Random random = new();

        [ObservableProperty]
        private TimeSpan questTime;

        [ObservableProperty]
        private string remainingQuestTimeText;

        [ObservableProperty]
        private string combinationName;
        //----


        // 생산
        public ObservableCollection<ProductionItem> ProductionItems { get; set; } = new();
        public ObservableCollection<ProductionItem> ProducedItems { get; set; } = new();   // 생산된 리스트용 (스크롤뷰)
        public ObservableCollection<ProductionItem> ProducingItems { get; set; } = new();   // 생산중인 리스트용
        // ----

        // 조합
        public ObservableCollection<CombinationItem> CombinationItems { get; set; } = new();
        public ObservableCollection<ProductionItem> SelectedProductionItems { get; set; } = new();
        public ObservableCollection<ProductionItem> SelectedCombinationItems { get; set; } = new();
        //----
        public ObservableCollection<CombinationItem> QuestItems { get; set; } = new();


        public int ProducingCount => ProducingItems.Count;
        public int ProducingMax => 6;
        public int ProducedCount => ProducedItems.Count;
        public int ProducedMax => 6;

        public string ProducingCountText => $"{ProducingCount} / {ProducingMax}"; // 생산중인 아이템 개수 텍스트
        public string ProducedCountText => $"{ProducedCount} / {ProducedMax}"; // 생산중인 아이템 개수 텍스트



        public ProductionViewModel()
        {
            ProductionItemDB.CreateDatabaseAndTable();

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

        // 생산
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
            ProductionItems.Add(new ProductionItem { LineNumber = 0, ProductionImagePath = "/Assets/Theranos.png", ProductionName = "암철석", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(10), ProductionTime = TimeSpan.FromSeconds(6), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 1, ProductionImagePath = "/Assets/Theranos.png", ProductionName = "진토금", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(200), ProductionTime = TimeSpan.FromSeconds(6), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 2, ProductionImagePath = "/Assets/Theranos.png", ProductionName = "석기정", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(6), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 3, ProductionImagePath = "/Assets/Aquarius.png", ProductionName = "심해석", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(6), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 4, ProductionImagePath = "/Assets/Aquarius.png", ProductionName = "청류정수", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(6), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 5, ProductionImagePath = "/Assets/Aquarius.png", ProductionName = "바다수정", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(6), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 6, ProductionImagePath = "/Assets/Silphy.png", ProductionName = "풍정석", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(6), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 7, ProductionImagePath = "/Assets/Silphy.png", ProductionName = "플라스크", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(6), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 8, ProductionImagePath = "/Assets/Silphy.png", ProductionName = "공명결정", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(6), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 9, ProductionImagePath = "/Assets/Inferna.png", ProductionName = "화염정광", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(6), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 10, ProductionImagePath = "/Assets/Inferna.png", ProductionName = "용석탄", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(6), Quantity = 0 });
            ProductionItems.Add(new ProductionItem { LineNumber = 11, ProductionImagePath = "/Assets/Inferna.png", ProductionName = "발화진주", ProductionProgress = 0, ExpirationTime = TimeSpan.FromSeconds(300), ProductionTime = TimeSpan.FromSeconds(6), Quantity = 0 });

        }
        //----

        //조합
        /*
        protected virtual void CombinationItem()
        {
            CombinationItems.Add(new CombinationItem { CombinationLineNumber = 0, CombinationImagePath = "/Assets/Plasma_Heart.png", CombinationName = "플라즈마 하트", CombinationQuantity = 0 });
            CombinationItems.Add(new CombinationItem { CombinationLineNumber = 1, CombinationImagePath = "/Assets/Skycore_Crystal.png", CombinationName = "천공핵", CombinationQuantity = 0 });
            CombinationItems.Add(new CombinationItem { CombinationLineNumber = 2, CombinationImagePath = "/Assets/Geomagma_Capsule.png", CombinationName = "지화탄", CombinationQuantity = 0 });
            CombinationItems.Add(new CombinationItem { CombinationLineNumber = 3, CombinationImagePath = "/Assets/Aether_Core.png", CombinationName = "에테르 코어", CombinationQuantity = 0 });
            CombinationItems.Add(new CombinationItem { CombinationLineNumber = 4, CombinationImagePath = "/Assets/Magnesyl_Alloy.png", CombinationName = "마그네실", CombinationQuantity = 0 });
            CombinationItems.Add(new CombinationItem { CombinationLineNumber = 5, CombinationImagePath = "/Assets/Fluirite_X.png", CombinationName = "플루오라이트", CombinationQuantity = 0 });
            CombinationItems.Add(new CombinationItem { CombinationLineNumber = 6, CombinationImagePath = "/Assets/Genesium.png", CombinationName = "제네시움", CombinationQuantity = 0 });
        }
        */
        private Dictionary<HashSet<string>, string> combinationRules = new() // HashSet<string> : 순서와 상관없이 비교가능
        {
            { new HashSet<string>{ "화염정광", "심해석", "풍정석", "암철석" }, "플라즈마 하트" },
            { new HashSet<string>{ "용석탄", "청류정수", "공명결정", "석기정" }, "천공핵" },
            { new HashSet<string>{ "발화진주", "바다수정", "플라스크", "진토금" }, "지화탄" },
            { new HashSet<string>{ "용석탄", "청류정수", "풍정석", "암철석" }, "에테르 코어" },
            { new HashSet<string>{ "화염정광", "심해석", "공명결정", "진토금" }, "마그네실" },
            { new HashSet<string>{ "발화진주", "청류정수", "플라스크", "석기정" }, "천공핵" },
            { new HashSet<string>{ "화염정광", "바다수정", "공명결정", "석기정" }, "제네시움" }
        };

        // 딕셔너리 추가 <- 한글이름으로 인해 경로를 못 찾음
        private Dictionary<string, string> imageFileNames = new()
        {
            { "플라즈마 하트", "Plasma_Heart" },
            { "천공핵", "Skycore_Crystal" },
            { "지화탄", "Geomagma_Capsule" },
            { "에테르 코어", "Aether_Core" },
            { "마그네실", "Magnesyl_Alloy" },
            { "플루오라이트", "Fluirite_X" },
            { "제네시움", "Genesium" }
        };
        //----



        // 생산
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

            
            for (int i = 0; i < 12; i++)  // 배열/리스트 길이만큼 반복
            {
                var item = ProductionItems[i];

                if (ProductionItems[i].ItemSelected && !ProductionItems[i].Started && !ProducedItems.Contains(item))
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
            //ProductionItemDB.InsertProduction(item.ProductionName, DateTime.Now, item.Quantity);


            // 생산 완료시 초기화
            item.ProductionProgress = 0;
            item.RemainingTime = 0;

            StartExpirationTimer(item);


            item.Started = false; // 생산 완료 후 Started 상태 초기화
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
            if (item.IsExpired && item.Quantity > 0)
            {
                item.Quantity--;

                if (ProducedItems.Contains(item))
                {
                    ProducedItems.Remove(item);
                }
            }

            ProductionItemDB.DeleteProduction(item.ProductionName);
            item.ExpirationTime = originalExpirationTime;

        }
        //----


        // 퀘스트 타이머
        private async void StartQuestTimer()
        {
            if (IsQuestActive == true)
            {
                while (QuestTime > TimeSpan.Zero)
                {
                    await Task.Delay(1000);
                    QuestTime = QuestTime.Subtract(TimeSpan.FromSeconds(1));
                }

                if (QuestTime <= TimeSpan.Zero)
                {
                    IsQuestActive = false;
                    MessageBox.Show("실패! 퀘스트 시간 종료!");
                }
            }
        }
        //----

        
        //조합
        [RelayCommand]
        private void Combination()
        {

            // [테스트] MessageBox.Show("조합 버튼 눌림");
            if (SelectedProductionItems.Count != 4)
            {
                MessageBox.Show("4개를 선택해야 합니다");
                return;
            }

            var selectedNames = new HashSet<string>(SelectedProductionItems.Select(i => i.ProductionName));

            foreach (var rule in combinationRules)
            {
                if (rule.Key.SetEquals(selectedNames))
                {
                    // 조합 성공
                    var resultItem = rule.Value;
                    MessageBox.Show($"조합 성공! 결과: {resultItem}");

                    // 결과 아이템 CombinationItems에 추가
                    CombinationItems.Add(new CombinationItem
                    {
                        CombinationName = resultItem,
                        CombinationQuantity = 1,
                        CombinationImagePath = "/Assets/" + imageFileNames[resultItem] +".png"
                    });


                    foreach (var item in SelectedProductionItems.ToList())
                    {
                        item.Quantity --;

                        // 수량 0이면 리스트에서 제거
                        if (item.Quantity <= 0)
                        {
                            item.IsExpired = true;
                            ProducedItems.Remove(item);
                        }
                    }

                    return;
                }
            }

            // 실패
            MessageBox.Show("조합 실패!");
        }
        //----




        // 전송
        [RelayCommand]
        private void Send()
        {

            if (SelectedCombinationItems.Count == 0)
            {
                MessageBox.Show("보낼 조합품이 없습니다");
                return;
            }
            var questMaterialNames = new HashSet<string>(QuestItems.Select(q => q.CombinationName));
            var selectedNames = new HashSet<string>(SelectedCombinationItems.Select(i => i.ProductionName));


            if (!questMaterialNames.SetEquals(selectedNames))
            {
                MessageBox.Show("퀘스트 재료와 선택한 조합품이 일치하지 않습니다.");
                return;
            }

            // 퀘스트 재료와 선택한 조합품이 일치하면 조합 규칙 체크
            foreach (var rule in combinationRules)
            {
                if (rule.Key.SetEquals(selectedNames))
                {
                    // 조합 성공
                    var resultItem = rule.Value;
                    MessageBox.Show($"조합 성공! 결과: {resultItem}");

                    CombinationItems.Add(new CombinationItem
                    {
                        CombinationName = resultItem,
                        CombinationQuantity = 1,
                        CombinationImagePath = "/Assets/" + imageFileNames[resultItem] + ".png"
                    });

                    foreach (var item in SelectedProductionItems.ToList())
                    {
                        ProducedItems.Remove(item);
                    }

                    // 퀘스트 완료 처리 (예: 비활성화)
                    IsQuestActive = false;
                    QuestItems.Clear();

                    return;
                }
            }
        }
        //----


        // 퀘스트

        [RelayCommand]
        public void AddQuest()
        {
            if (IsQuestActive)
            {
                MessageBox.Show("이미 진행 중인 퀘스트가 있습니다!");
                return;
            }
            /*
            if (Repository.CombinationItems == null || Repository.CombinationItems.Count < 2)
            {
                MessageBox.Show("오류");
                return;
            }
            */
            Random random = new Random();
            var questItem = Repository.CombinationItems[random.Next(Repository.CombinationItems.Count)];

            // CombinationItems에서 랜덤으로 2개 뽑기
            var questMaterials = Repository.CombinationItems.OrderBy(x => random.Next()).Take(2).ToList();

            // 퀘스트 재료에 랜덤 뽑은 2개 넣기
            QuestItems.Clear(); // 이전 퀘스트 재료 초기화
            foreach (var combinationItem in questMaterials)
            {
                QuestItems.Add(combinationItem);
            }

            // 퀘스트 활성화
            IsQuestActive = true;

            QuestTime = TimeSpan.FromMinutes(10); // 퀘스트 시간 설정 (10분)
            StartQuestTimer();


        }
        /*
        private void AddQuest()
        {

            var selectedNames = new HashSet<string>(SelectedCombinationItems.Select(x => x.ProductionName));

            foreach (var rule in combinationRules)
            {
                if (rule.Key.SetEquals(selectedNames))
                {
                    string combinationName = rule.Value;

                    var selected = CombinationItems.OrderBy(x => random.Next()).Take(2).ToList();


                    // 이미 등록된 퀘스트인지 확인
                    if (QuestItems.Any(q => questName == combinationName))
                    {
                        MessageBox.Show("이미 등록된 퀘스트입니다.");
                        return;
                    }


                    QuestItems.Add(new Quest
                    {
                        QuestName = combinationName,
                        QuestCompleted = false
                    });

                    MessageBox.Show($"'{combinationName}' 를 구해오시오.");
                    SelectedCombinationItems.Clear();
                    return;
                }
            }

            MessageBox.Show("해당 조합은 등록 가능한 퀘스트가 아닙니다.");

        }
        */
        //----




        // 생산된 물품삭제
        [RelayCommand]
        private void DeleteProducedItem()
        {
            try
            {
                if (SelectedProducedItem != null)
                {
                    SelectedProducedItem.IsExpired = true;
                    SelectedProducedItem.Quantity --;
                    SelectedProducedItem.Started = false;
                    ProducedItems.Remove(SelectedProducedItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}");
            }
        }
        
        // 조합품 삭제
        [RelayCommand]
        private void DeleteCombinationItem()
        {
            if (SelectedCombinationItem != null)
            {
                CombinationItems.Remove(SelectedCombinationItem);
            }
        }
        


    }
}
