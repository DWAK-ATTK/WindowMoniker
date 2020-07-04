using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace WindowMoniker {

	[Flags]
	public enum Edges {
		Top = 1,
		Bottom = 2,
		Left = 4,
		Right = 8,
		All = Top | Bottom | Left | Right,
	}



	public class Options {

		private static int _defaultThickness = 3;


		private ColorConverter _colorConverter = new ColorConverter();


		public Options() {
			Edges = (Edges)Enum.Parse(typeof(Edges), ConfigurationManager.AppSettings["Edges"], ignoreCase: true);

			BorderMode = ConfigurationManager.AppSettings["BorderMode"];

			string value = ConfigurationManager.AppSettings["ForeColor"];
			if (!string.IsNullOrWhiteSpace(value)) {
				ForeColor = (Color)_colorConverter.ConvertFromString(value);
			}
			value = ConfigurationManager.AppSettings["BackColor"];
			if (!string.IsNullOrWhiteSpace(value)) {
				BackColor = (Color)_colorConverter.ConvertFromString(value);
			}

			Title = ConfigurationManager.AppSettings["Title"];
			value = ConfigurationManager.AppSettings["TitleFontSize"];
			if (!string.IsNullOrWhiteSpace(value)) {
				TitleFontSize = float.Parse(value);
			}

			value = ConfigurationManager.AppSettings["TitleForeColor"];
			if (!string.IsNullOrWhiteSpace(value)) {
				TitleForeColor = (Color)_colorConverter.ConvertFromString(value);
			}
			value = ConfigurationManager.AppSettings["TitleBackColor"];
			if (!string.IsNullOrWhiteSpace(value)) {
				TitleBackColor = (Color)_colorConverter.ConvertFromString(value);
			}


			if (!string.IsNullOrWhiteSpace(Title)) {
				Buffers.Top = 10;
			}
		}




		public Buffers Buffers { get; set; } = new Buffers();

		public string BorderMode { get; set; } = "Solid";

		public Edges Edges { get; set; } = Edges.All;

		public Color ForeColor { get; set; } = Color.Black;

		public Color BackColor { get; set; } = Color.White;


		private Brush _foreBrush = null;
		public Brush ForeBrush {
			get {
				if (null == _foreBrush) { _foreBrush = new SolidBrush(ForeColor); }
				return _foreBrush;
			}
		}

		private Brush _backBrush = null;
		public Brush BackBrush {
			get {
				if (null == _backBrush) { _backBrush = new SolidBrush(BackColor); }
				return _backBrush;
			}
		}

		private Pen _forePen = null;
		public Pen ForePen {
			get {
				if (null == _forePen) { _forePen = new Pen(ForeColor, (float)_defaultThickness); }
				return _forePen;
			}
		}

		private Pen _backPen = null;
		public Pen BackPen {
			get {
				if (null == _backPen) { _backPen = new Pen(BackColor, (float)_defaultThickness); }
				return _backPen;
			}
		}


		public string Title { get; set; } = string.Empty;

		public float TitleFontSize { get; set; } = 8.0f;

		public Color TitleForeColor { get; set; } = Color.White;

		public Color TitleBackColor { get; set; } = Color.Black;


		private Brush _titleForeBrush = null;
		public Brush TitleForeBrush {
			get {
				if (null == _titleForeBrush) { _titleForeBrush = new SolidBrush(TitleForeColor); }
				return _titleForeBrush;
			}
		}

		private Brush _titleBackBrush = null;
		public Brush TitleBackBrush {
			get {
				if (null == _titleBackBrush) { _titleBackBrush = new SolidBrush(TitleBackColor); }
				return _titleBackBrush;
			}
		}


	}   //	class
}       //	ns
