﻿
namespace Lab2Solution
{

    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            EntriesLV.ItemsSource = MauiProgram.ibl.GetEntries();
        }

        void AddEntry(System.Object sender, System.EventArgs e)
        {
            String clue = clueENT.Text;
            String answer = answerENT.Text;
            String date = dateENT.Text;

            int difficulty;
            bool validDifficulty = int.TryParse(difficultyENT.Text, out difficulty);
            if (validDifficulty)
            {
                InvalidFieldError invalidFieldError = MauiProgram.ibl.AddEntry(clue, answer, difficulty, date);
                if(invalidFieldError != InvalidFieldError.NoError)
                {
                    DisplayAlert("An error has occurred while adding an entry", $"{invalidFieldError}", "OK");
                }
            }
            else
            {
                DisplayAlert("Difficulty", $"Please enter a valid number", "OK");
            }
        }

        void DeleteEntry(System.Object sender, System.EventArgs e)
        {
            Entry selectedEntry = EntriesLV.SelectedItem as Entry;
            if (selectedEntry == null)
            {
                DisplayAlert("An error has occurred while deleting an entry", "No entry selected", "OK");

            }
            else
            {
                EntryDeletionError entryDeletionError = MauiProgram.ibl.DeleteEntry(selectedEntry.Id);
                if (entryDeletionError != EntryDeletionError.NoError)
                {
                    DisplayAlert("An error has occurred while deleting an entry", $"{entryDeletionError}", "OK");
                }
            }

            
        }

        void EditEntry(System.Object sender, System.EventArgs e)
        {

            Entry selectedEntry = EntriesLV.SelectedItem as Entry;
            if (selectedEntry == null)
            {
                DisplayAlert("An error has occurred while editing an entry", "No entry selected", "OK");
                return;
            }

            selectedEntry.Clue = clueENT.Text;
            selectedEntry.Answer = answerENT.Text;
            selectedEntry.Date = dateENT.Text;


            int difficulty;
            bool validDifficulty = int.TryParse(difficultyENT.Text, out difficulty);
            if (validDifficulty)
            {
                selectedEntry.Difficulty = difficulty;
                Console.WriteLine($"Difficuilt is {selectedEntry.Difficulty}");
                EntryEditError entryEditError = MauiProgram.ibl.EditEntry(clueENT.Text, answerENT.Text, difficulty, dateENT.Text, selectedEntry.Id);
                if(entryEditError != EntryEditError.NoError)
                {
                    DisplayAlert("An error has occurred while editing an entry", $"{entryEditError}", "OK");
                }
            }
            else
            {
                DisplayAlert("An error has occurred while editing an entry", "Invalid Difficulty", "Ok");
            }
        }

        void EntriesLV_ItemSelected(System.Object sender, Microsoft.Maui.Controls.SelectedItemChangedEventArgs e)
        {
            Entry selectedEntry = e.SelectedItem as Entry;
            clueENT.Text = selectedEntry.Clue;
            answerENT.Text = selectedEntry.Answer;
            difficultyENT.Text = selectedEntry.Difficulty.ToString();
            dateENT.Text = selectedEntry.Date;

        }

        void SortByClue(System.Object sender, System.EventArgs e)
        {
            SortByClueError sortByClueError = MauiProgram.ibl.SortByClue();
            if (sortByClueError != SortByClueError.NoError)
            {
                DisplayAlert("An error has occurred while sorting entry by clue", $"{sortByClueError}", "OK");
            }

        }

        void SortByAnswer(System.Object sender, System.EventArgs e)
        {
            SortByAnswerError sortByAnswerError = MauiProgram.ibl.SortByAnswer();
            if (sortByAnswerError != SortByAnswerError.NoError)
            {
                DisplayAlert("An error has occurred while sorting entry by answer", $"{sortByAnswerError}", "OK");
            }

        }




    }
}

