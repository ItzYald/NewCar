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
        List<Drawable> drawables;
        List<Nextable> nextables;
        Screen screen;

        public Frame()
        {
            drawables = new List<Drawable>();
            nextables = new List<Nextable>();

            screen = new GameplayScreen(SetScreen);
            drawables.Add(screen);
            nextables.Add(screen);

        }

        private void SetScreen(ScreensEnum newScreen)
        {
            switch (newScreen)
            {
                case ScreensEnum.gameplay:
                    screen = new GameplayScreen(SetScreen); break;
            }
        }

        public void Next()
        {
            foreach (Nextable nextable in nextables)
            {
                nextable.Next();
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Drawable drawable in drawables)
            {
                target.Draw(drawable, states);
            }
        }

    }
}
