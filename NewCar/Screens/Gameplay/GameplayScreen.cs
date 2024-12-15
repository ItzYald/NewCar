using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar.Screens.Gameplay
{
    internal class GameplayScreen : Screen
    {
        PlayerCar car;
        BotCar botCar;
        Field field;

        public GameplayScreen(Action<ScreensEnum> setNextScreen) : base(setNextScreen)
        {
            car = new PlayerCar(Constants.car1FileName, 300, 9000);
            botCar = new BotCar(Constants.car1FileName, 300, 9000, car.getPixelDistance);

            field = new Field(car.getPixelDistance);

            nextables.Add(new CheckKey(Keyboard.Key.O, Start));
            nextables.Add(new CheckKey(Keyboard.Key.P, Stop));

            drawables.Add(field);
            nextables.Add(field);
            drawables.Add(car);
            nextables.Add(car);
            drawables.Add(botCar);
            nextables.Add(botCar);
        }

        private void Start()
        {
            car.Start();
            botCar.Start();
        }

        private void Stop()
        {
            car.Stop();
            botCar.Stop();
        }

    }
}
