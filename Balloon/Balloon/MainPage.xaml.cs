using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SQLite;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
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
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            init();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void init() {
            MyListView.ItemsSource = Sort(SelectAll());
            TopDate.ItemsSource = SelectTop();
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

        private ObservableCollection<ActivityListViewItem> SelectTop()
        {
            ObservableCollection<ActivityListViewItem> list = new ObservableCollection<ActivityListViewItem>();
            using (var db = MySQLiteHelper.CreateSQLiteConnection())
            {
                List<object> query = db.Query(new TableMapping(typeof(ActivityInfo)), "select * from ActivityInfo");

                if (query.Count == 0)
                {
                    return list;
                }
                ActivityInfo nearest = (ActivityInfo)query[0];
                foreach (ActivityInfo mem in query)
                {
                    ActivityInfo ai = mem;

                    if (ai.isTop == true)
                    {
                        ActivityListViewItem info = new ActivityListViewItem()
                        {
                            ID = ai.ID,
                            Theme = ai.Theme,
                            Date = (int)(ai.Date - DateTime.Now.Date).TotalDays
                        };
                        list.Add(info);
                        db.Close();
                        return list;
                    }
                    else
                    {
                        //选取不过期，但最近的日子
                        if ((int)(ai.Date - DateTime.Now.Date).TotalDays > 0 && (int)(ai.Date - nearest.Date).TotalDays < 0)
                            nearest = ai;
                    }

                }

                ActivityListViewItem near = new ActivityListViewItem()
                {
                    ID = nearest.ID,
                    Theme = nearest.Theme,
                    Date = (int)(nearest.Date - DateTime.Now.Date).TotalDays
                };
                list.Add(near);
                db.Close();
                return list;
                
            }
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
                    
                    //删除已过期的日子
                    if ((int)(ai.Date - DateTime.Now.Date).TotalDays < 0)
                    {
                        db.Delete(mem);
                    }
                    else
                    {
                        ActivityListViewItem info = new ActivityListViewItem()
                        {
                            ID = ai.ID,
                            Theme = ai.Theme,
                            Date = (int)(ai.Date - DateTime.Now.Date).TotalDays
                        };
                        list.Add(info);
                    }
                
                }
                db.Close();
            }
            return list;
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Scenario1));
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyListView.SelectedItems.Count > 0)
            {
                ActivityListViewItem temp = new ActivityListViewItem();
                temp = (ActivityListViewItem)MyListView.SelectedItems[0];
                var db = MySQLiteHelper.CreateSQLiteConnection();
                db.Delete<ActivityInfo>(temp.ID);
                init();
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyListView.SelectedItems.Count > 0)
            {
                ActivityListViewItem temp = new ActivityListViewItem();
                temp = (ActivityListViewItem)MyListView.SelectedItems[0];  
                Frame.Navigate(typeof(Scenario2), temp.ID);
            }
        }

        private void dateCalculateButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DateCalculatePage));
        }

    }
}
