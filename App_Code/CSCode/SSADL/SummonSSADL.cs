// -----------------------------------------------------------------------
// <copyright file="SummonUmbraco.cs" company="Chalmers">
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
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Xml;
using log4net;

namespace SSADL.Summon
{
    /// <summary>
    /// The base class for making queries to Summon trough Umbraco.
    /// </summary>
    public class Query
    {
        // Read the Summon Api ID and Api Key from web.config in section appSettings:
        // <add key="SummonSSADLApiId" value="MyNiceId" />
        // <add key="SummonSSADLApiKey" value="abc123" />
        static readonly string ApiId = System.Web.Configuration.WebConfigurationManager.AppSettings["SummonSSADLApiId"];
        static readonly string ApiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["SummonSSADLApiKey"];

        // Default queryString if left empty
        public static readonly string DefaultQueryString = "s.q=&s.ps=15&s.ho=true&s.hl=false";

        // Connect to the log4net LogManager
        private static readonly ILog Log = LogManager.GetLogger(typeof(Query));
        private static readonly bool IsDebugEnabled = Log.IsDebugEnabled;

        /// <summary>
        /// Builds a default queryString if left empty or runs UrlDecode on incoming string
        /// </summary>
        /// <param name="queryString">Search string in Summon URL format</param>
        /// <returns>Default or UrlDecoded string</returns>
        public static string ParseQueryString(string queryString)
        {
            Log.Info("Before UrlDecode: " + queryString);
            Log.Info("After UrlDecode: " + HttpUtility.UrlDecode(queryString));
            return String.IsNullOrEmpty(queryString) ? DefaultQueryString : HttpUtility.UrlDecode(queryString);
        }

        /// <summary>
        /// Makes a query to SummonJson and returns dynamic objects to use in the Razor macro engine.
        /// </summary>
        /// <param name="queryString">Search string in Summon URL format</param>
        /// <param name="sessionId">Session Id from Summon API</param>
        /// <returns>Dynamic object with result documents for use in Razor</returns>
        public static dynamic GetItems(string queryString, string sessionId)
        {
            // queryString = ParseQueryString(queryString);
            var summon = new SummonJson(ApiId, ApiKey);
            var result = String.IsNullOrEmpty(sessionId) ? summon.Query(queryString) : summon.Query(queryString, sessionId);
            var sb = new StringBuilder("[" + result + "]");
            var dynamicObject = Json.Decode(sb.ToString());
            Log.Info("queryString " + queryString);
            if (dynamicObject[0].recordCount != null)
            {
                Log.Info("recordCount " + dynamicObject[0].recordCount + " totalRequestTime " + dynamicObject[0].totalRequestTime);
            }
            else
            {
                Log.Info("No results in JSON response.");
            }
            return dynamicObject;
        }

        /// <summary>
        /// Makes a query to SummonJson and returns a string containing the raw JSON data.
        /// </summary>
        /// <param name="queryString">Search string in Summon URL format</param>
        /// <param name="sessionId">Session Id from Summon API</param>
        /// <returns>JSON result</returns>
        public static string GetItemsAsJsonString(string queryString, string sessionId)
        {
            queryString = ParseQueryString(queryString);
            var summon = new SummonJson(ApiId, ApiKey);
            var result = String.IsNullOrEmpty(sessionId) ? summon.Query(queryString) : summon.Query(queryString, sessionId);
            return result;
        }

        /// <summary>
        /// Makes a query to SummonXml and returns an XML node for use in XSLT parsing.
        /// </summary>
        /// <param name="queryString">Search string in Summon URL format</param>
        /// <param name="sessionId">Session Id from Summon API</param>
        /// <returns>XML node with results</returns>
        public static XmlNode GetItemsAsXml(string queryString, string sessionId)
        {
            queryString = ParseQueryString(queryString);
            var summon = new SummonXml(ApiId, ApiKey);
            var result = String.IsNullOrEmpty(sessionId) ? summon.Query(queryString) : summon.Query(queryString, sessionId);
            var doc = new XmlDocument();
            doc.LoadXml(result);
            XmlNode newNode = doc.DocumentElement;
            return newNode;
        }
    }
}