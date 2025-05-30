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
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;


namespace Project_Lily.ViewModels
{
    public partial class PageChangeViewModel : ObservableObject // ViewModelBase 클래스 / 속성과 UI 사이의 데이터 바인딩 자동 동작 / 반드시 partial class
    {
        // 생산 목록 (Model 객체들 담는 리스트)
        // public ObservableCollection<Page> pageNumber { get; set; } = new(); // Page 객체들을 담은 리스트를 뷰와 바인딩할 때 사용하는 컬렉션
        //리스트박스나 탭 컨트롤에 여러 페이지를 나열하거나 선택



        [ObservableProperty]
        private UserControl currentPage;


        [RelayCommand]
        // 페이지 변경 명령
        // userControlName에 문자열이 들어올때마다 객체를 생성하고 currentPage 변수에 객체를 저장
        private void ChangePage(string userControlName)
        {
            switch (userControlName)
            {
                case "UserControl1":
                    currentPage = new UserControl1();
                    break;
                case "UserControl2":
                    currentPage = new UserControl2();
                    break;
                case "UserControl3":
                    currentPage = new UserControl3();
                    break;
                case "UserControl4":
                    currentPage = new UserControl4();
                    break;
                case "UserControl5":
                    currentPage = new UserControl5();
                    break;
                case "UserControl6":
                    currentPage = new UserControl6();
                    break;
            }
        }
    }
}
