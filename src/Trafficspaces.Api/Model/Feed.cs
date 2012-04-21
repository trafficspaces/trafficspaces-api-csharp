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

namespace Trafficspaces.Api.Model {
	/// <summary>
	/// An implementation of the Feed resource as defined in http://support.trafficspaces.com/kb/api/api-feeds.
	/// </summary>
    public class Feed : Resource {
	    //******************************
	    //** INPUT & OUTPUT VARIABLES **
	    //******************************
        public string name { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public double weight { get; set; }

        public string channel { get; set; }

        public string provider { get; set; }

        public string ad_tag { get; set; }

        public LinkedResource linked_user { get; set; }
	
	    //******************************
	    //*** OUTPUT ONLY VARIABLES ****
	    //******************************
        public string realm { get; set; }

        public DateTime creation_date { get; set; }

        public DateTime last_modified_date { get; set; }

	    public Feed() {}
	
	    public static Feed createFeed(string name, int width, int height, double weight, string ad_tag) {
		    Feed feed = new Feed();
		    feed.name = name;
		    feed.width = width;
		    feed.height = height;
		    feed.weight = weight;
		    feed.ad_tag = ad_tag;
		    return feed;
	    }
    }
}