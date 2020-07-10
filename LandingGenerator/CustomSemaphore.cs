using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingGenerator
{
    public static class CustomSemaphore
    {
        public static bool IsFree { get; private set; }

        public static void Lock()
        {
            IsFree = false;
        }

        public static void Unlock()
        {
            IsFree = true;
        }
    }
}
