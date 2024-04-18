using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SmartLogics
{
	internal class MyButton : BaseObject
	{

		bool _pressed = false;
		Color _butColor;
		public MyButton(int X, int Y) : base(X, Y)
		{
			_backColor = Constants.GButYellow;
			_butColor = Constants.GButRed;
		}

		public bool GetState() { return _pressed; }

		public override void PressAction(Point pos, MouseButtons pressed, MouseButtons lastPressed)
		{
			
				if (pressed == lastPressed || Constants.EditMode != Constants.PICMODE)
				return;

			switch (pressed)
			{
				case MouseButtons.Left:
					if (InObj(pos))
					{
						PushButtonState(true);

					}
					break;
				case MouseButtons.Right:
					break;
				case MouseButtons.None:
					if (InObj(pos))
					{
						PushButtonState(false);
					}
					break;
				default:
					break;
			}

		}
		public void PushButtonState(bool pos)
		{
			_pressed = pos;
			if (_pressed)
				_butColor = Constants.GButPressedRed;
			else
				_butColor = Constants.GButRed;
		}


		public override bool Draw(Graphics gr, Pen pen)
		{
			if (!base.Draw(gr, pen))
				return false;

			int penAdd = 8;
			pen.Width = penAdd;
			Rectangle rect;
			pen.Color = _butColor;
			for (int i = 10; i < _box.Height - 10; i += penAdd - 1)
			{
				var rex = new Point(_point.X + i + globalPos.X, _point.Y + i + globalPos.Y);
				rect = new Rectangle(rex, new Size(_box.Width - 2 * i, _box.Height - 2 * i));
				gr.DrawEllipse(pen, rect);
			}
			pen.Width = 1;
			return true;
		}



		public static new void Draw(Graphics gr, Pen pen, List<MyButton> buttons)
		{
			for (int i = buttons.Count - 1; i >= 0; i--)
			{
				buttons[i].Draw(gr, pen);

			}
		}
		public static void Draw(Graphics gr, Pen pen, List<BaseObject> buttons)
		{
			foreach (var item in buttons)
			{
				item.Draw(gr, pen);
			}
		}

		public override bool InObj(Point pos)
		{
			if (_pressed || pos.X < (_point.X + _box.Width + globalPos.X) && pos.X > (_point.X + globalPos.X) && pos.Y < (_point.Y + _box.Height + globalPos.Y) && pos.Y > (_point.Y + globalPos.Y))
			{
				return true;
			}
			return false;
		}
	}
}
