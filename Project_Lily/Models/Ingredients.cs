using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace Project_Lily.Models
{
    class Ingredients : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public string IngredientName
        {
            set 
            {
                // public 변수인 IngredientName에 값을 할당하고 IngredientName을 통해 값을 저장하거나 받아올수있음
                IngredientName = value;
                OnPropertyChanged("IngredientName");
            }
            get 
            {
                return IngredientName;
            }
        }



        //  데이터를 UI에 바인딩했을 떄 실시간으로 업데이트하기 위해서 사용
        protected void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
