using System;
using System.Collections.Generic;
using System.ComponentModel;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;
        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blog");
            Console.WriteLine(" 2) Add A Blog");
            Console.WriteLine(" 3) Edit Blog");
            Console.WriteLine(" 4) Remove Blog");
            Console.WriteLine(" 5) Blog Details");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    List();
                    return this;
                case "2":
                    Console.Clear();
                    Add();
                    return this;
                case "3":
                    Console.Clear();
                    Edit();
                    return this;
                case "4":
                    Console.Clear();
                    Remove();
                    return this;
                case "5":
                    Console.Clear();
                    Blog blog = Choose();
                    if (blog == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new BlogDetailManager(this, _connectionString, blog.Id);
                    }
                case "0":
                    Console.Clear();
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            //blogs is a list of all the blogs in the database;
            List<Blog> blogs = _blogRepository.GetAll();
            //iterate through the blogs and display each blog's title and url
            foreach (Blog blog in blogs) 
            {
                //ToString() method on Blog class returns title and url of blog
                Console.WriteLine(blog.ToString());
            }
        }

        //user has option to choose a blog from the list
        //returns a blog
        private Blog Choose(string prompt = null)
        {
            //if there is no prompt, give prompt the string value
            if (prompt == null)
            {
                prompt = "Please choose a Blog:";
            }

            //display prompt
            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            //iterate through the blogs list; set blog to value of each iteration of an individual blog
            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                //whatever the index of i add 1(prevents showing the user 0 as an option) and display the title of blog
                Console.WriteLine($"{i + 1} {blog.Title}");
            }
            Console.Write("> ");

            //gets user's number choice as string
            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                //because added 1, now have to subtract 1 to get actual index of blog in list
                return blogs[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }

        }

        private void Add()
        {
            
            //instanstiate new blog class;
            Blog blog = new Blog();

            Console.WriteLine("***New Blog***");
            Console.Write("Title of this blog: ");
            blog.Title = Console.ReadLine();
            
            //while (blog.Title == "")
            //{
            //    Console.WriteLine("***You must input a title***");
            //    Console.WriteLine("Title of this blog? ");
            //    blog.Title = Console.ReadLine();
            //}

            while (blog.Title.Length > 55 || blog.Title.Length <= 0)
            {
                Console.WriteLine("***Invalid title format. You cannot leave this blank and you must input a title that is less than 55 characters.***");
                Console.WriteLine("What's the title of this entry? ");
                blog.Title = Console.ReadLine();
            }

            Console.Write("Url: ");
            blog.Url = Console.ReadLine();

            while (blog.Url == "")
            {
                Console.WriteLine("You must input an URL");
                Console.Write("Url: ");
                blog.Url = Console.ReadLine();
            }

                _blogRepository.Insert(blog);

        }

        private void Edit()
        {
            //blogToEdit will hold the user's choice --> Choose() method will return that specific blog they chose;
            Blog blogToEdit = Choose("Which blog would you like to edit?");
            //if user doesn't give an answer; return
            if (blogToEdit == null)
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
                blogToEdit.Title = title;
            }
            Console.Write("New Url (blank to leave unchanged): ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                blogToEdit.Url = url;
            }
           
            _blogRepository.Update(blogToEdit);
        }

        private void Remove()
        {
            Blog blogToDelete = Choose("Which blog would you like to remove?");
           
                if (blogToDelete != null)
                {
                    _blogRepository.Delete(blogToDelete.Id);
                }
          
        }
    }
}