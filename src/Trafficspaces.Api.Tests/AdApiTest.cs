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
    public class AdApiTest : ApiTest {

        public AdApiTest(ConnectorFactory factory, Dictionary<string, string> defaults) : base(factory, defaults) { }

        public override List<T> List<T>() {
            List<T> ads = factory.GetAdConnector().List<T>(defaults);
            return ads;
        }

        public override T Create<T>() {

            Ad ad = Ad.createAd("Test Ad", 300, 250, "text",
                    Ad.Creative.createTextCreative("My Ad Title", "My Ad Caption", "TestAd.com", null, "http://www.testad.com"));
            return factory.GetAdConnector().Create<T>(ad);
        }

        public override bool Update<T>(string id) {

            Connector connector = factory.GetAdConnector();
            
            Ad ad = connector.Read<Ad>(id);
            ad.name = string.Format("Test Ad 2 {0:yyyy-MM-dd'T'hh:mm:ss}", DateTime.UtcNow);
            ad.status = "approved";
            ad.creative.title = "Another Ad Title";
            ad.creative.caption = "Yet another caption";
            ad.creative.target_url = "http://www.testads.com/landing_page/";
            
            Ad updatedAd = connector.Update<Ad>(ad);

            return updatedAd != null && updatedAd.id.Equals(ad.id) && 
                        updatedAd.name.Equals(ad.name) && 
                        updatedAd.creation_date.Equals(ad.creation_date) &&
                        updatedAd.status.Equals(ad.status);
        }

        public override bool Delete<T>(string id) {

            Connector connector = factory.GetAdConnector();
            
            Ad ad = connector.Read<Ad>(id);
            return ad != null && connector.Delete(ad.id) && connector.Read<Ad>(ad.id) == null;
        }

    }
}
