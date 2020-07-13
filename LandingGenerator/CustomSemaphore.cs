using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingGenerator
{
    public class CustomSemaphore
    {
        public bool IsFree { get; private set; }

        public void Lock()
        {
            IsFree = false;
        }

        public void Unlock()
        {
            IsFree = true;
        }
    }
}
