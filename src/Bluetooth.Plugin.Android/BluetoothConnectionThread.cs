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
using Java.IO;
using Java.Lang;
using Plugin.Bluetooth.Abstractions.Exceptions;
using Exception = System.Exception;
using Java.Util;
using Plugin.Bluetooth.Abstractions.Args;

namespace Bluetooth.Plugin.Android
{
    public class BluetoothConnectionThread : Thread
    {
        volatile bool running = true;
        public BluetoothSocket Socket;
        private BluetoothDevice Device;
        private BluetoothAdapter mBluetoothAdapter;
        private int Port = 1;

        public BluetoothConnectionThread(BluetoothDevice device)
        {
            // Use a temporary object that is later assigned to mmSocket,
            // because mmSocket is final
            BluetoothSocket tmp = null;
            Device = device;
            mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            if (mBluetoothAdapter == null)
            {
                // Device does not support Bluetooth
                throw new System.Exception("No bluetooth device found");
            }


        }

        public override void Run()
        {
            if (IsInterrupted)
                return;
            try
            {
                ProbeConnection();
            }
            catch (IOException connectException)
            {

                // Unable to connect; close the socket and get out
                try
                {
                    Socket.Close();
                }
                catch (IOException closeException)
                {
                    
                }
                throw new BluetoothDeviceNotFoundException(connectException.Message);
                return;
            }

        }

        private void ProbeConnection()
        {
            ParcelUuid[] parcelUuids;
            parcelUuids = Device.GetUuids();
            bool isConnected = false;



            if (parcelUuids == null)
            {
                parcelUuids = new ParcelUuid[1];
                parcelUuids[0] = new ParcelUuid(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
            }

            mBluetoothAdapter.CancelDiscovery();

            foreach (var parcelUuid in parcelUuids)
            {
                
                //METHOD A

                try
                {
                    var method = Device.GetType().GetMethod("createRfcommSocket");
                    Socket = (BluetoothSocket)method.Invoke(Device, new object[] { Port });
                    Socket.Connect();
                    isConnected = true;
                    DoDeviceConnected();
                    break;
                }
                catch (Exception)
                {

                }

                //METHOD B

                try
                {
                    var method = Device.GetType().GetMethod("createInsecureRfcommSocket");
                    Socket = (BluetoothSocket)method.Invoke(Device, new object[] { Port });
                    Socket.Connect();
                    isConnected = true;
                    DoDeviceConnected();
                    break;
                }
                catch (Exception)
                {

                }

            }

            if (!isConnected)
            {
                //METHOD C

                try
                {
                    IntPtr createRfcommSocket = JNIEnv.GetMethodID(Device.Class.Handle, "createRfcommSocket", "(I)Landroid/bluetooth/BluetoothSocket;");
                    IntPtr _socket = JNIEnv.CallObjectMethod(Device.Handle, createRfcommSocket, new global::Android.Runtime.JValue(Port));
                    Socket = Java.Lang.Object.GetObject<BluetoothSocket>(_socket, JniHandleOwnership.TransferLocalRef);
                    Socket.Connect();
                    DoDeviceConnected();
                }
                catch (IOException connectException)
                {

                    // Unable to connect; close the socket and get out
                    try
                    {
                        Socket.Close();
                    }
                    catch (IOException closeException)
                    {

                    }
                    DoDeviceConnectionFailed();
                }
            }

            if(Socket != null && Socket.IsConnected)
            {

            }

        }

        public event EventHandler DeviceConnected;
        private void DoDeviceConnected()
        {

            if (DeviceConnected != null)
            {
                DeviceConnected(this, new EventArgs());
            }
        }

        public event EventHandler DeviceConnectionFailed;
        private void DoDeviceConnectionFailed()
        {
            if (DeviceConnectionFailed != null)
            {
                DeviceConnectionFailed(this, new EventArgs());
            }
        }

        public event EventHandler<BluetoothDataReceivedEventArgs> ReceivedData;
        private void DoReceivedData(byte[] data)
        {
            if (DeviceConnectionFailed != null)
            {
                ReceivedData(this, new BluetoothDataReceivedEventArgs(data));
            }
        }

        /** Will cancel an in-progress connection, and close the socket */
        public void Disconnect()
        {
            try
            {
                Socket.Close();
                Socket = null;
                this.Interrupt();
            }
            catch (IOException e) {

            }
        }

        //Method obtained from
        //https://stackoverflow.com/questions/44593085/android-bluetooth-how-to-read-incoming-data
        private async void ReceiveData(BluetoothSocket socket)
        {
            var socketInputStream = socket.InputStream;
            byte[] buffer = new byte[256];
            int bytes;

            // Keep looping to listen for received messages
            while (!IsInterrupted)
            {

                byte[] result;
                int length;
                length = (int)socketInputStream.Length;
                result = new byte[length];
                await socketInputStream.ReadAsync(result, 0, length);
                if(length>0)
                DoReceivedData(result);

            }

        }


    }
}