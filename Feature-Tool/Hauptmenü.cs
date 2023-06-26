using System;
using System.Drawing;

class Hauptmenü
{
    static void Main()
    {

        bool schleife;

        do
        {

            schleife = true;

            Console.Clear();

            Console.WriteLine("\r\n███╗░░░███╗███████╗███╗░░██╗██╗░░░██╗\r\n" +
                                  "████╗░████║██╔════╝████╗░██║██║░░░██║\r\n" +
                                  "██╔████╔██║█████╗░░██╔██╗██║██║░░░██║\r\n" +
                                  "██║╚██╔╝██║██╔══╝░░██║╚████║██║░░░██║\r\n" +
                                  "██║░╚═╝░██║███████╗██║░╚███║╚██████╔╝\r\n" +
                                  "╚═╝░░░░░╚═╝╚══════╝╚═╝░░╚══╝░╚═════╝░\n\n");
            
            Console.WriteLine("Log-In:");
            Console.WriteLine("Wer sind Sie?\n" +
                              "1. Chef\n" +
                              "2. Mitarbeiter");

            string eingabe;

            eingabe = Console.ReadLine();

            if (eingabe == "1" | eingabe == "2")
            {

                int ieingabe;

                int.TryParse(eingabe, out ieingabe);

                if (ieingabe == 1)
                {



                }

                else if (ieingabe == 2)
                {



                }

            }

            else if (eingabe == "exit")
            {

                Environment.Exit(0);            //Programm beendet

            }

            else
            {

                Console.WriteLine("'"+ eingabe + "' funktioniert nicht.\n" +
                                  "Versuchen Sie es erneut, mit '1', '2' oder 'exit'.");

                Console.ReadKey();

            }


        } while (schleife);
        
    }

}