using System;
using System.Collections.Generic;
using TabloidCLI.Models;


namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;
        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;

        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journal Entries");
            Console.WriteLine(" 2) Add Journal Entry");
            Console.WriteLine(" 3) Edit Journal Entry");
            Console.WriteLine(" 4) Remove Journal Entry");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Journal> journals = _journalRepository.GetAll();
            foreach (Journal journal in journals)
            {
                Console.WriteLine($"Entered on: {journal.CreateDateTime} \n {journal.Title}: {journal.Content}");
                
            }
        }

        private void Add()
        {

            Journal journal = new Journal();

            Console.WriteLine("-------------------");
            Console.WriteLine("***New Journal Entry***");
            Console.WriteLine("What's the title of this entry? ");
            journal.Title = Console.ReadLine();
            while (journal.Title == "")
            {
                Console.WriteLine("***You must input a title***");
                Console.WriteLine("What's the title of this entry?");
                journal.Title = Console.ReadLine();
            }

            while (journal.Title.Length > 55)
            {
                Console.WriteLine("***You cannot exceed 55 characters for the title. Please shorten your title***");
                Console.WriteLine("What's the title of this entry? ");
                journal.Title = Console.ReadLine();
            }

            Console.Write("What do you have to say today? ");
            journal.Content = Console.ReadLine();

            while (journal.Content == "")
            {
                Console.WriteLine("You must input content");
                Console.Write("What do you have to say today? ");
                journal.Content = Console.ReadLine();
            }

            journal.CreateDateTime = DateTime.Now;
            
            _journalRepository.Insert(journal);
        }

        private Journal Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a journal entry:";
            }

            Console.WriteLine(prompt);

            List<Journal> journals = _journalRepository.GetAll();

            for (int i = 0; i < journals.Count; i++)
            {
                Journal journal = journals[i];
                Console.WriteLine($" {i + 1}) {journal.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return journals[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Edit()
        {
            Journal journalToEdit = Choose("Which journal would you like to edit?");
            if (journalToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged): ");
            string title = Console.ReadLine();

            while (title.Length > 55)
            {
                Console.WriteLine("***Your Title cannot exceed 55 characters. Please try again.***");
                Console.Write("New Title (blank to leave unchanged): ");
                title = Console.ReadLine();
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                journalToEdit.Title = title;
            }
            Console.Write("New Content (blank to leave unchanged): ");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                journalToEdit.Content = content;
            }

            _journalRepository.Update(journalToEdit);
        }

        private void Remove()
        {
            Journal journalToDelete = Choose("Which journal would you like to remove?");
            if (journalToDelete != null)
            {
                _journalRepository.Delete(journalToDelete.Id);
            }
        }


    }
}