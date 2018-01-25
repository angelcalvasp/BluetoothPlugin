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
using Windows.Devices.SerialCommunication;
using Windows.Networking.Connectivity;
using System.Text.RegularExpressions;

namespace Plugin.Bluetooth
{
    public class BluetoothImplementation : BaseBluetooth
    {

        public override bool IsSupportedByDevice
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

            var bluetoothDevices = await GetBluetoothPairedDevices();

            var serialPortDevices = await GetSerialPortDevices();

            foreach (var deviceInfo in bluetoothDevices)
            {
                

                var address = GetAddress(deviceInfo);

                if(!string.IsNullOrEmpty(address))
                {
                    var bluetoothDevice = new UWPBluetoothDevice()
                    {
                        Name = deviceInfo.Name,
                        Address = address
                    };

                    var trimmedAddress = address.Replace(":", "");
                    ulong t = Convert.ToUInt64(trimmedAddress, 16);

                    var bluetoothDeviceQuery = await BluetoothDevice.FromBluetoothAddressAsync(t);
                    bluetoothDevice.BluetoothDeviceReference = bluetoothDeviceQuery;

                    var serialQuery = serialPortDevices.FirstOrDefault(x => x.Id.Contains(deviceInfo.Id));
                    if (serialQuery != null)
                    {
                        var identifiers = GetUniqueIdentifiers(serialQuery);
                        bluetoothDevice.UniqueIdentifiers.AddRange(identifiers);
                    }

                    devices.Add(bluetoothDevice);
                }

                
                
            }

            return devices;

        }

        //Source https://stackoverflow.com/questions/44308078/getting-the-com-port-name-for-a-known-bluetooth-device-in-uwp
        //modified 
        private string GetAddress(DeviceInformation deviceInfo)
        {
            // Example Bluetooth DeviceInfo.Id: "Bluetooth#Bluetooth9c:b6:d0:d6:d7:56-00:07:80:cb:56:6d"
            // from device with Association Endpoint Address: "00:07:80:cb:56:6d"

            var propertysegment = GetPropertySegmentThatContainsPartiallyKey(deviceInfo,"Bluetooth");

            var lengthOfTrailingAssociationEndpointAddresss = (2 * 6) + 5;
            var bluetoothDeviceAddress = propertysegment.Substring(propertysegment.Length - lengthOfTrailingAssociationEndpointAddresss, lengthOfTrailingAssociationEndpointAddresss);
            return bluetoothDeviceAddress;
        }

        private List<Guid> GetUniqueIdentifiers(DeviceInformation deviceInfo)
        {
            var result = new List<Guid>();
            var propertysegment = GetPropertySegmentThatContainsPartiallyKey(deviceInfo, "RFCOMM");

            var identifiers = Between(propertysegment,"{","}");

            if (!string.IsNullOrEmpty(identifiers))
            {
                var split = identifiers.Split(',');
                foreach(var id in split)
                {
                    result.Add(Guid.Parse(id));
                }
            }

            return result;
        }

        private string Between(string source, string left, string right)
        {
            return Regex.Match(
                    source,
                    string.Format("{0}(.*){1}", left, right))
                .Groups[1].Value;
        }

        private string GetPropertySegmentThatContainsPartiallyKey(DeviceInformation deviceInfo,string key)
        {
            if (string.IsNullOrEmpty(deviceInfo.Id))
                return null;
            var split = deviceInfo.Id.Split('#').Where(x=>x.Contains(":"));

            if(!split.Any())
            {
                return null;
            }

            return split.FirstOrDefault(x=>x.Contains(key));
        }

        public override Task<List<IBluetoothDevice>> FindDevicesWithIdentifier(string identifier)
        {
            throw new NotImplementedException();
        }

        private async Task<List<DeviceInformation>> GetBluetoothPairedDevices()
        {
            return await GetDevices(BluetoothDevice.GetDeviceSelector());
        }

        private async Task<List<DeviceInformation>> GetSerialPortDevices()
        {
            return await GetDevices(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));
        }

        private async Task<List<DeviceInformation>> GetDevices(string selector)
        {
            return (await DeviceInformation.FindAllAsync(selector)).ToList();
        }

    }
}
