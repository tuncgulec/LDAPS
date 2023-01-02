using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Tnc.LDAPS.Library
{
    public class AcDirectory : AcBaseDirectory, IAcDirectory
    {
        /// <summary>
        /// Constructor. 
        /// </summary>
        /// <param name="BaseDn">Your Base DN</param>
        /// <param name="LdapHost">Your LDAP Host Name</param><
        /// <param name="Port">LDAPS port number</param>
        /// <param name="PublicUsername">Your Admin Username</param>
        /// <param name="PublicPassword">Your Admin Password</param>
        /// <param name="ProtocolType">Default SecurityProtocolType.Tls12</param>
        /// <example>AcDirectory("DC=yourdomain,DC=com,DC=tr", "YourHostName", 636, "MyAdminUsername", "YourAdminPassword")</example>
        public AcDirectory(string BaseDn, string LdapHost, int Port, string PublicUsername, string PublicPassword, SecurityProtocolType ProtocolType = SecurityProtocolType.Tls12)
        {            
            base.Port = Port;
            base.LdapHost = LdapHost;
            base.BaseDn = BaseDn;
            base.PublicPassword = PublicPassword;
            base.PublicUsername = PublicUsername;
            System.Net.ServicePointManager.SecurityProtocol = ProtocolType;
        }

        /// <summary>
        /// Login With LDAPS person
        /// </summary>
        /// <param name="Username">Loginned username</param>
        /// <param name="Password">Loginned password</param>
        /// <param name="AttributesFilter">Attributes Filter</param>
        /// <param name="AcPersonData">Out parameters is AC person data </param>
        /// <returns></returns>
        /// <example>Login("myLdapUsername", "myLdapPassword", "string[] strAttributes = { "distinguishedname", "givenname", "name", "sn", "samaccountname", "mail", "mobile", "employeenumber", "adspath", "countrycode", "displayname", "accountexpires" };" )</example>
        public bool Login(string Username, string Password, string[] AttributesFilter, out dynamic AcPersonData)
        {            
            return base.DoLogin(Username, Password, AttributesFilter, out AcPersonData);
        }

        public dynamic Search(string Filter, string[] AttributesFilter)
        {
            return base.DoSearch(Filter, AttributesFilter);
        }
    }
}
