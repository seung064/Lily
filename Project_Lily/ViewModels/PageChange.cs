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
using Project_Lily.View;

/*
namespace Project_Lily.ViewModels
{
    public partial class PageChangeViewModel : ObservableObject // ViewModelBase 클래스 / 속성과 UI 사이의 데이터 바인딩 자동 동작 / 반드시 partial class
    {
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
                    CurrentPage = new UserControl1();
                    break;
                case "UserControl2":
                    CurrentPage = new UserControl2();
                    break;
                case "UserControl3":
                    CurrentPage = new UserControl3();
                    break;
                case "UserControl4":
                    CurrentPage = new UserControl4();
                    break;
                case "UserControl5":
                    CurrentPage = new UserControl5();
                    break;
                case "UserControl6":
                    CurrentPage = new UserControl6();
                    break;
                case "UserControl7":
                    CurrentPage = new UserControl7();
                    break;
            }
        }
    }
}
*/