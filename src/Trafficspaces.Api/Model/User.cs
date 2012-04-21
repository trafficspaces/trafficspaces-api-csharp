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
using System;

namespace Trafficspaces.Api.Model {
	/// <summary>
    /// An implementation of the User resource as defined in http://support.trafficspaces.com/kb/api/api-users.
	/// </summary>
	public class User : Resource {

		public string name { get; set; }
		
		public string role { get; set; }
		
		public Profile profile { get; set; }
		
		public DateTime creation_date { get; set; }
		
		public DateTime last_modified_date { get; set; }
		
		public DateTime expiration_date { get; set; }

        public class Profile : Resource {

            public string title { get; set; }

            public string first_name { get; set; }

            public string last_name { get; set; }

            public string gender { get; set; }

            public string date_of_birth { get; set; }

            public string company_name { get; set; }

            public string website { get; set; }

            public string email { get; set; }

            public Uri photo_url { get; set; }

            public string locale { get; set; }

            public ContactDetails contact_details { get; set; }

            public class ContactDetails : Resource {

                public string street { get; set; }

                public string street2 { get; set; }

                public string city { get; set; }

                public string state { get; set; }

                public string zip { get; set; }

                public string country { get; set; }

                public string mobile { get; set; }

                public string telephone { get; set; }

                public string fax { get; set; }

            }

        }
    }
}