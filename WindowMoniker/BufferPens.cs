using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowMoniker {
	public class BufferPens {
		public BufferPens(Buffers buffers, Color color) : base() {
			Top = new Pen(color, buffers.Top);
			Bottom = new Pen(color, buffers.Bottom);
			Left = new Pen(color, buffers.Left);
			Right = new Pen(color, buffers.Right);
		}

		public Pen Top { get; set; } = null;
		public Pen Bottom { get; set; } = null;
		public Pen Left { get; set; } = null;
		public Pen Right { get; set; } = null;
	}
}
