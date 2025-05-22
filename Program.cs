using System;
using System.Media;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace CybersecurityBot
{
    class Program
    {
        // ─────────────────────────────────────────────
        // Part 3: Conversation Flow (Track Topic)
        // ─────────────────────────────────────────────
        static string lastTopic = null;
        static Random random = new Random();

        // ─────────────────────────────────────────────
        // Part 4: Memory and Recall
        // ─────────────────────────────────────────────
        static string userInterest = null;

        // ─────────────────────────────────────────────
        // Part 1: Keyword Recognition Responses
        // ─────────────────────────────────────────────
        static Dictionary<string, string> keywordResponses = new Dictionary<string, string>()
        {
            { "password", "Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords." },
            { "scam", "Be cautious of messages asking for urgent action, payments, or sensitive info. Scammers often pretend to be trusted contacts." },
            { "privacy", "Adjust your social media and app settings to limit what data is shared. Be mindful of what you post online." }
        };

        // ─────────────────────────────────────────────
        // Part 2: Random Response Arrays
        // ─────────────────────────────────────────────
        static string[] phishingTips = new string[]
        {
            "Be cautious of emails asking for personal information.",
            "Scammers often disguise themselves as trusted organisations.",
            "Hover over links before clicking — they may lead to fake sites.",
            "Don't download attachments from unknown senders.",
            "Report phishing emails to your provider or IT department."
        };

        static string[] updateTips = new string[]
        {
            "Software updates often patch known security holes — install them promptly.",
            "Many attacks exploit outdated software. Keeping everything updated helps prevent this.",
            "Set your system to update automatically whenever possible.",
            "Updates don't just add features — they fix critical security flaws."
        };

        static string[] firewallTips = new string[]
        {
            "Firewalls act as gatekeepers between your device and the internet — they block threats.",
            "Make sure your operating system’s firewall is enabled and up-to-date.",
            "Hardware firewalls are great for network-wide protection at home or in business environments.",
            "Firewalls monitor network traffic and block unauthorized access attempts."
        };

        static void Main(string[] args)
        {
            DisplayAsciiArt();
            PlayVoiceGreeting();

            string userName = AskUserName();
            AskUserInterest(); // Part 4
            StartChat(userName);
        }

        // ─────────────────────────────────────────────
        // Voice Greeting Section
        // ─────────────────────────────────────────────
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
                player.PlaySync();
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
        /// Prompts the user to enter a topic of interest for memory recall.
        /// </summary>
        static void AskUserInterest()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nWhat cybersecurity topic are you most interested in?");
            Console.WriteLine("Examples: passwords, phishing, 2FA, privacy, etc.");
            Console.ResetColor();

            Console.Write("> ");
            string input = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(input))
            {
                userInterest = input;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Thanks! I’ll keep in mind that you're interested in {userInterest}.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("No worries! You can still ask questions anytime.");
                Console.ResetColor();
            }
        }

        static void DisplayDivider()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.ResetColor();
        }

        static void DisplaySection(string title)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"--- {title.ToUpper()} ---");
            Console.ResetColor();
        }

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
        // Part 3: Conversation Flow Helper
        // ─────────────────────────────────────────────
        static void HandleFollowUp()
        {
            if (!string.IsNullOrEmpty(lastTopic))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nWould you like to know more about {lastTopic}? You can ask follow-up questions.");
                Console.ResetColor();
            }
        }

        // ─────────────────────────────────────────────
        // Part 4: Recall Helper
        // ─────────────────────────────────────────────
        static void RemindUserInterest()
        {
            if (!string.IsNullOrEmpty(userInterest))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Earlier, you mentioned you're interested in {userInterest}. Want to ask more about that?");
                Console.ResetColor();
            }
        }

        // ─────────────────────────────────────────────
        // Chatbot Logic Section
        // ─────────────────────────────────────────────
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
- What are scams?
- I'm interested in privacy
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

                bool responded = false;

                // ─── Part 1: Keyword Recognition ───
                foreach (var keyword in keywordResponses.Keys)
                {
                    if (input.Contains(keyword))
                    {
                        TypeOut(keywordResponses[keyword]);
                        lastTopic = keyword;
                        responded = true;
                        break;
                    }
                }

                // ─── Part 2: Random Response for Phishing ───
                if (!responded && input.Contains("phishing"))
                {
                    string tip = phishingTips[random.Next(phishingTips.Length)];
                    TypeOut(tip);
                    lastTopic = "phishing";
                    responded = true;
                }

                // ─── Part 2 Extension: Random for Updates ───
                if (!responded && (input.Contains("software update") || input.Contains("updates")))
                {
                    string tip = updateTips[random.Next(updateTips.Length)];
                    TypeOut(tip);
                    lastTopic = "software updates";
                    responded = true;
                }

                // ─── Part 2 Extension: Random for Firewall ───
                if (!responded && input.Contains("firewall"))
                {
                    string tip = firewallTips[random.Next(firewallTips.Length)];
                    TypeOut(tip);
                    lastTopic = "firewalls";
                    responded = true;
                }

                // ─── Fixed Fallbacks ───
                if (!responded)
                {
                    if (input.Contains("how are you"))
                        TypeOut("I'm operating normally. Thanks for asking!");
                    else if (input.Contains("purpose"))
                        TypeOut("I'm here to provide helpful tips on cybersecurity.");
                    else if (input.Contains("what can") && input.Contains("ask"))
                        TypeOut("You can ask about passwords, phishing, 2FA, firewalls, antivirus, and more.");
                    else if (input.Contains("browsing"))
                        TypeOut("Use a secure browser, avoid suspicious sites, and enable pop-up blockers.");
                    else if (input.Contains("2fa") || input.Contains("two-factor") || input.Contains("multi-factor"))
                        TypeOut("Two-factor authentication adds extra security. Always enable it where possible.");
                    else if (input.Contains("wifi") || input.Contains("public network"))
                        TypeOut("Avoid using public Wi-Fi for banking or sensitive work. Use a VPN if needed.");
                    else if (input.Contains("antivirus"))
                        TypeOut("Yes, antivirus software helps detect and prevent threats.");
                    else if (input.Contains("social media"))
                        TypeOut("Be cautious of what you share and review your privacy settings often.");
                    else
                        TypeOut("I didn't understand that. Try rephrasing or ask something else.");

                    lastTopic = null;
                }

                HandleFollowUp();      // ─── Part 3
                RemindUserInterest();  // ─── Part 4

                Console.ResetColor();
                DisplayDivider();
            }
        }
    }
}
