using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plugin.Bluetooth.Abstractions
{
    public abstract class BaseBluetooth : IBluetooth, IDisposable
    {
        public abstract bool IsAvailable
        {
            get;
        }

        /// <summary>
        /// Gets if bluetooth is available
        /// </summary>
        public abstract bool IsTurnedOn
        {
            get;
        }

        /// <summary>
        /// Gets if there is an active internet connection
        /// </summary>
        public abstract Task<List<IBluetoothDevice>> GetPairedDevices();

        /// <summary>
        /// Gets if there is an active internet connection
        /// </summary>
        public abstract Task<List<IBluetoothDevice>> FindDevicesWithIdentifier(string identifier);

        /// <summary>
        /// Dispose of class and parent classes
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose up
        /// </summary>
        ~BaseBluetooth()
        {
            Dispose(false);
        }
        private bool disposed = false;

        /// <summary>
        /// Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose only
                }

                disposed = true;
            }
        }

        
    }
}
