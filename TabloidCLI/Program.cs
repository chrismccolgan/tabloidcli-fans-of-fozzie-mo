using TabloidCLI.UserInterfaceManagers;
using System;

namespace TabloidCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"
  _(___)_  
 ()'   `() 
 .' o o `. 
 :  _O_  : --- Welcome to Tabloid.ly by Team Fans of Fozzie.
 `. \_/ .' --- I'm Fozzie.
  .`---'.     --- I hope your experience is a pleasant one. Waka waka!
.' ()o() `.
:   ( \   : 
            ");
            
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
