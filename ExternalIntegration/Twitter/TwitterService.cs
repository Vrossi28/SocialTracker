using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Configuration;
using System.Collections.Specialized;

namespace ExternalIntegration.Twitter
{
    public class TwitterService
    {
        private static readonly string BEARER = "AAAAAAAAAAAAAAAAAAAAALDHdQEAAAAAe6daC47bJsdM4MwYgLDIm8lomNo%3DKgmYYm6C4ZQLACC966jvvHRCwwY92W8700p3aKtDX1qZKYjJLf";
        private static readonly string CONSUMER_KEY = "qmQNsadxMHI061vCIfZPAET2E";
        private static readonly string CONSUMER_KEY_SECRET = "yVURzxZLbAn1uRVz4wytTXojfNrd9IKkvMNd5JOinROqKWI5Vi";
        private static readonly string ACCESS_TOKEN = "3027896394-IE3dQiwgtFxhcNQIStPQswBCVEEY4MayFXiK37O";
        private static readonly string ACCESS_TOKEN_SECRET = "U3kh3aM7yEryec8t27l8v48TbXATv7q3L5bZdGoMH804L";
        private static readonly string CLIENT_ID = "ekkwaEFXdjBJLWFUSjB4R2JCZEw6MTpjaQ";
        private static readonly string CLIENT_ID_SECRET = "TgHXBuLqSoIEMgCSgIVVseuwsjY-4fL2fLxzTd_PNolRxa0Jmw";

        public static HttpClient BaseApi()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.twitter.com/2/");
            return httpClient;
        }
        public HttpClient BearerAuthentication()
        {
            HttpClient httpClient = BaseApi();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BEARER);
            return httpClient;
        }
    }
}
