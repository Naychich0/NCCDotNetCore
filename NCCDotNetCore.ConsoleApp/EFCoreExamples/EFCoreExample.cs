using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCCDotNetCore.ConsoleApp.Dtos;

namespace NCCDotNetCore.ConsoleApp.EFCoreExamples
{
    internal class EFCoreExample
    {
        private readonly AppDbContext dbContext = new AppDbContext();

        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(14);
            //Create("titleEfcore", "authorEfcore", "contentEfcore");
            //Update(10, "test2", "test2", "test2");
            Delete(2002);
        }

        private void Read()
        {
            var lst = dbContext.Blogs.ToList();
            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("-----------------------");
            }
        }

        private void Edit(int id)
        {
            var item = dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                Console.WriteLine("No Data found!");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("-----------------------");
        }

        private void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };
            dbContext.Blogs.Add(item);
            int result = dbContext.SaveChanges();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            var item = dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                Console.WriteLine("No Data found!");
                return;
            }

            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;

            int result = dbContext.SaveChanges();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            var item = dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                Console.WriteLine("No Data found!");
                return;
            }
            dbContext.Blogs.Remove(item);
            int result = dbContext.SaveChanges();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);
        }
    }
}
