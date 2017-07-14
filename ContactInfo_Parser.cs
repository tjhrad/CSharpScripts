using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This is the contact info parser and all of its comment-less glory.

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            string desktopFolder = "C:\\Users\\Trevor\\Desktop\\";
            string outputFile = desktopFolder + "Lawyers.csv";
            string text = "Name,Type,Status,Date Admitted, Law School, Organization, Mailing Address, City, State Province, Zip Code, Country, Work Phone, Email\n";
            string name, type, status, date, school, organization, address, city, state, zip, country, phone, email;
            name =type= status= date= school= organization= address= city= state= zip= country= phone= email= "";
            if (Directory.Exists(desktopFolder + "InfoFiles\\"))
            {
                Console.WriteLine("Scanning parent directory..");
                string[] fileNames = Directory.GetFiles(desktopFolder + "InfoFiles\\");
                foreach (string f in fileNames)
                {
                    Console.WriteLine(f);
                    string fileContents = File.ReadAllText(f);
                    string[] fileLines = fileContents.Split('\n');
                    int i = 1;
                    if (!fileContents.Contains("Inactive") && !fileContents.Contains("Deceased") 
                        && !fileContents.Contains("Susp") && !fileContents.Contains("Disability") 
                        && !fileContents.Contains("Disbarred") && !fileContents.Contains("NonMember")
                        && !fileContents.Contains("Ineligible") && !fileContents.Contains("Resign")
                        && !fileContents.Contains("Probation") && !fileContents.Contains("NotAdmit"))
                    {
                        name = type = status = date = school = organization = address = city = state = zip = country = phone = email = "";
                        foreach (string line in fileLines)
                        {
                       
                            if (line.Contains("<dt>"))
                            {
                                string editedLine = line.Replace("\t", "").Replace("<dt>", "")
                                    .Replace(@"</dt>", "").Replace("<dd>", "+")
                                    .Replace(@"</dd>", "").Replace(",", " ");
                                //Console.WriteLine(editedLine);
                                string[] editedLineArray = editedLine.Split('+');
                                string category = editedLineArray[0];
                                string data = editedLineArray[1];

                                if (line.Contains("Prefix"))
                                {
                                    name = data;
                                }
                                else if (line.Contains("First Name"))
                                {
                                    name = name + " " + data;
                                }
                                else if (line.Contains("Middle Name"))
                                {
                                    name = name + " " + data;
                                }
                                else if (line.Contains("Last Name"))
                                {
                                    name = name + " " + data;
                                }
                                else if (line.Contains("Type"))
                                {
                                    type = data;
                                }
                                else if (line.Contains("Status"))
                                {
                                    status = data;
                                }
                                else if (line.Contains("Date Admitted"))
                                {
                                    date = data;
                                }
                                else if (line.Contains("Law School"))
                                {
                                    school = data;
                                }
                                else if (line.Contains("Organization"))
                                {
                                    organization = data;
                                }
                                else if (line.Contains("Mailing Address Cont"))
                                {
                                    address = data;
                                }
                                else if (line.Contains("Mailing Address"))
                                {
                                    data = data + address;
                                    address = data;
                                }
                                else if (line.Contains("City"))
                                {
                                    city = data;
                                }
                                else if (line.Contains("Province"))
                                {
                                    state = data;
                                }
                                else if (line.Contains("Zip"))
                                {
                                    zip = data;
                                }
                                else if (line.Contains("Country"))
                                {
                                    country = data;
                                }
                                else if (line.Contains("Work Phone"))
                                {
                                    phone = data;
                                }
                                else if (line.Contains("Email Address"))
                                {
                                    email = data.Replace(@"</a>", "").Replace('>', '|').Split('|')[1];
                                    Console.WriteLine(email);
                                }

                                //category = new string(category.Where(c => char.IsLetter(c) || char.IsDigit(c)).ToArray());
                                //data = new string(data.Where(c => char.IsLetter(c) || char.IsDigit(c)).ToArray());
                                //Console.WriteLine(category + " : " + data);

                            }
                            i++;

                        }
                        text = text + name + "," + type + "," + status + "," + date + "," + school + "," + organization + ","
                               + address + "," + city + "," + state + "," + zip + "," + country + "," + phone + "," + email + "\n";
                    }
                    
                    

                }

            }

            File.WriteAllText(outputFile, text);

            Console.ReadLine();

        }
    }
}
