using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;
using SpotifyDemo.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpotifyDemo.MVVM.View
{
    internal class MainViewModel
    {
        public ObservableCollection<Item> Songs { get; set; }
        public MainViewModel()
        {
            Songs = new ObservableCollection<Item>();
            PopulateCollection();
        }

        void PopulateCollection()
        {

            /*latest version of restsharp does not support authenticator like this*/
            /*authoriztion failed means token expired*/
            string token = "BQCrdW2NWZnIQVIVoyIiGFe5cO4j2SsmN2dqNgWY-BGLv18UOoNqwlkoKYJ21RGm6K_t6mZi6kxTEtxUt3fDcwaxxLeWEQYUjZAJNTk5frtmh1aP_QiD";
            var client = new RestClient();
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer");

            var request = new RestRequest("https://api.spotify.com/v1/browse/new-releases", Method.Get);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            var response = client.GetAsync(request).GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<TrackModel>(response.Content);

            for (int i = 0; i < data.Albums.Limit; i++)
            {
                var track = data.Albums.Items[i];
                track.Duration = "3:24";
                Songs.Add(track);
            }

        }
    }
}
