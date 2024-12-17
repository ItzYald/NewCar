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
        PlayerCar playerCar;
        BotCar botCar;
        Field field;

        bool isGameplay;

        WinMenu menu;

        public GameplayScreen(Action<ScreensEnum> setNextScreen) : 
            base(setNextScreen)
        {
            playerCar = new PlayerCar(Constants.car1FileName, MainModel.playerCarSpecifications);
            botCar = new BotCar(Constants.car1FileName, MainModel.botCarSpecifications, playerCar.getPixelDistance);

            isGameplay = true;

            menu = new WinMenu(Restart, () => { });

            field = new Field(playerCar.getPixelDistance);

            Restart();
        }

        public override void Next()
        {
            base.Next();
            if (isGameplay)
            {
                if (playerCar.getRealDistance() > 200)
                {
                    isGameplay = false;
                    nextables.Clear();

                    drawables.Add(menu);
                }
            }
            else
            {
                menu.Next();
            }
            
        }

        private void Restart()
        {
            drawables.Clear();
            nextables.Clear();

            nextables.Add(new CheckKey(Keyboard.Key.O, Start));
            nextables.Add(new CheckKey(Keyboard.Key.P, Stop));

            drawables.Add(field);
            nextables.Add(field);
            drawables.Add(playerCar);
            nextables.Add(playerCar);
            drawables.Add(botCar);
            nextables.Add(botCar);
        }

        private void Start()
        {
            playerCar.Start();
            botCar.Start();
        }

        private void Stop()
        {
            playerCar.Stop();
            botCar.Stop();
        }

    }
}
