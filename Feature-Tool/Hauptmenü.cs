using Feature_Tool.Untermenüs;
using MySql.Data.MySqlClient;


class Hauptmenü
{
    public static void Main()
    {

        bool schleife;
        string eingabe;
        string adminpasswort = "1234";

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
            
            Console.WriteLine("1. Log-In   \t  2. Administration\n\n" +
                              "------------------------------------\n\n" +
                              "'exit' um das Programm zu schließen\n\n");

            eingabe = Console.ReadLine();

            if (eingabe == "1")
            {

                do
                {

                    schleife = true;

                    Console.Clear();

                    Console.WriteLine("\t\tLog-In\n" +
                                      "----------------------------------------------\n");

                    Console.WriteLine("Was wollen Sie tun?\n\n" +
                                      "1. Registrieren\n" +
                                      "2. Anmelden\n");

                    Console.WriteLine("----------------------------------------------\n\n" +
                                      "" +
                                      "'back' um zurück ins Hauptmenü zu kommen.\n\n");


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

                    else if (eingabe == "back")
                    {

                        schleife = false;

                    }

                    else
                    {

                        // Falsche Eingabe


                        Console.WriteLine(eingabe + " ist eine falsche Eingabe\n" +
                            "Versuche es mit '1', '2' oder 'back'.");

                    }

                } while(schleife);

                schleife = true;

            }

            else if (eingabe == "2")
            {

                int eingabe_pw = 0;
                bool falschePW_eingabe = false;

                Console.Clear();

                do
                {

                    schleife = true;

                    Console.Write("2. Administration\n\n" +
                                  "Passwort: ");
                    string adminpasswort_eingabe = Console.ReadLine();

                    if (adminpasswort_eingabe == adminpasswort)
                    {

                        Console.Clear();

                        do
                        {

                            schleife = true;

                            Console.WriteLine("Was wollen Sie administrieren?\n\n" +
                                              "1. Einträge löschen\n" +
                                              "2. Benutzerverwaltung\n" +
                                              "3. Programmübersicht\n");

                            Console.WriteLine("------------------------------------------------------------\n\n" +
                                              "" +
                                              "'back' um zurück ins Hauptmenü zu kommen.\n\n");

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

                            else if (eingabe == "back")
                            {

                                Main();

                            }

                            else
                            {

                                // Falsche Eingabe

                                Console.WriteLine("'" + eingabe + "' funktioniert nicht.\n" +
                                                  "Versuche Sie es erneut.");

                            }

                        } while (schleife);

                    }

                    else
                    {

                        // Falsches Password

                        eingabe_pw++;

                        if (eingabe_pw >= 3)
                        {

                            falschePW_eingabe = true;

                            Console.WriteLine("Passwort zu oft falsch eingegeben.\n" +
                                              "Zurück zum Hauptmenü.");

                            Console.ReadKey();

                        }

                        else
                        {

                            schleife = false;

                            Console.WriteLine("Das war leider falsch...\n" +
                                              "Versuchen Sie es erneut.");
                            Console.ReadKey();
                            Console.Clear();

                        }
                    }

                } while (!schleife && !falschePW_eingabe);

                schleife = true;            // Um Programmschleife nicht zu durchbrechen

            }

            else if (eingabe == "exit")
            {

                Console.WriteLine("Das Programm wurde beendet\n\n");

                Environment.Exit(0);            //Programm beenden

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