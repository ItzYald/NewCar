using NewCar.Buttons;

using SFML.System;
using SFML.Graphics;

namespace NewCar.Screens
{
    internal class Menu : Screen
    {
        List<BaseButton> buttons;

        public Menu(Action<ScreensEnum> setNextScreen) : base(setNextScreen)
        {
            buttons = new List<BaseButton>();

            buttons.Add(new RectangleButton(
                new Vector2f(Constants.windowWidth / 2 - 60, 250),
                new Vector2f(120, 60), new Color(230, 230, 30),
                message:"Играть"
                )
            {
                onClick = () => { this.setNextScreen(ScreensEnum.gameplay); }
            });

            foreach (BaseButton button in buttons)
            {
                drawables.Add(button);
                nextables.Add(button);
            }
        }
    }
}
