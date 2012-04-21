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
    public abstract class ApiTest : IApiTest {
        public ConnectorFactory factory { get; set; }
        
        public Dictionary<string, string> defaults { get; set; }

        protected ApiTest(ConnectorFactory factory, Dictionary<string, string> defaults) {
            this.factory = factory;
            this.defaults = defaults;
        }

        public virtual void run<T>() where T : Resource, new() {

            System.Console.WriteLine("\n------ START: " + GetType().Name);

            runTest<T>("List");

            T resource = (T) runTest<T>("Create");

            if (resource != null) {
                object[] args = new object[] { resource.id };

                runTest<T>("Update", args);

                runTest<T>("Process", args);

                runTest<T>("Delete", args);
            }

            System.Console.WriteLine("------ FINISH: " + GetType().Name + "\n");
        }

        public virtual List<T> List<T>() where T : Resource, new() { throw new NotImplementedException(); }

        public virtual T Create<T>() where T : Resource, new() { throw new NotImplementedException(); }

        public virtual bool Update<T>(string id) where T : Resource, new() { throw new NotImplementedException(); }

        public virtual bool Process<T>(string id) where T : Resource, new() { throw new NotImplementedException(); }

        public virtual bool Delete<T>(string id) { throw new NotImplementedException(); }

        private object runTest<T>(string testName, object[] args = null) {
            var thisType = GetType();
            var method = thisType.GetMethod(testName);
            object result = default(T);
            
            // If  the subclass has implemented the test method, invoke it
            if (method.DeclaringType == thisType && !method.IsAbstract) {
                method = method.MakeGenericMethod(typeof(T));
                onTestStart<T>(testName);
                
                try {
                    result = method.Invoke(this, args);
                    onTestComplete<T>(testName, result);
                } catch (Exception e) {
                    onTestComplete<T>(testName, e);
                    result = default(T); 
                }
            }
            return result;
        }

        public void onTestStart<T>(string testName) { }

        public void onTestComplete<T>(string testName, object result) {
            bool testPassed = false;
            string message = null;
            if (result != null) {
                var type = result.GetType();
                if (type == typeof(bool)) {
                    testPassed = (bool)result;
                } if (type.IsSubclassOf(typeof(Exception))) {
                    testPassed = false;
                    message = ((Exception)result).GetBaseException().Message;
                } else {
                    testPassed = result != null;
                    if (type == typeof(List<T>)) {
                        message = "Found " + ((List<T>)result).Count() + " resources";
                    } else {
                        message = result.ToString();
                    }
                }
            }
            System.Console.WriteLine(GetType().Name + ": " + testName + ": " + (testPassed ? "Success" : "Failed") + (message != null ? (": " + message) : ""));
        }
    }
}
