using System;
using TabloidCLI.UserInterfaceManagers;

namespace TabloidCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("");

            Console.WriteLine(@"                        
                                        WAKA WAKA!!
                            
                    Welcome to Tabloid.ly by Team Fans of Fozzie. Waka waka!

             --------------------------------------------------------------------
");

            

       

            // MainMenuManager implements the IUserInterfaceManager interface
            IUserInterfaceManager ui = new MainMenuManager();
            while (ui != null)
            {
                // Each call to Execute will return the next IUserInterfaceManager we should execute
                // When it returns null, we should exit the program;
                ui = ui.Execute();
                Console.Clear();
            }
        }
    }
}