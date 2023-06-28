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
                    Console.WriteLine("Wollen Sie Einträge aus der Tabelle löschen? (ja/nein)\n");

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

                    catch (Exception ex)
                    {

                        Console.WriteLine("Fehler beim Auslesen der Daten: " + ex.Message);

                    }

                    Console.WriteLine("");
                    Console.WriteLine("------------------------------------------------------------");

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



        }

        internal static void Programmübersicht()
        {



        }

    }

}
