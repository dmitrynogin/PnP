using System;
using System.Management;
using System.Threading;

namespace PnP
{
    class PnPWatcher : IDisposable
    {
        const string Query =
            "SELECT * FROM Win32_DeviceChangeEvent";

        public PnPWatcher()
        {
            Watcher = new ManagementEventWatcher(Query);
            Watcher.EventArrived += (s, e) => Change(this, EventArgs.Empty);
            Watcher.Start();
        }

        ManagementEventWatcher Watcher { get; }
        SynchronizationContext Context { get; }

        public void Dispose() => Watcher.Dispose();
        public event EventHandler Change = delegate { };
    }
}
