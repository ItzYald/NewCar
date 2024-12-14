using SFML.Graphics;
using SFML.System;
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
        protected Text? text;

        private Action? onClick;
        private Action? onAim;

        private Vector2f mousePosition;

        private bool isClicked;

        protected BaseButton(Action? onClick = null, Text? text = null, Action? onAim = null)
        {
            this.onClick = onClick;
            this.onAim = onAim;
            this.text = text;
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

            if (CheckAim())
            {
                isClicked = true;
            }
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
        
        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(text, states);
        }
    }
}
