// -----------------------------------------------------------------------
// <copyright file="SummonXml.cs" company="Chalmers">
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

namespace SSADL.Summon
{
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SummonXml : Summon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SummonXml">SummonXml</see> class.
        /// </summary>
        /// <param name="apiId">Id-string issued by Serial Solution</param>
        /// <param name="apiKey">Key-string issued by Serial Solution</param>        
        public SummonXml(string apiId, string apiKey) : base()
        {
            this.ApiKey = apiKey;
            this.ApiId = apiId;
            this.SummonResultType = ResultType.Xml;
        }

        /// <summary>
        /// Makes a query in Summon through a rpc call
        /// </summary>
        /// <param name="query">Query to pass to Summon</param>
        /// <returns>Query result in XML notaion</returns>
        public string Query(Dictionary<string, string> query) 
        {
            return base.Query(query, ResultType.Xml);
        }

        /// <summary>
        /// Makes a query in Summon with a defined summon session through a rpc call
        /// </summary>
        /// <param name="query">Query to pass to Summon</param>
        /// <param name="session">Summon session id</param>
        /// <returns>Query result in XML notaion</returns>
        public string Query(Dictionary<string, string> query, string session)
        {
            return base.Query(query, session, ResultType.Xml);
        }

        /// <summary>
        /// Makes a query in Summon with a defined summon session through a rpc call
        /// </summary>
        /// <param name="query">Query to pass to Summon</param>
        /// <returns>Query result in XML notaion</returns>
        public string Query(string query)
        {
            return base.Query(query, ResultType.Xml);            
        }

        /// <summary>
        /// Makes a query in Summon with a defined summon session through a rpc call
        /// </summary>
        /// <param name="query">Query to pass to Summon</param>
        /// <param name="session">Summon session id</param>
        /// <returns>Query result in XML notaion</returns>
        public string Query(string query, string session)
        {
            return base.Query(query, session, ResultType.Xml);
        }
    }
}
