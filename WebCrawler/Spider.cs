using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace WebCrawler
{

    struct dbLink
    {
        public int depth;
        public int branch;
        public string link;
        public string name;
    }

    class Spider
    {
        public string CurrentURL { get; set; }
        public int linksFound { get; set; }
        public List<dbLink> Links { get; set; }
        // public List<string> CurrentLinks { get; set; }
        //  public List<string> CurrentNames { get; set; }
        public bool isWorking { get; set; }
        public int Depth { get; set; }
        public int Branch { get; set; }
        public bool ConsoleOutput { get; set; }

        public Spider()
        {
            //CurrentLinks = new List<string>();
            // CurrentNames = new List<string>();
            Depth = 0;
            Branch = 0;
            Links = new List<dbLink>();
            linksFound = 0;
            isWorking = false;
            ConsoleOutput = true;
        }

        public string DoCrawl(string url)
        {
            isWorking = true;
            CurrentURL = url;

            HtmlWeb hw = new HtmlWeb();
            //Rest all values
            //CurrentLinks.Clear();
            //CurrentNames.Clear();
            linksFound = 0;

            string output = "";

            HtmlDocument doc = hw.Load(CurrentURL);

            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {

                if (link.Attributes["href"].Value.Equals("#")) { }
                else
                {

                    output += link.InnerText + " -- " + link.Attributes["href"].Value;
                    output += "\n";

                    //CurrentLinks.Add(link.Attributes["href"].Value);
                    //CurrentNames.Add(link.InnerText);
                    dbLink d = new dbLink();
                    d.branch = Branch;
                    d.depth = Depth;
                    d.link = link.Attributes["href"].Value;
                    d.name = link.InnerText;
                    Links.Add(d);

                    if (ConsoleOutput) { Console.WriteLine("ID: " + Links.IndexOf(d) + " | Link: " + d.link + "| Name: " + d.name + "| depth/branch: " + d.depth+ "/" + d.branch);}

                    linksFound++;
                    Branch++;
                }
            }

            isWorking = false;
            Branch = 0;
            return output;
            
        }

        public void nextCrawl(int branchID)
        {
            Depth++;
            DoCrawl(Links[branchID].link);
        }

        //Print the links at current depth
        public void printAtDepth()
        {
            foreach (dbLink d in Links)
            {
                if (d.depth == Depth)
                {
                    Console.WriteLine("ID: " + Links.IndexOf(d) + " | Link: " + d.link + "| Name: " + d.name + "| depth/branch: " + d.depth + "/" + d.branch);
                }
            }
        }


    }
}
