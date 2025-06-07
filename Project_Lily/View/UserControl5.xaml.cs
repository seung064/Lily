using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project_Lily.ViewModels;

namespace Project_Lily.View
{
    public partial class UserControl5 : UserControl
    {
        public UserControl5(ProductionViewModel planetVM)
        {
            InitializeComponent();
            //this.DataContext = new TheranosProductionViewModel();  // 뷰모델 연결
            DataContext = planetVM;
        }
    }
}


