using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanselman.Portable.Helpers
{
    public interface ILaunchTwitter
    {
        bool OpenUserName(string username);
        bool OpenStatus(string statusId);
    }
}
