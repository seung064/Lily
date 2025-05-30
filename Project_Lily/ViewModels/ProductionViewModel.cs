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

namespace Project_Lily.ViewModels
{
    public class ProductionViewModel : INotifyPropertyChanged
    {
        // 생산 목록 (Model 객체들 담는 리스트)
        public ObservableCollection<ProductionItem> ProductionList { get; } = new ObservableCollection<ProductionItem>();

        private int progress;
        public int Progress
        {
            get => progress;
            set
            {
                if (progress != value)
                {
                    progress = value;
                    OnPropertyChanged(nameof(Progress));
                }
            }
        }

        public ICommand StartProductionCommand { get; }

        public ProductionViewModel()
        {
            StartProductionCommand = new RelayCommand(async _ => await StartProductionAsync(), null);
        }

        // 생산 시작 함수

        private async Task StartProductionAsync()
        {
            // 새 생산 항목 생성 (시간 기록 포함)
            var newItem = new ProductionItem
            {
                Timer = TimeSpan.FromSeconds(50),
            };

            // 리스트에 추가 (UI에 자동 반영)
            ProductionList.Add(newItem);

            Progress = 0;
            for (int i = 0; i <= 100; i += 10)
            {
                Progress = i;
                await Task.Delay(500);
            }
        }



        private TimeSpan _timer;
        public TimeSpan Timer
        {
            get => _timer;
            set
            {
                if (_timer != value)
                {
                    _timer = value;
                    OnPropertyChanged(nameof(Timer));
                }
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}