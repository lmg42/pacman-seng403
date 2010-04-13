
using System;

namespace pacman
{
	
	
	public class Ghost: GameCharacter
	{
		public static readonly double SPEED = 1.0;
        protected Directions direction;
        protected bool weak=false;
        protected bool smart;
        protected int numScreenUpdates;
        protected bool currentlySmart;
        protected uint smartCounter;

        public Ghost(int start_x, int start_y, Directions dir, bool isSmart)
        {
            smart = isSmart;
            currentlySmart = isSmart;
            smartCounter = 0;
			radius = 4;
            x = start_x*Map.BLOCKSIZE + (Map.BLOCKSIZE/2);
            y = start_y*Map.BLOCKSIZE + (Map.BLOCKSIZE/2);
            direction = dir;
            numScreenUpdates = 20;
		}

        public void makeWeak() {
            weak = true;
        }

        public void makeStrong()
        {
            weak = false;
        }

        public void makeSmart()
        {
            smart = true;
        }

        public void makeStupid()
        {
            smart = false;
        }

        public bool isWeak() {
            return weak;
        }

        public void kill() {
            weak = false;
            //reset position to center
            y = (double)(12 * Map.BLOCKSIZE + Map.BLOCKSIZE / 2);
            x = (double)(12 * Map.BLOCKSIZE + Map.BLOCKSIZE / 2);
            direction = Directions.UP;
        }

