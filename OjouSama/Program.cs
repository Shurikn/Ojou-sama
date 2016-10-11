using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using System.Text.RegularExpressions;

namespace OjouSama
{
    
    class Program
    {
        /// <summary>
        /// Regex to find an emoji in a message
        /// </summary>
        const String regex = @"<:[a-zA-Z0-9_]{2,}:(\d+)>";

        static void Main(string[] args)
        {
            DiscordClient client = new DiscordClient("BOT_TOKEN_HERE", true);
            Console.WriteLine("Attempting to connect!");
            try
            {
                client.SendLoginRequest();
                client.Connect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            client.Connected += (sender, e) => 
            {
                Console.WriteLine("CLIENT CONNECTED");
            };
            client.MessageReceived += (sender, e) => // Channel message has been received
            {
                String message=e.MessageText;
                foreach(Match match in Regex.Matches(message,regex))
                {
                    String imageId = match.Groups[1].Value;
                    //Adds the id of the emoji to the discord url for emoji images
                    String url = "https://cdn.discordapp.com/emojis/"+imageId+".png";
                    e.Channel.SendMessage(url);
                }
            };
            Console.ReadKey();
        }
    }
}
