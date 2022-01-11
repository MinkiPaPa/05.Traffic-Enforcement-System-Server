using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRA.TBOS.Windows.Core.Splash
{
    public delegate void ManualCancelHandler();
    public interface ISplasher
    {
        event ManualCancelHandler ManualCanceled;
        void SetStatus(string message);
    }
}
