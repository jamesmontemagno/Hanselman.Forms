using System.Drawing;
using Hanselman.Models;

namespace Hanselman.Interfaces
{
    public interface IEnvironment
    {
        Theme GetOSTheme();
        void SetStatusBarColor(Color color, bool darkStatusBarTint);
    }
}
