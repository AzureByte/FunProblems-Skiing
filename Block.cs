using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace malasiya_surfing
{
    public class Block
    {
        public Queue<Tuple<int, int>> Left;
        public Queue<Tuple<int, int>> Up;
        public Queue<Tuple<int, int>> Right;
        public Queue<Tuple<int, int>> Down;
        public int maxLength;
        
        public Block()
        {
            Left = new Queue<Tuple<int, int>>();
            Up = new Queue<Tuple<int, int>>();
            Right = new Queue<Tuple<int, int>>();
            Down = new Queue<Tuple<int, int>>();
        }
    }
}
