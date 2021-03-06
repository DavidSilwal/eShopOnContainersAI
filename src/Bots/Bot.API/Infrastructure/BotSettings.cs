﻿using System.Configuration;

namespace Microsoft.Bots.Bot.API.Infrastructure
{
    public class BotSettings
    {
        public string PurchasingUrl { get; set; }
        public string IdentityUrl { get; set; }
        public string ArtificialIntelligenceUrl { get; set; }
        public string MvcUrl { get; set; }
        public string ProductSearchImageUrl { get; set; }

        public string ApiClient { get; set; }
        public string ApiClientSecret { get; set; }
        public string TokenEndPoint { get; set; }
        public string UserInfoEndpoint { get; set; }

        public string BotId { get; set; }
        public string MicrosoftAppId { get; set; }
        public string MicrosoftAppPassword { get; set; }
        public string WebChatSecret { get; set; }

        public BotSettings() {
            BotId = ConfigurationManager.AppSettings["BotId"];
            MicrosoftAppId = ConfigurationManager.AppSettings["MicrosoftAppId"];
            MicrosoftAppPassword = ConfigurationManager.AppSettings["MicrosoftAppPassword"];
            WebChatSecret = ConfigurationManager.AppSettings["WebChatSecret"];


            PurchasingUrl = ConfigurationManager.AppSettings["PurchasingUrl"];
            IdentityUrl = ConfigurationManager.AppSettings["IdentityUrl"];
            ArtificialIntelligenceUrl = ConfigurationManager.AppSettings["ArtificialIntelligenceUrl"];
            ProductSearchImageUrl = ConfigurationManager.AppSettings["ProductSearchImageUrl"];
            MvcUrl = ConfigurationManager.AppSettings["MvcUrl"];

            TokenEndPoint = ConfigurationManager.AppSettings["TokenEndPoint"];
            UserInfoEndpoint = ConfigurationManager.AppSettings["UserInfoEndpoint"];

            ApiClient = ConfigurationManager.AppSettings["ApiClient"];
            ApiClientSecret = ConfigurationManager.AppSettings["ApiClientSecret"];
        }
    }
}
