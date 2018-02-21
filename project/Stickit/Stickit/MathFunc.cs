using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stickit
{
    class MathFunc : ITerrainFunc
    {
        Func<float, float, float> func;
        public MathFunc(Func<float, float, float> func)
        {
            this.func = func;
        }
        public void init()
        { }

        public float get(float x, float z)
        {
            return func(x, z);
        }
    }
}
