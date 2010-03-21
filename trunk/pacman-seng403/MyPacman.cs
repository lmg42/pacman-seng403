using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace pacman
{
    //Assume the game map size is 50 by 50 blocks wide. Each block has size 15 which means each block is 10 by 10 pixels wide.
    //Each element is an integer whose first 4 LSB contain the possible moving directions for the pacman.
    public class MyPacman : GameCharacter
    {
        protected const int BLOCKSIZE = 10; //unit is pixel

        private int userInputXDeriction;
        private int userInputYDeriction;
        private int pacmanXDeriction;
        private int pacmanYDeriction;

        private int maparrayXPosition;
        private int maparrayYPosition;

        private int x;
        private int y;

        private Directions pacmanTowarding;

        public MyPacman(int x, int y)
        {
            this.x = x;
            this.y = y;
            userInputXDeriction = 0;
            userInputYDeriction = 0;
            pacmanXDeriction = 0;
            pacmanYDeriction = 0;
            maparrayXPosition = 0;
            maparrayYPosition = 0;
            radius = 4;
            pacmanTowarding = 0;
        }

        public void UserInput(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    userInputXDeriction = -1;
                    userInputYDeriction = 0;
                    break;
                case Keys.Right:
                    userInputXDeriction = 1;
                    userInputYDeriction = 0;
                    break;
                case Keys.Up:
                    userInputXDeriction = 0;
                    userInputYDeriction = -1;
                    break;
                case Keys.Down:
                    userInputXDeriction = 0;
                    userInputYDeriction = 1;
                    break;
            }
        }

        //public void PacmanMovement()
        public void screenUpdate()
        {
            
            //Assume in a 10 by 10 block the centre of the pacman is (5,5)
            int pacmanBlockXOrigin = x - 5;
            int pacmanBlockYOrigin = y - 5;

            Directions elementInfo = 0;
            //User inputs a reverse direction. It doesn't matter where the pacman is in the map. The deriction of pacman is reversed.
            if (((userInputXDeriction == -pacmanXDeriction) && (userInputYDeriction == 0)) || ((userInputYDeriction == -pacmanYDeriction) && (userInputXDeriction == 0)))
            {
                pacmanXDeriction = userInputXDeriction;
                pacmanYDeriction = userInputYDeriction;
                return;
            }

            //Pacman now is right in the block, otherwise whatever user inputs(except just reverse the deriction) won't be heard. Pacman will keep his latest deriction.  
            if (pacmanBlockXOrigin % BLOCKSIZE == 0 && pacmanBlockYOrigin % BLOCKSIZE == 0) //wait until pacman is right in the block
            {
                //Calculate where pacman is at the map array.
                maparrayYPosition = pacmanBlockYOrigin / BLOCKSIZE;
                maparrayXPosition = pacmanBlockXOrigin / BLOCKSIZE;

                elementInfo = Map.getMapEntry(maparrayXPosition, maparrayYPosition);

                if (!(userInputXDeriction == 0 && userInputYDeriction == 0)) //when user inputs something
                {
                    if (userInputXDeriction == -1 && userInputYDeriction == 0 && (elementInfo & Directions.LEFT) == Directions.LEFT) //pacman goes left if possible
                    {
                        pacmanYDeriction = 0;
                        pacmanXDeriction = -1;
                    }
                    if (userInputXDeriction == 0 && userInputYDeriction == -1 && (elementInfo & Directions.UP) == Directions.UP) //pacman goes up if possible
                    {
                        pacmanXDeriction = 0;
                        pacmanYDeriction = -1;
                    }
                    if (userInputXDeriction == 1 && userInputYDeriction == 0 && (elementInfo & Directions.RIGHT) == Directions.RIGHT) //pacman goes right if possible
                    {
                        pacmanYDeriction = 0;
                        pacmanXDeriction = 1;
                    }
                    if (userInputXDeriction == 0 && userInputYDeriction == 1 && (elementInfo & Directions.DOWN) == Directions.DOWN) //pacman goes down if possible
                    {
                        pacmanXDeriction = 0;
                        pacmanYDeriction = 1;
                    }
                }
                //Console.Write(pacmanYDeriction + "\n");

                if (pacmanXDeriction == -1 && pacmanYDeriction == 0 && (elementInfo & Directions.LEFT) == 0) //stop going left if there is a wall on the left
                    pacmanXDeriction = 0;
                if (pacmanXDeriction == 0 && pacmanYDeriction == -1 && (elementInfo & Directions.UP) == 0) //stop going up if there is a wall beyond
                    pacmanYDeriction = 0;
                if (pacmanXDeriction == 1 && pacmanYDeriction == 0 && (elementInfo & Directions.RIGHT) == 0) //stop going right if there is a wall on the right
                    pacmanXDeriction = 0;
                if (pacmanXDeriction == 0 && pacmanYDeriction == 1 && (elementInfo & Directions.DOWN) == 0) //stop going down if there is a wall undernies
                    pacmanYDeriction = 0;

            }

            PacmanTowarding(); //determine the direction the pacman is currently towarding to

            x = x + pacmanXDeriction; //update pacman position
            y = y + pacmanYDeriction;
        }
        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }
        
        public static bool CheckCollision(GameCharacter a, GameCharacter b)
        {
            double XDifference;
            double YDifference;

            XDifference = a.getX() - b.getX();
            YDifference = a.getY() - b.getY();

            if ((Math.Abs(XDifference) < BLOCKSIZE / 2) && (Math.Abs(YDifference) < BLOCKSIZE / 2))
                return true;
            else
                return false;
        }

        public void PacmanTowarding()
        {
            if (pacmanXDeriction == 1 && pacmanYDeriction == 0)
                pacmanTowarding = Directions.RIGHT;
            if (pacmanXDeriction == -1 && pacmanYDeriction == 0)
                pacmanTowarding = Directions.LEFT;
            if (pacmanXDeriction == 0 && pacmanYDeriction == 1)
                pacmanTowarding = Directions.DOWN;
            if (pacmanXDeriction == 0 && pacmanYDeriction == -1)
                pacmanTowarding = Directions.UP;
        }

        /*
        void main() //do some testing on pacman movement
        {
            //test if the pacman is not fully in the block, the pacmanX and Y diretion shouldn't be changed
            pacmanXDeriction = 0;
            pacmanYDeriction = 0;

            userInputXDeriction = 1;
            userInputYDeriction = 1;

            pacmanXPosition = 10;
            pacmanYPosition = 10;

            PacmanMovement();

            Console.WriteLine(pacmanXDeriction);
            Console.WriteLine(pacmanYDeriction);

            //test if the pacman is fully in the block, the pacmanX and Y diretion should be changed
            pacmanXDeriction = 0;
            pacmanYDeriction = 0;

            userInputXDeriction = 1;
            userInputYDeriction = 1;

            pacmanXPosition = 15;
            pacmanYPosition = 15;

            PacmanMovement();

            Console.WriteLine(pacmanXDeriction);
            Console.WriteLine(pacmanYDeriction);


        }
        */
    }
}
