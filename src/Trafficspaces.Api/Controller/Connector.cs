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

using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using RestSharp.Serializers;
using RestSharp.Deserializers;
using Newtonsoft.Json.Linq;

using System.Reflection;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace Trafficspaces.Api.Controller {
    /// <summary>
    /// REST API wrapper.
    /// </summary>
    public class Connector {
        /// <summary>
        /// The API end point to use when making requests
        /// </summary>
        private EndPoint ApiEndPoint { get; set; }
        /// <summary>
        /// The resource path for all requests 
        /// </summary>
        private string ResourcePath { get; set; }

        private readonly string DefaultDateFormat = "yyyy-MM-dd'T'HH:mm:ss";

        private RestClient _client;

        protected ISerializer jsonSerializer { get; set; }

        /// <summary>
        /// Initializes a new client with the specified credentials.
        /// </summary>
        /// <param name="endPoint">The api endpoint object that contains the connection details</param>
        /// <param name="resourcePath">The resource path prefix for all requests</param>
        public Connector(EndPoint endPoint, string resourcePath) {
            ApiEndPoint = endPoint;

			ResourcePath = resourcePath;
			
			// silverlight friendly way to get current version
			var assembly = Assembly.GetExecutingAssembly();
			AssemblyName assemblyName = new AssemblyName(assembly.FullName);
			var version = assemblyName.Version;

			_client = new RestClient();
			_client.UserAgent = "trafficspaces-csharp/" + version; 
			_client.Authenticator = new HttpBasicAuthenticator(ApiEndPoint.Username, ApiEndPoint.Password);
			_client.BaseUrl = string.Format("{0}", ApiEndPoint.BaseURI);
            _client.FollowRedirects = true;
            _client.MaxRedirects = 3;
            _client.AddHandler("application/json", new Trafficspaces.Api.Controller.Utilities.JsonDeserializer());


            jsonSerializer = new Trafficspaces.Api.Controller.Utilities.JsonSerializer();
            jsonSerializer.DateFormat = DefaultDateFormat;
		}

        /// <summary>
        /// List resources
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="parameters">The parameters that should be included in the GET request</param>
        public List<T> List<T>(Dictionary<string, string> parameters) where T : Resource, new() {
            
            var request = new RestRequest(Method.GET);
            request.Resource = string.Format("{0}", ResourcePath);
            foreach(KeyValuePair<string, string> kvp in parameters) {
                request.AddParameter(kvp.Key, kvp.Value);
            }

            return Execute<List<T>>(request);
        }

        /// <summary>
        /// Read a resource
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="id">The identifier of the required resource</param>
        public T Read<T>(string id) where T : Resource, new() {
            Require.Argument("id", id);

            var request = new RestRequest(Method.GET);
            request.Resource = string.Format("{0}/{1}.json", ResourcePath, id);
            request.JsonSerializer = jsonSerializer;
            return Execute<T>(request);
        }

        /// <summary>
        /// Creates a new resource
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="resource">The resource that needs to be created</param>
        public T Create<T>(Resource resource) where T : Resource, new() {
            Require.Argument("resource", resource);
            
            var request = new RestRequest(Method.POST);
            request.Resource = string.Format("{0}", ResourcePath);
            request.RequestFormat = DataFormat.Json;
            request.DateFormat = DefaultDateFormat;
            request.JsonSerializer = jsonSerializer;
            request.AddBody(resource);

            return Execute<T>(request);
        }

        /// <summary>
        /// Updates an existing resource
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="resource">The resource that needs to be updated</param>
        public T Update<T>(Resource resource) where T : Resource, new() {
            Require.Argument("resource", resource);

            var request = new RestRequest(Method.PUT);
            request.Resource = string.Format("{0}/{1}.json", ResourcePath, resource.id);
            request.RequestFormat = DataFormat.Json;
            request.DateFormat = DefaultDateFormat;
            request.JsonSerializer = jsonSerializer;
            request.AddBody(resource);
            return Execute<T>(request);
        }

        /// <summary>
        /// Deletes a resource
        /// </summary>
        /// <param name="id">The identifier of the resource that needs to be deleted</param>
        public bool Delete(string id) {
            Require.Argument("id", id);

            var request = new RestRequest(Method.DELETE);
            request.Resource = string.Format("{0}/{1}.json", ResourcePath, id);
            var response = _client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        /// <summary>
        /// Posts an arbitrary Http request
        /// </summary>
        /// <param name="id">The identifier of the resource that needs to be deleted</param>
        public bool Process(Dictionary<string, string> parameters) {
            Require.Argument("parameters", parameters);

            var request = new RestRequest(Method.POST);
            request.Resource = string.Format("{0}/process", ResourcePath);
            foreach (KeyValuePair<string, string> kvp in parameters) {
                request.AddParameter(kvp.Key, kvp.Value);
            }

            var response = _client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        public T Execute<T>(RestRequest request) where T : new() {
            request.OnBeforeDeserialization = (resp) => {
                // for individual resources when there's an error to make
                // sure that RestException props are populated
                if (((int)resp.StatusCode) >= 400) {
                    // have to read the bytes so .Content doesn't get populated
                    var content = resp.RawBytes.AsString();
                    var json = JArray.Parse(content);
                    var newJson = new JObject();
                    newJson["RestException"] = json.First;
                    content = null;
                    resp.Content = null;
                    resp.RawBytes = Encoding.UTF8.GetBytes(newJson.ToString());
                }
            };

            request.DateFormat = DefaultDateFormat;

            var response = _client.Execute<T>(request);

            // If the status code is "201 Created", 
            // then follow the URL in the "Location" header to load the new resource
            if (response.StatusCode == HttpStatusCode.Created) {
                string newResourcePath = null;
                foreach (var header in response.Headers) {
                    if (header.Name.Equals("Location") && header.Value.ToString().Length > 0) {
                        newResourcePath = header.Value.ToString();
                        break;
                    }
                }
                if (newResourcePath != null) {
                    return Execute<T>(new RestRequest(newResourcePath, Method.GET));
                }
            } else if (response.StatusCode == HttpStatusCode.NotFound) {
                return default(T);
            }
            return response.Data;
        }

        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        public RestResponse Execute(RestRequest request) {
            return _client.Execute(request);
        }
    }
}