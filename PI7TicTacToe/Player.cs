using System;
using System.Collections.Generic;
using System.Text;

namespace PI7TicTacToe
{
    class Player
    {
        public string name;
        public bool turn;
        public string symbol;
        public bool ai;

        public Player(string Name, bool Turn, string Symbol, bool Ai)
        {
            name = Name;
            turn = Turn;
            symbol = Symbol;
            ai = Ai;
        }
    }
}
