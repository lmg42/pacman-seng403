//Drawer.cs

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Reflection;

namespace pacman
{

    public class Drawer
    {

        private static Thread keyboardInputThread;
        private static KeyEventArgs keyCheck = new KeyEventArgs(new Keys());
        private static bool debugMenu = false;
        private static bool debugGame = true;


        public class AForm : Form
        {

            private bool showMenu = false;
            private bool firstTime = true;
            private static bool cursorUp = false;
            private static bool cursorDown = false;
            private static bool enterPushed = false;
            private int cursorPos = 1;
            private Graphics bg = null;


            private Graphics g_pacman = null;
            private Bitmap b_pacman = null;
            
            private Graphics g_blinky = null;
            private Graphics g_pinky = null;
            private Graphics g_inky = null;
            private Graphics g_clyde = null;

            private Graphics g_fruit = null;
            private Bitmap b_fruit = null;
            private int fruitCounter = 0;

            private GameData gd = new GameData();


            //Creates basic window - 'Pacman' will be in the title bar, size is Size, and 
            //background colour is black
            public AForm()
            {
                Text = "Pacman";
                Size = new System.Drawing.Size(519, 544);
            }

            static void Main()
            {
                Map.GenerateMap(1);
                keyboardInputThread = new Thread(new ThreadStart(checkForInput));
                AForm form = new AForm();
                keyboardInputThread.Start();
                form.Activate();
                Application.Run(form);

            }

