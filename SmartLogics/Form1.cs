using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartLogics
{
	public partial class Form1 : Form
	{

		static Bitmap bmp = new Bitmap(600, 500);
		Pen pen = new Pen(Color.Green, 1);
		Graphics gr = Graphics.FromImage(bmp);
		List<BaseObject> buttons = new List<BaseObject>();
		List<BaseObject> obj = new List<BaseObject>();
		List<Gate> gate = new List<Gate>();
		#region Библиотеки
		[DllImport("user32.dll")]
		static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);

		[DllImport("user32.dll")]
		static extern bool UnhookWindowsHookEx(IntPtr hInstance);

		[DllImport("kernel32.dll")]
		static extern IntPtr LoadLibrary(string lpFileName);

		private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

		const int WH_KEYBOARD_LL = 13; // Номер глобального LowLevel-хука на клавиатуру
		const int
			WM_KEYDOWN = 0x100,       //Key down
			WM_KEYUP = 0x101;         //Key up

		private LowLevelKeyboardProc _proc = hookProc;

		private static IntPtr hhook = IntPtr.Zero;
		#endregion
		static int vkCode;
		#region Функция хука
		public static IntPtr hookProc(int code, IntPtr wParam, IntPtr lParam)
		{
			if (code >= 0 && wParam == (IntPtr)WM_KEYDOWN || code >= 0 && wParam == (IntPtr)260)
			{
				vkCode = Marshal.ReadInt32(lParam); //Получить код клавиши

			}
			return IntPtr.Zero;
		}
		#endregion

		public Form1()
		{
			InitializeComponent();
			Constants.ClientWin = new Size(600, 500);

		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Loop();
		}

		private async void Loop()
		{
			MouseButtons mouseBut = Form1.MouseButtons;
			MouseButtons lastMouseBut;
			BaseObject SuperObj = new BaseObject(0, 0);
			var g = SystemInformation.MouseWheelScrollLines;
			Point winMousePos = new Point(0, 0);
			Size lastPicSize;
			for (int i = 0; i < 5; i++)
			{
				obj.Add(new BaseObject(200 + 120 * i, 100));
			}
			var picSize = pictureBox1.Size;
			ResizeWin();
			var gMousePos = Form1.MousePosition;
			Point gLastMousePos;
			Point moove = new Point(0, 0);
			while (true)
			{
				lastPicSize = picSize;      /// Edit WinPicSize
				picSize = pictureBox1.Size;
				if (picSize != lastPicSize)
					ResizeWin();



				gLastMousePos = gMousePos;                                          ///last mause position
				gMousePos = Form1.MousePosition;                                    ///mause position
				var gWinPos = PointToScreen(new Point(Left + 10, Top + 39 * 2));    ///window position

				winMousePos.Y = gMousePos.Y - gWinPos.Y / 2;        /// this mouse position in window 
				winMousePos.X = gMousePos.X - gWinPos.X / 2;
				moove.Y = gMousePos.Y - gLastMousePos.Y;            ///mouse delta
				moove.X = gMousePos.X - gLastMousePos.X;

				lastMouseBut = mouseBut;                    ///last mouse buttons
				mouseBut = Form1.MouseButtons;              ///this mouse buttons


				bool next = true;						///add accept pass
				if (next)
					foreach (var item in obj)
						if (!item.MooveObj(winMousePos, mouseBut, lastMouseBut))
						{
							next = false;
							break;
						}

				int counter = 0;
				foreach (var item in obj)
					if (!item.InObj(winMousePos))
						counter++;


				if (counter == obj.Count && mouseBut == MouseButtons.Left)
				{
					SuperObj.AddPos(moove, Constants.PICMODE);//global add
				}

				if (Constants.EditMode == Constants.EDITMODE)
				{
					foreach (var item in obj)
					{
						item.AddPos(moove, Constants.EDITMODE);
						break;
					}
				}

				gr.Clear(color: Color.FromArgb(25, 26, 39));
				BaseObject.Draw(gr, pen, obj);
				Cursor.Draw(gr, new Rectangle(winMousePos, new Size(10, 10)));
				pictureBox1.Image = bmp;
				await Task.Delay(1);
			}
		}
		private void ResizeWin()
		{
			var siv = pictureBox1.Size;
			if (siv.Width <= 0 || siv.Height <= 0)
			{
				siv = new Size(1, 1);
			}
			Constants.ClientWin = siv;
			bmp = new Bitmap(siv.Width, siv.Height);
			gr = Graphics.FromImage(bmp);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Constants.EditMode = Constants.PICMODE;

		}

		private void button1_Click(object sender, EventArgs e)
		{
			Constants.EditMode = Constants.EDITMODE;
		}

		private void button3_Click(object sender, EventArgs e)
		{

		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
		}
	}
}
