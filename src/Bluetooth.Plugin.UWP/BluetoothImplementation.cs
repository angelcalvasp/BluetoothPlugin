using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.Devices.Radios;
using Bluetooth.Plugin.UWP;
using Plugin.Bluetooth.Abstractions;

namespace Plugin.Bluetooth
{
    public class BluetoothImplementation : BaseBluetooth
    {

        public override bool IsAvailable
        {
            get
            {
                var task = Radio.GetRadiosAsync().AsTask();
                task.Wait();
                var radios = task.Result;
                return radios.FirstOrDefault(radio => radio.Kind == RadioKind.Bluetooth) != null;
            }
        }

        public override bool IsTurnedOn
        {
            get
            {   var task = Radio.GetRadiosAsync().AsTask();
                task.Wait();
                var radios = task.Result;
                var bluetoothRadio = radios.FirstOrDefault(radio => radio.Kind == RadioKind.Bluetooth);
                return bluetoothRadio != null && bluetoothRadio.State == RadioState.On;
            }
        }


        public override async Task<List<IBluetoothDevice>> GetPairedDevices()
        {
            var devices = new List<IBluetoothDevice>();

            var selector = BluetoothDevice.GetDeviceSelector();

            DeviceInformationCollection DeviceInfoCollection
                = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));

            foreach (var deviceInfo in DeviceInfoCollection)
            {
                var device = new UWPBluetoothDevice() { Name = deviceInfo.Name, Address = string.Empty };

                device.BluetoothDevice = deviceInfo;


                var id = string.Empty;
                if (deviceInfo.Id.Contains("BTHENUM#{"))
                {
                    var index = deviceInfo.Id.IndexOf("BTHENUM#{")+9;
                    id = deviceInfo.Id.Substring(index, 36);
                }

                if (!String.IsNullOrEmpty(id))
                {
                    var guid = Guid.Parse(id);
                    device.UniqueIdentifiers.Add(guid);
                }

                devices.Add(device);
                
            }

            return devices;

        }

        public override Task<List<IBluetoothDevice>> FindDevicesWithIdentifier(string identifier)
        {
            throw new NotImplementedException();
        }
    }
}
