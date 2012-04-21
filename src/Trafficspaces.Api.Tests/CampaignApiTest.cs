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
    public class CampaignApiTest : ApiTest {

        public CampaignApiTest(ConnectorFactory factory, Dictionary<string, string> defaults) : base(factory, defaults) { }

        public override List<T> List<T>() {
            List<T> campaigns = factory.GetCampaignConnector().List<T>(defaults);
            return campaigns;
        }

        public override T Create<T>() {

            Campaign campaign = Campaign.createCampaign("Test Campaign", null);
            return factory.GetCampaignConnector().Create<T>(campaign);
        }

        public override bool Update<T>(string id) {

            Connector connector = factory.GetCampaignConnector();

            Ad ad = Ad.createAd("Test Ad", 300, 250, "text",
                    Ad.Creative.createTextCreative("My Ad Title", "My Ad Caption", "TestAd.com", null, "http://www.testad.com"));
            ad = factory.GetAdConnector().Create<Ad>(ad);

            try {
                Campaign campaign = connector.Read<Campaign>(id);

                campaign.name = string.Format("Test Campaign 2 {0:yyyy-MM-dd'T'hh:mm:ss}", DateTime.UtcNow);
                campaign.GetLinkedAds().Clear();
                campaign.GetLinkedAds().Add(new LinkedResource(ad.id, ad.name));

                Campaign updatedCampaign = connector.Update<Campaign>(campaign);

                return updatedCampaign != null && updatedCampaign.id.Equals(campaign.id) &&
                            updatedCampaign.name.Equals(campaign.name) &&
                            updatedCampaign.creation_date.Equals(campaign.creation_date) &&
                            updatedCampaign.GetLinkedAds() != null && updatedCampaign.GetLinkedAds().Count() == 1;
            } finally {
                factory.GetAdConnector().Delete(ad.id);
            }
        }

        public override bool Delete<T>(string id) {

            Connector connector = factory.GetCampaignConnector();
            
            Campaign campaign = connector.Read<Campaign>(id);
            return campaign != null && connector.Delete(campaign.id) && connector.Read<Campaign>(campaign.id) == null;
        }

    }
}
