using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Lily.LogInUI
{
    public class User
    {
        public bool IsSelected { get; set; }  // 체크박스 선택 여부
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UserId { get; set; }
        public string? Birth { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? UserType { get; set; }
    }
}
