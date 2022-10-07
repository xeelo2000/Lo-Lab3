using System;
using System.Collections.Generic;

namespace Lab1_2021
{

    public enum InvalidFieldError
    {
        InvalidClueLength,
        InvalidAnswerLength,
        InvalidDifficulty,
        InvalidDate,
        NoError
    }

    public enum EntryDeletionError
    {
        EntryNotFound,
        DBDeletionError,
        NoError
    }

    public enum EntryEditError
    {
        EntryNotFound,
        InvalidFieldError,
        DBEditError,
        NoError
    }
    public class BusinessLogic : IBusinessLogic
    {
        const int MAX_CLUE_LENGTH = 250;
        const int MAX_ANSWER_LENGTH = 21;
        const int MAX_DIFFICULTY = 5;
        int latestId = 0;

        IUserInterface ui;
        IDatabase db;

        public BusinessLogic()
        {
            db = new Database();
            ui = new UserInterface(this);
            ui.DisplayMenu();
        }


        public List<Entry> GetEntries()
        {
            return db.GetEntries();
        }

        public Entry FindEntry(int id)
        {
            return db.FindEntry(id);
        }

        private InvalidFieldError CheckEntryFields(string clue, string answer, int difficulty, string date)
        {
            if (clue.Length < 1 || clue.Length > MAX_CLUE_LENGTH)
            {
                return InvalidFieldError.InvalidClueLength;
            }
            if (answer.Length < 1 || answer.Length > MAX_ANSWER_LENGTH)
            {
                return InvalidFieldError.InvalidAnswerLength;
            }
            if (difficulty < 1 || difficulty > MAX_DIFFICULTY)
            {
                return InvalidFieldError.InvalidDifficulty;
            }

            return InvalidFieldError.NoError;
        }


        public InvalidFieldError AddEntry(string clue, string answer, int difficulty, string date)
        {

            var result = CheckEntryFields(clue, answer, difficulty, date);
            if (result != InvalidFieldError.NoError)
            {
                return result;
            }
            Entry entry = new Entry(clue, answer, difficulty, date, ++latestId);
            db.AddEntry(entry);

            return InvalidFieldError.NoError;
        }

        public EntryDeletionError DeleteEntry(int entryId)
        {

            var entry = db.FindEntry(entryId);

            if (entry != null)
            {
                bool success = db.DeleteEntry(entry);
                if (success)
                {
                    return EntryDeletionError.NoError;

                }
                else
                {
                    return EntryDeletionError.DBDeletionError;
                }
            }
            else
            {
                return EntryDeletionError.EntryNotFound;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clue"></param>
        /// <param name="answer"></param>
        /// <param name="difficulty"></param>
        /// <param name="date"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public EntryEditError EditEntry(string clue, string answer, int difficulty, string date, int id)
        {

            var fieldCheck = CheckEntryFields(clue, answer, difficulty, date);
            if (fieldCheck != InvalidFieldError.NoError)
            {
                return EntryEditError.InvalidFieldError;
            }

            var entry = db.FindEntry(id);
            entry.Clue = clue;
            entry.Answer = answer;
            entry.Difficulty = difficulty;
            entry.Date = date;

            bool success = db.ReplaceEntry(entry);
            if (!success)
            {
                return EntryEditError.DBEditError;
            }

            return EntryEditError.NoError;
        }
    }


}
