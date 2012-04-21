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
    /// An implementation of the Ad resource as defined in http://support.trafficspaces.com/kb/api/api-ads.
	/// </summary>
    public class Ad : Resource {
	    //******************************
	    //** INPUT & OUTPUT VARIABLES **
	    //******************************
	    public string name { get; set; }

	    public int width { get; set; }

	    public int height { get; set; }

	    public string status { get; set; }

	    public string format { get; set; }

	    public Creative creative { get; set; }

	    public LinkedResource linked_user { get; set; }

	    public LinkedResource linked_contact { get; set; }

	    public LinkedResource linked_targeting_plan { get; set; }

	    //******************************
	    //*** OUTPUT ONLY VARIABLES ****
	    //******************************
	    public string realm { get; set; }

	    public DateTime creation_date { get; set; }

	    public DateTime last_modified_date { get; set; }

	    public Ad() {}
	
	    public static Ad createAd(string name, int width, int height, string format, Creative creative) {
		    return Ad.createAd(name, width, height, format, creative, null, null, null);
	    }
	
	    public static Ad createAd(string name, int width, int height, string format, Creative creative, 
			    LinkedResource linked_user, LinkedResource linked_contact, LinkedResource linked_targeting_plan) {
		    Ad ad = new Ad();
		    ad.name = name;
		    ad.width = width;
		    ad.height = height;
		    ad.format = format;
		    ad.creative = creative;
		    ad.linked_user = linked_user;
		    ad.linked_contact = linked_contact;
		    ad.linked_targeting_plan = linked_targeting_plan;
		    return ad;
	    }

	    public class Creative : Resource {
		    //******************************
		    //** INPUT & OUTPUT VARIABLES **
		    //******************************
            public string flash_url { get; set; }

            public string image_url { get; set; }

            public string audio_url { get; set; }

            public string video_url { get; set; }

            public string title { get; set; }

            public string caption { get; set; }

            public string anchor { get; set; }

            public string raw { get; set; }

            public string target_url { get; set; }
		
		    public Creative() {}
		
		    public static Creative createTextCreative(string title, string caption, string anchor, string image_url, string target_url) {
			    Creative creative = new Creative();
			    creative.title = title;
			    creative.caption = caption;
			    creative.anchor = anchor;
			    creative.image_url = image_url;
			    creative.target_url = target_url;
			    return creative;
		    }

		    public static Creative createImageCreative(string image_url, string target_url) {
			    Creative creative = new Creative();
			    creative.image_url = image_url;
			    creative.target_url = target_url;
			    return creative;
		    }
		
		    public static Creative createFlashCreative(string flash_url, string target_url) {
			    Creative creative = new Creative();
			    creative.flash_url = flash_url;
			    creative.target_url = target_url;
			    return creative;
		    }
		
		    public static Creative createAudioCreative(string audio_url, string target_url) {
			    Creative creative = new Creative();
			    creative.audio_url = audio_url;
			    creative.target_url = target_url;
			    return creative;
		    }

		    public static Creative createVideoCreative(string video_url, string target_url) {
			    Creative creative = new Creative();
			    creative.video_url = video_url;
			    creative.target_url = target_url;
			    return creative;
		    }

		    public static Creative createRawCreative(string raw, string target_url) {
			    Creative creative = new Creative();
			    creative.raw = raw;
			    creative.target_url = target_url;
			    return creative;
		    }
	    }
    }
}