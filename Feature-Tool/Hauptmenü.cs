using Feature_Tool.Untermenüs;
using MySql.Data.MySqlClient;


class Hauptmenü
{
    public static void Main()
    {

        bool schleife;
        string eingabe;

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
            
            Console.WriteLine("1. Log-In\n" +
                              "2. Administration");

            eingabe = Console.ReadLine();

            if (eingabe == "1")
            {

                Console.Clear();

                Console.WriteLine("\t\tLog-In\n" +
                                  "----------------------------------------------\n");

                Console.WriteLine("Was wollen Sie tun?\n\n" +
                                  "1. Registrieren\n" +
                                  "2. Anmelden\n");

                Console.WriteLine("----------------------------------------------\n");


                eingabe = Console.ReadLine();

                if (eingabe == "1" | eingabe == "2")
                {

                    int ieingabe = int.Parse(eingabe);

                    if (ieingabe == 1)
                    {

                        Console.Clear();
                        Log_In.Registrieren();

                    }

                    else if (ieingabe == 2)
                    {

                        Console.Clear();
                        Log_In.Anmelden();

                    }

                }

            }

            else if (eingabe == "2")
            {

                Console.Clear() ;

                do
                {

                    schleife = true;

                    Console.WriteLine("Was wollen Sie administrieren?\n\n" +
                                      "1. Einträge löschen\n" +
                                      "2. Benutzerverwaltung\n"+
                                      "3. Programmübersicht");

                    Console.WriteLine("------------------------------------------------------------");

                    eingabe = Console.ReadLine();

                    if (eingabe == "1")
                    {

                        Console.Clear();
                        Administration.Einträge_löschen();

                    }

                    else if (eingabe == "2")
                    {

                        Console.Clear();
                        Administration.Benutzerverwaltung();

                    }

                    else if (eingabe == "3")
                    {

                        Console.Clear();
                        Administration.Programmübersicht();

                    }

                    else
                    {

                        // Falsche Eingabe

                        schleife = false;

                        Console.WriteLine("'" + eingabe + "' funktioniert nicht.\n" +
                                          "Versuche Sie es erneut");

                    }

                } while (!schleife);

            }

            else if (eingabe == "exit")
            {

                Environment.Exit(0);            //Programm beendet

            } 

            else
            {

                // Falsche Eingabe

                Console.WriteLine("'" + eingabe + "' funktioniert nicht.\n" +
                                      "Versuchen Sie es erneut, mit '1', '2' oder 'exit'.");

                Console.ReadKey();

            }                    

        } while (schleife);
        
    }

}