using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Balloon  
{
    public class ActivityInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        [SQLite.AutoIncrement, SQLite.PrimaryKey]
        public int ID { set; get; }

        public bool isTop { set; get; }

        private DateTime date;
        public DateTime Date {
            set {
                date = value;
                if (PropertyChanged != null) { 
                    PropertyChanged(this, new PropertyChangedEventArgs("Date"));
                }
            }
            get{
                return date;
            }
        }

        private String theme;
        public String Theme {
            set {
                theme = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Theme"));
                }
            }
            get {
                return theme;
            }
        }
        private String content;
        public String Content {
            set {
                content = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Content"));
                }
            }
            get {
                return content;
            }
        }
        private String picture;
        public String Picture {
            set {
                picture = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Picture"));
                }
            }
            get {
                return picture;
            }
        }


        //imagePath = new BitmapImage(new Uri("ms-appx:///" + "Assets/wallpaper-2650838.jpg"))
    }
}
