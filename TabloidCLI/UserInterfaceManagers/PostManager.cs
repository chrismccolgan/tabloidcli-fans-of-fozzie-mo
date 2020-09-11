
using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;


namespace TabloidCLI.UserInterfaceManagers
{
    class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private string _connectionString;
        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Posts Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Remove post");
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
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine($"Title: {post.Title}\nUrl: {post.Url}");
                Console.WriteLine("-----------------------");
            }
        }

        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a post: ";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private Author ChooseAuthor(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an author:";
            }

            Console.WriteLine(prompt);

            AuthorRepository authRepository = new AuthorRepository(_connectionString);
            List<Author> authors = authRepository.GetAll();

            for (int i = 0; i < authors.Count; i++)
            {
                Author author = authors[i];
                Console.WriteLine($" {i + 1}) {author.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return authors[choice - 1];
            }
            catch
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private Blog ChooseBlog(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Blog:";
            }

            Console.WriteLine(prompt);

            BlogRepository blogRepository = new BlogRepository(_connectionString);
            List<Blog> blogs = blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1}) {blog.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("URL: ");
            post.Url = Console.ReadLine();

            Console.Write("DatePublished (Enter as MM/DD/YYYY): ");
            string strdate = Console.ReadLine();
            DateTime parsedDateTime;
            while (DateTime.TryParse(strdate, out parsedDateTime) == false)
            {
                Console.Write("DatePublished (Enter as MM/DD/YYYY): ");
                strdate = Console.ReadLine();
            }
            post.PublishDateTime = parsedDateTime;

            Author newauthor = ChooseAuthor();
            while (newauthor == null)
            {
                newauthor = ChooseAuthor();
                Console.WriteLine("Please Choose from the list above.");
            }

            
            Blog newblog = ChooseBlog();
            while (newblog == null)
            {
                newblog = ChooseBlog();
                Console.WriteLine("Please Choose from the list above.");
            }


            post.Author = newauthor;
            post.Blog = newblog;

            _postRepository.Insert(post);
            Console.WriteLine($"{post.Title} has been added.");
        }

        

        private void Edit()
        {
            Post postToEdit = Choose("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New title (blank to leave unchanged) ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.Write("New URL (blank to leave unchanged) ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }
            Console.WriteLine("New publish date (YYYY-MM-DD)(blank to leave unchanged) ");
            string date = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(date))
            {
                Nullable<DateTime> dateCheck = null;
                while (dateCheck == null)
                {
                    try
                    {
                        try
                        {
                            postToEdit.PublishDateTime = DateTime.Parse(date);
                        }
                        catch
                        {
                            Console.Write("Please use the correct format YYYY-MM-DD");
                            throw new System.Exception();
                        }
                    }
                    catch
                    {
                        date = Console.ReadLine();
                    }
                    
                }
            }  
            Console.Write("Choose new Author: ");
            Author author = ChooseAuthor();
            if (author != null)
            {
                postToEdit.Author = author;
            }
            Console.Write("Choose new Blog: ");
            Blog blog = ChooseBlog();
            if (blog != null)
            {
                postToEdit.Blog = blog;
            }

            _postRepository.Update(postToEdit);
            Console.WriteLine($"{postToEdit.Title} was edited.");
        }

        private void Remove()
        {
            Post postToDelete = Choose("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
                Console.WriteLine($"{postToDelete.Title} deleted");
            }
        }

    }
}



