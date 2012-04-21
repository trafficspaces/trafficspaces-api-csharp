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
	/// An implementation of the Order resource as defined in http://support.trafficspaces.com/kb/api/api-orders.
	/// </summary>
    public class Order : Resource {
	    //******************************
	    //** INPUT & OUTPUT VARIABLES **
	    //******************************
	    public string priority { get; set; }

	    public double gross_purchase_price { get; set; }

	    public double net_purchase_price { get; set; }

	    public double maximum_bid_price { get; set; }

	    public int total_volume { get; set; }

	    public int daily_volume { get; set; }

	    public DateTime start_date { get; set; }

	    public DateTime end_date { get; set; }

	    public LinkedResource linked_user { get; set; }

	    public LinkedResource linked_zone { get; set; }

	    public LinkedResource linked_campaign { get; set; }

	    public Statistic order_statistic { get; set; }


	    //******************************
	    //*** OUTPUT ONLY VARIABLES ****
	    //******************************
	    public string name { get; set; }

	    public string model { get; set; }

	    public string status { get; set; }

	    public int filled_volume { get; set; }

	    public double average_bid_price { get; set; }

	    public List<DateTime> scheduled_dates { get; set; }

	    public string realm { get; set; }

	    public DateTime last_run_date { get; set; }

	    public DateTime last_modified_date { get; set; }

	    public Order() {}
	
	    public static Order createOrder(double price, int total_volume, int daily_volume, DateTime start_date, DateTime end_date, 
			    LinkedResource linked_zone, LinkedResource linked_campaign) {
		    Order order = new Order();
		    order.gross_purchase_price = price;
		    order.net_purchase_price = price;
		    order.maximum_bid_price = price;
		    order.total_volume = total_volume;
		    order.daily_volume = daily_volume;
		    order.start_date = start_date;
            order.end_date = end_date != default(DateTime) ? end_date : end_date;
		    order.linked_zone = linked_zone;
		    order.linked_campaign = linked_campaign;
		    return order;
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

            public double average_conversion_amount { get; set; }

            public double days { get; set; }

            public string date { get; set; }

            public LinkedResource linked_order { get; set; }

		    public Statistic() {}
	    }
    }
}