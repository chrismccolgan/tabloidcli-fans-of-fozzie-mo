using TabloidCLI.UserInterfaceManagers;
using System;

namespace TabloidCLI
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(@"HI THERE!!! WAKA WAKA");
            Console.WriteLine(@"
                                  _(___)_  
                                 ()'   `() 
                                 .' o o `. 
                                 :  _O_  : 
                                 `. \_/ .' --- I'm Fozzie!
                                  .`---'.     --- I hope your experience is a pleasant one!
                                .' ()o() `.
                                :   ( \   : ");

            Console.WriteLine("Welcome to Tabloid.ly by Team Fans of Fozzie. Waka waka!");
            // MainMenuManager implements the IUserInterfaceManager interface
            IUserInterfaceManager ui = new MainMenuManager();
            while (ui != null)
            {
                // Each call to Execute will return the next IUserInterfaceManager we should execute
                // When it returns null, we should exit the program;
                ui = ui.Execute(); 
            }
        }
    }
}
