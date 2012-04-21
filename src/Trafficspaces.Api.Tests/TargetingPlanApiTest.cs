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
    public class TargetingPlanApiTest : ApiTest {

        public TargetingPlanApiTest(ConnectorFactory factory, Dictionary<string, string> defaults) : base(factory, defaults) { }

        public override List<T> List<T>() {
            List<T> targetingPlans = factory.GetTargetingPlanConnector().List<T>(defaults);
            return targetingPlans;
        }

        public override T Create<T>() {

            TargetingPlan targetingPlan = new TargetingPlan("Test Targeting Plan", null);
            return factory.GetTargetingPlanConnector().Create<T>(targetingPlan);
        }

        public override bool Update<T>(string id) {

            Connector connector = factory.GetTargetingPlanConnector();
            
            TargetingPlan targetingPlan = connector.Read<TargetingPlan>(id);

            targetingPlan.name = string.Format("Test Targeting Plan 2 {0:yyyy-MM-dd'T'hh:mm:ss}", DateTime.UtcNow);
            targetingPlan.targets = new TargetingPlan.Targets();
            targetingPlan.targets.geographics = "us,ca";
            targetingPlan.targets.keywords = "football,basketball,baseball,hockey";
            
            TargetingPlan updatedTargetingPlan = connector.Update<TargetingPlan>(targetingPlan);

            return updatedTargetingPlan != null && updatedTargetingPlan.id.Equals(targetingPlan.id) && 
                        updatedTargetingPlan.name.Equals(targetingPlan.name) && 
                        updatedTargetingPlan.creation_date.Equals(targetingPlan.creation_date) &&
				        csv2Set(updatedTargetingPlan.targets.geographics).SetEquals(csv2Set(targetingPlan.targets.geographics)) &&
                        csv2Set(updatedTargetingPlan.targets.keywords).SetEquals(csv2Set(targetingPlan.targets.keywords));
        }

        public override bool Delete<T>(string id) {

            Connector connector = factory.GetTargetingPlanConnector();
            
            TargetingPlan targetingPlan = connector.Read<TargetingPlan>(id);
            return targetingPlan != null && connector.Delete(targetingPlan.id) && connector.Read<TargetingPlan>(targetingPlan.id) == null;
        }

        private HashSet<string> csv2Set(String csv) {
            string[] parts = csv.Split(new char[1] { ',' });
            return new HashSet<string>(parts);
        }
    }
}
