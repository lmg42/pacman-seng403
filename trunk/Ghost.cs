
using System;

namespace pacman_seng403
{
	
	
	public abstract class Ghost: GameCharacter
	{
		public static readonly double SPEED = 1.0;
        protected char direction;

		public Ghost() {
			radius = 4;
		}

        protected void move(char dir) {
            if (dir == 'u') {
                x = (double)(((int)x) / 10 + 5);
                y -= SPEED;
                return;
            }
            else if (dir == 'd') {
                x = (double)(((int)x) / 10 + 5);
                y += SPEED;
                return;
            }
            else if (dir == 'l') {
                y = (double)(((int)y) / 10 + 5);
                x -= SPEED;
                return;
            }
            else if (dir == 'r') {
                y = (double)(((int)y) / 10 + 5);
                x += SPEED;
                return;
            }
        }
		
	}
}
