//Drawer.cs

using System;
using System.Windows.Forms;
using System.Drawing;
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
		
		//Creates basic window - 'Pacman' will be in the title bar, size is Size, and 
		//background colour is black
		public AForm(){
			Text = "Pacman";
			Size = new System.Drawing.Size(515, 530);
			BackColor = Color.Black;
		}
		
		static void Main(){
			keyboardInputThread = new Thread(new ThreadStart(checkForInput));
			keyboardInputThread.Start();
			AForm form = new AForm();
			form.Activate();
			Application.Run(form);
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
				Bitmap backgroundImage = GroupTest.Properties.Resources.PacmanLayoutWalls;
				e.Graphics.DrawImage(backgroundImage, this.ClientRectangle,
				                     new Rectangle(0,0, backgroundImage.Width, backgroundImage.Height),
				                     GraphicsUnit.Pixel);

				/*Directions[,] MapArray = Map.GenerateMap(1);
				for(int x = 0; x <= 10; x++){
					for(int y = 0; y <= 10; y++){
						//e.Graphics.DrawLine(new Pen(Color.Yellow, 1), new Point(x, y), new Point(x*10, y*10));
						e.Graphics.DrawRectangle(new Pen(Color.Yellow, 1), new Rectangle(new Point(0, 0), new Size(new Point(10, 10))));
						if(debugGame)
							Console.Write("x={0},y={1}", x, y);
					}
				}*/
				//draw game
			}
			
			//}
			
			//END TEST AREA
			
			//updates the location of the cursor
			//default location of cursor is pos 1
			//init all keyboard inputs to false
		/*	bool firstTime = true;
			bool enterPushed = false;
			bool cursorUp = false;
			bool cursorDown = false;
			int cursorPos = 1;
		
			//loop while enter is not pushed
			while(!enterPushed){
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
			//wait for keyboard input
			KeyEventArgs keyCheck = new KeyEventArgs(new Keys());
			bool noInput = true;
			while(noInput){
				//check for keyboard input
				if(keyCheck.KeyCode == Keys.Down){
					cursorDown = true;
					noInput = false;
				}
				else if(keyCheck.KeyCode == Keys.Up){
					cursorUp = true;
					noInput = false;
				}
				if(keyCheck.KeyCode == Keys.Enter){
					enterPushed = true;
					noInput = false;
				}
			}	
				//wait for keyboard input
				//check inputs
				//enterPushed = true/false
				//upPushed = true/false
				//downPushed = true/false
				//ignore other inputs
			}
			//enter has been pushed, so execute the menu option the cursor is on
			switch(cursorPos){
				case 1:
					//start a new game
					break;
				case 2:
					//view high scores
					break;
				case 3:
					//exit game
					break;
			}*/
		}
		
		protected static void checkForInput(){
			enterPushed = false;
			cursorDown = false;
			cursorUp = false;
			while(!enterPushed){
				Console.Write("Inside while loop\n");
				Console.Write("keyCheck value is {0}\n", keyCheck.KeyCode);
				if(keyCheck.KeyCode == Keys.Enter){
					Console.Write("Inside if -- enter has been pushed\n");
					enterPushed = true;
				}
				else{
					Console.Write("Inside else\n");
					if(keyCheck.KeyCode == Keys.Down){
						Console.Write("Inside if -- up key has been pushed\n");
						cursorDown = true;
					}
					else if(keyCheck.KeyCode == Keys.Up){
						Console.Write("Inside else if -- up key has been pushed\n");
						cursorUp = true;
					}
				}
				try{
					Form.ActiveForm.Update();
				}
				catch(Exception e){
					Console.Write("Exception: {0}", e);
				}
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
					Console.Write("Cursor up is false\n");
				Thread.Sleep(1000);
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