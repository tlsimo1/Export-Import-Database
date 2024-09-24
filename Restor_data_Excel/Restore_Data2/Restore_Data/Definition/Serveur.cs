using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restore_Data
{

    
    class Serveur
    {


        public static string NomServeur { get; set; } 
        public static string NomDatabase { get; set; }
        public static bool IsValide { get; set; }
        public static string TableSource { get; set; } 
        public static string ColumnName { get; set; }
        public static string ErrorMessage { get; set; }
        public static string FolderPath { get; set; }

        public static string MotDePass { get; set; } = "123";

        public static string CreatePathFolder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Temp";

        public static string Path_appdata { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Temp\\123456.gsv";
        public static  string MessageFromSqlServer { get; set; }
    }
}
