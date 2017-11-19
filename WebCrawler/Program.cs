using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;

namespace WebCrawler
{
    class Program
    {
        static void Main(string[] args)
        {

            Spider sp = new Spider();
            sp.DoCrawl("https://www.reddit.com/");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Enter ID of the next Link Type back to go back");
                Console.ResetColor();
                string il = Console.ReadLine().ToLower();
                if (il.Equals("back"))
                {
                    if (sp.Depth == 0)
                        Console.WriteLine("Already on top");
                    else
                    {
                        sp.Depth--;
                        sp.printAtDepth();
                    }

                }
                else
                {
                    sp.nextCrawl(Int32.Parse(il));
                }

            }
            
            
        }



    }
}
