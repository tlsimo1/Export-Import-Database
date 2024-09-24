using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restore_Data.Definition
{
    public class TableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void RaisePropertyChanged(string propertyName)
        {
            var handlers = PropertyChanged;

            handlers(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _values;
        private bool _isCheked = false;
        private string _countProgress;
        private string _countTable;

        public string Values { get { return _values; } set { _values = value; RaisePropertyChanged(""); } }
        
        public bool IsCheked { get { return _isCheked; } set { _isCheked = value; RaisePropertyChanged(""); } }
        public string CountProgress { get { return _countProgress; } set { _countProgress = value; RaisePropertyChanged(""); } }
        public string CountTable { get { return _countTable; } set { _countTable = value; RaisePropertyChanged(""); } }
    }
}
