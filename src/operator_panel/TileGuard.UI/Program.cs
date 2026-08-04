using System;
using System.Windows.Forms;

namespace TileGuard.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            while (true)
            {
                using (LoginForm loginForm = new LoginForm())
                {
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        using (Form1 mainForm = new Form1(loginForm.CurrentUser!, loginForm.CurrentShift!))
                        {
                            DialogResult result = mainForm.ShowDialog();

                            if (result == DialogResult.Retry)
                            {
                                continue; 
                            }
                            else
                            {
                                break; 
                            }
                        }
                    }
                    else
                    {
                        break; 
                    }
                }
            }
        }
    }
}