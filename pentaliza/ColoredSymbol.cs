﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Coloredsymbol
{
    class ColoredSymbol//holding a char and the color of that char to be displed with (this sould be an struct)
    {
        Char symbol;
        public string SymbolS { get { return "" + symbol; } }
        public Char Symbol { get { return symbol; } }
        Color color;
        public Color Color { get { return color; } }
        public ColoredSymbol(Char symbol, Color color)
        {
            this.symbol = symbol;
            this.color = color;
        }
    }
}
