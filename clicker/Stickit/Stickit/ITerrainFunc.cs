using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stickit
{
    interface ITerrainFunc
    {
        void init();
        float get(float x,float z);
    }
}
