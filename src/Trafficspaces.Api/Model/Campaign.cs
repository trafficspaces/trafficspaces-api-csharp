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
using System.Collections.Generic;

namespace Trafficspaces.Api.Model {
	/// <summary>
	/// An implementation of the Campaign resource as defined in http://support.trafficspaces.com/kb/api/api-campaigns.
	/// </summary>
    public class Campaign : Resource {
	    //******************************
	    //** INPUT & OUTPUT VARIABLES **
	    //******************************
        public Dictionary<string, List<LinkedResource>> linked_ads { get; private set; }
        public string name { get; set; }

        

        public LinkedResource linked_user { get; set; }
	
	    //******************************
	    //*** OUTPUT ONLY VARIABLES ****
	    //******************************
        public string realm { get; set; }

        public DateTime creation_date { get; set; }

        public DateTime last_modified_date { get; set; }

	    public Campaign() {
            linked_ads = new Dictionary<string, List<LinkedResource>>() { { "linked_ad", new List<LinkedResource>() } };
        }

	    public static Campaign createCampaign(string name, List<LinkedResource> linked_ads = null) {
		    Campaign campaign = new Campaign();
            campaign.name = name;
            if (linked_ads != null) {
                campaign.linked_ads["linked_ad"] = linked_ads;
            }
		    return campaign;
	    }

        public List<LinkedResource> GetLinkedAds() {
            return linked_ads["linked_ad"];
        }
    }
}