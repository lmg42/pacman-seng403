
using System;

namespace pacman_seng403
{
	
	
	public class StupidGhost: Ghost
	{
        public StupidGhost(int start_x, int start_y) {
            x = start_x;
            y = start_y;
            direction = 'u'; //starting direction is up
		}
		
		public void screenUpdate() {
            int mapCurrentPosition = (int)Map.getMapEntry((int)x/10, (int)y/10);
            if (direction == 'u') {
                if ((mapCurrentPosition & (int)Directions.RIGHT) == (int)Directions.RIGHT) {
                    direction = 'r';
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.LEFT) == (int)Directions.LEFT) {
                    direction = 'l';
                    move(direction);
                }
	    	else if ((mapCurrentPosition & (int)Directions.UP) == (int)Directions.UP) {
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.DOWN) == (int)Directions.DOWN) {
                    direction = 'd';
                    move(direction);
                }
            }
            else if (direction == 'd') {
                if ((mapCurrentPosition & (int)Directions.RIGHT) == (int)Directions.RIGHT) {
                    direction = 'r';
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.LEFT) == (int)Directions.LEFT) {
                    direction = 'l';
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.DOWN) == (int)Directions.DOWN) {
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.UP) == (int)Directions.UP) {
                    direction = 'u';
                    move(direction);
                }
            }
            else if (direction == 'l') {
                if ((mapCurrentPosition & (int)Directions.UP) == (int)Directions.UP) {
                    direction = 'u';
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.DOWN) == (int)Directions.DOWN) {
                    direction = 'd';
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.LEFT) == (int)Directions.LEFT) {
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.RIGHT) == (int)Directions.RIGHT) {
                    direction = 'r';
                    move(direction);
                }
            }
            else if (direction == 'r') {
                if ((mapCurrentPosition & (int)Directions.UP) == (int)Directions.UP) {
                    direction = 'u';
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.DOWN) == (int)Directions.DOWN) {
                    direction = 'd';
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.RIGHT) == (int)Directions.RIGHT) {
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.LEFT) == (int)Directions.LEFT) {
                    direction = 'l';
                    move(direction);
                }
            }
            else {
                //ERROR - ghost is not moving
                if ((mapCurrentPosition & (int)Directions.RIGHT) == (int)Directions.RIGHT) {
                    direction = 'r';
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.UP) == (int)Directions.UP) {
                    direction = 'u';
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.DOWN) == (int)Directions.DOWN) {
                    direction = 'd';
                    move(direction);
                }
                else if ((mapCurrentPosition & (int)Directions.LEFT) == (int)Directions.LEFT) {
                    direction = 'l';
                    move(direction);
                }
                else {
                    //ERROR - ghost is in a wall
		            //reset position to center
		            x = 250;
                    y = 250;
                }
            }
            return;
		}
	}
}
