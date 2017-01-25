using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Render
{
    public static class Constants
    {
        public const int OutOfRangeIndex = -1;

        public const float Eps = 0.000001f;

        public const int MaxCastDepth = 5;

        public const int RaysCount = 1000000;

        public const float RoomHeight = 5.0f;

        public const int LightCount = 3;

        public const float FloorCellCount = 5;

        public const float RoomSize = 10.0f;
    }
}
