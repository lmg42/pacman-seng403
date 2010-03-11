
using System;

namespace pacman
{
	
	
	public abstract class Ghost: GameCharacter
	{
		public static readonly double SPEED = 1.0;
        protected char direction;

		public Ghost() {
			
		}

        protected void move(char dir) {
            if (dir == 'u') {
                y -= SPEED;
                return;
            }
            else if (dir == 'd') {
                y += SPEED;
                return;
            }
            else if (dir == 'l') {
                x -= SPEED;
                return;
            }
            else if (dir == 'r') {
                x += SPEED;
                return;
            }
        }
		
	}
}
