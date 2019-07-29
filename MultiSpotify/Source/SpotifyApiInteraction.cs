using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MultiSpotify.Source.REST;
using RestSharp;

namespace MultiSpotify
{
    public static class SpotifyApiInteraction
    {
        private const string FILEPATH = "data.dat";
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

            Browser bro = new Browser("http://accounts.spotify.com/authorize?client_id=" + client_id +
                                                                                  "&response_type=code" +
                                                                                  "&redirect_uri=" + redirect_uri + 
                                                                                  "&scope=" + scope);
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
            request.AddParameter("redirect_uri", redirect_uri);

            IRestResponse<AccessTokenAuth> resp = _accountsClient.Execute<AccessTokenAuth>(request);

            accessToken = resp.Data;

            SaveToken();
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

            SaveToken();
        }

        private static void SaveToken()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(FILEPATH, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                formatter.Serialize(fs, accessToken);
            }
        }

        public static bool LoadToken()
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream fs = new FileStream(FILEPATH, FileMode.Open))
                {
                    accessToken = formatter.Deserialize(fs) as AccessTokenAuth;

                    if (accessToken.receiveTime.AddSeconds(accessToken.expires_in) < DateTime.Now.AddSeconds(-300))
                    {
                        RefreshToken();
                    }

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static CurrentUserInfo GetCurrentUserProfile()
        {
            RestRequest request = new RestRequest("v1/me", Method.GET);
            request.AddHeader("Authorization", accessToken.token_type + " " + accessToken.access_token);
            IRestResponse<CurrentUserInfo> resp = _apiClient.Execute<CurrentUserInfo>(request);
            return resp.Data;
        }

        public static void GetCurrentUserPlaylists()
        {
            RestRequest request = new RestRequest("v1/me/playlists", Method.GET);
            request.AddHeader("Authorization", accessToken.token_type + " " + accessToken.access_token);
            IRestResponse resp = _apiClient.Execute(request);
        }

        
    }
}
