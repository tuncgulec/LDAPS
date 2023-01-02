using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tnc.LDAPS.Library
{
    internal interface IAcDirectory
    {
        bool Login(string Username, string Password, string[] AttributesFilter, out dynamic AcPerson);
        dynamic Search(string Filter, string[] AttributesFilter);
    }
}
