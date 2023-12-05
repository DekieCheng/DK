using System.Collections;

namespace LNInterfaces
{
    internal interface LNCollection : IEnumerable
    {
        object Add(string sKey, object oValue);

        object Add(object oValue);

        void Remove(int index);

        void Remove(string sKey);

        int Count();

        void Clear();

        object Item(int index);

        object Item(string key);
    }
} // LNInterfaces
