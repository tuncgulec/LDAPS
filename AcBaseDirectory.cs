using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tnc.LDAPS.Library
{
    public class AcBaseDirectory
    {
        public string BaseDn;
        public string LdapHost;
        public int Port;
        public string PublicUsername;
        public string PublicPassword;

        /// <summary>
        /// Active Directory Kullanıcısı ile giriş yapmak
        /// </summary>
        /// <param name="Username">Giriş yapılması istenen kullanıcı adı</param>
        /// <param name="Password">Giriş yapılması istenen kullanıcı şifresi</param>
        /// <param name="AttributesFilter">Değerlerini getirmek istediğiniz içerikler</param>
        /// <param name="AcPerson">Giriş başarılı ise, Active directory içinde tanımlı olan ve istenen attibute lerin değerlerini döndürür.</param>
        /// <returns></returns>
        public bool DoLogin(string Username, string Password, string[] AttributesFilter, out dynamic AcPerson)
        {
            bool result = false;
            AcPerson = null;
            LdapDirectoryIdentifier Ldi = new LdapDirectoryIdentifier(LdapHost, Port);
            System.DirectoryServices.Protocols.LdapConnection ldapConnection = new System.DirectoryServices.Protocols.LdapConnection(Ldi);
            ldapConnection.SessionOptions.ProtocolVersion = 3;
            NetworkCredential Nc = new NetworkCredential(PublicUsername, PublicPassword);
            ldapConnection.Bind(Nc);
            string Filter = "(&(objectcategory=person)(objectclass=user)(samaccountname=" + Username + "))";

            var Request = new SearchRequest(BaseDn, Filter, SearchScope.Subtree, AttributesFilter);
            var Ldaprespone = (SearchResponse)ldapConnection.SendRequest(Request);

            if (Ldaprespone != null)
            {
                string newdn = Ldaprespone.Entries[0].DistinguishedName;

                if (newdn != "")
                {
                    ldapConnection.Bind(new NetworkCredential()
                    {
                        UserName = Username,
                        Password = Password
                    });
                    AcPerson = JsonConvert.SerializeObject(Ldaprespone.Entries);
                    result = true;
                }

            }

            return result;
        }

        public dynamic DoSearch(string Filter, string[] AttributesFilter)
        {
            dynamic result = false;
            LdapDirectoryIdentifier Ldi = new LdapDirectoryIdentifier(LdapHost, Port);
            System.DirectoryServices.Protocols.LdapConnection ldapConnection = new System.DirectoryServices.Protocols.LdapConnection(Ldi);
            ldapConnection.SessionOptions.ProtocolVersion = 3;
            NetworkCredential Nc = new NetworkCredential(PublicUsername, PublicPassword);
            ldapConnection.Bind(Nc);
            var Request = new SearchRequest(BaseDn, Filter, SearchScope.Subtree, AttributesFilter);
            var Ldaprespone = (SearchResponse)ldapConnection.SendRequest(Request);
            if (Ldaprespone != null)
                result = JsonConvert.SerializeObject(Ldaprespone.Entries);
            return result;
        }

    }
}
