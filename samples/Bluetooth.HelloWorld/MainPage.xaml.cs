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
            this.Appearing += MainPage_Appearing;
		}

        private void MainPage_Appearing(object sender, EventArgs e)
        {
            CheckStatus();
        }



        private void Button_Click(object sender, EventArgs e)
        {
            CheckStatus();
        }

        private async void Button_PairedDevicesClick(object sender, EventArgs e)
        {
            using (var bluetooth = CrossBluetooth.Current)
            {
                var pairedDevices = await bluetooth.GetPairedDevices();
                listView.ItemsSource = pairedDevices;
                if(!pairedDevices.Any())
                {
                    DisplayAlert("Warning","There are no paired devices","Ok");
                }
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
                    //This is to check if it was already connected from a previous call
                    //What should be done is maybe show a dialog to prevent user interaction while its trying to write to the device
                    if (!item.IsConnected)
                        await item.Connect();

                    if (!item.IsConnected)
                    {
                        throw new Exception("Couldnt connect");
                    }

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

        private void CheckStatus()
        {
            using (var bluetooth = CrossBluetooth.Current)
            {
                if (bluetooth.IsSupportedByDevice)
                {
                    var isON = bluetooth.IsTurnedOn;
                    StatusLabel.Text = isON ? "Bluetooth is On" : "Bluetooth is Off turn it on";

                    TurnOnBluetoothButton.IsVisible = !isON;

                    if(!isON)
                    {
                        GetPairedDevicesButton.IsVisible = false;
                        listView.IsVisible = false;
                    }
                    else
                    {
                        TurnOnBluetoothButton.IsVisible = false;
                        GetPairedDevicesButton.IsVisible = true;
                        listView.IsVisible = true;
                    }

                }
                else
                {
                    StatusLabel.Text = "Device doesnt seem to support bluetooth";
                    TurnOnBluetoothButton.IsVisible = false;
                    GetPairedDevicesButton.IsVisible = false;
                    listView.IsVisible = false;
                }

            }
        }

    }
}
