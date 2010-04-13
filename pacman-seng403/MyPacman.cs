using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace pacman
{
    //Assume the game map size is 25 by 25 blocks wide. Each block has size 20 which means each block is 20 by 20 pixels wide. 
    public class MyPacman : GameCharacter
    {
        private int BLOCKSIZE; //unit is pixel 
        private int numberofEdibles;

        private int userInputXDeriction;
        private int userInputYDeriction;
        private int pacmanXDeriction;
        private int pacmanYDeriction;

        private int maparrayXPosition;
        private int maparrayYPosition;

        private int x;
        private int y;

        private Directions whichQuadrant;
        private bool dotFound;
        private bool bigdotFound;
        private bool fruitFound;
        private DateTime whenPacmanEatsBigdot;
        private TimeSpan timePastFromWhenPacmanEatsBigdot;
        private bool superPacman;

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
            radius = BLOCKSIZE / 2 - 1;
            pacmanTowarding = 0;
            whichQuadrant = 0;
            dotFound = false;
            bigdotFound = false;
            whenPacmanEatsBigdot = new DateTime();//year, month, day, hour, minute, second 
            timePastFromWhenPacmanEatsBigdot = new TimeSpan();
            superPacman = false;
            numberofEdibles = CountAllEdibles();
            BLOCKSIZE = Map.BLOCKSIZE;
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

            //Assume in a 20 by 20 block the centre of the pacman is (10,10) 
            int pacmanBlockXOrigin = x - BLOCKSIZE / 2;
            int pacmanBlockYOrigin = y - BLOCKSIZE / 2;

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

            //update score when pacman eats dot 
            if (DetermineCollisionBetweenPacmanAndDots())
            {
                GameData.incrementScore(10);
            }
            if (DetermineCollisionBetweenPacmanAndFruit())
            {
                GameData.incrementScore(200);
            }
            //update score, ghosts'status and pacman's status when pacman eats big dot 
            if (DetermineCollisionBetweenPacmanAndBigdots())
            {
                GameData.incrementScore(100);
                whenPacmanEatsBigdot = DateTime.Now;
                CurrentGameCharacters.inky.makeWeak();
                CurrentGameCharacters.pinky.makeWeak();
                CurrentGameCharacters.clyde.makeWeak();
                CurrentGameCharacters.blinky.makeWeak();
                superPacman = true;
            }
            //in superpacman mode, pacman can kill ghosts. the duration is 10 seconds. 
            if (superPacman)
            {
                
                DateTime currentTime = DateTime.Now;
                timePastFromWhenPacmanEatsBigdot = currentTime - whenPacmanEatsBigdot;
                if (timePastFromWhenPacmanEatsBigdot.Seconds <= 10)
                {
                    if (CheckCollision(CurrentGameCharacters.inky,CurrentGameCharacters.pacman))
                    {
                        CurrentGameCharacters.inky.kill();
                        GameData.incrementScore(500);
                    }
                    if (CheckCollision(CurrentGameCharacters.pinky, CurrentGameCharacters.pacman))
                    {
                        CurrentGameCharacters.pinky.kill();
                        GameData.incrementScore(500);
                    }
                    if (CheckCollision(CurrentGameCharacters.clyde, CurrentGameCharacters.pacman))
                    {
                        CurrentGameCharacters.clyde.kill();
                        GameData.incrementScore(500);
                    }
                    if (CheckCollision(CurrentGameCharacters.blinky, CurrentGameCharacters.pacman))
                    {
                        CurrentGameCharacters.blinky.kill();
                        GameData.incrementScore(500);
                    }
                }
                if (timePastFromWhenPacmanEatsBigdot.Seconds > 10)
                {
                    superPacman = false;
                    CurrentGameCharacters.inky.makeStrong();
                    CurrentGameCharacters.pinky.makeStrong();
                    CurrentGameCharacters.clyde.makeStrong();
                    CurrentGameCharacters.blinky.makeStrong();
                }
                
            }
            //not in superpacman mode, pacman will be killed if he is caught by any of the ghosts. 
            if (!superPacman)
            {

                if (CheckCollision(CurrentGameCharacters.inky, CurrentGameCharacters.pacman) || CheckCollision(CurrentGameCharacters.pinky, CurrentGameCharacters.pacman) || CheckCollision(CurrentGameCharacters.clyde, CurrentGameCharacters.pacman) || CheckCollision(CurrentGameCharacters.blinky, CurrentGameCharacters.pacman))
                {
                    GameData.decrementNumLives();
                    x = 250; //re-place the pacman
                    y = 250;
                    pacmanXDeriction = 0; //initialize pacman's movement
                    pacmanYDeriction = 0;
                    userInputXDeriction = 0;
                    userInputYDeriction = 0;

                }

            }
            //when pacman eats all the dots(including big dots), the player wins. 
            if (numberofEdibles == 0)
            {
                //call game finished function 
            }
        }
        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }
        public Directions getPacmanTowarding()
        {
            return pacmanTowarding;
        }

        public bool CheckCollision(GameCharacter a, MyPacman b)
        {
            int XDifference;
            int YDifference;

            XDifference = Math.Abs(a.getX() - b.getX());
            YDifference = Math.Abs(a.getY() - b.getY());

            if ((XDifference < (a.getRadius() + b.getRadius())) && (YDifference < (a.getRadius() + b.getRadius())))
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
        public void DetermineWhichQuardant()
        {
            if (x < 260 && y < 260)
                whichQuadrant = Directions.UP_LEFT;
            if (x > 260 && y < 260)
                whichQuadrant = Directions.UP_RIGHT;
            if (x < 260 && y > 260)
                whichQuadrant = Directions.DOWN_LEFT;
            if (x > 260 && y > 260)
                whichQuadrant = Directions.DOWN_RIGHT;
        }
        public bool DetermineCollisionBetweenPacmanAndDots()
        {
            dotFound = false;
            DetermineWhichQuardant();
            switch (whichQuadrant)
            {
                case Directions.UP_LEFT:
                    foreach (Edibles lunch in CurrentGameCharacters.dots_topleft)
                    {
                        dotFound = CheckCollision(lunch, CurrentGameCharacters.pacman);
                        if (dotFound == true)
                        {
                            CurrentGameCharacters.dots_topleft.Remove(lunch);
                            numberofEdibles--;
                            return true;
                        }
                    }
                    return false;
                    break;
                case Directions.UP_RIGHT:
                    foreach (Edibles lunch in CurrentGameCharacters.dots_topright)
                    {
                        dotFound = CheckCollision(lunch, CurrentGameCharacters.pacman);
                        if (dotFound == true)
                        {
                            CurrentGameCharacters.dots_topright.Remove(lunch);
                            numberofEdibles--;
                            return true;
                        }
                    }
                    return false;
                    break;
                case Directions.DOWN_LEFT:
                    foreach (Edibles lunch in CurrentGameCharacters.dots_bottomleft)
                    {
                        dotFound = CheckCollision(lunch, CurrentGameCharacters.pacman);
                        if (dotFound == true)
                        {
                            CurrentGameCharacters.dots_bottomleft.Remove(lunch);
                            numberofEdibles--;
                            return true;
                        }
                    }
                    return false;
                    break;
                case Directions.DOWN_RIGHT:
                    foreach (Edibles lunch in CurrentGameCharacters.dots_bottomright)
                    {
                        dotFound = CheckCollision(lunch, CurrentGameCharacters.pacman);
                        if (dotFound == true)
                        {
                            CurrentGameCharacters.dots_bottomright.Remove(lunch);
                            numberofEdibles--;
                            return true;
                        }
                    }
                    return false;
                    break;
                default:
                    return false;
            }
        }
        public bool DetermineCollisionBetweenPacmanAndBigdots()
        {
            bigdotFound = false;
            foreach (Edibles dinner in CurrentGameCharacters.bigdots)
            {
                bigdotFound = CheckCollision(dinner, CurrentGameCharacters.pacman);
                if (bigdotFound == true)
                {
                    CurrentGameCharacters.bigdots.Remove(dinner);
                    numberofEdibles--;
                    return true;
                }
            }
            return false;
        }
        public bool DetermineCollisionBetweenPacmanAndFruit()
        {
            fruitFound = false;
            if (CurrentGameCharacters.fruit != null)
            {
                fruitFound = CheckCollision(CurrentGameCharacters.fruit, CurrentGameCharacters.pacman);
                if (fruitFound == true)
                {
                    CurrentGameCharacters.fruit = null;
                    numberofEdibles--;
                    return true;
                }
                return false;
            }
            return false;
        }
        public int CountAllEdibles()
        {
            int all = 0;
            all++; //1 fruit
            foreach (Edibles breakfast in CurrentGameCharacters.bigdots)
                all++;
            foreach (Edibles breakfast in CurrentGameCharacters.dots_bottomleft)
                all++;
            foreach (Edibles breakfast in CurrentGameCharacters.dots_bottomright)
                all++;
            foreach (Edibles breakfast in CurrentGameCharacters.dots_topleft)
                all++;
            foreach (Edibles breakfast in CurrentGameCharacters.dots_topright)
                all++;
            return all;

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
 
