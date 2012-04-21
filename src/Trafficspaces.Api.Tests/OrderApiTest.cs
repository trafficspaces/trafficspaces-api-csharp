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
    public class OrderApiTest : ApiTest {

        public OrderApiTest(ConnectorFactory factory, Dictionary<string, string> defaults) : base(factory, defaults) { }

        public override List<T> List<T>() {
            List<T> orders = factory.GetOrderConnector().List<T>(defaults);
            return orders;
        }

        public override T Create<T>() {

            Zone zone = Zone.createZone("Test Zone", 300, 250, "text", new Zone.Pricing("cpc", 0.5));
            zone = factory.GetZoneConnector().Create<Zone>(zone);

            Ad ad = Ad.createAd("Test Ad", 300, 250, "text", Ad.Creative.createTextCreative("My Ad Title", "My Ad Caption", "TestAd.com", null, "http://www.testad.com"));
            ad = factory.GetAdConnector().Create<Ad>(ad);

            Campaign campaign = Campaign.createCampaign("Test Campaign", null);
            campaign.GetLinkedAds().Add(new LinkedResource(ad.id, ad.name));
            campaign = factory.GetCampaignConnector().Create<Campaign>(campaign);

            Order order = Order.createOrder(0.5, 100000, 1000, DateTime.UtcNow, default(DateTime) /* "default" denotes no end date*/,
                    new LinkedResource(zone.id, zone.name), new LinkedResource(campaign.id, campaign.name));
            
            return factory.GetOrderConnector().Create<T>(order);
        }

        public override bool Update<T>(string id) {

            Connector connector = factory.GetOrderConnector();
            
            Order order = connector.Read<Order>(id);
            order.maximum_bid_price = 0.75;
		    order.daily_volume = 5000;
            
            Order updatedOrder = connector.Update<Order>(order);

            return updatedOrder != null && updatedOrder.id.Equals(order.id) && 
                        updatedOrder.name.Equals(order.name) && 
                        updatedOrder.start_date.Equals(order.start_date) &&
                        updatedOrder.maximum_bid_price == order.maximum_bid_price &&
                        updatedOrder.daily_volume == order.daily_volume;
        }

        public override bool Process<T>(string id) {

            Connector connector = factory.GetOrderConnector();

            Order order = connector.Read<Order>(id);

            Dictionary<string, string> orderProcessParameters = new Dictionary<string, string>() {
                {"action", "stop"},
                {"orderid", order.id}
            };
            connector.Process(orderProcessParameters);
            Order processedOrder = connector.Read<Order>(order.id);

            return processedOrder != null && processedOrder.id.Equals(order.id) &&
                        processedOrder.name.Equals(order.name) &&
                        processedOrder.start_date.Equals(order.start_date) &&
                        processedOrder.status != null && (processedOrder.status.Equals("stopping") || processedOrder.status.Equals("stopped"));
        }

        public override bool Delete<T>(string id) {

            Connector connector = factory.GetOrderConnector();
            
            Order order = connector.Read<Order>(id);

            if (order != null) {
                Campaign campaign = factory.GetCampaignConnector().Read<Campaign>(order.linked_campaign.id);
                if (campaign != null) {
                    List<LinkedResource> linked_ads = campaign.GetLinkedAds();
                    foreach (var linked_ad in linked_ads) {
                        factory.GetAdConnector().Delete(linked_ad.id);
                    }
                    factory.GetCampaignConnector().Delete(order.linked_campaign.id);
                }
                factory.GetZoneConnector().Delete(order.linked_zone.id);
            }

            return order != null && connector.Delete(order.id) && connector.Read<Order>(order.id) == null;
        }

    }
}
