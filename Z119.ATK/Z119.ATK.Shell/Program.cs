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

            //Application.Run(new fSwitchForm());

            fLoginForm loginForm = new fLoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                //Z119.ATK.Common.Const.projMan.LoadProject();

                //projMan.SelectedProject += frm_SelectedProject;
                Z119.ATK.Common.Const.projMan.LoadProject();
                Application.Run(Z119.ATK.Common.ProjectManager.LoadObject<fMainWindow>(Z119.ATK.Common.Const.FILE_MAINWINDOW));

            }

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
