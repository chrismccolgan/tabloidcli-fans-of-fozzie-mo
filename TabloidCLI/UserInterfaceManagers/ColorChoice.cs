using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.UserInterfaceManagers
{
    class ColorChoice : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;

        public ColorChoice(IUserInterfaceManager parentUI)
        {
            _parentUI = parentUI;
        }
        public IUserInterfaceManager Execute()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine(@"
                                  _(___)_  
                                 ()'   `() 
                                 .' o o `. 
                                 :  _O_  : 
                                 `. \_/ .' -- What's your favorite color?
                                  .`---'.   --- Just hit enter when you're all done trying everything out.
                                .' ()o() `.
                                :   ( \   : ");
            Console.WriteLine("");
            Console.WriteLine("");
            
            Console.WriteLine(" 1) Red");
            Console.WriteLine(" 2) Green");
            Console.WriteLine(" 3) Yellow");
            Console.WriteLine(" 4) None of them!");
            Console.WriteLine(" 5) Dark Grey");
            Console.WriteLine(" 6) Blue");
            Console.WriteLine(" 7) Cyan");
            Console.WriteLine(" 8) Dark Magenta");
            Console.WriteLine(" 9) All of them!");
            Console.WriteLine(" 0) Get me out of here");
            Console.WriteLine(" Remember, press 'Enter' or 'Return' to continue when you're done deciding.  I've got some more questions for you.  But don't worry, they're not hard.");

            Console.Write("> ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.Red;
                    return this;
                case "2":
                    Console.BackgroundColor = ConsoleColor.Green;
                    return this;
                case "3":
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    return this;
                case "4":
                    Console.BackgroundColor = ConsoleColor.White;
                    return this;
                case "5":
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    return this;
                case "6":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    return this;
                case "7":
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    return this;
                case "8":
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    return this;
                case "9":
                    Console.BackgroundColor = ConsoleColor.Black;
                    return this;
                case "0":
                    Console.Clear();
                    return _parentUI;
          
                default:
                    Console.Clear();
                    Console.WriteLine("");

                    Console.WriteLine(@"
                                  _(___)_  
                                 ()'   `() 
                                 .' o o `. 
                                 :  _O_  : 
                                 `. \_/ .' -- That's my favorite color too!
                                  .`---'.     ---What's your second favorite color?
                                .' ()o() `.
                                :   ( \   : ");
                    Console.WriteLine("");
          
                    Console.WriteLine("");

                    Console.WriteLine(" 1) Red");
                    Console.WriteLine(" 2) Green");
                    Console.WriteLine(" 3) Yellow");
                    Console.WriteLine(" 4) None of them!");
                    Console.WriteLine(" 5) Dark Grey");
                    Console.WriteLine(" 6) Blue");
                    Console.WriteLine(" 7) Cyan");
                    Console.WriteLine(" 8) Dark MAgenta");
                    Console.WriteLine(" 9) All of Them!");
                    Console.WriteLine(" 'Enter/Return') to go back to choosing your favorite color.");
                    Console.WriteLine(" 0) Get me out of here");

                    Console.Write("> ");
                    string choiceTwo = Console.ReadLine();
                    switch (choiceTwo)
                    {
                        case "1":
                            Console.ForegroundColor = ConsoleColor.Red;
                            return this;
                        case "2":
                            Console.ForegroundColor = ConsoleColor.Green;
                            return this;
                        case "3":
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            return this;
                        case "4":
                            Console.ForegroundColor = ConsoleColor.White;
                            return this;
                        case "5":
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            return this;
                        case "6":
                            Console.ForegroundColor = ConsoleColor.Blue;
                            return this;
                        case "7":
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            return this;
                        case "8":
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            return this;
                        case "9":
                            Console.ForegroundColor = ConsoleColor.Black;
                            return this;
                        case "0":
                            Console.Clear();
                            return _parentUI;
                    }

                    return this;

            }
            

        }
    }
}
