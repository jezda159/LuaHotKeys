using System;
using System.Collections.Generic;
using System.Linq;

namespace LuaHotKey.Classes
{
    /* All credit goes to this StackOverflow answer: 
     *  >> https://stackoverflow.com/a/32671480 <<
     */
    class Bictionary<T1, T2> : Dictionary<T1, T2>
    {
        public T1 this[T2 index]
        {
            get
            {
                if (!this.Any(x => x.Value.Equals(index)))
                    throw new System.Collections.Generic.KeyNotFoundException();
                return this.First(x => x.Value.Equals(index)).Key;
            }
        }
    }
}
