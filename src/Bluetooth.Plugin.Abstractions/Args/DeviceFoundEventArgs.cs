using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Bluetooth.Abstractions.Args
{
    public class DeviceFoundEventArgs : System.EventArgs
    {
        public IPairableBluetoothDevice Device { get; set; }

    }
}
