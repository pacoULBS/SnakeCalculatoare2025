using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeCalculatoare2025.BusinessLogic
{
    interface ICollidable
    {
        public bool CollidesWith(Position2D position);
    }
}
