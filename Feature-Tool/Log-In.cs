using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Feature_Tool
{

    internal class Log_In
    {

        internal static void Chef()
        {

            // Verbindungsinformationen zur Datenbank
            string connectionString = "Server=localhost;Port=3306;Database=LogIn;Uid=Danny;Pwd=DanDan-05K;";

            // Eingabeaufforderungen für Name und Passwort
            Console.WriteLine("Bitte gib einen Namen ein:");
            string name = Console.ReadLine();

            Console.WriteLine("Bitte gib ein Passwort ein:");
            string passwort = Console.ReadLine();

            // Verbindung zur Datenbank herstellen
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL-Befehl zum Einfügen des Namens und Passworts
                    string query = $"INSERT INTO Benutzer (Name, Passwort) VALUES ('{name}', '{passwort}')";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("Daten erfolgreich gespeichert!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fehler beim Speichern der Daten: " + ex.Message);
                }
            }

            Console.ReadLine();

        }

        internal static void Mitarbeiter() 
        { 



        }

    }

}
