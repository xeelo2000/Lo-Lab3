using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

// https://www.dotnetperls.com/serialize-list
// https://www.daveoncsharp.com/2009/07/xml-serialization-of-collections/


namespace Lab1_2021
{
    public class Database : IDatabase
    {
        const String filename = "clues.db";

        List<Entry> entries;
        JsonSerializerOptions options;

        public Database()
        {
            GetEntries();
            options = new JsonSerializerOptions { WriteIndented = true };
        }


        public void AddEntry(Entry entry)
        {
            try
            {
                entry.Id = entries.Count + 1;
                entries.Add(entry);

                string jsonString = JsonSerializer.Serialize(entries, options);
                File.WriteAllText(filename, jsonString);
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Error while adding entry: {0}", ioe);
            }
        }

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
                string jsonString = JsonSerializer.Serialize(entries, options);
                File.WriteAllText(filename, jsonString);
                return true;
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Error while deleting entry: {0}", ioe);
            }
            return false;
        }

        public bool ReplaceEntry(Entry replacementEntry)
        {
            foreach (Entry entry in entries) // iterate through entries until we find the Entry in question
            {
                if (entry.Id == replacementEntry.Id) // found it
                {
                    entry.Answer = replacementEntry.Answer;
                    entry.Clue = replacementEntry.Clue;
                    entry.Difficulty = replacementEntry.Difficulty;
                    entry.Date = replacementEntry.Date;         // change it then write it out

                    try
                    {
                        string jsonString = JsonSerializer.Serialize(entries, options);
                        File.WriteAllText(filename, jsonString);
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
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Entry> GetEntries()
        {
            if (!File.Exists(filename))
            {
                File.CreateText(filename);
                entries = new List<Entry>();
                return entries;
            }

            string jsonString = File.ReadAllText(filename);
            if (jsonString.Length > 0)
            {
                entries = JsonSerializer.Deserialize<List<Entry>>(jsonString);
            }
            return entries;
        }
    }
}
