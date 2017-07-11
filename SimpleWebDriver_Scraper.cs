using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using WindowsInput;

namespace ConsoleApp1
{
    class Program
    {
    
    // The purpose of this script is to download a website after it has received information from a local server.
    
    // I used this script as an alternative to curl and wget because those two programs were only returning the source code
    // from the page and not the text of the page after it had received sensitive data from the site's server.
    
    // The only solution I could come up with (i'm sure there is a better one, but I tried many things) is to use a webdriver 
    // to open up the website in the Chrome browser (trick the server), and then send simulated keyboard shortcut commands 
    // to the window to allow me to save the site to my computer.
    
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();

            string url = "url.goes.here";
            
            //This code contains a loop because I wanted to download a ton of hidden pages from the domain
            //This loop would need to be modified for whatever you are trying to download
            int i = 0; 
            
            while (i <= 20000)
            {
                //Open the web page
                driver.Navigate().GoToUrl(url + i.ToString()); 

                System.Threading.Thread.Sleep(3000); // wait for the page to load completely
                InputSimulator inputSimulator = new InputSimulator();
                
                //Simulate a control + S key press
                inputSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.CONTROL);
                inputSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_S);
                inputSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.CONTROL);
                inputSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_S);
                
                //Wait for the save as message box to come up and then simulate a RETURN key press
                System.Threading.Thread.Sleep(500);
                inputSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.RETURN);
                inputSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.RETURN);
                
                //If a message box comes up saying you are going to override a save file,
                //simulate hitting the "yes" button on the message box by hitting LEFT and then RETURN
                inputSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.LEFT);
                inputSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.LEFT);

                inputSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.RETURN);
                inputSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.RETURN);
                System.Threading.Thread.Sleep(3000); // Wait a bit for the download to start so you don't cancel it
                
                i++;
            }
            
        }
    }
}
