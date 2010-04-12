using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pacman
{
    class BigDots
    {
        public BigDots()
        {
            Edibles a = new Edibles(130,170,"bigDot");
            Edibles b = new Edibles(370,170,"bigDot");
            Edibles c = new Edibles(190,350 ,"bigDot");
            Edibles d = new Edibles(470,470,"bigDot");
            CurrentGameCharacters.bigdots.Add(a);
            CurrentGameCharacters.bigdots.Add(b);
            CurrentGameCharacters.bigdots.Add(c);
            CurrentGameCharacters.bigdots.Add(d);
        }
    }
}
