using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.KinectExplorer
{
        public static class Extension
        {
            public static T RandomElement<T>(this IEnumerable<T> coll)
            {
                var rnd = new Random();
                return coll.ElementAt(rnd.Next(coll.Count()));
            }
        }
}
