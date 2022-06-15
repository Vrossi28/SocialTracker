using ExternalIntegration.Twitter.Models;
using ExternalIntegration.Twitter.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterBot
{
    public static class FollowTracker
    {
        public static async Task FollowingDontFollowBack(string username)
        {
            var followersResponse = await Users.GetFollowersByUsername(username);
            if(followersResponse.Data.data == null)
            {
                Console.WriteLine($"Error {followersResponse.Status} | {followersResponse.Message}");
            }
            var followingResponse = await Users.GetFollowingByUsername(username);

            if (followingResponse.Data != null && followersResponse.Data != null)
            {
                List<UserBaseData> followingDontFollowBack = followingResponse.Data.data.Where(i => followersResponse.Data.data.Select(x => x.id).Contains(i.id) == false).ToList();

                Console.WriteLine($"Following don't follow you back: {followingDontFollowBack.Count()}");
                foreach (var following in followingDontFollowBack)
                {
                    Console.WriteLine(following.username);
                }

            }

            Console.WriteLine($"{followersResponse.Message}");
            return;
        }

        public static async Task FollowersDontFollowBack(string username)
        {
            var followersResponse = await Users.GetFollowersByUsername(username);
            var followingResponse = await Users.GetFollowingByUsername(username);

            if (followingResponse.Data != null && followersResponse.Data != null)
            {
                List<UserBaseData> followersDontFollowed = followersResponse.Data.data.Where(i => followingResponse.Data.data.Select(x => x.id).Contains(i.id) == false).ToList();

                Console.WriteLine($"Followers you don't follow back: {followersDontFollowed.Count()}");
                foreach (var following in followersDontFollowed)
                {
                    Console.WriteLine(following.username);
                }

            }

            Console.WriteLine($"{followersResponse.Message}");
            return;
        }
    }
}