            protected override void OnPaintBackground(PaintEventArgs e)
            {
                if (showMenu == true)
                {
                    this.CreateGraphics();
                    this.BackColor = Color.Black;
                }
                else
                {
                    if (bg == null)
                    {
                        bg = this.CreateGraphics();
                        Bitmap backgroundImage = pacmanSENG403.Properties.Resources.PacmanLayoutWalls;
                        bg.DrawImage(backgroundImage, this.ClientRectangle, new Rectangle(0, 0, 500, 500), GraphicsUnit.Pixel);

                    }
                }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                if (showMenu == true)
                {
                    /*Font title = new Font("Purisa", 26);
                    Font mainMenuItems = new Font("Ouhod", 16);
                    e.Graphics.DrawString("Pacman", title, Brushes.Green, 175, 50);
                    //main image goes here
                    e.Graphics.DrawString("New Game", mainMenuItems, Brushes.Blue, 200, 340);
                    e.Graphics.DrawString("High Scores", mainMenuItems, Brushes.Blue, 200, 370);
                    e.Graphics.DrawString("Exit", mainMenuItems, Brushes.Blue, 200, 400);
                    e.Graphics.DrawLine(new Pen(Color.Beige, 1), new Point(0, 0), new Point(0, 500));
                    e.Graphics.DrawLine(new Pen(Color.Beige, 1), new Point(0, 500), new Point(500, 500));
                    e.Graphics.DrawLine(new Pen(Color.Beige, 1), new Point(500, 500), new Point(500, 0));
                    e.Graphics.DrawLine(new Pen(Color.Beige, 1), new Point(500, 0), new Point(0, 0));

                    //TEST AREA -- DRAWING MENU OPTION
                    e.Graphics.DrawEllipse(new Pen(Color.Yellow, 5), 175, 353, 10, 10);
                    if (firstTime == true)
                    {
                        e.Graphics.DrawEllipse(new Pen(Color.Yellow, 5), 175, 353, 10, 10);
                        firstTime = false;
                    }
                    if ((cursorUp || cursorDown) && !(cursorUp && cursorDown))
                    {
                        //redraw cursor
                        switch (cursorPos)
                        {
                            //case 1: if cursorDown, goto pos2
                            case 1:
                                if (cursorDown)
                                {
                                    cursorPos = 2;
                                    //erase pos1
                                    e.Graphics.DrawEllipse(new Pen(Color.Black, 5), 175, 353, 10, 10);
                                    //draw pos2
                                    e.Graphics.DrawEllipse(new Pen(Color.Yellow, 5), 175, 383, 10, 10);
                                }
                                break;
                            //case 2: if cursorDown, goto pos3
                            //		  if cursorUp, goto pos1
                            case 2:
                                if (cursorDown)
                                {
                                    cursorPos = 3;
                                    //erase pos2
                                    e.Graphics.DrawEllipse(new Pen(Color.Black, 5), 175, 383, 10, 10);
                                    //draw pos3
                                    e.Graphics.DrawEllipse(new Pen(Color.Yellow, 5), 175, 413, 10, 10);
                                }
                                else if (cursorUp)
                                {
                                    cursorPos = 1;
                                    //erase pos2
                                    e.Graphics.DrawEllipse(new Pen(Color.Black, 5), 175, 383, 10, 10);
                                    //draw pos1
                                    e.Graphics.DrawEllipse(new Pen(Color.Yellow, 5), 175, 353, 10, 10);
                                }
                                break;
                            //case 3: if cursorUp, goto pos2
                            case 3:
                                if (cursorUp)
                                {
                                    cursorPos = 2;
                                    //erase pos3
                                    e.Graphics.DrawEllipse(new Pen(Color.Black, 5), 175, 413, 10, 10);
                                    //draw pos2
                                    e.Graphics.DrawEllipse(new Pen(Color.Yellow, 5), 175, 383, 10, 10);
                                }
                                break;
                        }
                    }
                    else
                    {
                        //do nothing -- no cursor
                    }*/
                }//end if(showMenu == true)
                else
                {

                    //fruit spawning counter
                    if ((fruitCounter > 100) && (fruitCounter < 1000))
                    {
                        g_fruit = this.CreateGraphics();
                        g_fruit.DrawEllipse(new Pen(Color.Teal, 6), 250, 250, 1, 1);
                        /*b_fruit = this.CreateGraphics();
                        g_fruit = DrawImage(b_fruit, this.ClientRectangle, new Rectangle(), GraphicsUnit.Pixel);*/
                    }
                    // Pacman Movement
                    if (g_pacman == null)
                    {
                        //CurrentGameCharacters.pacman = new MyPacman(250, 245);
                        //CurrentGameCharacters.pacman = new MyPacman(120, 90);
                        //g_pacman = this.CreateGraphics();
                        // b_pacman = pacmanSENG403.Properties.Resources.pacman;
                        // g_pacman.DrawImage(b_pacman, this.ClientRectangle, new Rectangle(CurrentGameCharacters.pacman.getX(), CurrentGameCharacters.pacman.getY(), 20, 20), GraphicsUnit.Pixel);

                        
                        CurrentGameCharacters.pacman = new MyPacman(250, 250);
                        g_pacman = this.CreateGraphics();
                        g_pacman.DrawEllipse(new Pen(Color.Yellow, 12), CurrentGameCharacters.pacman.getX(), CurrentGameCharacters.pacman.getY(), 1, 1);
                        
                        
                    }
                    
                    else
                    {
                        int oldx = CurrentGameCharacters.pacman.getX();
                        int oldy = CurrentGameCharacters.pacman.getY();
                        CurrentGameCharacters.pacman.screenUpdate();
                       
                        
                        /*g_pacman.DrawRectangle(new Pen(Color.Black, 1), oldx, oldy, 20, 20);
                        g_pacman.FillRectangle(new SolidBrush(Color.Black), oldx, oldy, 20, 20);
                        g_pacman.DrawImage(b_pacman, this.ClientRectangle, new Rectangle(CurrentGameCharacters.pacman.getX(), CurrentGameCharacters.pacman.getY(), 20, 20), GraphicsUnit.Pixel);*/
                        
                        
                        g_pacman.DrawEllipse(new Pen(Color.Black, 12), oldx, oldy, 1, 1);
                        g_pacman.DrawEllipse(new Pen(Color.Yellow, 12), CurrentGameCharacters.pacman.getX(), CurrentGameCharacters.pacman.getY(), 1, 1);
                    }     
       

                    // Ghost Movement
                    //blinky (red)
                    if (g_blinky == null)
                    {
                        CurrentGameCharacters.blinky = new SmartGhost(1, 1, Directions.UP);
                        g_blinky = this.CreateGraphics();
                        g_blinky.DrawEllipse(new Pen(Color.Red, 6), CurrentGameCharacters.blinky.getX(), CurrentGameCharacters.blinky.getY(), 5, 5);
                    }
                    else
                    {
                        int oldx = CurrentGameCharacters.blinky.getX();
                        int oldy = CurrentGameCharacters.blinky.getY();
                        CurrentGameCharacters.blinky.screenUpdate();
                        g_blinky.DrawEllipse(new Pen(Color.Black, 6), oldx, oldy, 5, 5);
                        g_blinky.DrawEllipse(new Pen(Color.Red, 6), CurrentGameCharacters.blinky.getX(), CurrentGameCharacters.blinky.getY(), 5, 5);
                    }
                    
                    //pinky (pink)
                    if (g_pinky == null)
                    {
                        CurrentGameCharacters.pinky = new StupidGhost(23, 1, Directions.UP);
                        g_pinky = this.CreateGraphics();
                        g_pinky.DrawEllipse(new Pen(Color.Crimson, 6), CurrentGameCharacters.pinky.getX(), CurrentGameCharacters.pinky.getY(), 5, 5);
                    }
                    else
                    {
                        int oldx = CurrentGameCharacters.pinky.getX();
                        int oldy = CurrentGameCharacters.pinky.getY();
                        CurrentGameCharacters.pinky.screenUpdate();
                        g_pinky.DrawEllipse(new Pen(Color.Black, 6), oldx, oldy, 5, 5);
                        g_pinky.DrawEllipse(new Pen(Color.Crimson, 6), CurrentGameCharacters.pinky.getX(), CurrentGameCharacters.pinky.getY(), 5, 5);
                    }
                    
                    //inky (blue)
                    if (g_inky == null)
                    {
                        CurrentGameCharacters.inky = new StupidGhost(23, 23, Directions.UP);
                        g_inky = this.CreateGraphics();
                        g_inky.DrawEllipse(new Pen(Color.Blue, 6), CurrentGameCharacters.inky.getX(), CurrentGameCharacters.inky.getY(), 5, 5);
                    }
                    else
                    {
                        int oldx = CurrentGameCharacters.inky.getX();
                        int oldy = CurrentGameCharacters.inky.getY();
                        CurrentGameCharacters.inky.screenUpdate();
                        g_inky.DrawEllipse(new Pen(Color.Black, 6), oldx, oldy, 5, 5);
                        g_inky.DrawEllipse(new Pen(Color.Blue, 6), CurrentGameCharacters.inky.getX(), CurrentGameCharacters.inky.getY(), 5, 5);
                    }
                    
                    //clyde (orange)
                    if (g_clyde == null)
                    {
                        CurrentGameCharacters.clyde = new StupidGhost(1, 23, Directions.UP);
                        g_clyde = this.CreateGraphics();
                        g_clyde.DrawEllipse(new Pen(Color.Orange, 6), CurrentGameCharacters.clyde.getX(), CurrentGameCharacters.clyde.getY(), 5, 5);
                    }
                    else
                    {
                        int oldx = CurrentGameCharacters.clyde.getX();
                        int oldy = CurrentGameCharacters.clyde.getY();
                        CurrentGameCharacters.clyde.screenUpdate();
                        g_clyde.DrawEllipse(new Pen(Color.Black, 6), oldx, oldy, 5, 5);
                        g_clyde.DrawEllipse(new Pen(Color.Orange, 6), CurrentGameCharacters.clyde.getX(), CurrentGameCharacters.clyde.getY(), 5, 5);
                    }

                    //score
                    e.Graphics.DrawString("Score: " + gd.getScore(), new Font("Ouhod", 12, FontStyle.Bold), Brushes.Yellow, 2, 480);

                    //lives left
                    int tempLives = gd.getNumLives();
                    if (tempLives == 1)
                        e.Graphics.DrawString("1 life", new Font("Ouhod", 12, FontStyle.Bold), Brushes.Yellow, 225, 480);
                    else
                        e.Graphics.DrawString(tempLives+" lives", new Font("Ouhod", 12, FontStyle.Bold), Brushes.Yellow, 225, 480);
                    
                    //level
                    e.Graphics.DrawString("Level " + gd.getLevel(), new Font("Ouhod", 12, FontStyle.Bold), Brushes.Yellow, 425, 480);

                }

            }

