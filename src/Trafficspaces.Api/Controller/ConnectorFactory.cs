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
using Trafficspaces.Api.Model;

using RestSharp;
using RestSharp.Extensions;
using RestSharp.Deserializers;
using Newtonsoft.Json.Linq;

using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Trafficspaces.Api.Controller {
	/// <summary>
	/// REST API wrapper factory.
	/// </summary>
	public class ConnectorFactory {
		/// <summary>
		/// The ad store API end point to use when making requests
		/// </summary>
		private EndPoint AdStoreApiEndPoint { get; set; }
		/// <summary>
		/// The ad server API end point to use when making requests
		/// </summary>
		private EndPoint AdServerApiEndPoint { get; set; }
		
        private Dictionary<string, Connector> connectors { get; set; }


        private ConnectorFactory() {
            connectors = new Dictionary<string, Connector>();
        }

		/// <summary>
		/// Initializes a new Connector factory with the specified credentials.
		/// </summary>
		/// <param name="subDomain">The subdomain of the ad store account for which API requests will be made</param>
		/// <param name="apiKey">The api key with which the API requests will be authenticated</param>
		public ConnectorFactory(string subDomain, string apiKey) : this() {
		    AdStoreApiEndPoint = new EndPoint(string.Format("https://{0}.trafficspaces.com", subDomain), subDomain, apiKey);
		    AdServerApiEndPoint = new EndPoint("http://ads.trafficspaces.net");
		}
		/// <summary>
		/// Initializes a new Connector factory with the specified credentials.
		/// </summary>
		/// <param name="subDomain">The subdomain of the ad store account for whisch API requests will be made</param>
		/// <param name="apiKey">The api key with which the API requests will be authenticated</param>
		public ConnectorFactory(EndPoint adStoreApiEndPoint, EndPoint adServerApiEndPoint) : this() {
		    AdStoreApiEndPoint = adStoreApiEndPoint;
		    AdServerApiEndPoint = adServerApiEndPoint;
		}

        /// <summary>
        /// Initializes a new Connector that is dedicated to User resources.
        /// </summary>
        protected Connector GetConnector(EndPoint endPoint, string resourcePath) {
            if (!connectors.ContainsKey(resourcePath)) {
                connectors[resourcePath] = new Connector(endPoint, resourcePath);
            }
            return connectors[resourcePath];
		}
	
		/// <summary>
		/// Initializes a new Connector that is dedicated to User resources.
		/// </summary>
        public Connector GetUserConnector() {
            return GetConnector(AdStoreApiEndPoint, "/resources/users");
		}

        /// <summary>
        /// Initializes a new Connector that is dedicated to Contact resources.
        /// </summary>
        public Connector GetContactConnector() {
            return GetConnector(AdStoreApiEndPoint, "/resources/contacts");
        }

        /// <summary>
        /// Initializes a new Connector that is dedicated to Zone resources.
        /// </summary>
        public Connector GetZoneConnector() {
            return GetConnector(AdStoreApiEndPoint, "/resources/zones");
        }

        /// <summary>
        /// Initializes a new Connector that is dedicated to Ad resources.
        /// </summary>
        public Connector GetAdConnector() {
            return GetConnector(AdStoreApiEndPoint, "/resources/ads");
        }

        /// <summary>
        /// Initializes a new Connector that is dedicated to Campaign resources.
        /// </summary>
        public Connector GetCampaignConnector() {
            return GetConnector(AdStoreApiEndPoint, "/resources/campaigns");
        }

        /// <summary>
        /// Initializes a new Connector that is dedicated to Targeting-Plan resources.
        /// </summary>
        public Connector GetTargetingPlanConnector() {
            return GetConnector(AdStoreApiEndPoint, "/resources/targetingplans");
        }

        /// <summary>
        /// Initializes a new Connector that is dedicated to Feed resources.
        /// </summary>
        public Connector GetFeedConnector() {
            return GetConnector(AdStoreApiEndPoint, "/resources/feeds");
        }

        /// <summary>
        /// Initializes a new Connector that is dedicated to Order resources.
        /// </summary>
        public Connector GetOrderConnector() {
            return GetConnector(AdStoreApiEndPoint, "/resources/orders");
        }

        /// <summary>
        /// Initializes a new Connector that is dedicated to Coupon resources.
        /// </summary>
        public Connector GetCouponConnector() {
            return GetConnector(AdStoreApiEndPoint, "/resources/coupons");
        }

        /// <summary>
        /// Initializes a new Connector that is dedicated to Placement resources.
        /// </summary>
        public PlacementConnector GetPlacementConnector() {
            string resourcePath = "/resources/placements.json";
            if (!connectors.ContainsKey(resourcePath)) {
                connectors[resourcePath] = new PlacementConnector(AdServerApiEndPoint, resourcePath);
            }
            return (PlacementConnector)connectors[resourcePath];
        }
    }
}