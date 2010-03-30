using System;
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
            if (((dirtopacman & Directions.RIGHT) == Directions.RIGHT) && ((mapCurrentPosition & Directions.RIGHT) == Directions.RIGHT)) {
                direction = Directions.RIGHT;
                move(direction);
            }
            else if (((dirtopacman & Directions.LEFT) == Directions.LEFT) && ((mapCurrentPosition & Directions.LEFT) == Directions.LEFT)) {
                direction = Directions.LEFT;
                move(direction);
            }
            else if (((dirtopacman & Directions.UP) == Directions.UP) && ((mapCurrentPosition & Directions.UP) == Directions.UP)) {
                direction = Directions.UP;
                move(direction);
            }
            else if (((dirtopacman & Directions.DOWN) == Directions.DOWN) && ((mapCurrentPosition & Directions.DOWN) == Directions.DOWN)) {
                direction = Directions.DOWN;
                move(direction);
            }
            else { //cant move toward pacman
                
            }
        }
    }
}
