using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Z119.ATK.Shell
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()                          
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            System.Threading.ThreadPool.SetMinThreads(100, 100);

            // UI thread Exceptions
            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Make this for boosting RestSharp performance.
            System.Net.ServicePointManager.UseNagleAlgorithm = false;

            fLoginForm loginForm = new fLoginForm();
             if (loginForm.ShowDialog() == DialogResult.OK)
                Application.Run(new fMainWindow());
            else
                Application.Exit();

        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            using (ThreadExceptionDialog dialog = new ThreadExceptionDialog(e.Exception))
            {
                if (dialog.ShowDialog() == DialogResult.Cancel)
                    return;
            }
            Application.Exit();
            Environment.Exit(0);
        }
    }
}
