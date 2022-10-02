using System;
using System.Linq;
using SqlServerEntityFramework.Context;
using SqlServerEntityFramework.Model;

namespace SqlServerEntityFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                // Create and save a new Blog
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };
                db.Set<Blog>().Add(blog);
                db.SaveChanges();

                // Display all Blogs from the database
                var query = from b in db.Set<Blog>()
                            orderby b.Name
                    select b;

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
