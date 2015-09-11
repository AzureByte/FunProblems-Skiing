using System;
using System.Collections.Generic;

namespace malasiya_surfing
{
    public class Block
    {
        public bool Left;
        public bool Up;
        public bool Right;
        public bool Down;
        public List<Stack<Tuple<int, int>>> Paths;
        public int maxLength;
        public int maxDepth;

        public Block()
        {
            Left = false;
            Up = false;
            Right = false;
            Down = false;
            Paths = new List<Stack<Tuple<int, int>>>();
        }
    }
}
