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

        private Vector2f mousePosition;

        private bool isClicked;

        protected BaseButton(Vector2f position, Vector2f size)
        {
            this.position = position;
            this.size = size;
            text = new Text("", Constants.font, 25);
        }

        private bool CheckAim()
        {
            mousePosition = (Vector2f)GameCycle.GetMousePosition();

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
