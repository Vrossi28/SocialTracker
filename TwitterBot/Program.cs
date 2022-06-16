using ExternalIntegration.Twitter.Interfaces.User;
using ExternalIntegration.Twitter.Models;
using ExternalIntegration.Twitter.Models.User;
using ExternalIntegration.Twitter.Requests;
using OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Follow Tracker");
            Console.Write("User: ");

            string username = Console.ReadLine();

            //FollowTracker.FollowersDontFollowBack(username);
            var response = await Users.FollowingDontFollowBack(username);

            var users = response.Data;

            Console.WriteLine($"Following don't follow you back: {users.Count()}");

            foreach (var user in users)
            {
                Console.WriteLine(user.username);
            }
        }
    }
}
