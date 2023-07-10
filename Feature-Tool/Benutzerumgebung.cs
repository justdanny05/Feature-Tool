using Feature_Tool.Untermenüs;
using MySql.Data.MySqlClient;
using System;
using System.Xml.Linq;

namespace Feature_Tool
{
    internal class Benutzerumgebung
    {
        private static string aktuellerBenutzer;


        internal static void Verwaltung(string name)
        {

            bool schleife;

            do
            {

                Console.Clear();

                do
                {

                    schleife = true;

                    aktuellerBenutzer = name;

                    Console.WriteLine("Du bist eingeloggt als '" + name + "':\n");
                    Console.WriteLine("------------------------------------\n\n");

                    Console.WriteLine("Was willst du tun?\n" +
                                      "1. Notizen Eingeben\n" +
                                      "2. Notizen Auslesen\n" +
                                      "3. Zurück ins Hauptmenü\n");

                    string eingabe = Console.ReadLine();

                    if (eingabe == "1")
                    {

                        TakeNote();

                    }

                    else if (eingabe == "2")
                    {

                        ReadNotesForUser();

                    }

                    else if (eingabe == "3")
                    {

                        Hauptmenü.Main();

                    }

                    else
                    {

                        // Ungültige Eingabe

                        schleife = false;

                        Console.WriteLine(eingabe + " ist eine falsche Eingabe.\n" +
                                          "Veruche es erneut...");

                    }

                } while (!schleife);

            } while (schleife);

        }

        internal static void TakeNote()
        {

            string tableName = "Benutzer";
            string connectionString = "Server=localhost;Port=3306;Database=LogIn;Uid=Danny;Pwd=DanDan-05K;";

            Console.Write("Geben Sie Ihre Notiz ein (max. 255 Zeichen): ");
            string note = Console.ReadLine();

            // Erstelle eine neue MySqlConnection mit dem Verbindungsstring
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                try
                {

                    connection.Open();

                    // Erstelle eine parameterisierte SQL-Abfrage, um die Notiz in die Datenbank einzufügen
                    string query = "INSERT INTO " + tableName + " (name, note) VALUES (@name, @note)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@name", aktuellerBenutzer);
                        command.Parameters.AddWithValue("@note", note);
                        command.ExecuteNonQuery();

                    }

                    Console.WriteLine("Die Notiz wurde gespeichert.");

                }

                catch (Exception ex)
                {

                    Console.WriteLine("Fehler beim Speichern der Notiz: " + ex.Message);
                    
                }

                Console.ReadKey();

            }

        }

        internal static void ReadNotesForUser()
        {

            string tableName = "Benutzer";
            string connectionString = "Server=localhost;Port=3306;Database=LogIn;Uid=Danny;Pwd=DanDan-05K;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                try
                {

                    connection.Open();

                    // Benutzer-ID abrufen
                    int benutzerId;
                    string query = "SELECT id FROM Benutzer WHERE name = @username";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@username", aktuellerBenutzer);
                        benutzerId = Convert.ToInt32(command.ExecuteScalar());

                    }

                    // Notizen für den Benutzer abrufen
                    query = "SELECT note FROM " + tableName + " WHERE benutzerid = @benutzerid";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@benutzerid", benutzerId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {

                            Console.WriteLine("Notizen für Benutzer: " + aktuellerBenutzer);
                            Console.WriteLine("------------------------------------\n");

                            while (reader.Read())
                            {

                                string note = reader.GetString(0);
                                Console.WriteLine("- " + note);

                            }

                        }

                    }

                }

                catch (Exception ex)
                {

                    Console.WriteLine("Fehler beim Lesen der Notizen: " + ex.Message);

                }

                Console.ReadKey();
            }

        }

        internal static void SetAktuellerBenutzer(string name)
        {

            aktuellerBenutzer = name;

        }

    }

}
