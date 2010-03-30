
using System;

namespace pacman
{
	
	
	public abstract class Ghost: GameCharacter
	{
		public static readonly double SPEED = 1.0;
        protected char direction;
        protected bool weak;

		public Ghost() {
			radius = 4;
		}

        public abstract void screenUpdate();

        protected void move(char dir) {
            if (dir == 'u') {
                x = (double)((((int)x) / Map.BLOCKSIZE) * Map.BLOCKSIZE + (Map.BLOCKSIZE/2));
                y -= SPEED;
                return;
            }
            else if (dir == 'd') {
                x = (double)((((int)x) / Map.BLOCKSIZE)*Map.BLOCKSIZE + (Map.BLOCKSIZE/2));
                y += SPEED;
                return;
            }
            else if (dir == 'l') {
                y = (double)((((int)y) / Map.BLOCKSIZE)*Map.BLOCKSIZE + (Map.BLOCKSIZE/2));
                x -= SPEED;
                return;
            }
            else if (dir == 'r') {
                y = (double)((((int)y) / Map.BLOCKSIZE)*Map.BLOCKSIZE + (Map.BLOCKSIZE));
                x += SPEED;
                return;
            }
        }
		
	}
}
