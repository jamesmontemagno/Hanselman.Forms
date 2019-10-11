using System;
using System.Collections.Generic;
using System.Text;
using Hanselman.Models;

namespace Hanselman.Interfaces
{
    public interface IEnvironment
    {
        Theme GetOSTheme();
    }
}
