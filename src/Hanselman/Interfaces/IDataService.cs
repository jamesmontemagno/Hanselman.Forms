using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hanselman.Models;

namespace Hanselman.Interfaces
{
    public interface IDataService
    {
        Task<IEnumerable<Podcast>> GetPodcastsAsync();
    }
}
