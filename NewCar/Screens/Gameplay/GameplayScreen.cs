using NewCar.Models;
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
        BotCar botCar2;
        Field field;

        public GameplayScreen(Action<ScreensEnum> setNextScreen) : 
            base(setNextScreen)
        {
            car = new PlayerCar(Constants.car1FileName, new CarSpecifications(new EngineSpecifications(300, 7000), 60));
            botCar = new BotCar(Constants.car1FileName, new CarSpecifications(new EngineSpecifications(200, 9000), 60), car.getPixelDistance);
            botCar2 = new BotCar(Constants.car1FileName, new CarSpecifications(new EngineSpecifications(300, 9000), 60), car.getPixelDistance);

            field = new Field(car.getPixelDistance);

            nextables.Add(new CheckKey(Keyboard.Key.O, Start));
            nextables.Add(new CheckKey(Keyboard.Key.P, Stop));

            drawables.Add(field);
            nextables.Add(field);
            drawables.Add(car);
            nextables.Add(car);
            drawables.Add(botCar);
            nextables.Add(botCar);
            drawables.Add(botCar2);
            nextables.Add(botCar2);
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
