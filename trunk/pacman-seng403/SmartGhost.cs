﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pacman
{
    class SmartGhost: Ghost
    {
        public override void screenUpdate() {
            //find direction to pacman
            Directions mapCurrentPosition = Map.getMapEntry((int)x/Map.BLOCKSIZE, (int)y/Map.BLOCKSIZE);
            Directions dirtopacman = Directions.NONE;
            if (CurrentGameCharacters.pacman.getX() < this.x) {
                dirtopacman = Directions.LEFT;
            }
            else if (CurrentGameCharacters.pacman.getX() > this.x) {
                dirtopacman = Directions.RIGHT;
            }
            if (CurrentGameCharacters.pacman.getY() < this.y) {
                dirtopacman = (dirtopacman | Directions.UP);
            }
            else if (CurrentGameCharacters.pacman.getY() > this.y) {
                dirtopacman = (dirtopacman | Directions.DOWN);
            }

            //move ghost in direction closest to pacman
            if (((dirtopacman & Directions.UP) == Directions.UP) && mapCurrentPosition ) {

            }
        }
    }
}