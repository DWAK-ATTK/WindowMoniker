using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Onova;
using Onova.Services;

namespace WindowMoniker {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {

#if !DEBUG
			//	Do app update first.
			try {
				using (UpdateManager manager = new UpdateManager(new GithubPackageResolver("DWAK-ATTK", "WindowMoniker", "WindowMoniker-*.zip"), new ZipPackageExtractor())) {
					manager.CheckPerformUpdateAsync().GetAwaiter().GetResult();
				}
			} catch (Exception ex) {
				if (args.All(a => a.ToLower() != "--no-update-warn")) {
					MessageBox.Show($"There was an error updating to the latest version.\r\n{ex.Message}");
				}
			}
#endif

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
