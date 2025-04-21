
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
        // Voice Recordding implemetation
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
        // Start of Art Section/ code
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
        // chatbot and its questions with answers
        static void StartChat(string name)
        {
            DisplayDivider();
            DisplaySection("Welcome Chat");

            TypeOut($"Hey {name}, I'm here to help you stay safe online. 😊");
            TypeOut("You can ask me questions like:");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
  • How are you?
  • What's your purpose?
  • What can I ask you about?
  • Tell me about password safety
  • What is phishing?
  • Tips for safe browsing
  • What is 2FA?
  • Is public Wi-Fi safe?
  • Why are software updates important?
  • Do I need antivirus?
  • How do I stay safe on social media?
  • What's a firewall?
");
            Console.ResetColor();

            TypeOut("Type your question below or type 'exit' to leave the chat.\n");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("> ");
                Console.ResetColor();

                string input = Console.ReadLine()?.ToLower().Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    TypeOut("Hmm... I didn’t catch that. Try typing a question.");
                    continue;
                }

                if (input == "exit")
                {
                    DisplayDivider();
                    TypeOut("Goodbye! Stay cyber-safe out there! 🛡️");
                    break;
                }

                DisplayDivider();
                Console.ForegroundColor = ConsoleColor.White;

                if (input.Contains("how are you"))
                {
                    TypeOut("I'm running as expected! Thank you for asking 😊");
                }
                else if (input.Contains("purpose"))
                {
                    TypeOut("I'm here to guide you through cybersecurity tips and tricks.");
                }
                else if (input.Contains("what can") && input.Contains("ask"))
                {
                    TypeOut("You can ask me about password safety, phishing, safe browsing, 2FA, social media, firewalls, and more!");
                }
                else if (input.Contains("password"))
                {
                    TypeOut("Use a mix of upper/lowercase letters, numbers, and symbols — and never reuse passwords!");
                }
                else if (input.Contains("phishing"))
                {
                    TypeOut("Avoid clicking suspicious links. Always verify the sender before replying to emails.");
                }
                else if (input.Contains("safe browsing") || input.Contains("browsing"))
                {
                    TypeOut("Keep your browser updated, block pop-ups, and avoid suspicious websites.");
                }
                else if (input.Contains("2fa") || input.Contains("two-factor") || input.Contains("multi-factor"))
                {
                    TypeOut("2FA adds an extra layer of security. Always enable it where possible!");
                }
                else if (input.Contains("wifi") || input.Contains("public network"))
                {
                    TypeOut("Avoid accessing sensitive accounts over public Wi-Fi. Use a VPN when needed.");
                }
                else if (input.Contains("software update") || input.Contains("updates"))
                {
                    TypeOut("Always install software updates — they often fix security vulnerabilities.");
                }
                else if (input.Contains("antivirus"))
                {
                    TypeOut("Keep your antivirus up-to-date and run regular scans to catch threats early.");
                }
                else if (input.Contains("social media"))
                {
                    TypeOut("Be mindful of what you share online. Use strong privacy settings on social platforms.");
                }
                else if (input.Contains("firewall"))
                {
                    TypeOut("A firewall monitors incoming/outgoing traffic — always keep it enabled!");
                }
                else
                {
                    TypeOut("I didn’t quite understand that. Could you rephrase?");
                }

                Console.ResetColor();
                DisplayDivider();
            }
        }

    }
}
