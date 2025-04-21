
using System;
using System.Media;
using System.Threading;
using System.IO;
using System.Windows.Media;

namespace CybersecurityBot
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayAsciiArt();
            PlayVoiceGreeting();

            string userName = AskUserName();
            StartChat(userName);
        }

        static void PlayVoiceGreeting()
        {
            string audioPath = "welcome.wav";

            if (!File.Exists(audioPath))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️ Voice greeting file not found. Skipping audio...");
                Console.ResetColor();
                return;
            }

            try
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(Path.GetFullPath(audioPath)));
                player.Play();

                // Wait for the audio to finish playing  
                Thread.Sleep(3000); // Adjust the duration based on the audio length  
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not play greeting: " + ex.Message);
                Console.ResetColor();
            }
        }

        static void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"  
            ______  
           |      |  
           | Cyber|  
           |______|  
            ||  ||  
            ||  ||  
           (__) (__)  

        Cybersecurity Awareness Bot  
          Helping you stay safe online!  
       ");
            Console.ResetColor();
        }

        static string AskUserName()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Before we begin, what's your name? ");
            Console.ResetColor();
            string name = Console.ReadLine()?.Trim();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Name cannot be empty. Please enter your name.");
                Console.ResetColor();
                name = Console.ReadLine()?.Trim();
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Nice to meet you, {name}! 😊");
            Console.ResetColor();
            return name;
        }

        static void DisplayDivider()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n--------------------------------------------------\n");
            Console.ResetColor();
        }

        static void DisplaySection(string title)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"=== {title.ToUpper()} ===");
            Console.ResetColor();
        }

        static void TypeOut(string message, int delay = 40)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        

    }
}
