using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restore_Data
{
    [Serializable()]
    public class XmlFile
    {
        public string DataBaseName { get; set; }
        public string SQLVersion { get; set; }
        public string SQLVersionNumber { get; set; }
        public string VersionDatabase { get; set; }


        public string  TableSource { get; set; }
        public string  Version { get; set; }
        public DateTime Date { get; set; }
    }
}
