using SFML.Graphics;
using SFML.Window;
using SFML.System;
using NewCar.Buttons;
using static SFML.Window.Mouse;
using NewCar.Screens.Gameplay;
using NewCar.Models;
using NewCar.Screens;

namespace NewCar
{
    internal class Frame : Drawable, Nextable
    {
        Screen screen;

        public Frame()
        {
            screen = new Menu(SetScreen);
        }

        private void SetScreen(ScreensEnum newScreen)
        {
            switch (newScreen)
            {
                case ScreensEnum.gameplay:
                    screen = new GameplayScreen(SetScreen);
                    break;
                case ScreensEnum.menu:
                    screen = new Menu(SetScreen);
                    break;
            }
        }

        public void Next()
        {
            screen.Next();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(screen, states);
        }

    }
}
