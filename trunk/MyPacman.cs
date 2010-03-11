using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using pacman;

namespace SENG403GroupProject2010
{
    //Assume the game map size is 15 by 15 blocks wide. Each block has size 15 which means each block is 15 by 15 pixels wide.
    //The information of each block is stored in an element in a one dimentional array. The size of the array is 15*15=225(elements).
    //Each element is an integer whose first 4 LSB contain the possible moving directions for the pacman.
    public class MyPacman
    {
        protected int[] mapArray;
        protected const int BLOCKSIZE = 15; //unit is pixel
        protected const int MAPSIZE = 15; //unit is block
        protected Bitmap pacmanImage;
        protected int userInputXDeriction;
        protected int userInputYDeriction;
        private int pacmanXDeriction;
        private int pacmanYDeriction;
        private int pacmanXPosition;
        private int pacmanYPosition;
        private int maparrayPosition;

        public MyPacman()
        {
            mapArray = new int[225];
            userInputXDeriction = 0;
            userInputYDeriction = 0;
            pacmanXDeriction = 0;
            pacmanYDeriction = 0;
            pacmanXPosition = 0;
            pacmanYPosition = 0;
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
                    userInputYDeriction = 1;
                    break;
                case Keys.Down:
                    userInputXDeriction = 0;
                    userInputYDeriction = -1;
                    break;
            }
        }
        public void PacmanMovement()
        {
            int elementInfo = 0;
            //User inputs a reverse direction. It doesn't matter where the pacman is in the map. The deriction of pacman is reversed.
            if (((userInputXDeriction == -pacmanXDeriction) && (userInputYDeriction == 0)) || ((userInputYDeriction == -pacmanYDeriction) && (userInputXDeriction == 0)))
            {
                pacmanXDeriction = userInputXDeriction;
                pacmanYDeriction = userInputYDeriction;
                return;
            }
            //Pacman now is right in the block, otherwise whatever user inputs(except just reverse the deriction) won't be heard. Pacman will keep his latest deriction.  
            if (pacmanXPosition % BLOCKSIZE == 0 && pacmanYPosition % BLOCKSIZE == 0) //wait until pacman is right in the block
            {
                //Calculate where pacman is at the map array. Array position = raw number * map size + column number. 
                maparrayPosition = MAPSIZE * (pacmanYPosition / BLOCKSIZE) + pacmanXPosition / BLOCKSIZE;
                elementInfo = mapArray[maparrayPosition]; //Assume mapArray is the map created in class Form

                if (!(userInputXDeriction == 0 && userInputYDeriction == 0)) //when user inputs something
                {
                    //elementInfo = dxxxx = dot down right up left
                    //0 = no wall/no dot ; 1 = wall/dot exists
                    if (userInputXDeriction == -1 && userInputYDeriction == 0 && (elementInfo & 0x1) == 0) //pacman goes left if possible
                        pacmanXDeriction = -1;
                    if (userInputXDeriction == 0 && userInputYDeriction == 1 && (elementInfo & 0x2) == 0) //pacman goes up if possible
                        pacmanYDeriction = 1;
                    if (userInputXDeriction == 1 && userInputYDeriction == 0 && (elementInfo & 0x4) == 0) //pacman goes right if possible
                        pacmanXDeriction = 1;
                    if (userInputXDeriction == 0 && userInputYDeriction == -1 && (elementInfo & 0x8) == 0) //pacman goes down if possible
                        pacmanYDeriction = -1;
                }

                if (pacmanXDeriction == -1 && pacmanYDeriction == 0 && (elementInfo & 0x1) == 1) //stop going left if there is a wall on the left
                    pacmanXDeriction = 0;
                if (pacmanXDeriction == 0 && pacmanYDeriction == 1 && (elementInfo & 0x2) == 1) //stop going up if there is a wall beyond
                    pacmanYDeriction = 0;
                if (pacmanXDeriction == 1 && pacmanYDeriction == 0 && (elementInfo & 0x4) == 1) //stop going right if there is a wall on the right
                    pacmanXDeriction = 0;
                if (pacmanXDeriction == 0 && pacmanYDeriction == -1 && (elementInfo & 0x8) == 1) //stop going down if there is a wall undernies
                    pacmanYDeriction = 0;

                EatDot(); // If the dot in the current block has not been consumed, pacman will eat it.

            }
            pacmanXPosition = pacmanXPosition + pacmanXDeriction; //update pacman position
            pacmanYPosition = pacmanYPosition + pacmanYDeriction;
            // DrawPacman(); //update pacman image
        }
        public void EatDot()
        {
            if ((mapArray[maparrayPosition] & 0x10) == 1)
            {
                mapArray[maparrayPosition] = mapArray[maparrayPosition] & 0xffef; //Change the "dot" bit from 1 to 0 leaving other bits unchanged. 
                //some functions update the map image and erase the dot at the block.
                //some functions update the score
            }

        }
        public static bool CheckCollision(MyPacman p, MyGhost g)
        {
            if ((Math.Abs(p.pacmanXPosition - 10) < BLOCKSIZE / 2)&& (Math.Abs(p.pacmanYPosition - 20) < BLOCKSIZE / 2))
                return true;
            else
                return false;
        }
        public int GetPacmanXPosition()
        {
            return pacmanXPosition;
        }
        public int GetPacmanYPosition()
        {
            return pacmanYPosition;
        }
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

    }
}