        public void screenUpdate()
        {
            if (currentlySmart && !weak)
            {
                if (numScreenUpdates < 20)
                {
                    move(direction, false);
                    numScreenUpdates++;
                }
                else if (numScreenUpdates == 20)
                {
                    //find direction to pacman
                    Directions mapCurrentPosition = Map.getMapEntry((int)x / Map.BLOCKSIZE, (int)y / Map.BLOCKSIZE);
                    Directions dirtopacman = Directions.NONE;
                    if (CurrentGameCharacters.pacman.getX() < this.x)
                    {
                        dirtopacman = Directions.LEFT;
                    }
                    else if (CurrentGameCharacters.pacman.getX() > this.x)
                    {
                        dirtopacman = Directions.RIGHT;
                    }
                    if (CurrentGameCharacters.pacman.getY() < this.y)
                    {
                        dirtopacman = (dirtopacman | Directions.UP);
                    }
                    else if (CurrentGameCharacters.pacman.getY() > this.y)
                    {
                        dirtopacman = (dirtopacman | Directions.DOWN);
                    }

                    //move ghost in direction closest to pacman
                    if (((dirtopacman & Directions.RIGHT) == Directions.RIGHT) && ((mapCurrentPosition & Directions.RIGHT) == Directions.RIGHT))
                    {
                        direction = Directions.RIGHT;
                        move(direction, true);
                    }
                    else if (((dirtopacman & Directions.LEFT) == Directions.LEFT) && ((mapCurrentPosition & Directions.LEFT) == Directions.LEFT))
                    {
                        direction = Directions.LEFT;
                        move(direction, true);
                    }
                    else if (((dirtopacman & Directions.UP) == Directions.UP) && ((mapCurrentPosition & Directions.UP) == Directions.UP))
                    {
                        direction = Directions.UP;
                        move(direction, true);
                    }
                    else if (((dirtopacman & Directions.DOWN) == Directions.DOWN) && ((mapCurrentPosition & Directions.DOWN) == Directions.DOWN))
                    {
                        direction = Directions.DOWN;
                        move(direction, true);
                    }
                    else
                    { //cant move toward pacman
                        if ((mapCurrentPosition & Directions.UP) == Directions.UP)
                        {
                            direction = Directions.UP;
                            move(direction, true);
                        }
                        else if ((mapCurrentPosition & Directions.RIGHT) == Directions.RIGHT)
                        {
                            direction = Directions.RIGHT;
                            move(direction, true);
                        }
                        else if ((mapCurrentPosition & Directions.LEFT) == Directions.LEFT)
                        {
                            direction = Directions.LEFT;
                            move(direction, true);
                        }

                        else if ((mapCurrentPosition & Directions.DOWN) == Directions.DOWN)
                        {
                            direction = Directions.DOWN;
                            move(direction, true);
                        }
                    }
                    numScreenUpdates = 0;
                }
            }
            else //not smart
            {
                if (numScreenUpdates < 20)
                {
                    move(direction, false);
                    numScreenUpdates++;
                }
                else if (numScreenUpdates == 20)
                {
                    Directions mapCurrentPosition = Map.getMapEntry((int)x / Map.BLOCKSIZE, (int)y / Map.BLOCKSIZE);
                    Directions dir = Directions.NONE; //all possible directions ghost can move in
                    if (direction == Directions.UP)
                    {
                        if ((mapCurrentPosition & Directions.RIGHT) == Directions.RIGHT)
                        {
                            dir = (dir | Directions.RIGHT);
                        }
                        if ((mapCurrentPosition & Directions.LEFT) == Directions.LEFT)
                        {
                            dir = (dir | Directions.LEFT);
                        }
                        if ((mapCurrentPosition & Directions.UP) == Directions.UP)
                        {
                            dir = (dir | Directions.UP);
                        }
                    }
                    else if (direction == Directions.DOWN)
                    {
                        if ((mapCurrentPosition & Directions.RIGHT) == Directions.RIGHT)
                        {
                            dir = (dir | Directions.RIGHT);
                        }
                        if ((mapCurrentPosition & Directions.LEFT) == Directions.LEFT)
                        {
                            dir = (dir | Directions.LEFT);
                        }
                        if ((mapCurrentPosition & Directions.DOWN) == Directions.DOWN)
                        {
                            dir = (dir | Directions.DOWN);
                        }
                    }
                    else if (direction == Directions.LEFT)
                    {
                        if ((mapCurrentPosition & Directions.DOWN) == Directions.DOWN)
                        {
                            dir = (dir | Directions.DOWN);
                        }
                        if ((mapCurrentPosition & Directions.UP) == Directions.UP)
                        {
                            dir = (dir | Directions.UP);
                        }
                        if ((mapCurrentPosition & Directions.LEFT) == Directions.LEFT)
                        {
                            dir = (dir | Directions.LEFT);
                        }
                    }
                    else if (direction == Directions.RIGHT)
                    {
                        if ((mapCurrentPosition & Directions.DOWN) == Directions.DOWN)
                        {
                            dir = (dir | Directions.DOWN);
                        }
                        if ((mapCurrentPosition & Directions.UP) == Directions.UP)
                        {
                            dir = (dir | Directions.UP);
                        }
                        if ((mapCurrentPosition & Directions.RIGHT) == Directions.RIGHT)
                        {
                            dir = (dir | Directions.RIGHT);
                        }
                    }
                    //randomly choose one of the possible directions
                    //if chosen direction is impossible, choose different direction
                    Random random = new Random();
                    int shiftBy = random.Next(0, 4);

                    for (int i = 0; i <= 4; i++)
                    {
                        if ((((int)dir >> shiftBy) & 0x1) == 0x1)
                        {
                            move((Directions)(0x1 << shiftBy), true);
                            break;
                        }
                        else
                        {
                            shiftBy = ((shiftBy + 1) % 5);
                        }
                    }

                    numScreenUpdates = 0;
                }
                else
                {
                    //this should never happen
                    //put error handling code here
                    Console.WriteLine("StupidGhost.numScreenUpdates is over 20");
                }
            }
            if (smart) {
                smartCounter++;
                if (smartCounter >= 100) {
                    smartCounter = 0;
                    currentlySmart = !currentlySmart;
                }
            }
        }

        protected void move(Directions dir, bool center) {
            direction = dir;
            if (dir == Directions.UP) {
                if(center)
                    x = (double)((((int)x) / Map.BLOCKSIZE) * Map.BLOCKSIZE + (Map.BLOCKSIZE/2));
                y -= SPEED;
            }
            else if (dir == Directions.DOWN) {
                if(center)
                    x = (double)((((int)x) / Map.BLOCKSIZE)*Map.BLOCKSIZE + (Map.BLOCKSIZE/2));
                y += SPEED;
            }
            else if (dir == Directions.LEFT) {
                if(center)
                    y = (double)((((int)y) / Map.BLOCKSIZE)*Map.BLOCKSIZE + (Map.BLOCKSIZE/2));
                x -= SPEED;
            }
            else if (dir == Directions.RIGHT) {
                if(center)
                    y = (double)((((int)y) / Map.BLOCKSIZE)*Map.BLOCKSIZE + (Map.BLOCKSIZE/2));
                x += SPEED;
            }
        }

	}
}
