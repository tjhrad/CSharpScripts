using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Text.RegularExpressions;


// This program was used to parse webpages from search results that were returned and saved from a search engine.
// The parsed sites were saved to an individualized text file for each set of search terms (determined in another script).
namespace WebSearchParser
{
    class Program
    {
        static void Main(string[] args)
        {

            string downloadsFolder = "C:\\Users\\Trevor\\Downloads\\";
            string desktopFolder = "C:\\Users\\Trevor\\Desktop\\";
            string outputFolder = "C:\\Users\\Trevor\\Desktop\\webparsed\\";

            string[] fileNames = Directory.GetFiles(downloadsFolder);

            foreach (string file in fileNames)
            {
                Console.WriteLine(file);
                string fileName = Path.GetFileName(file);
                string[] names = fileName.Split(' ');
                string fileContents = File.ReadAllText(file);
                string[] fileLines = fileContents.Split('\n');
                string textToParse = "";

                foreach(string line in fileLines)
                {
                    if(line.Contains("<a href="))
                    {
                        //Console.WriteLine("Parse Me!");
                        textToParse = textToParse + line;
                    }
                    
                }

                MatchCollection matchCollection = Regex.Matches(textToParse, "<a href=\"[^\"]*\"");
                List<string> matches = matchCollection
                                        .Cast<Match>()
                                        .Select(m => m.Value)
                                        .Distinct()
                                        .ToList();

                string almostFinalSites = "";
                foreach (string site in matches) 
                {
                    foreach (string n in names)
                    {
                        if(!n.Contains("Attorney") && !n.Contains("Utah") && !n.Contains("Law") && !n.Contains("Bing") && !n.Contains("-") && n.Length > 2)
                        {
                            if (site.Contains(n) || site.Contains(n.ToLower()))
                            {
                                if (!site.Contains("bing"))
                                {
                                    if (!almostFinalSites.Contains(site.Replace("<a href=", "").Replace("\"", "")))
                                    {
                                        Console.WriteLine(site.Replace("<a href=", "").Replace("\"", ""));
                                        almostFinalSites = almostFinalSites + site.Replace("<a href=", "").Replace("\"", "") + "\n";
                                    } 
                                }

                            }
                        }
                        
                    }
                    

                }

                string[] finalizedSites = almostFinalSites.Split('\n');
                
                File.WriteAllText(outputFolder + fileName.Replace(".html", ".txt"), almostFinalSites);
            }
            Console.WriteLine("Finished!");
            Console.Read();
        }

    }
}
