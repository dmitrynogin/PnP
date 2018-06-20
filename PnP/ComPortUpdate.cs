namespace PnP
{
    public abstract class ComPortUpdate
    {
        public ComPortUpdate(ComPort port) => Port = port;
        public ComPort Port { get; }
    }

    public class ComPortAddition : ComPortUpdate
    {
        public ComPortAddition(ComPort port) : base(port) { }
        public override string ToString() => $"{Port} was added.";
    }

    public class ComPortRemoval : ComPortUpdate
    {
        public ComPortRemoval(ComPort port) : base(port) { }
        public override string ToString() => $"{Port} was removed.";
    }
}
