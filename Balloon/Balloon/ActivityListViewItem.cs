using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balloon
{
    public class ActivityListViewItem : INotifyPropertyChanged
    {
        public int ID { set; get; }
        public bool isTop { set; get; }
        public event PropertyChangedEventHandler PropertyChanged;
        private String theme;
        public String Theme {
            set
            {
                theme = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Theme"));
                }
            }
            get
            {
                return theme;
            }
        }
        private int date;
        public int Date {
            set {
                date = value;
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("Date"));
                }
            }
            get {
                return date;
            }
        }
    }
}
