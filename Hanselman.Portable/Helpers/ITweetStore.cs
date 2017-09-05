using System;


namespace $safeprojectname$.Portable
{
    public interface ITweetStore
    {
        void Save(System.Collections.Generic.List<$safeprojectname$.Portable.Tweet> tweets);
        //System.Collections.Generic.List<$safeprojectname$.Shared.Tweet> Load ();
    }
}

