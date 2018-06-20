using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;

namespace PnP
{
    public class ComPort : ValueObject<ComPort>
    {
        public static ComPort[] Detect()
        {
            using (var searcher = new ManagementObjectSearcher
                ("SELECT * FROM Win32_PnPEntity WHERE ClassGuid='{4d36e978-e325-11ce-bfc1-08002be10318}'"))
            {
                return searcher.Get().Cast<ManagementBaseObject>()
                    .Select(p => new ComPort($"{p["Caption"]}"))
                    .ToArray();
            }
        }

        ComPort(string description)
        {
            Description = description;
            Name = new string(
                description
                .Reverse()
                .SkipWhile(c => c != ')')
                .TakeWhile(c => c != '(')
                .Reverse()
                .ToArray())
                .TrimStart('(')
                .TrimEnd(')');
        }

        public string Description { get; }
        public string Name { get; }

        public override string ToString() => Description;
        protected override IEnumerable<object> EqualityCheckAttributes =>
            new object[] { Name, Description };
    }
}
