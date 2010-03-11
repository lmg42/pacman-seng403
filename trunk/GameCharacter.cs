using System;
using System.Collections.Generic;
using System.Text;

namespace pacman
{
    public abstract class GameCharacter
    {
        protected double x;
        protected double y;
        protected int radius;

        public int getX() {
            return (int)x;
        }
		
		public int getY() {
			return (int)y;
		}
		
		public int getRadius() {
			return radius;
		}
    }
}
