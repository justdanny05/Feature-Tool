using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace Feature_Tool.Untermenüs
{

    internal class Log_In
    {

        internal static void Registrieren()
        {
            string name;
            string passwort;
            bool schleife;
            ConsoleKeyInfo key;

            // Verbindungsinformationen zur Datenbank

            string connectionString = "Server=localhost;Port=3306;Database=LogIn;Uid=Danny;Pwd=DanDan-05K;";

            // Eingabeaufforderungen für Name und Passwort

            Console.Write("Bitte gib deinen Namen ein: ");

            name = Console.ReadLine();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    connection.Open();

                    // Überprüfen ob der Name schon existiert
                    string überprüfung = "SELECT COUNT(*) FROM Benutzer WHERE Name = @name";

                    using (MySqlCommand checkname = new MySqlCommand(überprüfung, connection))
                    {

                        checkname.Parameters.AddWithValue("@name", name);

                        int count = Convert.ToInt32(checkname.ExecuteScalar());

                        while (count > 0)
                        {
                            Console.WriteLine("Der angegebene Name " + name + " existiert bereits.\n" +
                                              "Bitte gib einen anderen Namen ein.");
                            Console.Write("Bitte gib deinen Namen ein: ");
                            name = Console.ReadLine();

                            checkname.Parameters["@name"].Value = name;
                            count = Convert.ToInt32(checkname.ExecuteScalar());
                        }

                        Console.Write("Bitte gib ein Passwort ein: ");
                        passwort = "";

                        do
                        {

                            key = Console.ReadKey(true);

                            // Überprüfen, ob die Eingabetaste gedrückt wurde

                            if (key.Key != ConsoleKey.Enter)
                            {

                                // Den eingegebenen Zeichen mit einem Sternchen (*) ersetzen

                                passwort += key.KeyChar;

                                Console.Write("*");

                            }

                        } while (key.Key != ConsoleKey.Enter);

                        Console.WriteLine();

                        do
                        {

                            schleife = true;

                            //Überprüfen ob das Passwort gleich ist

                            Console.Write("Gib dein Passwort erneut ein: ");
                            string passwort_erneut = "";

                            do
                            {

                                key = Console.ReadKey(true);

                                // Überprüfen, ob die Eingabetaste gedrückt wurde

                                if (key.Key != ConsoleKey.Enter)
                                {

                                    // Den eingegebenen Zeichen mit einem Sternchen (*) ersetzen

                                    passwort_erneut += key.KeyChar;

                                    Console.Write("*");

                                }

                            } while (key.Key != ConsoleKey.Enter);

                            if (passwort == passwort_erneut)
                            {

                                //Code wird fortgesetzt, da Passwort korrekt eingegeben wurde!

                                passwort = passwort_erneut;

                            }

                            else
                            {

                                schleife = false;

                                Console.WriteLine("Das Passwort stimmt nicht überein...\n" +
                                                  "Bitte erneut versuchen");

                            }

                        } while (!schleife);

                        // Verbindung zur Datenbank herstellen


                        try
                        {

                            // SQL-Befehl zum Einfügen des Namens und Passworts

                            string query = $"INSERT INTO Benutzer (Name, Passwort) VALUES (@name, @passwort)";

                            using (MySqlCommand command = new MySqlCommand(query, connection))
                            {

                                command.Parameters.AddWithValue("@name", name);
                                command.Parameters.AddWithValue("@passwort", passwort);
                                command.ExecuteNonQuery();

                            }

                            Console.WriteLine("Daten erfolgreich gespeichert!");

                            // SQL-Abfrage erstellen

                            string sqlQuery = "SELECT id, name, passwort FROM Benutzer";

                            using (MySqlCommand selectCommand = new MySqlCommand(sqlQuery, connection))
                            {

                                using (MySqlDataReader reader = selectCommand.ExecuteReader())
                                {

                                    // Auf die Spaltenwerte zugreifen

                                    while (reader.Read())
                                    {

                                        int id = reader.GetInt32(0);                    // Index 0 entspricht der ID-Spalte
                                        string userName = reader.GetString(1);          // Index 1 entspricht der Name-Spalte
                                        string password = reader.GetString(2);          // Index 2 entspricht der Passwort-Spalte

                                        Console.WriteLine($"ID: {id}, Name: {userName}, Passwort: {password}");

                                    }

                                }

                            }

                        }

                        catch (Exception ex)
                        {

                            Console.WriteLine("Fehler beim Speichern oder Abrufen der Daten: " + ex.Message);

                        }

                    }

                }

                catch (Exception ex)
                {

                    Console.WriteLine("Fehler bei der Registrierung: " + ex.Message);

                }

                Console.ReadKey();

            }

        }

        internal static void Anmelden()
        {

            bool schleife;

            do
            {

                schleife = true;

                // Verbindungsinformationen zur Datenbank

                string connectionString = "Server=localhost;Port=3306;Database=LogIn;Uid=Danny;Pwd=DanDan-05K;";

                // Eingabeaufforderungen für Name und Passwort

                Console.WriteLine("Bitte gib deinen Namen ein:");
                string name = Console.ReadLine();

                Console.WriteLine("Bitte gib dein Passwort ein:");
                string passwort = "";
                ConsoleKeyInfo key;

                do
                {

                    key = Console.ReadKey(true);

                    // Überprüfen, ob die Eingabetaste gedrückt wurde

                    if (key.Key != ConsoleKey.Enter)
                    {

                        // Den eingegebenen Zeichen mit einem Sternchen (*) ersetzen

                        passwort += key.KeyChar;

                        Console.Write("*");

                    }

                } while (key.Key != ConsoleKey.Enter);

                // Verbindung zur Datenbank herstellen

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {

                    try
                    {

                        connection.Open();

                        // SQL-Abfrage zum Überprüfen von Name und Passwort

                        string query = "SELECT COUNT(*) FROM Benutzer WHERE Name = @name AND Passwort = @passwort";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {

                            command.Parameters.AddWithValue("@name", name);
                            command.Parameters.AddWithValue("@passwort", passwort);

                            int count = Convert.ToInt32(command.ExecuteScalar());

                            if (count > 0)
                            {

                                Console.WriteLine("Anmeldung erfolgreich!");

                            }

                            else
                            {

                                Console.WriteLine("Ungültiger Benutzername oder Passwort.");

                            }

                        }

                    }

                    catch (Exception ex)
                    {

                        // Falsche Eingabe

                        schleife = false;

                        Console.WriteLine("Fehler bei der Anmeldung: " + ex.Message);

                    }

                }

                Console.ReadLine();

            } while (!schleife);

        }

    }

}



