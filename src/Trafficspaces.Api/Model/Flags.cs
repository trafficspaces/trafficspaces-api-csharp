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
    /// An implementation of the ad targeting flags as defined in http://support.trafficspaces.com/kb/api/api-placement.
	/// </summary>
    public class Flags : Resource {

	    //******************************
	    //*** INPUT ONLY VARIABLES  ****
	    //******************************

        public string keywords { get; set; }

        public string interests { get; set; }

        public string geographics { get; set; }

        public string coordinates { get; set; }

        public string genders { get; set; }

        public string ageranges { get; set; }

        public string incomeranges { get; set; }

        public string ethnicities { get; set; }

        public string relationships { get; set; }

        public string qualifications { get; set; }

        public string jobs { get; set; }

        public string industries { get; set; }

        public string politics { get; set; }

        public string religions { get; set; }

        public string languages { get; set; }

	    public Flags() {}
    }
}