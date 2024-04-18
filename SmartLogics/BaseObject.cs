using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SmartLogics
{
	internal class BaseObject
	{
		protected static Point globalPos = new Point(0, 0);
		protected Point _point = new Point();
		protected Size _box = new Size();
		protected Color _backColor;
		protected Color _borderColor = Color.AliceBlue;
		public bool mouseInObj { get; protected set; }

		public BaseObject(int X, int Y)
		{
			_backColor = Constants.Black;
			_point.X = X;
			_point.Y = Y;
			_box.Width = 100;
			_box.Height = 100;
		}
		public virtual void PressAction(Point pos, MouseButtons pressed, MouseButtons lastPressed) { }

		public virtual bool Draw(Graphics gr, Pen pen)
		{
			var wS = Constants.ClientWin;
			if (_point.X + globalPos.X > wS.Width + _box.Width || _point.X + globalPos.X < -_box.Width * 1.5 || _point.Y + globalPos.Y > wS.Height + _box.Height || _point.Y + globalPos.Y < -_box.Height)
			{
				return false;
			}
			int penAdd = 2;
			pen.Color = _borderColor;
			pen.Width = penAdd;
			Rectangle rect = new Rectangle(new Point(_point.X + globalPos.X - penAdd, _point.Y + globalPos.Y - penAdd), _box + new Size(penAdd, penAdd));
			pen.Color = _backColor;
			gr.DrawRectangle(pen, rect);
			for (int i = 0; i <= _box.Height; i += penAdd)
			{
				var rex = new Point(_point.X + i + globalPos.X, _point.Y + i + globalPos.Y);
				rect = new Rectangle(rex, new Size(_box.Width - 2 * i, _box.Height - 2 * i));
				gr.DrawRectangle(pen, rect);
			}
			pen.Width = 1;
			return true;
		}

		public static void Draw(Graphics gr, Pen pen, List<BaseObject> objects)
		{
			for (int i = objects.Count - 1; i >= 0; i--)
			{
				objects[i].Draw(gr, pen);

			}

		}
		
		public void SetActiveState(Point pos, bool active)
		{
			if (InObj(pos))
			{
				mouseInObj = active;
			}
		}

		public bool MooveObj(Point pos, MouseButtons pressed, MouseButtons lastPressed)
		{
			if (pressed == lastPressed)
				return false;
			switch (pressed)
			{
				case MouseButtons.Left:
					SetActiveState(pos, true);
					break;

				case MouseButtons.Right:
					SetActiveState(pos, true);
					break;

				case MouseButtons.None:
					SetActiveState(pos, false);
					break;

				default:
					break;
			}
			return true;	
		}
		public void AddPos(Point pos, int mode)
		{
			switch (mode)
			{
				case Constants.EDITMODE:
					if (mouseInObj)
					{
						_point.X += pos.X;
						_point.Y += pos.Y;
					}
					break;
				case Constants.PICMODE:
				case Constants.STRAVEMODE:
					globalPos.X += pos.X;
					globalPos.Y += pos.Y;
					break;
			}
		}

		public virtual bool InObj(Point pos)
		{
			var rezPos = new Point(pos.X + globalPos.X, pos.Y + globalPos.Y);

			if (pos.X < (rezPos.X + _box.Width) && pos.X > (rezPos.X) && pos.Y < (rezPos.Y + _box.Height) && pos.Y > (rezPos.Y))
			{
				return true;
			}

			return false;
		}
	}
}
