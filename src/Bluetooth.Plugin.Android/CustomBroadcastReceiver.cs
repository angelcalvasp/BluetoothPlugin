using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Bluetooth.Plugin.Android.Extensions;
using Plugin.Bluetooth.Abstractions;
using Plugin.Bluetooth.Abstractions.Args;

namespace Bluetooth.Plugin.Android
{
    public class CustomBroadcastReceiver : BroadcastReceiver
    {

        public event EventHandler DeviceDiscoveryStarted;

        public event EventHandler DeviceDiscoverEnded;

        public event EventHandler<DeviceFoundEventArgs> DeviceDiscovered;

        public CustomBroadcastReceiver()
        {

        }

        public override void OnReceive(Context context, Intent intent)
        {

            String action = intent.Action;

            if (BluetoothAdapter.ActionDiscoveryStarted.Equals(action))
            {
                DoDeviceDiscoveryStarted();
            }
            else if (BluetoothAdapter.ActionDiscoveryFinished.Equals(action))
            {
                DoDeviceDiscoverEnded();
            }
            else if (BluetoothDevice.ActionFound.Equals(action))
            {
                System.Diagnostics.Debug.WriteLine("Found a device");
                BluetoothDevice device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
                DoOnDeviceDiscovered(device.GetPairableBluetoothDevice());

            }
        }

        public void DoDeviceDiscoveryStarted()
        {
            if (DeviceDiscoveryStarted != null)
            {
                DeviceDiscoveryStarted(this, new EventArgs());
            }
        }

        public void DoDeviceDiscoverEnded()
        {
            if (DeviceDiscoverEnded != null)
            {
                DeviceDiscoverEnded(this, new EventArgs());
            }
        }

        public void DoOnDeviceDiscovered(IPairableBluetoothDevice device)
        {
            if (DeviceDiscovered != null)
            {
                DeviceDiscovered(this, new DeviceFoundEventArgs()
                {
                    Device = device
                });
            }
        }



    }
}