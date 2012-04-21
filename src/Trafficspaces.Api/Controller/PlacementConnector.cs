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

using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;

namespace Trafficspaces.Api.Controller {
    /// <summary>
    /// REST API wrapper.
    /// </summary>
    public class PlacementConnector : Connector {

        /// <summary>
        /// Initializes a new client with the specified credentials.
        /// </summary>
        /// <param name="accountSid">The AccountSid to authenticate with</param>
        /// <param name="authTokens">The AuthToken to authenticate with</param>
        public PlacementConnector(EndPoint endPoint, string resourcePath)
            : base(endPoint, resourcePath) {
        }

        /// <summary>
        /// List placement resources
        /// </summary>
        /// 
        public Placement FetchAds(Placement placement) {
            List<Placement> placements = new List<Placement>() { placement };
            placements = List(placements);
            return placements[0];
        }

        public List<Placement> FetchAds(List<Placement> placements, Flags flags = null, string medium = null, string frame = null, string title = null, bool useIframe = false) {
            return List(placements, flags, medium, frame, title, useIframe);
        }

        public List<Placement> List(List<Placement> placements, Flags flags = null, string medium = null, string frame = null, string title = null, bool useIframe = false) {

            Dictionary<string, string> placementParameters = new Dictionary<string, string>();
            placementParameters.Add("request", getRequestJSONObject(placements, flags, medium, frame, title, useIframe).ToString());
            return base.List<Placement>(placementParameters);
        }

        private JObject getRequestJSONObject(List<Placement> placements, Flags flags = null, string medium = null, string frame = null, string title = null, bool useIframe = false) {
            JObject jsonObject = new JObject();
            
            if (placements != null && placements.Count > 0) {
                JArray placementsJSONArray = new JArray();
                foreach (var placement in placements) {
                    placementsJSONArray.Add(JObject.Parse(base.jsonSerializer.Serialize(placement)));
                }
                jsonObject.Add("placements", placementsJSONArray);
            }

            if (flags != null) {
                jsonObject.Add("flags", JObject.Parse(base.jsonSerializer.Serialize(flags)));
            }

            if (medium != null) {
                jsonObject.Add("medium", new JValue(medium));
            }
            
            if (frame != null) {
                jsonObject.Add("frame", new JValue(frame));
            }
            
            if (title != null) {
                jsonObject.Add("title", new JValue(title));
            }
            
            jsonObject.Add("useiframe", new JValue(useIframe));
            
            return jsonObject;
        }
    }
}