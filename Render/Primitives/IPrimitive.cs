using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Render.Primitives
{
    public interface IPrimitive
    {
        EPrimitiveType Type { get; }

        Material Material { get; set; }

        bool FindIntersection(ref Ray ray);
    }
}
