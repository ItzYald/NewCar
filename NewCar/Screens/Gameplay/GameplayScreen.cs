using NewCar.Models;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar.Screens.Gameplay
{
    internal class GameplayScreen : Screen
    {
        Car car;
        Field field;

        public GameplayScreen(Action<ScreensEnum> setNextScreen) : base(setNextScreen)
        {
            car = new Car("Images/Cars/Car1.png", 300, 7000);

            field = new Field(car.getPixelDistance);

            drawables.Add(field);
            nextables.Add(field);
            drawables.Add(car);
            nextables.Add(car);
        }

    }
}
