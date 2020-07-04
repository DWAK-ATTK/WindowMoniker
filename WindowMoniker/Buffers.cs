using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowMoniker {
	public class Buffers {

		public Buffers() : base() { }

		public Buffers(int top, int bottom, int left, int right) : this() {
			Top = top;
			Bottom = bottom;
			Left = left;
			Right = right;
		}



		public int Top { get; set; } = 3;
		public int Bottom { get; set; } = 3;
		public int Left { get; set; } = 3;
		public int Right { get; set; } = 3;



		public static Buffers operator +(Buffers left, Buffers right) {
			Buffers result = new Buffers(
				left.Top + right.Top, left.Bottom + right.Bottom,
				left.Left + right.Left, left.Right + right.Right);
			return result;
		}

		public static Buffers operator *(Buffers left, int right) {
			Buffers result = new Buffers(
				left.Top * right, left.Bottom * right,
				left.Left * right, left.Right * right);
			return result;
		}

	}
}
