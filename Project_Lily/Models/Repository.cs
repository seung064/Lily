using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Lily.Models;

namespace Project_Lily
{
    public static class Repository
    {
        public static List<ProductionItem> ProductionItems { get; } = new()
        {

        };


        public static List<CombinationItem> CombinationItems { get; } = new()
        {
            new CombinationItem { CombinationLineNumber = 0, CombinationImagePath = "/Assets/Plasma_Heart.png", CombinationName = "플라즈마 하트", CombinationQuantity = 0 },
            new CombinationItem { CombinationLineNumber = 1, CombinationImagePath = "/Assets/Skycore_Crystal.png", CombinationName = "천공핵", CombinationQuantity = 0 },
            new CombinationItem { CombinationLineNumber = 2, CombinationImagePath = "/Assets/Geomagma_Capsule.png", CombinationName = "지화탄", CombinationQuantity = 0 },
            new CombinationItem { CombinationLineNumber = 3, CombinationImagePath = "/Assets/Aether_Core.png", CombinationName = "에테르 코어", CombinationQuantity = 0 },
            new CombinationItem { CombinationLineNumber = 4, CombinationImagePath = "/Assets/Magnesyl_Alloy.png", CombinationName = "마그네실", CombinationQuantity = 0 },
            new CombinationItem { CombinationLineNumber = 5, CombinationImagePath = "/Assets/Fluorite_X.png", CombinationName = "플루오라이트", CombinationQuantity = 0 },
            new CombinationItem { CombinationLineNumber = 6, CombinationImagePath = "/Assets/Genesium.png", CombinationName = "제네시움", CombinationQuantity = 0 },
        };

    }
}
