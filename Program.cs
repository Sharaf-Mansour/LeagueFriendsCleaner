using Newtonsoft.Json;
using PoniLCU;
using System;
using static PoniLCU.LeagueClient;

namespace LOLFriendsCleaner
{
    public class Program
    {
        static LeagueClient leagueClient = new LeagueClient(credentials.cmd);
        static async Task Main(string[] args)
        {
            var data = await leagueClient.Request(requestMethod.GET, "/lol-chat/v1/friends");
            var friends = JsonConvert.DeserializeObject<List<Friend>>(data);
            Console.WriteLine("Are you sure you want to delete all of your friends?");
            Console.WriteLine($"You have {friends.Count} friends in game!");
            Console.WriteLine("Type 1 for yes, 2 for no");
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 1)
            {
                foreach (var friend in friends)
                {
                    await leagueClient.Request(requestMethod.DELETE, $"/lol-chat/v1/friends/{friend.puuid}");
                    Console.WriteLine($"Removed {friend.gameName}");
                }
                Console.WriteLine("Removed All friends!");
                Console.WriteLine("Please restart your game to make sure that It's not bugged!");
            }
            else
            {
                Console.WriteLine("Exiting...");
                Thread.Sleep(1000);
            }
        }
    }
}