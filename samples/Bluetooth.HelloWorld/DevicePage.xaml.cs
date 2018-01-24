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
        }

        protected override void OnAppearing()
        {
            try
            {
                if (!_bluetoothDevice.IsConnected)
                {
                    _bluetoothDevice.Connect().Wait();
                }
                base.OnAppearing();
            }
            catch
            {
                DisplayAlert("Error","Couldnt connect to device","Ok");
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