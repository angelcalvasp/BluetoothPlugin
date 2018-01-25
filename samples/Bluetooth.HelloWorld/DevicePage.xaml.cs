using Plugin.Bluetooth.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bluetooth.HelloWorld
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DevicePage : ContentPage
	{
        private IBluetoothDevice _bluetoothDevice;
        public DevicePage (IBluetoothDevice bluetoothDevice)
		{
			InitializeComponent();
            _bluetoothDevice = bluetoothDevice;
            this.Appearing += DevicePage_Appearing;
        }

        private async void DevicePage_Appearing(object sender, EventArgs e)
        {
            try
            {
                if (!_bluetoothDevice.IsConnected)
                {
                    await _bluetoothDevice.Connect();
                }

            }
            catch
            {
                await DisplayAlert("Error", "Couldnt connect to device", "Ok");
                await Navigation.PopAsync();
            }
        }

        protected override void OnDisappearing()
        {
            try
            {
                if (_bluetoothDevice.IsConnected)
                {
                    _bluetoothDevice.Disconnect().Wait();
                }
            }
            catch
            {
                //TODO
            }
            base.OnDisappearing();
        }

    }
}