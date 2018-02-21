using System;

namespace Stickit
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThreadAttribute]
        static void Main(string[] args)
        {
         MainForm form = new MainForm();
            form.ShowDialog();
        }
    }
#endif
}

