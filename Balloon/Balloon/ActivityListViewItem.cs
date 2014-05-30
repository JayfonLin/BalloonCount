using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Balloon
{
    public class ActivityListViewItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ActivityListViewItem(String pTheme, int pDate)
        {
            Theme = pTheme;
            Date = pDate;
            Radius = 200;
            //if (Radius >= 200)
            Fill = new SolidColorBrush(Colors.Brown);
            fontSize = Radius / 12;
            TextblockWidth = Radius * 0.9;
        }
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
        private double radius;
        public double Radius {
            set {
                radius = value;
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("Radius"));
                }
            }
            get {
                return radius;
            }
        }
        private SolidColorBrush fill;
        public SolidColorBrush Fill
        {
            set {
                fill = value;
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("Fill"));
                }
            }
            get {
                return fill;
            }
        }
        private double fontSize;
        public double FontSize {
            set
            {
                fontSize = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("FontSize"));
                }
            }
            get
            {
                return fontSize;
            }
        }
        private double textblockWidth;
        public double TextblockWidth {
            set
            {
                textblockWidth = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TextblockWidth"));
                }
            }
            get
            {
                return textblockWidth;
            }
        }
    }
}
