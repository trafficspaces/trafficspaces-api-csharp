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
    public class ZoneApiTest : ApiTest {

        public ZoneApiTest(ConnectorFactory factory, Dictionary<string, string> defaults) : base(factory, defaults) { }

        public override List<T> List<T>() {
            List<T> zones = factory.GetZoneConnector().List<T>(defaults);
            return zones;
        }

        public override T Create<T>() {

            Zone zone = Zone.createZone("Test Zone", 300, 250, "text,image,flash", new Zone.Pricing("cpm", 5.0));
            return factory.GetZoneConnector().Create<T>(zone);
        }

        public override bool Update<T>(string id) {

            Connector connector = factory.GetZoneConnector();
            
            Zone zone = connector.Read<Zone>(id);
            zone.name = string.Format("Test Zone 2 {0:yyyy-MM-dd'T'hh:mm:ss}", DateTime.UtcNow);
		    zone.formats = "text,image";
		    zone.description = "Just another test zone";
		    zone.default_ad_tag = "<!-- Insert Google Adsense Tag -->";
		    zone.position = "anywhere";
		    zone.channel = "blog";
            
            Zone updatedZone = connector.Update<Zone>(zone);

            return updatedZone != null && updatedZone.id.Equals(zone.id) && 
                        updatedZone.name.Equals(zone.name) && 
                        updatedZone.creation_date.Equals(zone.creation_date) &&
                        updatedZone.description.Equals(zone.description);
        }

        public override bool Delete<T>(string id) {

            Connector connector = factory.GetZoneConnector();
            
            Zone zone = connector.Read<Zone>(id);
            return zone != null && connector.Delete(zone.id) && connector.Read<Zone>(zone.id) == null;
        }

    }
}
