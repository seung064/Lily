using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Windows;



namespace ProjectDB.Model
{
    public class User
    {
        public string Nickname { get; set; }
        public string Id { get; set; }
        public string Pw { get; set; }


    }
}