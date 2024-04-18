using System.Collections.Generic;
using System.Drawing;

namespace SmartLogics
{
	internal class Gate : BaseObject
	{
		List<bool> inCode = new List<bool>();
		List<bool> outCode = new List<bool>();

		public Gate(int X, int Y, int interCount, int outCount) : base(X, Y)
		{
			for (int i = 0; i < interCount; i++)
				inCode.Add(false);
			for (int i = 0; i < outCount; i++)
				outCode.Add(false);
			_box = new Size(100, (interCount > outCount ? interCount : outCount) * 30);
		}
		public Point GetPos()
		{
			return new Point(_point.X + globalPos.X, _point.Y + globalPos.Y);
		}
		public override bool Draw(Graphics gr, Pen pen)
		{
			if (!base.Draw(gr, pen))
				return false;
			
			
			pen.Width = 2;
			Point Pos = GetPos();
			pen.Color = Constants.Strave;
			int moove = 10;
			int height = _box.Height - moove * 2;
			int circleRad = 6;
			int lineLen = 15;


			for (int i = 0; i < height; i += (int)(height / inCode.Count - 1))
			{
				Point ConnectPos = new Point(Pos.X - lineLen - circleRad, (int)(Pos.Y + moove + i));

				gr.DrawEllipse(pen, ConnectPos.X - circleRad, ConnectPos.Y-circleRad, circleRad * 2, circleRad * 2);
				gr.DrawLine(pen, ConnectPos.X+circleRad, ConnectPos.Y, ConnectPos.X + circleRad+lineLen, ConnectPos.Y);
			}
			for (int i = 0; i < height; i += (int)(height / outCode.Count - 1))
			{
				Point ConnectPos = new Point(Pos.X + _box.Width + lineLen + circleRad, (int)(Pos.Y + moove + i));

				gr.DrawEllipse(pen, ConnectPos.X - circleRad, ConnectPos.Y - circleRad, circleRad * 2, circleRad * 2);
				gr.DrawLine(pen, ConnectPos.X - circleRad, ConnectPos.Y, ConnectPos.X - circleRad - lineLen, ConnectPos.Y);
			}
			pen.Width = 1;
			return true;
		}
		public static void Draw(Graphics gr, Pen pen, List<Gate> gates)
		{
			for (int i = gates.Count - 1; i >= 0; i--)
			{
				gates[i].Draw(gr, pen);
			}
		}
	}
}
