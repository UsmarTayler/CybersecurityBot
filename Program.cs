using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading;

namespace CybersecurityBot
{
    class Program
    {
        static string lastTopic = null;
        static List<string> userInterests = new List<string>();
        static string userMood = null;
        static Random random = new Random();

        static Dictionary<string, string> keywordResponses = new Dictionary<string, string>()
        {
            { "password", "Use strong, unique passwords and avoid reusing them. Consider using a password manager." },
            { "scam", "Be cautious of messages urging urgent action or asking for private info. Scammers often impersonate trusted sources." },
            { "privacy", "Review your social media and app permissions. Only share what’s necessary." },
            { "malware", "Malware is malicious software designed to harm your system. Keep your antivirus up to date to help detect it." },
            { "vpn", "A VPN encrypts your internet connection and is useful when using public networks." },
            { "encryption", "Encryption protects your data by making it unreadable to unauthorized users." },
            { "backups", "Regular backups ensure you can recover your data in case of loss or ransomware attacks." },
            { "ransomware", "Ransomware locks your files and demands payment. Always keep backups and never pay the ransom." },
            { "patch", "Software patches fix security flaws. Always install them promptly." },
            { "identity theft", "Protect your personal information and monitor accounts to prevent identity theft." }
        };

        static string[] phishingTips = {
            "Be cautious of emails asking for personal info.",
            "Hover over links before clicking them.",
            "Don't open unexpected attachments.",
            "Verify sender identity before responding."
        };

        static string[] updateTips = {
            "Software updates patch known security holes — install them promptly.",
            "Enable automatic updates to stay protected.",
            "Hackers target outdated software. Don't wait to update."
        };

        static string[] firewallTips = {
            "A firewall acts as a barrier between your device and the internet.",
            "Keep your firewall enabled to prevent unauthorized access.",
            "Use hardware firewalls for better protection at home or work."
        };

        static string[] sentimentWords = { "worried", "scared", "nervous", "confused", "frustrated", "curious" };

        static void Main(string[] args)
        {
            DisplayAsciiArt();
            PlayVoiceGreeting();

            string name = AskUserName();
            AskUserMood();
            AskUserInterest();
            StartChat(name);
        }

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

Helping you stay safe online!");
            Console.ResetColor();
        }

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

        static void AskUserMood()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nHow are you feeling today?");
            Console.WriteLine("Examples: curious, confused, worried, frustrated, etc.");
            Console.ResetColor();

