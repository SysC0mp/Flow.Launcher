using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Flow.Launcher.Plugin.SharedCommands
{
    public static class SearchWeb
    {
        /// <summary> 
        /// Opens search in a new browser. If no browser path is passed in then Chrome is used. 
        /// Leave browser path blank to use Chrome.
        /// </summary>
		public static void NewBrowserWindow(this string url, string browserPath = "")
        {
            var browserExecutableName = browserPath?
                                        .Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.None)
                                        .Last();

            var browser = string.IsNullOrEmpty(browserExecutableName) ? "chrome" : browserPath;

            // Internet Explorer will open url in new browser window, and does not take the --new-window parameter
            var browserArguements = browserExecutableName == "iexplore.exe" ? url : "--new-window " + url;

            var psi = new ProcessStartInfo
            {
                FileName = browser,
                Arguments = browserArguements,
                UseShellExecute = true
            };

            try
            {
                Process.Start(psi);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
            }
        }

        /// <summary> 
        /// Opens search as a tab in the default browser chosen in Windows settings.
        /// </summary>
        public static void NewTabInBrowser(this string url, string browserPath = "")
        {
            var psi = new ProcessStartInfo() { UseShellExecute = true};
            try
            {
                if (!string.IsNullOrEmpty(browserPath))
                {
                    psi.FileName = browserPath;
                    psi.Arguments = url;
                }
                else
                {
                    psi.FileName = url;
                }

                Process.Start(psi);
            }
            // This error may be thrown if browser path is incorrect
            catch (System.ComponentModel.Win32Exception)
            {
                Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
            }
        }
    }
}
