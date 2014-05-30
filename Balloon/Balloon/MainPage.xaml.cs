using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SQLite;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Balloon
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            MySQLiteHelper.createDB();
            //init();
            
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            init();
        }
        private void Next_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Scenario1));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void init() {
            MyListView.ItemsSource = Sort(SelectAll());
        }

        private ObservableCollection<ActivityListViewItem> Sort(ObservableCollection<ActivityListViewItem> list)
        {
            var values1 = from l in list
                         where l.Date >= 0
                         orderby l.Date ascending
                         select l;
            var values2 = from l in list
                          where l.Date < 0
                          orderby l.Date descending
                          select l;
            //values1.Concat(values2);
            ObservableCollection<ActivityListViewItem> result = new ObservableCollection<ActivityListViewItem>();
            foreach (ActivityListViewItem a in values1) {
                result.Add(a);
            }
            foreach (ActivityListViewItem a in values2)
            {
                result.Add(a);
            }
            return result;
        }

        private ObservableCollection<ActivityListViewItem> SelectAll()
        {
            ObservableCollection<ActivityListViewItem> list = new ObservableCollection<ActivityListViewItem>();
            using (var db = MySQLiteHelper.CreateSQLiteConnection())
            {
                List<object> query = db.Query(new TableMapping(typeof(ActivityInfo)), "select * from ActivityInfo");
                foreach (ActivityInfo mem in query)
                {
                    ActivityInfo ai = mem;
                    ActivityListViewItem info = new ActivityListViewItem(ai.Theme, (int)(ai.Date - DateTime.Now.Date).TotalDays);
                    list.Add(info);
                }
                db.Close();
            }
            return list;
        }
    }
}
