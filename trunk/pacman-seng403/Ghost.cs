
using System;

namespace pacman
{
	
	
	public abstract class Ghost: GameCharacter
	{
		public static readonly double SPEED = 1.0;
        protected Directions direction;
        protected bool weak;

		public Ghost() {
			radius = 4;
		}

        public abstract void screenUpdate();

        protected void move(Directions dir) {
            if (dir == Directions.UP) {
                x = (double)((((int)x) / Map.BLOCKSIZE) * Map.BLOCKSIZE + (Map.BLOCKSIZE/2));
                y -= SPEED;
                return;
            }
            else if (dir == Directions.DOWN) {
                x = (double)((((int)x) / Map.BLOCKSIZE)*Map.BLOCKSIZE + (Map.BLOCKSIZE/2));
                y += SPEED;
                return;
            }
            else if (dir == Directions.LEFT) {
                y = (double)((((int)y) / Map.BLOCKSIZE)*Map.BLOCKSIZE + (Map.BLOCKSIZE/2));
                x -= SPEED;
                return;
            }
            else if (dir == Directions.RIGHT) {
                y = (double)((((int)y) / Map.BLOCKSIZE)*Map.BLOCKSIZE + (Map.BLOCKSIZE/2));
                x += SPEED;
                return;
            }
        }
		
	}
}
