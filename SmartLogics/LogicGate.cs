using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogics
{
	internal class LogicGate: BaseObject
	{
		protected bool _input;
		private Color BackColor = Color.FromArgb(20, 20, 20);
		public LogicGate(int X, int Y) : base(X, Y)
		{

		}
		public override bool Draw(Graphics gr, Pen pen)
		{
			if (!base.Draw(gr, pen))
				return false;
			return true;
		}
	}
	

}
