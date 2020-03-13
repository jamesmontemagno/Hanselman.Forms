using System.Drawing;
using Hanselman.Models;

namespace Hanselman.Interfaces
{
    public interface IEnvironment
    {
        void SetStatusBarColor(Color color, bool darkStatusBarTint);
    }
}
