using System;
using System.Collections.Generic;
using System.Text;

namespace ExpertSokoban
{
    public class ESPackedBooleans
    {
        private byte[] arr;
        public ESPackedBooleans(int length) { arr = new byte[(length+7)/8]; }
        public bool get(int index) { return (arr[index/8] & (1 << (index % 8))) != 0; }
        public void set(int index, bool value)
        {
            if (value)
                arr[index/8] |= (byte) (1 << (index % 8));
            else
                arr[index/8] &= (byte) (~(1 << (index % 8)));
        }
    }
}
