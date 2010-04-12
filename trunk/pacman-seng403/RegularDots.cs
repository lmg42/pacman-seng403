using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pacman
{
    class RegularDots
    { 
        public RegularDots()
        {
            for(int i= 0 ; i < 13 ; i++ ) {
                for (int j = 0; j < 13; j++)
                {
                    if (Map.getMapEntry(i, j) != Directions.NONE)
                    {
                        Edibles e = new Edibles(((i * 20) + 10), ((j * 20) + 10), "smallDot");
                        CurrentGameCharacters.dots_topleft.Add(e);
                    }
                }
            }
            for (int i = 0; i < 13; i++)
            {
                for (int j = 13; j < 25; j++)
                {
                    if(Map.getMapEntry(i,j) != Directions.NONE) {
                        Edibles e = new Edibles(((i * 20) + 10), ((j * 20) + 10), "smallDot");
                        CurrentGameCharacters.dots_bottomleft.Add(e);
                    }
                }
            }
            for (int i = 13; i < 25; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (Map.getMapEntry(i, j) != Directions.NONE)
                    {
                        Edibles e = new Edibles(((i * 20) + 10), ((j * 20) + 10), "smallDot");
                        CurrentGameCharacters.dots_topright.Add(e);
                    }
                }
            }
            for (int i = 13; i < 25; i++)
            {
                for (int j = 13; j < 25; j++)
                {
                    if (Map.getMapEntry(i, j) != Directions.NONE)
                    {
                        Edibles e = new Edibles(((i * 20) + 10), ((j * 20) + 10), "smallDot");
                        CurrentGameCharacters.dots_bottomright.Add(e);
                    }
                }
            }     
        }
    }
}
