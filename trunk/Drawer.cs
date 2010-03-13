//Drawer.cs

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Reflection;

public class Drawer{
	
	private static Thread keyboardInputThread;
	private static KeyEventArgs keyCheck = new KeyEventArgs(new Keys());
	private static bool debugMenu = false;
	private static bool debugGame = true;
    
	
	public class AForm:Form{
		
		private bool showMenu = false;
		private bool firstTime = true;
		private static bool cursorUp = false;
		private static bool cursorDown = false;
		private static bool enterPushed = false;
		private int cursorPos = 1;
        private Graphics bg = null;
        private Graphics g_pacman = null;
        private int blah = 1;
        private int testx = 103;
        private int testy = 101;

        private Graphics ghost1 = null;
		
		//Creates basic window - 'Pacman' will be in the title bar, size is Size, and 
		//background colour is black
		public AForm(){
			Text = "Pacman";
			Size = new System.Drawing.Size(515, 530);
			BackColor = Color.Black;
		}
		
		static void Main(){
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
                BackColor = Color.Black;
            }
            else {
                if (bg == null)
                {
                    bg = this.CreateGraphics();
                    Bitmap backgroundImage = pacmanSENG403.Properties.Resources.PacmanLayoutWalls;
                    bg.DrawImage(backgroundImage, this.ClientRectangle, new Rectangle(0, 0, backgroundImage.Width, backgroundImage.Height), GraphicsUnit.Pixel);

                }
            }
        }

		protected override void OnPaint(PaintEventArgs e){
			if(showMenu == true){
				Font title = new Font("Purisa", 26);
				Font mainMenuItems = new Font("Ouhod", 16);
				e.Graphics.DrawString("Pacman", title, Brushes.Green, 175, 50);
				//main image goes here
				e.Graphics.DrawString("New Game", mainMenuItems, Brushes.Blue, 200, 340);
				e.Graphics.DrawString("High Scores", mainMenuItems, Brushes.Blue, 200, 370);
				e.Graphics.DrawString("Exit", mainMenuItems, Brushes.Blue, 200, 400);
				e.Graphics.DrawLine(new Pen(Color.Beige, 1), new Point(0,0), new Point(0,500));
				e.Graphics.DrawLine(new Pen(Color.Beige, 1), new Point(0,500), new Point(500,500));
				e.Graphics.DrawLine(new Pen(Color.Beige, 1), new Point(500,500), new Point(500,0));
				e.Graphics.DrawLine(new Pen(Color.Beige, 1), new Point(500,0), new Point(0,0));

				//TEST AREA -- DRAWING MENU OPTION
				e.Graphics.DrawEllipse(new Pen(Color.Yellow, 5), 175, 353, 10, 10);
				if(firstTime == true){
					e.Graphics.DrawEllipse(new Pen(Color.Yellow, 5), 175, 353, 10, 10);
					firstTime = false;
				}
				if((cursorUp || cursorDown) && !(cursorUp && cursorDown)){
					//redraw cursor
					switch(cursorPos){
						//case 1: if cursorDown, goto pos2
						case 1: 
							if(cursorDown){
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
							if(cursorDown){
								cursorPos = 3;
								//erase pos2
								e.Graphics.DrawEllipse(new Pen(Color.Black, 5), 175, 383, 10, 10);
								//draw pos3
								e.Graphics.DrawEllipse(new Pen(Color.Yellow, 5), 175, 413, 10, 10);
							}
							else if(cursorUp){
								cursorPos = 1;
								//erase pos2
								e.Graphics.DrawEllipse(new Pen(Color.Black, 5), 175, 383, 10, 10);
								//draw pos1
								e.Graphics.DrawEllipse(new Pen(Color.Yellow, 5), 175, 353, 10, 10);
							}
							break;
						//case 3: if cursorUp, goto pos2
						case 3:
							if(cursorUp){
								cursorPos = 2;
								//erase pos3
								e.Graphics.DrawEllipse(new Pen(Color.Black, 5), 175, 413, 10, 10);
								//draw pos2
								e.Graphics.DrawEllipse(new Pen(Color.Yellow, 5), 175, 383, 10, 10);
							}
							break;
					}
				}
				else{
					//do nothing -- no cursor
				}
			}//end if(showMenu == true)
			else{
                

                // Pacman Movement
                if (g_pacman == null)
                {
                    g_pacman = this.CreateGraphics();
                    g_pacman.DrawEllipse(new Pen(Color.Black, 6), testx, testy, 1, 1);
                  
                }
                else
                {
                    g_pacman.DrawEllipse(new Pen(Color.White, 6), testx, testy, 1, 1);
                    Matrix X = new Matrix();
                    X.Translate(1*blah, 0);
                    g_pacman.Transform = X;
                    g_pacman.DrawEllipse(new Pen(Color.Black, 6), testx, testy, 1, 1);
                    blah++;
                }

                // Ghost Movement
                if (ghost1 == null)
                {
                    ghost1 = this.CreateGraphics();
                    ghost1.DrawEllipse(new Pen(Color.Red, 6), 250, 250, 1, 1);
                }
                else
                {

                }
			}
	
		}

		protected static void checkForInput(){
			enterPushed = false;
			cursorDown = false;
			cursorUp = false;
			while(!enterPushed){
				//Console.Write("Inside while loop\n");
				//Console.Write("keyCheck value is {0}\n", keyCheck.KeyCode);
				if(keyCheck.KeyCode == Keys.Enter){
					//Console.Write("Inside if -- enter has been pushed\n");
					enterPushed = true;
				}
				else{
					//Console.Write("Inside else\n");
					if(keyCheck.KeyCode == Keys.Down){
						//Console.Write("Inside if -- up key has been pushed\n");
						cursorDown = true;
					}
					else if(keyCheck.KeyCode == Keys.Up){
						//Console.Write("Inside else if -- up key has been pushed\n");
						cursorUp = true;
					}
				}
				try{
					Form.ActiveForm.Refresh();
				}
				catch(Exception e){
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
				Thread.Sleep(10);
			}
		}
		
		protected override void Dispose(bool disposing){
			if(disposing){
				keyboardInputThread.Abort();
			}
  			base.Dispose(disposing);
		}
		
	}

}