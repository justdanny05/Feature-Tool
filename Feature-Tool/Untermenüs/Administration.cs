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

    internal class Administration
    {

        internal static void Einträge_löschen()
        {

            bool schleife;

            // SQL Einträge löschen

            do
            {

                schleife = true;

                string connectionString = "Server=localhost;Port=3306;Database=LogIn;Uid=Danny;Pwd=DanDan-05K;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    
                    Console.WriteLine("Gespeicherte Daten\n");

                    Console.WriteLine("------------------------------------------------------------\n");

                    try
                    {

                        connection.Open();

                        // SQL-Abfrage zum Auslesen der Daten

                        string query = "SELECT id, Name, Passwort FROM Benutzer";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                
                                if (!reader.HasRows)
                                {

                                    Console.WriteLine("LEER");

                                }

                                else
                                {

                                    // Daten aus dem Reader lesen und anzeigen

                                    while (reader.Read())
                                    {

                                        int id = reader.GetInt32("id");
                                        string name = reader.GetString("Name");
                                        string passwort = reader.GetString("Passwort");

                                        Console.WriteLine($"ID: {id}, Name: {name}, Passwort: {passwort}");

                                    }

                                }
                                
                            }
                        }

                    }

                    catch (Exception ex)
                    {

                        Console.WriteLine("Fehler beim Auslesen der Daten: " + ex.Message);

                    }

                    Console.WriteLine("");
                    Console.WriteLine("------------------------------------------------------------\n\n");

                    Console.WriteLine("Wollen Sie Einträge aus der Tabelle löschen? (ja/nein)\n");

                    string eingabe = Console.ReadLine();

                    if (eingabe == "ja" | eingabe == "nein")
                    {

                        if (eingabe == "ja")
                        {

                            do
                            {

                                schleife = true;

                                Console.WriteLine("Möchten Sie die Einträge nach der ID löschen oder alle Einträge komplett löschen? (id/k)");

                                string deleteOption = Console.ReadLine();

                                if (deleteOption.ToLower() == "id")
                                {

                                    Console.WriteLine("Bitte geben Sie die ID des Eintrags ein, den Sie löschen möchten:");

                                    int id = int.Parse(Console.ReadLine());

                                    // SQL-Befehl zum Löschen des Eintrags nach ID

                                    string deleteQuery = "DELETE FROM Benutzer WHERE id = @id";


                                    using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                                    {

                                        deleteCommand.Parameters.AddWithValue("@id", id);

                                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                                        if (rowsAffected > 0)
                                        {

                                            Console.WriteLine("Eintrag erfolgreich gelöscht!");
                                            Console.ReadKey();

                                        }

                                        else
                                        {

                                            Console.WriteLine("Eintrag mit der angegebenen ID nicht gefunden.");
                                            Console.ReadKey();

                                        }

                                    }

                                    Console.WriteLine("Wollen Sie noch einen Eintrag löschen? (ja/nein)");

                                    eingabe = Console.ReadLine();

                                    if (eingabe == "ja")
                                    {

                                        schleife = false;

                                    }

                                    else if (eingabe == "nein")
                                    {

                                        Console.Clear();
                                        Hauptmenü.Main();

                                    }

                                }

                                else if (deleteOption.ToLower() == "k")
                                {

                                    // SQL-Befehl zum Löschen aller Einträge aus der Tabelle

                                    string deleteQuery = "DELETE FROM Benutzer";

                                    using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                                    {

                                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                                        Console.WriteLine($"{rowsAffected} Einträge erfolgreich gelöscht!");
                                        Console.ReadKey();

                                    }

                                }

                                else
                                {

                                    schleife = false;

                                    Console.WriteLine("Ungültige Option. Bitte wählen Sie 'id' oder 'k'.");

                                }

                            } while (!schleife);

                        }

                        else if (eingabe == "nein")
                        {

                            Hauptmenü.Main();

                        }

                    }

                    else
                    {

                        // Falsche Eingabe

                        schleife = false;

                        Console.WriteLine(eingabe + " ist eine falsche Eingabe\n" +
                                          "Versuche es erneut.");

                    }

                    connection.Close();

                }

            } while (!schleife);

        }

        internal static void Benutzerverwaltung()
        {

            bool schleife;
            string eingabe;
            string name;

            do
            {

                schleife = true;

                Console.WriteLine("Was wollen Sie tun?\n");
                Console.WriteLine("1. Passwort zurücksetzen\n" +
                                  "2. Namen zurücksetzen\n\n" +
                                  "'back' um zurück zu gehen");

                eingabe = Console.ReadLine();

                if (eingabe == "1")
                {

                    string connectionString = "Server=localhost;Port=3306;Database=LogIn;Uid=Danny;Pwd=DanDan-05K;";

                    Console.Write("Bitte gib deinen Namen ein: ");
                    name = Console.ReadLine();

                    Console.Write("Bitte gib dein neues Passwort ein: ");
                    string neuesPasswort = Console.ReadLine();

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {

                        try
                        {

                            connection.Open();

                            string updateQuery = "UPDATE Benutzer SET Passwort = @neuesPasswort WHERE Name = @name";

                            using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                            {

                                command.Parameters.AddWithValue("@neuesPasswort", neuesPasswort);
                                command.Parameters.AddWithValue("@name", name);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {

                                    Console.WriteLine("Passwort erfolgreich zurückgesetzt!");
                                    Console.ReadKey();

                                }

                                else
                                {

                                    Console.WriteLine("Ungültiger Benutzername. Passwort konnte nicht zurückgesetzt werden.");
                                    Console.ReadKey();

                                }

                            }

                        }

                        catch (Exception ex)
                        {

                            Console.WriteLine("Fehler beim Zurücksetzen des Passworts: " + ex.Message);
                            Console.ReadKey();

                        }

                    }

                }

                else if (eingabe == "2")
                {

                    string connectionString = "Server=localhost;Port=3306;Database=LogIn;Uid=Danny;Pwd=DanDan-05K;";

                    Console.Write("Bitte gib den aktuellen Namen des Benutzers ein: ");
                    string aktuellerName = Console.ReadLine();

                    Console.Write("Bitte gib den neuen Namen für den Benutzer ein: ");
                    string neuerName = Console.ReadLine();

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {

                        try
                        {

                            connection.Open();

                            string updateQuery = "UPDATE Benutzer SET Name = @neuerName WHERE Name = @aktuellerName";

                            using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                            {

                                command.Parameters.AddWithValue("@neuerName", neuerName);
                                command.Parameters.AddWithValue("@aktuellerName", aktuellerName);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {

                                    Console.WriteLine("Name erfolgreich zurückgesetzt!");
                                    Console.ReadKey();

                                }

                                else
                                {

                                    Console.WriteLine("Ungültiger Benutzername. Name konnte nicht zurückgesetzt werden.");
                                    Console.ReadKey();

                                }

                            }

                        }

                        catch (Exception ex)
                        {

                            Console.WriteLine("Fehler beim Zurücksetzen des Namens: " + ex.Message);
                            Console.ReadKey();

                        }

                    }

                }

                else if (eingabe == "back")
                {

                    schleife = true;

                }

                else
                {

                    // Falsche Eingabe

                    schleife = false;

                    Console.WriteLine(eingabe+ " ist eine falsche Eingabe\n" +
                                               "Versuche es erneut.");

                }

            } while (!schleife);                    

        }

        internal static void Programmübersicht()
        {

            // Verbindungsinformationen zur Datenbank

            string connectionString = "Server=localhost;Port=3306;Database=LogIn;Uid=Danny;Pwd=DanDan-05K;";

            // Verbindung zur Datenbank herstellen

            Console.WriteLine("Datenbank:");
            Console.WriteLine("--------------------------------\n\n");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                try
                {

                    connection.Open();

                    // SQL-Abfrage zum Auslesen der Daten

                    string query = "SELECT id, Name, Passwort FROM Benutzer";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {

                            if (!reader.HasRows)
                            {

                                Console.WriteLine("LEER");

                            }
                            else
                            {

                                while (reader.Read())
                                {

                                    int id = reader.GetInt32("id");
                                    string name = reader.GetString("Name");
                                    string passwort = reader.GetString("Passwort");

                                    Console.WriteLine($"ID: {id}, Name: {name}, Passwort: {passwort}");

                                }

                            }                                                      

                        }

                    }

                }

                catch (Exception ex)
                {

                    Console.WriteLine("Fehler beim Auslesen der Daten: " + ex.Message);

                }

            }

            Console.ReadKey();
                       
        }

    }

}
