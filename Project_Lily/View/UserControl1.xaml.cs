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
using Project_Lily.Models;
using Project_Lily.ViewModels;

namespace Project_Lily.View
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1(ProductionViewModel planetVM)
        {
            InitializeComponent();
            DataContext = planetVM;
        }


        // ListBox 선택항목이 변경될 때마다 실행 / 선택하거나 해제할때마다 List 동기화
        private void ProductionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is ProductionViewModel vm)
            {
                vm.SelectedProductionItems.Clear();
                foreach (ProductionItem item in ProductionListBox.SelectedItems)
                {
                    vm.SelectedProductionItems.Add(item);
                }
            }
        }

        private void CombinationItemDelete(object sender, RoutedEventArgs e)
        {
            
        }
    }
}