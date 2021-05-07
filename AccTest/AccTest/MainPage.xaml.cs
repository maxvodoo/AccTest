using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace AccTest
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        IOcrKit ocrKit;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            ocrKit = ocrKit ?? DependencyService.Get<IOcrKit>();
            var fileInfo = await MediaPicker.PickPhotoAsync();
            var stream = await fileInfo.OpenReadAsync();
            textArea.Text = await ocrKit.ExtractText(stream);
        }
    }
}
