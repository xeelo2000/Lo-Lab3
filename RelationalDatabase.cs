﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Collections.ObjectModel;
using Npgsql;

// https://www.dotnetperls.com/serialize-list
// https://www.daveoncsharp.com/2009/07/xml-serialization-of-collections/


namespace Lab2Solution
{

    /// <summary>
    /// This is the database class, currently a FlatFileDatabase
    /// </summary>
    public class RelationalDatabase : IDatabase
    {


        String connectionString;
        /// <summary>
        /// A local version of the database, we *might* want to keep this in the code and merely
        /// adjust it whenever Add(), Delete() or Edit() is called
        /// Alternatively, we could delete this, meaning we will reach out to bit.io for everything
        /// What are the costs of that?
        /// There are always tradeoffs in software engineering.
        /// </summary>
        ObservableCollection<Entry> entries = new ObservableCollection<Entry>();

        JsonSerializerOptions options;


        /// <summary>
        /// Here or thereabouts initialize a connectionString that will be used in all the SQL calls
        /// </summary>
        public RelationalDatabase()
        {

            connectionString = InitializeConnectionString();
        }


        /// <summary>
        /// Adds an entry to the database
        /// </summary>
        /// <param name="entry">the entry to add</param>
        public void AddEntry(Entry entry)
        {
            try
            {
                entry.Id = entries.Count + 1;
                entries.Add(entry);

                // write the SQL to INSERT entry into bit.io
              

            }
            catch (IOException ioe)
            {
                Console.WriteLine("Error while adding entry: {0}", ioe);
            }
        }


        /// <summary>
        /// Finds a specific entry
        /// </summary>
        /// <param name="id">id to find</param>
        /// <returns>the Entry (if available)</returns>
        public Entry FindEntry(int id)
        {
            foreach (Entry entry in entries)
            {
                if (entry.Id == id)
                {
                    return entry;
                }
            }
            return null;
        }

        /// <summary>
        /// Deletes an entry 
        /// </summary>
        /// 
        /// <param name="entry">An entry, which is presumed to exist</param>
        public bool DeleteEntry(Entry entry)
        {
            try
            {
                var result = entries.Remove(entry);


                // Write the SQL to DELETE entry from bit.io. You have its id, that should be all that you need



                return true;
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Error while deleting entry: {0}", ioe);
            }
            return false;
        }

        /// <summary>
        /// Edits an entry
        /// </summary>
        /// <param name="replacementEntry"></param>
        /// <returns>true if editing was successful</returns>
        public bool EditEntry(Entry replacementEntry)
        {
            foreach (Entry entry in entries) // iterate through entries until we find the Entry in question
            {
                if (entry.Id == replacementEntry.Id) // found it
                {
                    entry.Answer = replacementEntry.Answer;
                    entry.Clue = replacementEntry.Clue;
                    entry.Difficulty = replacementEntry.Difficulty;
                    entry.Date = replacementEntry.Date;

                    try
                    {
                       /// write the SQL to UPDATE the entry. Again, you have its id, which should be all you need.

                        return true;
                    }
                    catch (IOException ioe)
                    {
                        Console.WriteLine("Error while replacing entry: {0}", ioe);
                    }

                }
            }
            return false;
        }

        /// <summary>
        /// Retrieves all the entries
        /// </summary>
        /// <returns>all of the entries</returns>
        public ObservableCollection<Entry> GetEntries()
        {
            while (entries.Count > 0)
            {
                entries.RemoveAt(0);
            }

            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            var sql = "SELECT * FROM \"entries\" limit 10;";

            using var cmd = new NpgsqlCommand(sql, con);

            using NpgsqlDataReader reader = cmd.ExecuteReader();

            // Columns are clue, answer, difficulty, date, id in that order ...
            // Show all data
            while (reader.Read())
            {
                for (int colNum = 0; colNum < reader.FieldCount; colNum++)
                {
                    Console.Write(reader.GetName(colNum) + "=" + reader[colNum] + " ");
                }
                Console.Write("\n");
                entries.Add(new Entry(reader[0] as String, reader[1] as String, (int)reader[2], reader[3] as String, (int)reader[4]));
            }

            con.Close();



            return entries;
        }

        /// <summary>
        /// Creates the connection string to be utilized throughout the program
        /// 
        /// </summary>
        public String InitializeConnectionString()
        {
            var bitHost = "db.bit.io";
            var bitApiKey = ""; // from the "Password" field of the "Connect" menu

            var bitUser = "";
            var bitDbName = "";

            return connectionString = $"Host={bitHost};Username={bitUser};Password={bitApiKey};Database={bitDbName}";
        }
    }
}