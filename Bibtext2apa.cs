// This code was copied from a much larger program that I wrote. It probably won't run as is, but I wanted to save the general idea
// to use later if I want.


// LoadReference takes a bibtex file, parses it, and then returns an APA reference.

public string author, title, journal, volume, number, pages, year, publisher, note, month, edition;
public string authorData, titleData, journalData, volumeData, numberData, pagesData, yearData, publisherData, noteData, monthData, editionData;
public string LoadReference(string reference)
{
    author = title = journal = volume = number = pages = year = publisher = note = month = edition = "";
    authorData = titleData = journalData = volumeData = numberData = pagesData = yearData = publisherData = noteData = monthData = editionData = "";
    //Console.WriteLine(reference);
    char symbol = '@';
    if (reference.Contains(symbol.ToString()))
    {
        string[] lines = reference.Split('\n');
        foreach (string ln in lines)
        {
            if (ln.Contains('='))
            {
                string[] data = ln.Replace("\n", "").Replace("\r", "").Split('=');
                if (data[0].Contains("author"))
                {
                    author = data[0];
                    authorData = data[1].Replace("{", "")
                                            .Replace("}", "");
                    authors = authorData.Replace(" and ", "|").Split('|');
                    StringBuilder sb = new StringBuilder();
                    int i = 0;
                    int last = authors.Length - 1;
                    //Console.WriteLine(last);
                    foreach (string a in authors)
                    {
                        string finalName;
                        string[] names = a.Replace(", ", "|").Split('|');
                        if (names.Length > 1)
                        {
                            string first = names[1];
                            first = first[0].ToString();
                            finalName = names[0] + ", " + first;
                            if (i == 0)
                            {
                                sb.Append(finalName);
                            }
                            else if (i == last)
                            {
                                sb.Append(" & " + finalName);
                            }
                            else
                            {
                                sb.Append(", " + finalName);
                            }

                        }
                        else
                        {
                            finalName = names[0];
                            if (i == 0)
                            {
                                sb.Append(finalName);
                            }
                            else if (i == last)
                            {
                                sb.Append(" & " + finalName);
                            }
                            else
                            {
                                sb.Append(", " + finalName);
                            }
                        }

                        i++;
                        //Console.WriteLine(finalName);
                    }
                    authorData = sb.ToString();
                    //Console.WriteLine("Here: " + authorData);
                }
                else if (data[0].Contains("title"))
                {
                    title = data[0];
                    titleData = data[1].Replace("{", "")
                                            .Replace("}", "")
                                            .Replace(".", "")
                                            .Replace(",", ""); ;
                }
                else if (data[0].Contains("journal"))
                {
                    journal = data[0];
                    journalData = data[1].Replace("{", "")
                                            .Replace("}", "")
                                            .Replace(",", ""); ;
                }
                else if (data[0].Contains("volume"))
                {
                    volume = data[0];
                    volumeData = data[1].Replace("{", "")
                                            .Replace("}", "")
                                            .Replace(",", ""); ;
                }
                else if (data[0].Contains("number"))
                {
                    number = data[0];
                    numberData = data[1].Replace("{", "")
                                            .Replace("}", "")
                                            .Replace(",", ""); ;
                }
                else if (data[0].Contains("pages"))
                {
                    pages = data[0];
                    pagesData = data[1].Replace("{", "")
                                            .Replace("}", "")
                                            .Replace(",", ""); ;
                }
                else if (data[0].Contains("year"))
                {
                    year = data[0];
                    yearData = data[1].Replace("{", "")
                                            .Replace("}", "")
                                            .Replace(",", ""); ;
                }
                else if (data[0].Contains("publisher"))
                {
                    publisher = data[0];
                    publisherData = data[1].Replace("{", "")
                                            .Replace("}", "")
                                            .Replace(",", ""); ;
                }
                else if (data[0].Contains("note"))
                {
                    note = data[0];
                    noteData = data[1].Replace("{", "")
                                            .Replace("}", "")
                                            .Replace(",", ""); ;
                }
                else if (data[0].Contains("month"))
                {
                    month = data[0];
                    monthData = data[1].Replace("{", "")
                                            .Replace("}", "")
                                            .Replace(",", ""); ;
                }
                else if (data[0].Contains("edition"))
                {
                    edition = data[0];
                    editionData = data[1].Replace("{", "")
                                            .Replace("}", "")
                                            .Replace(",", ""); ;
                }
            }

        }


        String workingReference = ""; // This string will be used to build the final apa formatted junk.
        
        //Build the reference based on what you do and don't have from the file.
        if (!authorData.Equals(""))
        {
            workingReference = workingReference + authorData + " ";
        }
        if (!yearData.Equals(""))
        {
            workingReference = workingReference + "(" + yearData + "). ";
        }
        if (!titleData.Equals(""))
        {
            workingReference = workingReference + titleData + ". ";
        }
        if (!journalData.Equals(""))
        {
            workingReference = workingReference + journalData + " ";
        }
        if (!volumeData.Equals(""))
        {
            workingReference = workingReference + volumeData;
        }
        if (!numberData.Equals(""))
        {
            workingReference = workingReference + "(" + numberData + "), ";
        }
        else
        {
            workingReference = workingReference + ", ";
        }
        if (!pagesData.Equals(""))
        {
            workingReference = workingReference + pagesData + ". ";
        }
        if (!publisherData.Equals(""))
        {
            workingReference = workingReference + publisherData + ". ";
        }

        if (!workingReference.Equals(""))
        {
            return workingReference;
        }
        else
        {
            return "An error occured. BibTeX code not complete.";
        }

    }
    else
    {
        return "An error occured. BibTeX code not detected.";
        //Console.WriteLine("Not Bibtex");
    }

}
