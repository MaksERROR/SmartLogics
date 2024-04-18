using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogics
{
	internal class Bulb : BaseObject
	{
		public Bulb(int X, int Y) : base(X, Y)
		{

		}
		public override bool Draw(Graphics gr, Pen pen)
		{
			int penAdd = 8;
			pen.Width = penAdd;
			Rectangle rect = new Rectangle(new Point(_point.X + globalPos.X, _point.Y + globalPos.Y), _box);
			gr.DrawEllipse(pen, rect);
			pen.Color = Constants.BulbOff;
			for (int i = 4; i < _box.Height; i += penAdd - 1)
			{
				var rex = new Point(_point.X + i + globalPos.X, _point.Y + i + globalPos.Y);
				rect = new Rectangle(rex, new Size(_box.Width - 2 * i, _box.Height - 2 * i));
				gr.DrawEllipse(pen, rect);
			}
			pen.Width = 1;
			return true;
		}
	}
}
