using System;
using System.Media;
using System.Threading;
using System.IO;

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

        // ─────────────────────────────────────────────
        // Voice Greeting Section
        // ─────────────────────────────────────────────

        /// <summary>
        /// Plays a welcome voice greeting if the audio file exists.
        /// </summary>
        static void PlayVoiceGreeting()
        {
            string audioPath = "welcome.wav";

            if (!File.Exists(audioPath))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Voice greeting file not found. Skipping audio...");
                Console.ResetColor();
                return;
            }

            try
            {
                using SoundPlayer player = new SoundPlayer(audioPath);
                player.Load();
                player.PlaySync(); // Wait for completion
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not play greeting: " + ex.Message);
                Console.ResetColor();
            }
        }

        // ─────────────────────────────────────────────
        // UI Display Section
        // ─────────────────────────────────────────────

        /// <summary>
        /// Displays ASCII art to welcome the user.
        /// </summary>
        static void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
======================================================
              CYBERSECURITY AWARENESS BOT
======================================================

        ______                 ______         
       |      |               |      |        
       | Cyber|               |Secure|       
       |______|               |______|       
        ||  ||                [::][::]      
        ||  ||                [::][::]      
       (__)(__)               (__)(__)      

Helping you stay safe online!
");
            Console.ResetColor();
        }

        /// <summary>
        /// Asks for the user's name and validates input.
        /// </summary>
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
            Console.WriteLine($"Nice to meet you, {name}!");
            Console.ResetColor();
            return name;
        }

        /// <summary>
        /// Displays a visual divider line.
        /// </summary>
        static void DisplayDivider()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays section titles in a uniform format.
        /// </summary>
        static void DisplaySection(string title)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"--- {title.ToUpper()} ---");
            Console.ResetColor();
        }

        /// <summary>
        /// Types out a string one character at a time with optional delay.
        /// </summary>
        static void TypeOut(string message, int delay = 35)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        // ─────────────────────────────────────────────
        // Chatbot Logic Section
        // ─────────────────────────────────────────────

        /// <summary>
        /// Main chatbot interaction loop.
        /// </summary>
        static void StartChat(string name)
        {
            DisplayDivider();
            DisplaySection("Welcome Chat");

            TypeOut($"Hello {name}, I'm your Cybersecurity Assistant.");
            TypeOut("Here are some things you can ask me about:");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
- How are you?
- What's your purpose?
- What can I ask you about?
- Tell me about password safety
- What is phishing?
- Tips for safe browsing
- What is 2FA?
- Is public Wi-Fi safe?
- Why are software updates important?
- Do I need antivirus?
- How do I stay safe on social media?
- What's a firewall?
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
                    TypeOut("I didn’t catch that. Please type a question.");
                    continue;
                }

                if (input == "exit")
                {
                    DisplayDivider();
                    TypeOut("Goodbye! Stay safe online.");
                    break;
                }

                DisplayDivider();
                Console.ForegroundColor = ConsoleColor.White;

                if (input.Contains("how are you"))
                    TypeOut("I'm operating normally. Thanks for asking!");

                else if (input.Contains("purpose"))
                    TypeOut("I'm here to provide helpful tips on cybersecurity.");

                else if (input.Contains("what can") && input.Contains("ask"))
                    TypeOut("You can ask about passwords, phishing, 2FA, firewalls, antivirus, and more.");

                else if (input.Contains("password"))
                    TypeOut("Use long, complex passwords and avoid reusing them across sites.");

                else if (input.Contains("phishing"))
                    TypeOut("Never click unknown links. Always verify the source before responding.");

                else if (input.Contains("browsing"))
                    TypeOut("Use a secure browser, avoid suspicious sites, and enable pop-up blockers.");

                else if (input.Contains("2fa") || input.Contains("two-factor") || input.Contains("multi-factor"))
                    TypeOut("Two-factor authentication adds extra security. Always enable it where possible.");

                else if (input.Contains("wifi") || input.Contains("public network"))
                    TypeOut("Avoid using public Wi-Fi for banking or sensitive work. Use a VPN if needed.");

                else if (input.Contains("software update") || input.Contains("updates"))
                    TypeOut("Software updates patch vulnerabilities. Always update when available.");

                else if (input.Contains("antivirus"))
                    TypeOut("Yes, antivirus software helps detect and prevent threats.");

                else if (input.Contains("social media"))
                    TypeOut("Be cautious of what you share and review your privacy settings often.");

                else if (input.Contains("firewall"))
                    TypeOut("A firewall helps block unauthorized access. Keep it enabled.");

                else
                    TypeOut("I didn't understand that. Try rephrasing or ask something else.");

                Console.ResetColor();
                DisplayDivider();
            }
        }
    }
}

