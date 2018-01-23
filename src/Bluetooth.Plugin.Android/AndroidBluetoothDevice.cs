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
using Java.Util;
using Plugin.Bluetooth.Abstractions;
using System.IO;

namespace Bluetooth.Plugin.Android
{
    public class AndroidBluetoothDevice : IBluetoothDevice
    {

        private static UUID MY_UUID = UUID.FromString("fa87c0d0-afac-11de-8a39-0800200c9a66");
        //private BluetoothSocket Socket = null;
        private Guid CurrentUniqueIdentifier { get; set; }

        private readonly List<Guid> _uniqueIdentifiers = new List<Guid>();
        public List<Guid> UniqueIdentifiers
        {
            get { return _uniqueIdentifiers; }
        }

        public string Name { get; set; }
        public string Address { get; set; }

        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
        }

        private bool _isWriting;
        public bool IsWriting
        {
            get { return _isWriting; }
        }

        public BluetoothDeviceType Type { get; set; }
        public BluetoothDevice BluetoothDevice { get; set; }
        //private Stream outStream = null;
        //private Stream inStream = null;
        private BluetoothConnectionThread _bluetoothConnectionThread = null;

        public void SetUniqueIdentifier(Guid uniqueIdentifier)
        {
            CurrentUniqueIdentifier = uniqueIdentifier;
        }

        public Task Connect()
        {
            var tc = new TaskCompletionSource<bool>();

            _bluetoothConnectionThread = new BluetoothConnectionThread(BluetoothDevice);

            _bluetoothConnectionThread.DeviceConnected += (o, t) =>
            {
                _isConnected = true;
                tc.TrySetResult(true);
            };

            _bluetoothConnectionThread.DeviceConnectionFailed += (o, t) =>
            {
                _isConnected = false;
                tc.TrySetResult(false);
            };

            _bluetoothConnectionThread.Run();

            return tc.Task;
        }

        public Task Disconnect()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    _bluetoothConnectionThread.Disconnect();
                    _bluetoothConnectionThread = null;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    _isConnected = false;
                }
            });
        }


        public async Task Write(string message)
        {
            await Write(Encoding.UTF8.GetBytes(message));
        }

        public Task Write(byte[] bytes)
        {
            return Task.Factory.StartNew(()=> {
                _isWriting = true;
                var msgBuffer = bytes;
                try
                {
                    _bluetoothConnectionThread.Socket.OutputStream.Write(msgBuffer, 0, msgBuffer.Length);
                }
                catch (IOException ex)
                {
                    throw new System.IO.IOException(ex.Message);
                }
                finally
                {
                    _isWriting = false;
                }
                
            });

            

        }


        public bool ContainsUniqueIdentifier(string uniqueIdentifier)
        {
            return ContainsUniqueIdentifier(Guid.Parse(uniqueIdentifier));
        }

        public void SetUniqueIdentifier(string uniqueIdentifier)
        {
            SetUniqueIdentifier(Guid.Parse(uniqueIdentifier));
        }

        public bool ContainsUniqueIdentifier(Guid uniqueIdentifier)
        {
            var uuids = BluetoothDevice.GetUuids().ToList();

            foreach (var uuid in uuids)
            {
                var stringUUID = uuid.ToString();
                if (stringUUID == uniqueIdentifier.ToString())
                {
                    return true;
                }
            }


            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}