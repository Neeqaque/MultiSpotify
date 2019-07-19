using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace MultiSpotify
{
    public static class SpotifyApiInteraction
    {
        private static RestClient client = new RestClient("http://api.spotify.com");

        public static void Authorize(string username, string password)
        {
            RestRequest request = new RestRequest("/login", Method.POST);


        }
    }
}
