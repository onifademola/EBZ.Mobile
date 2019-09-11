using Akavache;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBZ.Mobile.ServicesInterface
{
    public interface IBlobCacheInstanceHelper
    {
        void Init();
        IBlobCache LocalMachineCache { get; set; }
    }

}
