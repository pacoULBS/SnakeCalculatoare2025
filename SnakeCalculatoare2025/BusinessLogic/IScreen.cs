using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeCalculatoare2025.BusinessLogic
{
    internal interface IScreen
    {
        void Clear(ScreenColor color = ScreenColor.Black);

        void Write(Position2D position,
            string text,
            ScreenColor color = ScreenColor.White,
            ScreenColor backgroundColor = ScreenColor.Black);

        void Flush();
    }
}
