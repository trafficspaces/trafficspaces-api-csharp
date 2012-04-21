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
	/// An implementation of the Coupon resource as defined in http://support.trafficspaces.com/kb/api/api-coupons.
	/// </summary>
    public class Coupon : Resource {
	    //******************************
	    //** INPUT & OUTPUT VARIABLES **
	    //******************************
        public string name { get; set; }

        public string code { get; set; }

        public string type { get; set; }

        public double base_value { get; set; }

        public double discount_value { get; set; }

        public double maximum_cumulative_discount { get; set; }

        public int maximum_cumulative_uses { get; set; }

        public string reference { get; set; }

        public string linked_user { get; set; }
	
	    //******************************
	    //*** OUTPUT ONLY VARIABLES ****
	    //******************************
        public string realm { get; set; }

        public double cumulative_discount { get; set; }

        public int cumulative_uses { get; set; }

        public DateTime creation_date { get; set; }

        public DateTime last_modified_date { get; set; }

        public DateTime expiration_date { get; set; }
	
	    public Coupon() {}
	
	    public static Coupon createAbsoluteCoupon(string name, string code, double base_value, double discount_value) {
		    return Coupon.createCoupon(name, code, base_value, discount_value, "absolute");
	    }
	
	    public static Coupon createRelativeCoupon(string name, string code, double base_value, double discount_value) {
		    return Coupon.createCoupon(name, code, base_value, discount_value, "relative");
	    }
	
	    public static Coupon createCoupon(string name, string code, double base_value, double discount_value, string type) {
		    Coupon coupon = new Coupon();
		    coupon.name = name;
		    coupon.code = code;
		    coupon.base_value = base_value;
		    coupon.discount_value = discount_value;
		    coupon.type = type;
		    return coupon;
	    }
    }
}