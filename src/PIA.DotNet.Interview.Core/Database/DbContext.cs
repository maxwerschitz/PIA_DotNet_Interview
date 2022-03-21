using Newtonsoft.Json;
using PIA.DotNet.Interview.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PIA.DotNet.Interview.Core.Database
{
    public class DbContext : IDbContext
    {
        private const string DATABASE_PATH = @"C:\Source\database.json"; // to do task_4
        private Database _database;

        public DbContext()
        {
            LoadDatabase();
        }

        public ICollection<T> GetDataset<T>()
        {
            var property = _database.GetType()
                .GetProperties()
                .Where(p => p.PropertyType.IsGenericType &&
                    p.PropertyType.GenericTypeArguments?[0] == typeof(T))
                .FirstOrDefault();

            return (ICollection<T>)property.GetValue(_database);
        }

        public bool Save()
        {
            if (_database == null)
                return false;

            string json = JsonConvert.SerializeObject(_database);

            if (File.Exists(DATABASE_PATH))
                File.Delete(DATABASE_PATH);

            File.WriteAllText(DATABASE_PATH, json);

            return true;
        }

        private void LoadDatabase()
        {
            if (!File.Exists(DATABASE_PATH))
            {
                _database = InitializeDatabase();
                Save();
            }

            string json = File.ReadAllText(DATABASE_PATH);
            _database = JsonConvert.DeserializeObject<Database>(json);
        }

        private Database InitializeDatabase()
        {
            var database = new Database();

            database.Tasks = new List<Task>();
                      
            database.Tasks.Add(new Task
            {
                Id = Guid.NewGuid(),
                Title = "Starten Sie die Lösung von Ihrem Visual Studio aus",
                Description = "Gut gemacht! Wenn Sie diese Ausgabe in Ihrem Browser sehen, haben Sie die erste Aufgabe bereits erledigt.<br />" +
                "Jetzt können Sie mit den unten stehenden Aufgaben beginnen und sie später als erledigt markieren, wenn Sie die Funktionalität dafür erstellt haben.<br />" +
                "Stellen Sie sicher, dass Sie die Entwicklungsrichtlinien in der Readme.md im Stammverzeichnis dieser Lösung befolgen.<br />" +
                "Good luck!"
            });

            database.Tasks.Add(new Task
            {
                Id = Guid.NewGuid(),
                Title = "Bearbeitungsfunktionen - Aufgabe als erledigt markieren",
                Description = "Die Schaltfläche 'Erledigt' dient dazu um den Bearbeitungsstatus der Aufgabe auf Erledigt oder Nicht-Erledigt einstellen zu können.<br />" +
                "Die Anpassungen sollen vom WebUI (Frontend) bis zur Datenbank (Backend) durchgeführt werden.",
                Example = "example_task2.JPG"
            });

            database.Tasks.Add(new Task
            {
                Id = Guid.NewGuid(),
                Title = "Zentrales Logging",
                Description = "Es soll ein Loggingmechanismus für alle Services eingeführt werden, um eine zentrale Protokollierung der Logs zu gewährleisten.<br />"
            });

            database.Tasks.Add(new Task
            {
                Id = Guid.NewGuid(),
                Title = "Zentrale Konfiguration",
                Description = "Alle Services sollen über eine zentrales .ini-File konfigurierbar sein (Http-Endpunkt, Datenbankfile etc.).<br />",
                Example = "example_task4.JPG"
            });

            database.Tasks.Add(new Task
            {
                Id = Guid.NewGuid(),
                Title = "UI Erweiterung",
                Description = "Erweitern Sie die Benutzeroberfläche um einen weiteren Tab, auf der Sie die Anzahl der erledigten Aufgaben im Vergleich zur Anzahl der offenen Aufgaben mit Hilfe eines Tortendiagramms visualisieren können.<br />",
                Example = "example_task5.JPG"
            });

            database.Tasks.Add(new Task
            {
                Id = Guid.NewGuid(),
                Title = "Refactoring to Async",
                Description = "Refactoring der gesamten Anwendung, so dass es keine synchronen Methodenaufrufe zum Datenbank Layer bzw. bei den Rest-Schnittstellen mehr gibt.<br />"
            });

            database.Tasks.Add(new Task
            {
                Id = Guid.NewGuid(),
                Title = "Umbau der Datenbank auf SQLite",
                Description = "Die Datenbank soll nicht mit der Datei 'database.json' abgebildet werden, sondern mit einer SQLite-Datenbank realisiert werden.<br />"
            });

            return database;
        }
    }
}
