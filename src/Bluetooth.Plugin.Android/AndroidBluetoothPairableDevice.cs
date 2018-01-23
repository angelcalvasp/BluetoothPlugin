using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Bluetooth.Abstractions;

namespace Bluetooth.Plugin.Android
{
    sealed class AndroidBluetoothPairableDevice : IPairableBluetoothDevice
    {

        public BluetoothDevice BluetoothDevice { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }

        public async Task Pair()
        {

            var methods = BluetoothDevice.GetType().GetMethods().ToList();

            foreach (var methodInfo in methods)
            {
                
            }

            var method = BluetoothDevice.GetType().GetMethod("CreateBond");

            method.Invoke(BluetoothDevice, null);




        }
    }
}