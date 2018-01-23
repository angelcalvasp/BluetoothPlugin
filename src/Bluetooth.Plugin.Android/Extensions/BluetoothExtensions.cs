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
using Plugin.Bluetooth.Abstractions;

namespace Bluetooth.Plugin.Android.Extensions
{
    public static class BluetoothExtensions
    {

        public static IBluetoothDevice GetIDevice(this BluetoothDevice bluetoothDevice)
        {
            System.Diagnostics.Debug.WriteLine("-" + bluetoothDevice.Name + " " + bluetoothDevice.Address);
            var device = new AndroidBluetoothDevice() { Name = bluetoothDevice.Name, Address = bluetoothDevice.Address };
            device.BluetoothDevice = bluetoothDevice;
            try
            {
                switch (bluetoothDevice.Type)
                {
                    case global::Android.Bluetooth.BluetoothDeviceType.Classic:
                        device.Type = BluetoothDeviceType.Classic;
                        break;
                    case global::Android.Bluetooth.BluetoothDeviceType.Dual:
                        device.Type = BluetoothDeviceType.Dual;
                        break;
                    case global::Android.Bluetooth.BluetoothDeviceType.Le:
                        device.Type = BluetoothDeviceType.Le;
                        break;
                    case global::Android.Bluetooth.BluetoothDeviceType.Unknown:
                        device.Type = BluetoothDeviceType.Unknown;
                        break;
                }
            }
            catch (Exception ex)
            {

            }

            try
            {
                System.Diagnostics.Debug.WriteLine("Bluetooth getting Uuids with SDP");
                device.BluetoothDevice.FetchUuidsWithSdp();

                System.Diagnostics.Debug.WriteLine("Bluetooth Android: Getting UUIDs");
                var array = bluetoothDevice.GetUuids();
                if (array != null)
                {
                    var uuids = array.ToList();

                    System.Diagnostics.Debug.WriteLine("Bluetooth Android: " + uuids.Count + " found");

                    foreach (var uuid in uuids)
                    {
                        System.Diagnostics.Debug.WriteLine(device.Name + " Adding uuid " + uuid.Uuid.ToString());
                        var stringUUID = uuid.ToString();
                        device.UniqueIdentifiers.Add(Guid.Parse(stringUUID));
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Bluetooth Android: No Paired devices");
                }


            }
            catch (Java.Lang.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return device;

        }


        public static IPairableBluetoothDevice GetPairableBluetoothDevice(this BluetoothDevice bluetoothDevice)
        {
            var device = new AndroidBluetoothPairableDevice() { Name = bluetoothDevice.Name, Address = bluetoothDevice.Address };
            device.BluetoothDevice = bluetoothDevice;

            return device;

        }

    }
}