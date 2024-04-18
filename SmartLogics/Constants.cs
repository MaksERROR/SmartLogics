using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogics
{
	internal class Constants
	{
		public readonly static Color GButRed = Color.FromArgb(211, 47, 46);
		public readonly static Color GButPressedRed = Color.FromArgb(125, 19, 19);
		public readonly static Color GButYellow = Color.FromArgb(231, 174, 7);
		public readonly static Color GButBorderYellow = Color.FromArgb(252, 192, 16);
		public readonly static Color Strave = Color.FromArgb(146, 146, 146);
		public readonly static Color Black = Color.FromArgb(127, 127, 127);
		//public readonly static Color Black = Color.FromArgb(27, 27, 27);
		public readonly static Color Background = Color.FromArgb(26, 28, 40);
		public readonly static Color Blue = Color.FromArgb(33, 150, 243);
		public readonly static Color BulbOff = Color.FromArgb(20, 20, 20);
		public const int PICMODE = 0;
		public const int EDITMODE = 1;
		public const int STRAVEMODE = 2;
		public static Size ClientWin;
		public static int EditMode = 1;

	}
}
