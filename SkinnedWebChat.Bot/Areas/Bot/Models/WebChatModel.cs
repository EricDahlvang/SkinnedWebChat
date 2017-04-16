using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace SkinnedWebChat.Bot.Areas.Bot.Models
{
    public class WebChatModel
    {
        public string Token { get; set; }
        
        public async Task SetToken()
        {            
            string url = ConfigurationManager.AppSettings["BotTokenPath"];
            string botChatSecret = ConfigurationManager.AppSettings["BotChatSecret"];
            
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", "BOTCONNECTOR " + botChatSecret);

            using (HttpResponseMessage response = await new HttpClient().SendAsync(request))
            {
                string token = await response.Content.ReadAsStringAsync();
                Token = token.Replace("\"", "");
            }  
        }
    }
}