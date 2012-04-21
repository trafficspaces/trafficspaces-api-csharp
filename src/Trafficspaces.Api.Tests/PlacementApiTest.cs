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
    public class PlacementApiTest : ApiTest {

        public PlacementApiTest(ConnectorFactory factory, Dictionary<string, string> defaults) : base(factory, defaults) { }

        public override List<T> List<T>() {

            Dictionary<string, string> orderParameters = new Dictionary<string, string>(defaults);
            PlacementConnector connector = factory.GetPlacementConnector();

            List<T> allPlacements = new List<T>();
            
            orderParameters["pagesize"] = "10";
            orderParameters["status"] = "playing";

            List<Order> orders = factory.GetOrderConnector().List<Order>(orderParameters);

            if (orders.Count() == 0) {
                System.Console.WriteLine("There are no available insertion orders");
            } else {
                foreach (var order in orders) {

                    Zone zone = factory.GetZoneConnector().Read<Zone>(order.linked_zone.id);

                    DateTime startTime = DateTime.UtcNow;


                    Placement placement = Placement.createPlacement(zone.handle, null, 5);

                    // Fetch ads
                    placement = connector.FetchAds(placement);

                    int adCount = 0;
                    if (placement != null && placement.ads != null) {
                        adCount = placement.ads.Count();
                        allPlacements.Add((T)(object)placement);
                    }
                    System.Console.WriteLine("Zone: " + zone.handle + ": Found  " + adCount + " ads in " + (DateTime.UtcNow - startTime).TotalMilliseconds + " (msecs)");
                }
            }
            return allPlacements;
        }
    }
}
