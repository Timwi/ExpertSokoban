using System;
using System.Collections.Generic;
using System.Text;

namespace ExpertSokoban
{
    public class ESIntQueue
    {
        private int[] contents;
        private int firstElement, lastElementPlusOne;
        private bool empty;

        public ESIntQueue()
        {
            contents = new int[64];
            firstElement = 0;
            lastElementPlusOne = 0;
            empty = true;
        }
        public void add(int i)
        {
            if (firstElement == lastElementPlusOne && !empty)
            {
                int newLength = 2*contents.Length;
                int[] newContents = new int[newLength];
                for (int j = 0; j < firstElement; j++)
                    newContents[j] = contents[j];
                for (int j = firstElement; j < contents.Length; j++)
                    newContents[j+contents.Length] = contents[j];
                firstElement += contents.Length;
                contents = newContents;
            }
            contents[lastElementPlusOne] = i;
            lastElementPlusOne++;
            if (lastElementPlusOne == contents.Length) lastElementPlusOne = 0;
            empty = false;
        }
        public int extract()
        {
            int ret = contents[firstElement];
            firstElement++;
            if (firstElement == contents.Length) firstElement = 0;
            if (firstElement == lastElementPlusOne) empty = true;
            return ret;
        }
        public int size()
        {
            if (empty) return 0;
            int i = lastElementPlusOne - firstElement;
            return (i <= 0) ? i + contents.Length : i;
        }
        public bool isEmpty() { return empty; }
    }
}
