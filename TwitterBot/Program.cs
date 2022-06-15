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
            Console.WriteLine("Follower Finder");
            Console.WriteLine("User: ");
            string username = Console.ReadLine();
            var user = await Users.GetBaseDataByUsername(username);
            //var userId = user.Result.Data.data.id;

            if(!user.Message.Equals("OK"))
            {
                Console.WriteLine($"User: '{username}' was not found");
                return;
            }

            var followersResponse = await Users.GetFollowersByUsername(username);
            var followingResponse = await Users.GetFollowingByUsername(username);

            if (followingResponse.Data != null && followersResponse.Data != null)
            {
                List<UserBaseData> followersDontFollowed = followersResponse.Data.data.Where(i => followingResponse.Data.data.Select(x => x.id).Contains(i.id) == false).ToList();

                List<UserBaseData> followingDontFollowBack = followingResponse.Data.data.Where(i => followersResponse.Data.data.Select(x => x.id).Contains(i.id) == false).ToList();

                Console.WriteLine($"Followers dont followed: {followersDontFollowed.Count()}");
                Console.WriteLine($"Following dont follow you back: {followingDontFollowBack.Count()}");

            }
                
            Console.WriteLine($"{followersResponse.Message}");
            return;

        }
    }
}
