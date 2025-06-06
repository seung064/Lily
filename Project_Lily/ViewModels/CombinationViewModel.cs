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
    public partial class CombinationViewModel : ObservableObject
    {
        [ObservableProperty]
        private CombinationItem selectedProducedItem;

        [ObservableProperty]
        private int combinationItemQuantity = 0;

        [ObservableProperty]
        private int combinationLineNumber;


        public ObservableCollection<CombinationItem> CombinationItems { get; set; } = new();
        public ObservableCollection<ProductionItem> SelectedProductionItems { get; set; } = new();

        public CombinationViewModel()
        {

        }

        protected virtual void CombinationItem()
        {
            CombinationItems.Add(new CombinationItem{ CombinationLineNumber = 0, CombinationImagePath = "/Assets/Plasma Heart.png", CombinationName = "플라즈마 하트", CombinationQuantity = 0 });
            CombinationItems.Add(new CombinationItem{ CombinationLineNumber = 1, CombinationImagePath = "/Assets/Skycore Crystal.png", CombinationName = "천공핵", CombinationQuantity = 0 });
            CombinationItems.Add(new CombinationItem{ CombinationLineNumber = 2, CombinationImagePath = "/Assets/Geomagma Capsule.png", CombinationName = "지화탄", CombinationQuantity = 0 });
            CombinationItems.Add(new CombinationItem{ CombinationLineNumber = 3, CombinationImagePath = "/Assets/Aether Core.png", CombinationName = "에테르 코어", CombinationQuantity = 0 });
            CombinationItems.Add(new CombinationItem{ CombinationLineNumber = 4, CombinationImagePath = "/Assets/Magnesyl Alloy.png", CombinationName = "마그네실", CombinationQuantity = 0 });
            CombinationItems.Add(new CombinationItem{ CombinationLineNumber = 5, CombinationImagePath = "/Assets/Fluirite X.png", CombinationName = "플루오라이트", CombinationQuantity = 0 });
            CombinationItems.Add(new CombinationItem{ CombinationLineNumber = 6, CombinationImagePath = "/Assets/Genesium.png", CombinationName = "제네시움", CombinationQuantity = 0 });
        }

        private Dictionary<HashSet<string>, string> combinationRules = new()
        {
            { new HashSet<string>{ "화염정광", "", "지화탄", "에테르 코어" }, "제네시움" },
            { new HashSet<string>{ "마그네실", "플루오라이트", "천공핵", "지화탄" }, "에테르 코어" }
            // 필요 시 더 추가
        };

        [RelayCommand]
        private void Combination()
        {
            if (CombinationItems)
        }
    }
}
