﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopOnContainers.Bot.API.Extensions
{
    public static class HttpClientExtensions
    {
        public static void SetBasicAuthentication(this HttpClient client, string userName, string password) =>
            client.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(userName, password);

        public static void SetToken(this HttpClient client, string scheme, string token) =>
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, token);

        public static void SetBearerToken(this HttpClient client, string token) =>
            client.SetToken(JwtConstants.TokenType, token);

        public static async Task<HttpResponseMessage> PostFileAsync(this HttpClient httpClient, string uri, byte[] fileRaw, string apiParamName, string fileName = null, string requestId = null)
        {
            var method = HttpMethod.Post;

            var requestMessage = new HttpRequestMessage(method, uri);

            requestMessage.Content = new MultipartFormDataContent
                    {
                        { new ByteArrayContent(fileRaw), $"\"{apiParamName}\"", $"\"{fileName ?? apiParamName}\"" }
                    };

            if (requestId != null)
            {
                requestMessage.Headers.Add("x-requestid", requestId);
            }

            var response = await httpClient.SendAsync(requestMessage);

            // raise exception if HttpResponseCode 500
            // needed for circuit breaker to track fails

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return response;
        }

    }

    public class BasicAuthenticationHeaderValue : AuthenticationHeaderValue
    {
        public BasicAuthenticationHeaderValue(string userName, string password)
            : base("Basic", EncodeCredential(userName, password))
        { }

        private static string EncodeCredential(string userName, string password)
        {
            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            string credential = String.Format("{0}:{1}", userName, password);

            return Convert.ToBase64String(encoding.GetBytes(credential));
        }
    }

    public static class HttpRequestExtensions
    {
        public static string AbsoluteHost(this HttpRequest httpRequest)
        {
            return $"{httpRequest.Scheme}://{httpRequest.Host.ToUriComponent()}";
        }
    }

}
