using System;


namespace Hanselman
{
    public interface ITweetStore
    {
        void Save(System.Collections.Generic.List<Hanselman.Tweet> tweets);
        //System.Collections.Generic.List<Hanselman.Shared.Tweet> Load ();
    }
}

