using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRA.iTOPS.Service.Core
{
    public class ContextBase : ContextBoundObject
    {
        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
