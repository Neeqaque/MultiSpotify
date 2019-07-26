using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MultiSpotify.Source.REST;
using RestSharp;

namespace MultiSpotify
{
    public static class SpotifyApiInteraction
    {
        private static RestClient _apiClient = new RestClient("https://api.spotify.com");
        private static RestClient _accountsClient = new RestClient("https://accounts.spotify.com");

        private const string client_id = "df8b15ff97c042058457f764cafdfc9b";
        private const string client_secret = "535eb0d5e0ad42cfb20369c004b149b2";
        private const string redirect_uri = "http://localhost:1448";
        private const string scope = "user-top-read user-read-playback-state user-library-read " +
                                     "user-read-currently-playing user-modify-playback-state user-follow-read " +
                                     "playlist-read-private user-read-recently-played ";

        private static AccessTokenAuth accessToken;

        public static async void Authorize()
        {
            Socket serverSock = new Socket(SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1448);
            serverSock.Bind(ipep);
            serverSock.Listen(20);

            byte[] buffer = new byte[10240];
            int length = 0;

            Task receiveTask = Task.Run(() =>
            {
                Socket client = serverSock.Accept();
                length = client.Receive(buffer);
                client.Close();
            });

            Browser bro = new Browser("http://accounts.spotify.com/authorize?client_id=df8b15ff97c042058457f764cafdfc9b&response_type=code&redirect_uri=http://localhost:1448&scope=user-top-read user-read-recently-played user-read-playback-state user-read-currently-playing user-modify-playback-state user-library-modify user-library-read streaming app-remote-control user-read-private user-read-email user-follow-modify user-follow-read playlist-modify-public playlist-read-collaborative playlist-read-private playlist-modify-private");
            bro.Show();

            await receiveTask;
            bro.Close();
            serverSock.Close();

            string code = Encoding.UTF8.GetString(buffer, 0, length);
            code = code.Substring(code.IndexOf("/?code=") + "/?code=".Length);
            code = code.Substring(0, code.IndexOf(" HTTP"));

            RestRequest request = new RestRequest("/api/token", Method.POST);
            request.AddParameter("client_id", client_id);
            request.AddParameter("client_secret", client_secret);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", code);
            request.AddParameter("redirect_uri", "http://localhost:1448");

            IRestResponse<AccessTokenAuth> resp = _accountsClient.Execute<AccessTokenAuth>(request);

            accessToken = resp.Data;
        }

        public static void RefreshToken()
        {
            RestRequest request = new RestRequest("/api/token", Method.POST);
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("refresh_token", accessToken.refresh_token);
            request.AddParameter("client_id", client_id);
            request.AddParameter("client_secret", client_secret);

            IRestResponse<AccessTokenAuth> resp = _accountsClient.Execute<AccessTokenAuth>(request);

            accessToken = resp.Data;
        }

        public static CurrentUserInfo GetCurrentUserProfile()
        {
            RestRequest request = new RestRequest("v1/me", Method.GET);
            request.AddHeader("Authorization", accessToken.token_type + " " + accessToken.access_token);
            IRestResponse<CurrentUserInfo> resp = _apiClient.Execute<CurrentUserInfo>(request);
            return resp.Data;
        }
    }
}
