
using System;

namespace pacman
{
	
	
	public class StupidGhost: Ghost
	{
        public StupidGhost(int start_x, int start_y, Directions dir) {
            x = start_x*Map.BLOCKSIZE + (Map.BLOCKSIZE/2);
            y = start_y*Map.BLOCKSIZE + (Map.BLOCKSIZE/2);
            direction = dir;
		}
		
		public override void screenUpdate() {
            int mapCurrentPosition = (int)Map.getMapEntry((int)x/Map.BLOCKSIZE, (int)y/Map.BLOCKSIZE);
            if (direction == Directions.UP) {
                if ((mapCurrentPosition & (int)Directions.UP) == (int)Directions.UP) {
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.RIGHT) == (int)Directions.RIGHT) {
                    direction = Directions.RIGHT;
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.LEFT) == (int)Directions.LEFT) {
                    direction = Directions.LEFT;
                    move(direction);
                }
	    	    
                else if ((mapCurrentPosition & (int)Directions.DOWN) == (int)Directions.DOWN) {
                    direction = Directions.DOWN;
                    move(direction);
                }
            }
            else if (direction == Directions.DOWN) {
                if ((mapCurrentPosition & (int)Directions.DOWN) == (int)Directions.DOWN) {
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.RIGHT) == (int)Directions.RIGHT) {
                    direction = Directions.RIGHT;
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.LEFT) == (int)Directions.LEFT) {
                    direction = Directions.LEFT;
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.UP) == (int)Directions.UP) {
                    direction = Directions.UP;
                    move(direction);
                }
            }
            else if (direction == Directions.LEFT) {
                if ((mapCurrentPosition & (int)Directions.LEFT) == (int)Directions.LEFT) {
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.UP) == (int)Directions.UP) {
                    direction = Directions.UP;
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.DOWN) == (int)Directions.DOWN) {
                    direction = Directions.DOWN;
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.RIGHT) == (int)Directions.RIGHT) {
                    direction = Directions.RIGHT;
                    move(direction);
                }
            }
            else if (direction == Directions.RIGHT) {
                if ((mapCurrentPosition & (int)Directions.RIGHT) == (int)Directions.RIGHT) {
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.UP) == (int)Directions.UP) {
                    direction = Directions.UP;
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.DOWN) == (int)Directions.DOWN) {
                    direction = Directions.DOWN;
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.LEFT) == (int)Directions.LEFT) {
                    direction = Directions.LEFT;
                    move(direction);
                }
            }
            else {
                //ERROR - ghost is not moving
                if ((mapCurrentPosition & (int)Directions.RIGHT) == (int)Directions.RIGHT) {
                    direction = Directions.RIGHT;
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.UP) == (int)Directions.UP) {
                    direction = Directions.UP;
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.DOWN) == (int)Directions.DOWN) {
                    direction = Directions.DOWN;
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.LEFT) == (int)Directions.LEFT) {
                    direction = Directions.LEFT;
                    move(direction);
                }
                else {
                    //ERROR - ghost is in a wall
		            //reset position to center
		            x = 250;
                    y = 250;
                    direction = Directions.UP;
                }
            }
            return;
		}
	}
}
