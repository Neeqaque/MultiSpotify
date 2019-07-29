using System;

namespace MultiSpotify.Source.REST
{
    [Serializable]
    class AccessTokenAuth
    {
        private string _token;
        public string access_token
        {
            get { return _token; }
            set
            {
                receiveTime = DateTime.Now;
                _token = value;
            }
        }
        public string token_type { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }

        public DateTime receiveTime;
    }
}
