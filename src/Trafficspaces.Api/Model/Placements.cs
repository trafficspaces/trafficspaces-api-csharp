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
using System.Collections.Generic;

namespace Trafficspaces.Api.Model {
	/// <summary>
	/// An implementation of the Placement resource as defined in http://support.trafficspaces.com/kb/api/api-placement.
	/// </summary>
    public class Placement : Resource {

	    //******************************
	    //** INPUT & OUTPUT VARIABLES **
	    //******************************
	    public string handle { get; set; }
	
	    //******************************
	    //*** INPUT ONLY VARIABLES  ****
	    //******************************
	    public string medium { get; set; }

	    public int count { get; set; }

	    public bool useiframe { get; set; }

	    public string frame { get; set; }

	    public string title { get; set; }

	    //******************************
	    //*** OUTPUT ONLY VARIABLES ****
	    //******************************
        public List<Ad> ads { get; private set; }

	    public Placement() {}
	
	    public static Placement createPlacement(string handle) {
		    return createPlacement(handle, null, 1);
	    }
	
	    public static Placement createPlacement(string handle, string medium, int count) {
		    Placement placement = new Placement();
		    placement.handle = handle;
		    placement.medium = medium;
		    placement.count = count;
		    return placement;
	    }

	    public class Ad : Resource {
	
		    //******************************
		    //*** OUTPUT ONLY VARIABLES ****
		    //******************************
		    public string medium { get; set; }

		    public int width { get; set; }

		    public int height { get; set; }

		    public Creative creative { get; set; }

		    public Ad() {}
		
		    public class Creative : Resource {
			    //******************************
			    //*** OUTPUT ONLY VARIABLES ****
			    //******************************
                public Uri flash_url { get; set; }

                public Uri video_url { get; set; }

                public Uri audio_url { get; set; }

                public Uri image_url { get; set; }

                public string title { get; set; }

                public string caption { get; set; }

                public string anchor { get; set; }

                public string raw { get; set; }

                public Uri target_url { get; set; }

			    public Creative() {}
		    }
	    }
    }
}