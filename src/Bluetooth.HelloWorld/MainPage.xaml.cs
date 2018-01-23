using Plugin.Bluetooth;
using Plugin.Bluetooth.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bluetooth.HelloWorld
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private void Button_Click(object sender, EventArgs e)
        {
            using (var bluetooth = CrossBluetooth.Current)
            {
                if(bluetooth.IsAvailable)
                {
                    StatusLabel.Text = bluetooth.IsTurnedOn ? "Bluetooth is On" : "Bluetooth is Off";
                }
                else
                {
                    StatusLabel.Text = "Device doesnt seem to support bluetooth";
                }
                
            }

        }

        private async void Button_PairedDevicesClick(object sender, EventArgs e)
        {
            using (var bluetooth = CrossBluetooth.Current)
            {
                listView.ItemsSource = await bluetooth.GetPairedDevices();
            }
        }

        private async void Bluetooth_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                var item = listView.SelectedItem as IBluetoothDevice;
                listView.SelectedItem = null;
                if (item == null)
                    return;

                if (item.IsWriting)
                    return;

                try
                {
                    if (!item.IsConnected)
                        await item.Connect();

                }
                catch
                {
                    DisplayAlert("Error", "Couldnt connect with device.", "Ok");
                    return;
                }

                try
                {
                    await item.Write("Hello world!");
                    await DisplayAlert("Success", "Hello world message sent", "Ok");
                }
                catch
                {
                    DisplayAlert("Error", "Couldnt write to device.", "Ok");
                }

                try
                {
                    await item.Disconnect();
                }
                catch
                {
                    DisplayAlert("Error", "Couldnt disconnect from device.", "Ok");
                }

            }
        }

    }
}
