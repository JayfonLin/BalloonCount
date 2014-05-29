using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Balloon
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Scenario2 : Page
    {
        DataTransferManager dtm;
        StorageFile photo;
        private int ID;
        public Scenario2()
        {
            this.InitializeComponent();
        }

        //进入时注册DataRequested事件
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ID = (int)e.Parameter;
            dtm = DataTransferManager.GetForCurrentView();
            SQLiteConnection db = MySQLiteHelper.CreateSQLiteConnection();

            //初始化界面设置
            List<object> query = db.Query(new TableMapping(typeof(ActivityInfo)), "select * from ActivityInfo");
            foreach (ActivityInfo mem in query)
            {
                if (mem.ID == ID)
                {
                    Title.Text = mem.Theme;
                    TextSource.Text = mem.Content;
                    MyDate.Date = mem.Date;
                    isTopSwitch.IsOn = mem.isTop;
                }
            }
            db.Close();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            dtm.DataRequested -= dtm_DataRequested;
        }
        void dtm_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            string textSource = "";
            string source = TextSource.Text;
            string textTitle = Title.Text;
            string textDescription = MyDate.Date.ToString().Split('+')[0];

            DataPackage data = args.Request.Data;
            data.Properties.Title = textTitle;
            data.Properties.Description = textDescription;
            textSource += "主题：";
            textSource += textTitle;
            textSource += "    ";
            textSource += "时间：";
            textSource += textDescription;
            textSource += "   ";
            textSource += "内容：";
            textSource += source;
            data.SetText(textSource);
            data.SetBitmap(RandomAccessStreamReference.CreateFromFile(photo));
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        protected void ShowUIButton_Click(object sender, RoutedEventArgs e)
        {
            //创建event handler
            dtm.DataRequested += dtm_DataRequested;
            // If the user clicks the share button, invoke the share flow programatically.
            DataTransferManager.ShowShareUI();
        }

        private async void OnCapturePhoto(object sender, RoutedEventArgs e)
        {
            var camera = new CameraCaptureUI();
            var file = await camera.CaptureFileAsync(CameraCaptureUIMode.Photo);
            photo = file;
            if (photo != null)
            {
                ImageSource imagesource = ImageFromFile(photo).Result;
                MyPhoto.Source = imagesource;
            }
            CameraButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            MyPhoto.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void SureButton_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection db = MySQLiteHelper.CreateSQLiteConnection();
            List<object> query = db.Query(new TableMapping(typeof(ActivityInfo)), "select * from ActivityInfo");
            //如果出现置顶，将其他置顶设为false
            if ((bool)isTopSwitch.IsOn == true)
            {
                foreach (ActivityInfo mem in query)
                {
                    mem.isTop = false;
                    db.Update(mem);
                }
            }
            foreach (ActivityInfo mem in query)
            {
                if (mem.ID == ID)
                {
                    mem.Date = MyDate.Date.Date;
                    mem.isTop = (bool)isTopSwitch.IsOn;
                    if (Title.Text == String.Empty) Title.Text = "某天";
                    mem.Theme = Title.Text;
                    mem.Content = TextSource.Text;
                    db.Update(mem);
                }
            }



            
            db.Close();
            Frame.Navigate(typeof(MainPage));
        }



        static async Task<ImageSource> ImageFromFile(StorageFile file)
        {
            var bitmapImage = new BitmapImage();
            var stream = await file.OpenReadAsync();
            {
                bitmapImage.SetSource(stream);
                return bitmapImage;
            }
        }
    }
}
