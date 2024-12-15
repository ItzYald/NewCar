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
        DrawCar car;
        BotCar botCar;
        Field field;

        public GameplayScreen(Action<ScreensEnum> setNextScreen) : base(setNextScreen)
        {
            car = new DrawCar(Constants.car1FileName, 300, 7000);
            botCar = new BotCar(Constants.car1FileName, 300, 7000, car.getPixelDistance);

            field = new Field(car.getPixelDistance);

            drawables.Add(field);
            nextables.Add(field);
            drawables.Add(car);
            nextables.Add(car);
            drawables.Add(botCar);
            nextables.Add(botCar);
        }

    }
}
