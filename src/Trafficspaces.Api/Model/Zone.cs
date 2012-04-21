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
    /// An implementation of the Zone resource as defined in http://support.trafficspaces.com/kb/api/api-zones.
	/// </summary>
    public class Zone : Resource {
	    public string handle { get; set; }

	    public string name { get; set; }

	    public int width { get; set; }

	    public int height { get; set; }

	    public string formats { get; set; }

	    public string language { get; set; }

	    public string channel { get; set; }

	    public string position { get; set; }

	    public string scope { get; set; }

	    public string info_url { get; set; }

	    public string preview_url { get; set; }

	    public string thumbnail_url { get; set; }

	    public string default_ad_tag { get; set; }

	    public string description { get; set; }

	    public LinkedResource linked_user { get; set; }

	    public Pricing pricing { get; set; }

	    public Statistic zone_statistic { get; set; }

	    //******************************
	    //*** OUTPUT ONLY VARIABLES ****
	    //******************************
	    public string realm { get; set; }

	    public DateTime creation_date { get; set; }

	    public DateTime last_modified_date { get; set; }

	    public Zone() {}
	
	    public static Zone createZone(string name, int width, int height, string formats, Pricing pricing) {
		    Zone zone = new Zone();
		    zone.name = name;
		    zone.width = width;
		    zone.height = height;
		    zone.formats = formats;
		    zone.pricing = pricing;
		    return zone;
	    }
	
	    public class Pricing : Resource {
		    //******************************
		    //** INPUT & OUTPUT VARIABLES **
		    //******************************
		    
            public string model { get; set; }

		    public double price { get; set; }

		    public double price_increment { get; set; }

		    public int volume_minimum { get; set; }

		    public int volume_maximum { get; set; }

		    public int volume_increment { get; set; }

		    public int order_concurrency { get; set; }
            
            public Dictionary<string, List<Discount>> discounts { get; private set; }
            
            //******************************
		    //** OUTPUT ONLY	VARIABLES **
		    //******************************
		    public bool accept_bids { get; set; }

            public Pricing() { 
                discounts = new Dictionary<string, List<Discount>>() { { "discount", new List<Discount>() }}; 
            }
		
		    public Pricing(string model, double price) : this() {
			    this.model = model;
			    this.price = price;
		    }

            public List<Discount> GetDiscounts() { return discounts["discount"]; }

            public class Discount {

                //******************************
                //** INPUT & OUTPUT VARIABLES **
                //******************************

                public double discount_rate { get; set; }

                public int volume_minimum { get; set; }

                public Discount() { }

                public Discount(double discount_rate, int volume_minimum) {
                    this.discount_rate = discount_rate;
                    this.volume_minimum = volume_minimum;
                }
            }

	    }
		
	    public class Statistic : Resource {
		    //******************************
		    //**** OUTPUT ONLY VARIABLES ***
		    //******************************
            public double hits { get; set; }

            public double uniques { get; set; }

            public double clicks { get; set; }

            public double conversions { get; set; }

            public double views { get; set; }

            public double duration { get; set; }

            public double timespent { get; set; }

            public double very_short_stay_uniques { get; set; }

            public double short_stay_uniques { get; set; }

            public double medium_stay_uniques { get; set; }

            public double long_stay_uniques { get; set; }

            public double very_long_stay_uniques { get; set; }

		    public DateTime date;

            public LinkedResource linked_zone { get; set; }
	
		    public Statistic() {}
	    }
    }
}