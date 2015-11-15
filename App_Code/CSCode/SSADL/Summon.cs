// -----------------------------------------------------------------------
// <copyright file="Summon.cs" company="Chalmers">
// 
// Copyright (c) 2012 Chalmers University of Technology
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in 
// the Software without restriction, including without limitation the rights to 
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies 
// of the Software, and to permit persons to whom the Software is furnished to do 
// so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all 
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using log4net;
using SSADL.CMS;
namespace SSADL.Summon
{
    /// <summary>
    /// Base class for Summon-communication
    /// </summary>
    public class Summon
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Summon">Summon</see> class. 
        /// </summary>
        public Summon()
        {
            SummonHost = "api.summon.serialssolutions.com";
            SummonPath = "/2.0.0/search";
            SessionId = string.Empty;
            SummonString = string.Empty;
        }

        #endregion

        #region Enum for Class

        /// <summary>
        /// Summon can deliver results in both JSON and XML format
        /// </summary>
        public enum ResultType
        {
            /// <summary>
            /// JSON format
            /// </summary>
            Json,

            /// <summary>
            /// XML Format
            /// </summary>
            Xml,

            /// <summary>
            /// Plain .net objects
            /// </summary>
            Poco
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets a value indicating whether debug information should be printed or not
        /// </summary>
        public bool Debug { private get; set; }

        #endregion

        #region Private properties

        /// <summary>        
        /// Gets or sets the secret Key used for authentication
        /// </summary>
        protected string ApiKey { private get; set; }

        /// <summary>
        /// Gets or sets the Client ID used for authentication
        /// </summary>
        protected string ApiId { private get; set; }

        /// <summary>
        /// Gets or sets the result type from Summon. JSON or XML.
        /// </summary>
        protected ResultType SummonResultType { get; set; }

        /// <summary>
        /// Gets or sets the session for the current transaction
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the hostname of the Summon api
        /// </summary>
        private string SummonHost { get; set; }

        /// <summary>
        /// Gets or sets the path of summon api uri
        /// </summary>
        private string SummonPath { get; set; }

        /// <summary>
        /// Gets or sets the Query to send to Summon
        /// </summary>
        private Dictionary<string, string> SummonQuery { get; set; }

        /// <summary>
        /// Unsorted query string
        /// </summary>
        private string SummonString { get; set; }

        #endregion

        // Connect to the log4net LogManager
        private static readonly ILog Log = LogManager.GetLogger(typeof(Summon));
        private static readonly bool IsDebugEnabled = Log.IsDebugEnabled;

        /// <summary>
        /// Make a query against Summon 
        /// </summary>
        /// <param name="searchExpression">Search string</param>
        /// <param name="format">Requested output format</param>
        /// <returns>Search result in requested output format</returns>
        protected string Query(Dictionary<string, string> searchExpression, ResultType format)
        {
            SummonQuery = searchExpression;
            SummonResultType = format;

            return HttpGet();
        }

        /// <summary>
        /// Make a query against Summon 
        /// </summary>
        /// <param name="searchExpression">Search string</param>
        /// <param name="format">Requested output format</param>
        /// <returns>Search result in requested output format</returns>
        protected string Query(string searchExpression, ResultType format)
        {
            SummonString = searchExpression;
            SummonResultType = format;

            return HttpGet();
        }


        /// <summary>
        /// Make a query against Summon using a summon session id
        /// </summary>
        /// <param name="searchExpression">Search string</param>
        /// <param name="session">Search session</param>
        /// <param name="format">Requested output format</param>
        /// <returns>Search result in requested output format</returns>
        protected string Query(Dictionary<string, string> searchExpression, string session, ResultType format)
        {
            this.SummonQuery = searchExpression;
            this.SummonResultType = format;
            this.SessionId = session;

            return HttpGet();
        }

        /// <summary>
        /// Make a query against Summon using a summon session id
        /// </summary>
        /// <param name="searchExpression">Search string</param>
        /// <param name="session">Search session</param>
        /// <param name="format">Search result in requested output format</param>
        /// <returns>Search result in requested output format</returns>
        protected string Query(string searchExpression, string session, ResultType format)
        {
            SummonString = searchExpression;
            this.SummonResultType = format;
            this.SessionId = session;

            return HttpGet();
        }


        /// <summary>
        /// Makes the rpc call
        /// </summary>
        /// <returns>Search result in given format</returns>
        private string HttpGet()
        {
            var webClient = new WebClient();

            if (!(String.IsNullOrEmpty(this.SessionId)))
            {
                if (this.SessionId.Length > 0)
                {
                    webClient.Headers.Add("x-summon-session-id", this.SessionId);
                    if (Debug)
                    {
                        Console.WriteLine("x-summon-session-id: {0}", this.SessionId);
                    }
                }
            }

            webClient.Headers.Add("x-summon-date", GetDateTime());
            webClient.Headers.Add("Accept", GetResultType(SummonResultType));
            webClient.Headers.Add("Host", SummonHost);
            webClient.Headers.Add("Authorization", AuthorizationHeader());

            if (Debug)
            {
                Console.WriteLine("Request headers:");
                Console.WriteLine("{0}", webClient.Headers.ToString());
            }

            Stream stream;

            try
            {
                if (Debug)
                {
                    Console.WriteLine("GET {0}", ConstructUrl());
                }
                // HttpContext.Current.Response.Write(ConstructUrl());
               // if (commonfunctions.Environment == "PROD")
              //  {
                    WebProxy wp = new WebProxy("http://access.lb.ssa.gov:80/", true);
                    webClient.Proxy = wp;
              //  }

                stream = webClient.OpenRead(ConstructUrl());
            }
            catch (Exception e)
            {
                if (Debug)
                {
                    Console.WriteLine("Error from remote server: {0}", e.ToString());
                    Console.ReadLine();
                }
                throw;
            }

            if (stream != null)
            {
                var reader = new StreamReader(stream);
                string request = reader.ReadToEnd();
                return request;
            }

            // Cast exception if StreamReader allocation failed
            throw new Exception("Stream reader failed!");
        }

