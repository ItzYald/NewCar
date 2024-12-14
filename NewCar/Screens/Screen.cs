using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar.Screens
{
    enum ScreensEnum
    {
        gameplay,
        menu
    }

    internal abstract class Screen : Drawable, Nextable
    {
        protected Action<ScreensEnum> setNextScreen;

        protected List<Drawable> drawables;
        protected List<Nextable> nextables;

        public Screen(Action<ScreensEnum> setNextScreen)
        {
            drawables = new List<Drawable>();
            nextables = new List<Nextable>();
            this.setNextScreen = setNextScreen;
        }

        public virtual void Next()
        {
            foreach (Nextable nextable in nextables)
            {
                nextable.Next();
            }
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Drawable drawable in drawables)
            {
                target.Draw(drawable, states);
            }
        }

    }
}
