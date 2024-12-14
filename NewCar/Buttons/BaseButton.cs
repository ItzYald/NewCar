using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar.Buttons
{
    internal abstract class BaseButton : Drawable, Nextable
    {
        protected Vector2f position;
        protected Vector2f size;
        public Text? text;

        public Action? onClick = null;
        public Action? onAim = null;

        private bool isClicked;

        protected BaseButton(Vector2f position, Vector2f size, string message="")
        {
            this.position = position;
            this.size = size;
            uint characterSize = 25;
            text = new Text(message, Constants.font, characterSize);
            text.FillColor = Color.Black;
            text.Position = new Vector2f(position.X + size.X / 2, position.Y + size.Y / 2 - text.GetLocalBounds().Height / 2);
            text.Origin = new Vector2f(text.GetLocalBounds().Width / 2, text.GetLocalBounds().Height / 2);
        }

        private bool CheckAim()
        {
            Vector2f mousePosition = (Vector2f)GameCycle.GetMousePosition();

            if (mousePosition.X > position.X && mousePosition.X < position.X + size.X &&
                mousePosition.Y > position.Y && mousePosition.Y < position.Y + size.Y)
            {
                return true;
            }
            return false;
        }

        private void CheckClick()
        {
            if (onClick == null) return;
            if (!CheckAim()) return;

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
                isClicked = true;
            else
            {
                if (isClicked)
                {
                    onClick();
                    isClicked = false;
                }
            }
        }

        public void Next()
        {
            CheckClick();
            if (onAim == null) return;
            if (CheckAim())
                onAim();

        }

        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}
