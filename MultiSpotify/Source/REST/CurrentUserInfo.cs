using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSpotify.Source.REST
{
    public class CurrentUserInfo
    {
        public struct ExplicitContentInfo
        {
            public bool filter_enabled { get; set; }
            public bool filter_locked { get; set; }
        }

        public struct FollowersInfo
        {
            public string href { get; set; }
            public int total { get; set; }
        }

        public struct ExternalUrlsInfo
        {
            public string spotify { get; set; }
        }

        public struct ImageInfo
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public string country { get; set; }
        public string display_name { get; set; }
        public string email { get; set; }
        public ExplicitContentInfo explicit_content { get; set; }
        public FollowersInfo followers { get; set; }
        public ExternalUrlsInfo external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string product { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public List<ImageInfo> images { get; set; }

    }
}
