#region License
// Copyright (c) 2012 Trafficspaces Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
// Reference Documentation: http://support.trafficspaces.com/kb/api/api-introduction
#endregion
using System;

namespace Trafficspaces.Api.Controller {
	/// <summary>
	/// Encapsulates all the data required to authenticate and access an API endpoint (gateway)
	/// </summary>
	public class EndPoint {
		/// <summary>
		/// The base URL that is used to access the API
		/// </summary>
		public string BaseURI { get; set; }
		/// <summary>
		/// The username that is used to authenticate API requests
		/// </summary>
		public string Username { get; set; }
		/// <summary>
		/// The password that is used to authenticate API requests
		/// </summary>
		public string Password { get; set; }
		
		public EndPoint(string baseUri, string username, string password) {
		    BaseURI = baseUri;
		    Username = username;
		    Password = password;
		}

        public EndPoint(string baseUri) {
            BaseURI = baseUri;
        }
	}
}