            protected override void OnKeyDown(KeyEventArgs e)
            {
                CurrentGameCharacters.pacman.UserInput(e);
            }

            protected static void checkForInput()
            {
                enterPushed = false;
                cursorDown = false;
                cursorUp = false;
                while (!enterPushed)
                {
                    /*
                    //Console.Write("Inside while loop\n");
                    //Console.Write("keyCheck value is {0}\n", keyCheck.KeyCode);
                    if (keyCheck.KeyCode == Keys.Enter)
                    {
                        //Console.Write("Inside if -- enter has been pushed\n");
                        enterPushed = true;
                    }
                    else
                    {
                        //Console.Write("Inside else\n");
                        if (keyCheck.KeyCode == Keys.Down)
                        {
                            //Console.Write("Inside if -- up key has been pushed\n");
                            cursorDown = true;
                        }
                        else if (keyCheck.KeyCode == Keys.Up)
                        {
                            //Console.Write("Inside else if -- up key has been pushed\n");
                            cursorUp = true;
                        }
                    }
                    try
                    {
                        Form.ActiveForm.Refresh();
                    }
                    catch (Exception e)
                    {
                        Console.Write("Exception: {0}", e);
                    }
                    /*
                    if(enterPushed)
                        Console.Write("Enter is true\n");
                    else
                        Console.Write("Enter is false\n");
                    if(cursorDown)
                        Console.Write("Cursor down is true\n");
                    else
                        Console.Write("Cursor down is false\n");
                    if(cursorUp)
                        Console.Write("Cursor up is true\n");
                    else
                        Console.Write("Cursor up is false\n"); */

                    try
                    {
                        Form.ActiveForm.Refresh();
                    }
                    catch
                    {

                    }
                    Thread.Sleep(10);
                }
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    keyboardInputThread.Abort();
                }
                base.Dispose(disposing);
            }

        }

    }

}