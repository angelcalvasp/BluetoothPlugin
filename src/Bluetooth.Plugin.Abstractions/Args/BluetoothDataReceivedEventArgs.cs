using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Bluetooth.Abstractions.Args
{
    public class BluetoothDataReceivedEventArgs : EventArgs
    {
        public BluetoothDataReceivedEventArgs(byte[] data)
        {
            Data = data;
        }

        public byte[] Data { get; set; }
    }
}
