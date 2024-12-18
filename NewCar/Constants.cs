﻿using NewCar.Buttons;
using SFML.Graphics;
using SFML.System;

namespace NewCar
{
    internal class Constants
    {
        public const int windowWidth = 1280;
        public const int windowHeight = 720;
        public const int fieldSize = 400;
        public const float airResistance = 0.09f;

        public const string car1FileName = "Images/Cars/Car1.png";

        public static Font font = new Font("Fonts/arial.ttf");
        
        public static ButtonColors baseButtonColors = new ButtonColors(new Color(0, 100, 0), new Color(0, 200, 0), 2);

    }
}
