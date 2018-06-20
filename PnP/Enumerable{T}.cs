using System.Collections;
using System.Collections.Generic;

namespace PnP
{
    public abstract class Enumerable<T> : IEnumerable<T>
    {
        public abstract IEnumerator<T> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}