            Console.Write("> ");
            string mood = Console.ReadLine()?.Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(mood))
            {
                foreach (var word in sentimentWords)
                {
                    if (mood.Contains(word))
                    {
                        userMood = word;
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"Thanks for sharing. I understand you're feeling {userMood} — let's work through things together.");
                        Console.ResetColor();
                        return;
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Got it. Let's dive into your questions whenever you're ready.");
            Console.ResetColor();
        }

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
                if (!userInterests.Contains(input, StringComparer.OrdinalIgnoreCase))
                {
                    userInterests.Add(input);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"Thanks! I'll remember you're interested in {input}.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"You've already shared you're interested in {input}.");
                }
                Console.ResetColor();
            }
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

        static void DisplayDivider()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.ResetColor();
        }

        static void RemindUserInterest()
        {
            if (userInterests.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Earlier, you mentioned you're interested in: " + string.Join(", ", userInterests));
                Console.ResetColor();
            }
        }

        static void HandleFollowUp()
        {
            if (!string.IsNullOrWhiteSpace(lastTopic))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nWould you like to know more about {lastTopic}?");
                Console.ResetColor();
            }
        }

        static void StartChat(string name)
        {
            DisplayDivider();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("--- WELCOME CHAT ---");
            Console.ResetColor();

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
                    TypeOut("I didn’t catch that. Try typing something like 'what is phishing?'.");
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

                if (input.StartsWith("i'm interested in") || input.StartsWith("i am interested in"))
                {
                    string interest = input.Replace("i'm interested in", "").Replace("i am interested in", "").Trim();
                    if (!userInterests.Contains(interest, StringComparer.OrdinalIgnoreCase))
                    {
                        userInterests.Add(interest);
                        TypeOut($"Got it! I'll remember you're interested in {interest}.");
                    }
                    else
                    {
                        TypeOut($"You've already mentioned you're interested in {interest}.");
                    }
                    lastTopic = null;

                    // Moved below to avoid overriding randomized keywords

                    if (!responded && input.Contains("phishing"))
                    {
                        int index = random.Next(phishingTips.Length);
                        while (lastTopic == $"phishing_{index}")
                        {
                            index = random.Next(phishingTips.Length);
                        }
                        TypeOut(phishingTips[index]);
                        lastTopic = $"phishing_{index}";
                        responded = true;
                    }
                    else if (!responded && input.Contains("password"))
                    {
                        string[] passwordTips = {
                        "Use long, complex passwords that include symbols and numbers.",
                        "Never reuse the same password across multiple sites.",
                        "Consider using a password manager to generate and store passwords securely."
                    };
                        int index = random.Next(passwordTips.Length);
                        while (lastTopic == $"password_{index}")
                        {
                            index = random.Next(passwordTips.Length);
                        }
                        TypeOut(passwordTips[index]);
                        lastTopic = $"password_{index}";
                        responded = true;
                    }
                    else if (!responded && input.Contains("privacy"))
                    {
                        string[] privacyTips = {
                        "Review your privacy settings on social media regularly.",
                        "Only share personal information with trusted sources.",
                        "Limit location sharing unless absolutely necessary."
                    };
                        int index = random.Next(privacyTips.Length);
                        while (lastTopic == $"privacy_{index}")
                        {
                            index = random.Next(privacyTips.Length);
                        }
                        TypeOut(privacyTips[index]);
                        lastTopic = $"privacy_{index}";
                        responded = true;
                    }
                    else if (!responded && input.Contains("antivirus"))
                    {
                        string[] antivirusTips = {
                        "Keep your antivirus software updated to detect new threats.",
                        "Run regular scans to ensure your system is secure.",
                        "Avoid downloading files from untrusted sources."
                    };
                        int index = random.Next(antivirusTips.Length);
                        while (lastTopic == $"antivirus_{index}")
                        {
                            index = random.Next(antivirusTips.Length);
                        }
                        TypeOut(antivirusTips[index]);
                        lastTopic = $"antivirus_{index}";
                        responded = true;
                    }
                    else if (!responded && input.Contains("social media"))
                    {
                        string[] socialMediaTips = {
                        "Think before you post — what you share can be permanent.",
                        "Adjust your privacy settings to control who sees your content.",
                        "Avoid accepting friend requests from strangers."
                    };
                        int index = random.Next(socialMediaTips.Length);
                        while (lastTopic == $"socialmedia_{index}")
                        {
                            index = random.Next(socialMediaTips.Length);
                        }
                        TypeOut(socialMediaTips[index]);
                        lastTopic = $"socialmedia_{index}";
                        responded = true;
                    }
                    else if (!responded && (input.Contains("software update") || input.Contains("updates")))
                    {
                        TypeOut(updateTips[random.Next(updateTips.Length)]);
                        lastTopic = "software updates";
                        responded = true;
                    }
                    else if (!responded && input.Contains("firewall"))
                    {
                        TypeOut(firewallTips[random.Next(firewallTips.Length)]);
                        lastTopic = "firewalls";
                        responded = true;
                    }

                    if (!responded)
                    {
                        foreach (var keyword in keywordResponses)
                        {
                            if (input.Contains(keyword.Key))
                            {
                                TypeOut(keyword.Value);
                                lastTopic = keyword.Key;
                                responded = true;
                                break;
                            }
                        }

                        if (!responded)
                        {
                            if (input.Contains("how are you"))
                                TypeOut("I'm operating smoothly, thanks for asking!");
                            else if (input.Contains("purpose"))
                                TypeOut("My purpose is to guide you through cybersecurity topics and keep you informed.");
                            else if (input.Contains("what can") && input.Contains("ask"))
                                TypeOut("You can ask me about topics like password safety, phishing, antivirus, updates, and more.");
                            else if (input.Contains("browsing"))
                                TypeOut("Avoid clicking unknown links. Always use updated browsers and avoid popups.");
                            else if (input.Contains("2fa") || input.Contains("two-factor") || input.Contains("multi-factor"))
                                TypeOut("Two-factor authentication adds an extra layer of security. Always enable it.");
                            else if (input.Contains("wifi") || input.Contains("public network"))
                                TypeOut("Avoid banking on public Wi-Fi. Use VPNs when necessary.");
                            else
                            {
                                string[] fallbackResponses = {
                                "I'm not sure I understand that yet. Try asking about phishing, passwords, or 2FA.",
                                "Hmm, that’s a new one for me. Are you asking about a cybersecurity concept or threat?",
                                "Interesting! I'm not familiar with that. Can you try asking it another way?"
                            };
                                TypeOut(fallbackResponses[random.Next(fallbackResponses.Length)]);
                            }

                            lastTopic = null;
                            responded = true;
                        }
                    }
                    responded = true;
                }

                HandleFollowUp();
                RemindUserInterest();
                Console.ResetColor();
                DisplayDivider();
            }
        }
    }
}
