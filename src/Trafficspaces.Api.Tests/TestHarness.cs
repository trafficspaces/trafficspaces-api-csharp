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
    class TestHarness {

        protected ConnectorFactory factory { get; set; }

        protected Dictionary<string, string> defaults { get; set; }

        protected TestHarness() {
            defaults = new Dictionary<string, string>() {
                {"page", "1"},
                {"pagesize", "50"}
            };
        }

        public TestHarness(string subDomain, string apiKey) : this() {
            factory = new ConnectorFactory(subDomain, apiKey);
        }

        public TestHarness(EndPoint adStoreApiEndPoint, EndPoint adServerApiEndPoint) : this() {
            factory = new ConnectorFactory(adStoreApiEndPoint, adServerApiEndPoint);
        }

        static void Main(string[] args) {
            if (args == null || args.Length != 2) {
			    System.Console.WriteLine("Usage: APITest.exe <subdomain> <apikey>");
                return;
		    }
		
            TestHarness testHarness = new TestHarness(args[0], args[1]);
            testHarness.runTests();
        }

        public virtual void runTests() {
            
            new UserApiTest(factory, defaults).run<User>();
            
            new ContactApiTest(factory, defaults).run<Contact>();

            new ZoneApiTest(factory, defaults).run<Zone>();

            new AdApiTest(factory, defaults).run<Ad>();
            
            new CampaignApiTest(factory, defaults).run<Campaign>();
            
            new TargetingPlanApiTest(factory, defaults).run<TargetingPlan>();

            new FeedApiTest(factory, defaults).run<Feed>();

            new OrderApiTest(factory, defaults).run<Order>();

            new CouponApiTest(factory, defaults).run<Coupon>();

            new PlacementApiTest(factory, defaults).run<Placement>();
            
        }
    }
}
