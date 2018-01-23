using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Bluetooth.Abstractions.Exceptions
{
    public class BluetoothDeviceNotFoundException : Exception
    {

        public BluetoothDeviceNotFoundException(string messsage) : base(messsage)
        {

        }

    }
}
