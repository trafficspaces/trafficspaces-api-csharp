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
    public class FeedApiTest : ApiTest {

        public FeedApiTest(ConnectorFactory factory, Dictionary<string, string> defaults) : base(factory, defaults) { }

        public override List<T> List<T>() {
            List<T> feeds = factory.GetFeedConnector().List<T>(defaults);
            return feeds;
        }

        public override T Create<T>() {

            Feed feed = Feed.createFeed("Test Feed", 728, 90, 100.0, "<!-- Google AdSense Backfill -->");
            return factory.GetFeedConnector().Create<T>(feed);
        }

        public override bool Update<T>(string id) {

            Connector connector = factory.GetFeedConnector();
            
            Feed feed = connector.Read<Feed>(id);
            feed.name = string.Format("Test Feed 2 {0:yyyy-MM-dd'T'hh:mm:ss}", DateTime.UtcNow);
            feed.weight = 20.0;
            feed.ad_tag = "<!-- Another 3rd party Ad Tag-->";
            
            Feed updatedFeed = connector.Update<Feed>(feed);

            return updatedFeed != null && updatedFeed.id.Equals(feed.id) && 
                        updatedFeed.name.Equals(feed.name) && 
                        updatedFeed.creation_date.Equals(feed.creation_date) &&
                        updatedFeed.weight == feed.weight && updatedFeed.ad_tag.Equals(feed.ad_tag);
        }

        public override bool Delete<T>(string id) {

            Connector connector = factory.GetFeedConnector();
            
            Feed feed = connector.Read<Feed>(id);
            return feed != null && connector.Delete(feed.id) && connector.Read<Feed>(feed.id) == null;
        }

    }
}
