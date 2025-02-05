﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TheDesert.Collision
{
    public static class CollisionHelper
    {
        public static bool Collides(BoundingRectangle a, BoundingRectangle b)
        {
            return !(a.Right < b.Left || a.Left > b.Right ||
                     a.Top > b.Bottom || a.Bottom < b.Top);
        }
    }
}