        /// <summary>
        /// Gets the RFC1123 formated datetime.now()
        /// </summary>
        /// <returns>DateTime formated as RFC1123</returns>
        private string GetDateTime()
        {
            // Ugly hack. Should be done with Culture-classes
            DateTime now = DateTime.Now;
            TimeZone localTimeZone = TimeZone.CurrentTimeZone;

            string dateTime = now.ToString("r").Replace("GMT", "EST");

            if (Debug)
            {
                Console.WriteLine("Timestamp Header:");
                Console.WriteLine(dateTime);
            }

            return dateTime;
        }

        /// <summary>
        /// Gets the accept header value
        /// </summary>
        /// <param name="resultFormat">The format of the result</param>
        /// <returns>String with for the accept field in the http header</returns>
        private string GetResultType(ResultType resultFormat)
        {
            switch (resultFormat)
            {
                case ResultType.Xml:
                    return "application/xml";
                case ResultType.Json:
                    return "application/json";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Creates a authentication string required for the summon api.
        /// </summary>
        /// <returns>Authentication string</returns>
        private string CreateAuthenticationString()
        {
            var result = new StringBuilder();

            result.Append(GetResultType(SummonResultType))
                .Append("\n")
                .Append(GetDateTime())
                .Append("\n")
                .Append(SummonHost)
                .Append("\n")
                .Append(SummonPath)
                .Append("\n")
                .Append(HttpUtility.UrlDecode(ComputeSortedQueryString()))
                .Append("\n");

            if (Debug)
            {
                Console.WriteLine("Authentication string: {0}", result.ToString());
            }

            return result.ToString();
        }

        /// <summary>
        /// Computes a query string from the dictionary SummonQuery. Ugly hack to make it work with question 
        /// stored in this.SummonString
        /// </summary>
        /// <returns>A sorted query string</returns>
        private string ComputeSortedQueryString()
        {
            if (SummonString.Length == 0)
            {
                // Temporary list for storing queries
                var parameters = new List<string>();

                foreach (var parameter in SummonQuery)
                {
                    parameters.Add(parameter.Key + "=" + parameter.Value);
                }
                parameters.Sort();

                var result = new StringBuilder();
                foreach (string parameter in parameters)
                {
                    result.Append(parameter).Append("&");
                }

                if (result.Length > 0)
                {
                    result.Length = result.Length - 1;
                }

                return result.ToString();
            }
            return SummonizeString(SummonString);
        }

        /// <summary>
        /// Sorts a query string according to Summon's taste
        /// </summary>
        /// <param name="queryString">Sorted query string</param>
        private string SummonizeString(string queryString)
        {
            if (!queryString.Contains("&") || queryString.Length < 1)
            {
                throw new ArgumentException("Invalid query string!");
            }

            // string[] strArray = queryString.Split('&');
            List<string> workList = new List<string>();

            foreach (string part in queryString.Split('&'))
            {
                workList.Add(part);
            }

            workList.Sort();

            string result = string.Empty;

            foreach (string part in workList)
            {
                result = result + part + '&';
            }

            // Need to remove the last trailing &-character
            // Log.Info("SummonizeString: " + result.Substring(0, result.Length - 1));
            return result.Substring(0, result.Length - 1);
        }

        /// <summary>
        /// Returns a digest for the string
        /// </summary>
        /// <param name="key">key used for signing hmac.sha1</param>
        /// <param name="id">id for the signing</param>
        /// <returns>A UTF-8 Base64 encoded HMACSHA1 digest of the string</returns>
        private string BuildDigest(string key, string id)
        {
            string result;

            // This should give the seed value for HMACSHA1
            using (var hmac = new HMACSHA1(Encoding.ASCII.GetBytes(key)))
            {
                // Convert the id string an array of bytes
                result = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(id)));
            }

            // Need to convert to UTF-8
            return Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(result));
        }

        /// <summary>
        /// Builds the authorization header
        /// </summary>
        /// <returns>Authorization header</returns>
        private string AuthorizationHeader()
        {
            string authentication = CreateAuthenticationString();

            return "Summon " + this.ApiId + ";" + this.BuildDigest(this.ApiKey, authentication);
        }

        /// <summary>
        /// Constructs the Summon url used for web rpc calls
        /// </summary>
        /// <returns>The correct url for the web rpc call</returns>
        private string ConstructUrl()
        {
            var urlstr = new StringBuilder();

            urlstr.Append("http://")
                .Append(SummonHost)
                .Append(SummonPath)
                .Append("?")
                .Append(ComputeSortedQueryString());

            // Log.Info("urlstr: " + urlstr.ToString());
            return urlstr.ToString();
        }
    }
}