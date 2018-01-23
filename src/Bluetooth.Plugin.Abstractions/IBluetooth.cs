using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plugin.Bluetooth.Abstractions
{
    /// <summary>
    /// Interface for Bluetooth
    /// </summary>
    public interface IBluetooth : IDisposable
    {

        /// <summary>
        /// Gets if bluetooth is available
        /// </summary>
        bool IsAvailable { get; }

        /// <summary>
        /// Gets if bluetooth is on
        /// </summary>
        bool IsTurnedOn { get; }

        Task<List<IBluetoothDevice>> GetPairedDevices();

        Task<List<IBluetoothDevice>> FindDevicesWithIdentifier(string identifier);

    }
}
