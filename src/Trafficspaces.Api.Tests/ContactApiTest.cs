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
    public class ContactApiTest : ApiTest {

        public ContactApiTest(ConnectorFactory factory, Dictionary<string, string> defaults) : base(factory, defaults) { }

        public override List<T> List<T>() {
            List<T> contacts = factory.GetContactConnector().List<T>(defaults);
            return contacts;
        }

        public override T Create<T>() {

            Contact contact = Contact.CreateContact("John Doe",
                    Contact.CreateProfile("john@test.com", "Test Company", Contact.Profile.TYPE_ADVERTISER),
                    null);
            return factory.GetContactConnector().Create<T>(contact);
        }

        public override bool Update<T>(string id) {

            Connector connector = factory.GetContactConnector();
            
            Contact contact = connector.Read<Contact>(id);
            contact.name = string.Format("Jane Smith {0:yyyy-MM-dd'T'hh:mm:ss}", DateTime.UtcNow);
            contact.profile.email = "jane@test.com";
            contact.profile.company_name = "Test Ad Agency";
            contact.profile.contact_details.street = "1 Madison Avenue";
            contact.profile.contact_details.city = "New York";
            contact.profile.contact_details.state = "NY";
            contact.profile.contact_details.country = "us";
            
            Contact updatedContact = connector.Update<Contact>(contact);

            return updatedContact != null && updatedContact.id.Equals(contact.id) && 
                        updatedContact.name.Equals(contact.name) && 
                        updatedContact.creation_date.Equals(contact.creation_date);
        }

        public override bool Delete<T>(string id) {

            Connector connector = factory.GetContactConnector();
            
            Contact contact = connector.Read<Contact>(id);
            return contact != null && connector.Delete(contact.id) && connector.Read<Contact>(contact.id) == null;
        }

    }
}
