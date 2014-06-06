using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Balloon
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class DateCalculatePage : Page
    {
        public DateCalculatePage()
        {
            this.InitializeComponent();
            update();
        }


        public void update()
        {
            untilDay.Text = ((int)(endDatePicker.Date.Date - startDatePicker.Date.Date).TotalDays).ToString();

            if (beforeTextBox.Text == string.Empty)
            {

                beforeDay.Text = DateTimeOffset.Now.Year.ToString() + "-" + DateTimeOffset.Now.Month.ToString() + "-" + DateTimeOffset.Now.Day.ToString() + " " + DateTimeOffset.Now.DayOfWeek.ToString();
                beforeDayofWeek.Text = DateTimeOffset.Now.DayOfWeek.ToString();
            }
            else
            {
                int n = 0;
                try
                {
                    n = int.Parse(beforeTextBox.Text.Trim());
                    TimeSpan pass = new TimeSpan(n, 0, 0, 0);
                    DateTime bd = startDatePicker.Date.Date.Subtract(pass);
                    beforeDay.Text = bd.Year.ToString() + "-" + bd.Month.ToString() + "-" + bd.Day.ToString();
                    beforeDayofWeek.Text = bd.DayOfWeek.ToString();
                }
                catch
                {
                    MessageDialog msg = new MessageDialog("您输入的不是数字", "提示");
                   
                }
                
            }

            if (afterTextBox.Text == string.Empty)
            {
                afterDay.Text = DateTimeOffset.Now.Year.ToString() + "-" + DateTimeOffset.Now.Month.ToString() + "-" + DateTimeOffset.Now.Day.ToString() + " " + DateTimeOffset.Now.DayOfWeek.ToString();
                afterDayofWeek.Text = DateTimeOffset.Now.DayOfWeek.ToString();
            }
            else
            {
                int n = 0;
                try
                {
                    n = int.Parse(afterTextBox.Text.Trim());
                    TimeSpan coming = new TimeSpan(n, 0, 0, 0);
                    DateTime ad = startDatePicker.Date.Date.Add(coming);
                    afterDay.Text = ad.Year.ToString() + "-" + ad.Month.ToString() + "-" + ad.Day.ToString();
                    afterDayofWeek.Text = ad.DayOfWeek.ToString();
                }
                catch {
                    MessageDialog msg = new MessageDialog("您输入的不是数字", "提示");
                }
            }
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            update();
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

    }
}
