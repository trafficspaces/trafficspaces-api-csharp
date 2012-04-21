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
using Trafficspaces.Api.Controller;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trafficspaces.Api.Tests {
    public class CouponApiTest : ApiTest {

        public CouponApiTest(ConnectorFactory factory, Dictionary<string, string> defaults) : base(factory, defaults) { }

        public override List<T> List<T>() {
            List<T> coupons = factory.GetCouponConnector().List<T>(defaults);
            return coupons;
        }

        public override T Create<T>() {
            String couponCode = "HALFPRICE";

		    // Remove the coupon if it already exists 
		    Coupon coupon = readCouponByCode(couponCode);
		    if (coupon != null) {
                factory.GetCouponConnector().Delete(coupon.id);
		    }
            
		    coupon = Coupon.createRelativeCoupon("Test Coupon", "HALFPRICE", 0, 50.0);
            return factory.GetCouponConnector().Create<T>(coupon);
        }

        public override bool Update<T>(string id) {

            Connector connector = factory.GetCouponConnector();
            
            Coupon coupon = connector.Read<Coupon>(id);
            coupon.name = string.Format("Test Coupon 2 {0:yyyy-MM-dd'T'hh:mm:ss}", DateTime.UtcNow);
            coupon.base_value = 100.0;
            coupon.maximum_cumulative_discount = 1000.0;
            coupon.maximum_cumulative_uses = 10;

            Coupon updatedCoupon = connector.Update<Coupon>(coupon);

            return updatedCoupon != null && updatedCoupon.id.Equals(coupon.id) && 
                        updatedCoupon.name.Equals(coupon.name) && 
                        updatedCoupon.creation_date.Equals(coupon.creation_date) &&
                        updatedCoupon.base_value == coupon.base_value &&
                        updatedCoupon.maximum_cumulative_discount == coupon.maximum_cumulative_discount &&
                        updatedCoupon.maximum_cumulative_uses == coupon.maximum_cumulative_uses;
        }

        public override bool Process<T>(string id) {

            Connector connector = factory.GetCouponConnector();

            Coupon coupon = connector.Read<Coupon>(id);

            double discount = 50;
            Dictionary<string, string> couponProcessParameters = new Dictionary<string, string>() {
                {"action", "use"},
                {"couponcode", coupon.code},
                {"discount", String.Format("{0}", discount)}
            };
            connector.Process(couponProcessParameters);
            
            Coupon processedCoupon = connector.Read<Coupon>(coupon.id);

            return processedCoupon != null && processedCoupon.id.Equals(coupon.id) &&
                        processedCoupon.name.Equals(coupon.name) &&
                        processedCoupon.creation_date.Equals(coupon.creation_date) &&
                        processedCoupon.cumulative_discount == (coupon.cumulative_discount + discount) &&
                        processedCoupon.cumulative_uses == (coupon.cumulative_uses + 1);
        }   

        public override bool Delete<T>(string id) {            Connector connector = factory.GetCouponConnector();
            
            Coupon coupon = connector.Read<Coupon>(id);
            return coupon != null && connector.Delete(coupon.id) && connector.Read<Coupon>(coupon.id) == null;
        }

        private Coupon readCouponByCode(String couponCode) {

            Dictionary<string, string> couponParameters = new Dictionary<string, string>();
            couponParameters.Add("couponcode", couponCode);
            List<Coupon> coupons = factory.GetCouponConnector().List<Coupon>(couponParameters);

            return (coupons != null && coupons.Count() == 1) ? coupons.First<Coupon>() : null;
        }
    }
}
