
using System;

namespace pacman
{
	
	
	public abstract class Ghost: GameCharacter
	{
		public static readonly double SPEED = 1.0;
        protected Directions direction;
        protected bool weak=false;

		public Ghost() {
			radius = 4;
		}

        public void makeWeak() {
            weak = true;
        }

        public void isWeak() {
            return weak;
        }

        public void kill() {
            if (weak == true) {
                weak = false;
                //reset position to center
                y = (double)(12 * Map.BLOCKSIZE + Map.BLOCKSIZE / 2);
                x = (double)(12 * Map.BLOCKSIZE + Map.BLOCKSIZE / 2);
                direction = Directions.UP;
            }
        }

        public abstract void screenUpdate();

        /*
        //trying to make ghost turning smoother
        protected void move(Directions dir, Directions previous) {
            if (dir == Directions.UP) {
                if((x%10) < 3) {
                    
                }
                else if ((x%10) > 7) {

                }
                else {
                    x = (double)((((int)x) / Map.BLOCKSIZE) * Map.BLOCKSIZE + (Map.BLOCKSIZE / 2));
                    y -= SPEED;
                }
            }
            else if (dir == Directions.DOWN) {
                x = (double)((((int)x) / Map.BLOCKSIZE) * Map.BLOCKSIZE + (Map.BLOCKSIZE / 2));
                y += SPEED;
            }
            else if (dir == Directions.LEFT) {
                y = (double)((((int)y) / Map.BLOCKSIZE) * Map.BLOCKSIZE + (Map.BLOCKSIZE / 2));
                x -= SPEED;
            }
            else if (dir == Directions.RIGHT) {
                y = (double)((((int)y) / Map.BLOCKSIZE) * Map.BLOCKSIZE + (Map.BLOCKSIZE / 2));
                x += SPEED;
            }
        }
        */

        protected void move(Directions dir) {
            if (dir == Directions.UP) {
                x = (double)((((int)x) / Map.BLOCKSIZE) * Map.BLOCKSIZE + (Map.BLOCKSIZE/2));
                y -= SPEED;
            }
            else if (dir == Directions.DOWN) {
                x = (double)((((int)x) / Map.BLOCKSIZE)*Map.BLOCKSIZE + (Map.BLOCKSIZE/2));
                y += SPEED;
            }
            else if (dir == Directions.LEFT) {
                y = (double)((((int)y) / Map.BLOCKSIZE)*Map.BLOCKSIZE + (Map.BLOCKSIZE/2));
                x -= SPEED;
            }
            else if (dir == Directions.RIGHT) {
                y = (double)((((int)y) / Map.BLOCKSIZE)*Map.BLOCKSIZE + (Map.BLOCKSIZE/2));
                x += SPEED;
            }
        }
	}
}
