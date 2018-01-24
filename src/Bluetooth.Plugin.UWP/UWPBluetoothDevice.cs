using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Plugin.Bluetooth.Abstractions;
using Plugin.Bluetooth.Abstractions.Exceptions;
using System.Diagnostics;
using System.Linq;
using Windows.Devices.Bluetooth;

namespace Bluetooth.Plugin.UWP
{
    public class UWPBluetoothDevice : IBluetoothDevice
    {
        /// <summary>
        /// DataWriter used to send commands easily.
        /// </summary>
        private DataWriter dataWriter;

        /// <summary>
        /// DataReader used to receive messages easily.
        /// </summary>
        private DataReader dataReader;

        private Guid CurrentUniqueIdentifier { get; set; }

        StreamSocket _socket { get; set; }

        public BluetoothDevice BluetoothDeviceReference { get; set; }

        private RfcommDeviceService _service;

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


        public bool ContainsUniqueIdentifier(string uniqueIdentifier)
        {
            return false;
        }

        public void SetUniqueIdentifier(string uniqueIdentifier)
        {

        }

        public bool ContainsUniqueIdentifier(Guid uniqueIdentifier)
        {
            return false;
        }

        public void SetUniqueIdentifier(Guid uniqueIdentifier)
        {

        }

        public async Task Connect()
        {


            try
            {

                var services = (await BluetoothDeviceReference.GetRfcommServicesAsync()).Services;
                if(services.Count > 0)
                {
                    _service = services[0];
                }
                if(_service == null)
                {
                    throw new Exception();
                }
                //lock (this)
                //{
                //    _socket = new StreamSocket();
                //}
                _socket = new StreamSocket();
                await _socket.ConnectAsync(_service.ConnectionHostName, _service.ConnectionServiceName);
                dataReader = new DataReader(_socket.InputStream);
                dataWriter = new DataWriter(_socket.OutputStream);
                _isConnected = true;

            }
            catch (Exception ex)
            {
                switch ((uint)ex.HResult)
                {
                    case (0x80070490): // ERROR_ELEMENT_NOT_FOUND
                        _isConnected = false;
                        throw new Exception("Please verify that you are running the BluetoothRfcomm server.");
                        break;
                    default:
                        _isConnected = false;
                        throw;
                }


            }
            
        }


        public async Task Disconnect()
        {

            try
            {
                await _socket.CancelIOAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            try
            {
                if(_socket != null)
                {
                    _socket.Dispose();
                    _socket = null;
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            try
            {
                if (_service != null)
                {
                    _service.Dispose();
                    _service = null;
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            _isConnected = false;

        }

        public async Task Write(string message)
        {
            try
            {
                _isWriting = true;
                dataWriter.WriteString(message);
                await dataWriter.StoreAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                _isWriting = false;
            }
            

        }

        public async Task Write(byte[] bytes)
        {
            dataWriter.WriteBytes(bytes);
            await dataWriter.StoreAsync();

        }
    }
}
