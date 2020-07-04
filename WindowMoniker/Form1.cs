using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowMoniker {
	public partial class Form1 : Form {





		private ColorConverter _colorConverter = new ColorConverter();
		private Options _options = new Options();

		private Size _lastWindowSize = Size.Empty;



		public Form1() {
			InitializeComponent();

#if !DEBUG
			this.TopMost = true;
#endif
		}



		protected override CreateParams CreateParams {
			get {
				var Params = base.CreateParams;
#if !DEBUG
				Params.ExStyle |= 0x80;
#endif
				return Params;
			}
		}



		private void Form1_Load(object sender, EventArgs e) {
			UpdateTimer.Tick += UpdateTimer_Tick;
		}



		private void UpdateTimer_Tick(object sender, EventArgs e) {
			DrawBorderMoniker();
		}



		private void DrawBorderMoniker() {
			if (this.ClientSize == _lastWindowSize) { return; }

			this.WindowState = FormWindowState.Maximized;
			_lastWindowSize = this.ClientSize;

			UpdateWindowRegion();
			BackgroundImage = CreateWindowMonikerImage();

			panel1.Visible = false;
			this.TransparencyKey = Color.Empty;
		}



		private void UpdateWindowRegion() {
			GraphicsPath path = new GraphicsPath();

			if (_options.Edges.HasFlag(Edges.Left)) {
				path.AddRectangle(new Rectangle(0, 0, _options.Buffers.Left, this.Height));
			}
			if (_options.Edges.HasFlag(Edges.Bottom)) {
				path.AddRectangle(new Rectangle(
					_options.Buffers.Bottom, this.Height - _options.Buffers.Bottom,
					this.Width - (_options.Buffers.Bottom * 2), _options.Buffers.Bottom));
			}
			if (_options.Edges.HasFlag(Edges.Right)) {
				path.AddRectangle(new Rectangle(
					this.Width - _options.Buffers.Left, 0,
					_options.Buffers.Left, this.Height));
			}
			if (_options.Edges.HasFlag(Edges.Top)) {
				path.AddRectangle(new Rectangle(
					_options.Buffers.Left, 0,
					this.Width - (_options.Buffers.Left + _options.Buffers.Right), _options.Buffers.Top));
			}

			Region region = new Region(path);
			this.Region = region;
		}



		private Image CreateWindowMonikerImage() {
			Image result = new Bitmap(this.Width * 2, this.Height * 2);
			Graphics g = Graphics.FromImage(result);

			g.FillRectangle(_options.BackBrush, this.ClientRectangle);

			switch (_options.BorderMode.Trim().ToUpper()) {
				case "LEFTDIAGONAL":
					DrawLeftDiagonal(g);
					break;

				case "RIGHTDIAGONAL":
					DrawRightDiagonal(g);
					break;

				case "DASHED":
					DrawDashed(g);
					break;

				case "DOTTED":
					DrawDotted(g);
					break;
			}

			DrawTitle(g);

			return result;
		}



		private void DrawLeftDiagonal(Graphics g) {
			int x = 0;
			int y = 0;
			int buffer = _options.Buffers.Left;

			for (int index = buffer; index < this.Width * 2; index += buffer * 2) {
				x += buffer * 3;
				y += buffer * 3;

				g.DrawLine(_options.ForePen, x, -20, -20, y);
			}
		}



		private void DrawRightDiagonal(Graphics g) {
			int x = 0;
			int y = this.Height;
			int buffer = _options.Buffers.Left;
			for (int index = buffer; index < this.Width * 2; index += buffer * 2) {
				x += buffer * 3;
				y -= buffer * 3;

				g.DrawLine(_options.ForePen, x, this.Height + 20, -20, y);
			}
		}



		private void DrawDashed(Graphics g) {
			Buffers positions = new Buffers(0, 0, 0, 0);
			BufferPens pens = new BufferPens(_options.Buffers * 3, _options.ForeColor);
			int maxIndex = Math.Max(this.Width, this.Height);

			while (positions.Left < maxIndex) {
				positions += _options.Buffers * 6;

				//	Left/Right edge: only draw when below the top border and above the bottom border.
				if (_options.Buffers.Top <= positions.Left && positions.Left <= (this.Height - (_options.Buffers.Bottom * 2))) {
					g.DrawLine(pens.Left, 0, positions.Left, _options.Buffers.Left, positions.Left);                          //	Left
				}
				if (_options.Buffers.Top <= positions.Right && positions.Right <= (this.Height - (_options.Buffers.Bottom * 2))) {
					g.DrawLine(pens.Right, this.Width - _options.Buffers.Right, positions.Right, this.Width, positions.Right);   //	Right
				}

				if (_options.Buffers.Left <= positions.Top && positions.Top <= (this.Width - (_options.Buffers.Right * 2))) {
					g.DrawLine(pens.Top, positions.Top, 0, positions.Top, _options.Buffers.Top);
				}
				if (_options.Buffers.Left <= positions.Bottom && positions.Bottom <= (this.Width - (_options.Buffers.Right * 2))) {
					g.DrawLine(pens.Bottom, positions.Bottom, this.Height - _options.Buffers.Bottom, positions.Bottom, this.Height);
				}

			}
		}


		//	TODO:	Fix this.  Dashed too.
		//			Draw only on the visible region.
		private void DrawDotted(Graphics g) {
			Buffers positions = new Buffers(0, 0, 0, 0);
			BufferPens pens = new BufferPens(_options.Buffers, _options.ForeColor);
			int maxIndex = Math.Max(this.Width, this.Height);

			while (positions.Left < maxIndex) {
				positions += _options.Buffers * 2;

				//	Left/Right edge: only draw when below the top border and above the bottom border.
				if (_options.Buffers.Top <= positions.Left && positions.Left <= (this.Height - (_options.Buffers.Bottom * 2))) {
					g.DrawLine(pens.Left, 0, positions.Left, _options.Buffers.Left, positions.Left);                          //	Left
				}
				if (_options.Buffers.Top <= positions.Right && positions.Right <= (this.Height - (_options.Buffers.Bottom * 2))) {
					g.DrawLine(pens.Right, this.Width - _options.Buffers.Right, positions.Right, this.Width, positions.Right);   //	Right
				}

				if (_options.Buffers.Left <= positions.Top && positions.Top <= (this.Width - (_options.Buffers.Right * 2))) {
					g.DrawLine(pens.Top, positions.Top, 0, positions.Top, _options.Buffers.Top);
				}
				if (_options.Buffers.Left <= positions.Bottom && positions.Bottom <= (this.Width - (_options.Buffers.Right * 2))) {
					g.DrawLine(pens.Bottom, positions.Bottom, this.Height - _options.Buffers.Bottom, positions.Bottom, this.Height);
				}

			}
		}



		private void DrawTitle(Graphics g) {
			if (string.IsNullOrWhiteSpace(_options.Title)) { return; }

			Size titleSize = GetTitleSize(g);
			Rectangle rectangle = new Rectangle(
				(this.Width / 2) - (titleSize.Width / 2)-20, 0,
				titleSize.Width+40, _options.Buffers.Top);
			g.FillRectangle(_options.TitleBackBrush, rectangle);
			g.DrawString(_options.Title, _options.TitleFont, _options.TitleForeBrush, rectangle.X+20, -3);
		}



		private Size GetTitleSize(Graphics g) {
			Size result = new Size();

			StringFormat stringFormat = new StringFormat(); ;
			stringFormat.SetMeasurableCharacterRanges(new CharacterRange[] { new CharacterRange(0, _options.Title.Length) });

			Region[] regions =
				g.MeasureCharacterRanges(_options.Title, _options.TitleFont,
					new RectangleF(0, 0, this.Width, this.Height), stringFormat);

			foreach (Region region in regions) {
				result.Width += (int)region.GetBounds(g).Width;
			}
			result.Height = (int)regions[0].GetBounds(g).Height;

			return result;
		}







#region Window styles
		[Flags]
		public enum ExtendedWindowStyles {
			// ...
			WS_EX_TOOLWINDOW = 0x00000080,
			// ...
		}

		public enum GetWindowLongFields {
			// ...
			GWL_EXSTYLE = (-20),
			// ...
		}

		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

		public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong) {
			int error = 0;
			IntPtr result = IntPtr.Zero;
			// Win32 SetWindowLong doesn't clear error on success
			SetLastError(0);

			if (IntPtr.Size == 4) {
				// use SetWindowLong
				Int32 tempResult = IntSetWindowLong(hWnd, nIndex, IntPtrToInt32(dwNewLong));
				error = Marshal.GetLastWin32Error();
				result = new IntPtr(tempResult);
			} else {
				// use SetWindowLongPtr
				result = IntSetWindowLongPtr(hWnd, nIndex, dwNewLong);
				error = Marshal.GetLastWin32Error();
			}

			if ((result == IntPtr.Zero) && (error != 0)) {
				throw new System.ComponentModel.Win32Exception(error);
			}

			return result;
		}

		[DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
		private static extern IntPtr IntSetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		[DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
		private static extern Int32 IntSetWindowLong(IntPtr hWnd, int nIndex, Int32 dwNewLong);

		private static int IntPtrToInt32(IntPtr intPtr) {
			return unchecked((int)intPtr.ToInt64());
		}

		[DllImport("kernel32.dll", EntryPoint = "SetLastError")]
		public static extern void SetLastError(int dwErrorCode);
#endregion



	}       //	class
}           //	ns
