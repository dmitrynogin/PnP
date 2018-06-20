using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;

namespace PnP
{
    public class ComPortList : Enumerable<ComPort>, IObservable<ComPortUpdate>, IDisposable
    {
        public ComPortList()
        {
            Watcher = new PnPWatcher();
            Items = new List<ComPort>();
            Subject = new Subject<ComPortUpdate>();
            Watcher.Change += (s, e) => Sync();
            Sync();
        }

        PnPWatcher Watcher { get; }
        List<ComPort> Items { get; }
        Subject<ComPortUpdate> Subject { get; }

        public void Dispose()
        {
            Watcher.Dispose();
            Subject.Dispose();
        }
                
        public override IEnumerator<ComPort> GetEnumerator()
        {
            lock (Items)
                return Items.ToList().GetEnumerator();
        }

        public IDisposable Subscribe(IObserver<ComPortUpdate> observer) =>
            Subject.Subscribe(observer);

        void Sync()
        {
            lock (Items)
            {
                var items = ComPort.Detect();

                foreach (var port in Items.Except(items).ToArray())
                {                    
                    Items.Remove(port);
                    Subject.OnNext(new ComPortRemoval(port));
                }

                foreach (var port in items.Except(Items).ToArray())
                {
                    Items.Add(port);
                    Subject.OnNext(new ComPortAddition(port));
                }
            }
        }
    }
}
