using NewCar.Models;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar.Screens.Gameplay
{
    internal abstract class BaseDrawCar : Drawable, Nextable
    {
        protected Car car;
        protected Sprite sprite;
        protected Vector2f size;

        protected List<Nextable> nextables;
        protected List<Drawable> drawables;

        protected BaseDrawCar(string fileName, CarSpecifications carSpecifications)
        {
            nextables = new List<Nextable>();
            drawables = new List<Drawable>();
            size = new Vector2f(300, 150);

            sprite = new Sprite(new Texture(fileName));
            sprite.Scale = new Vector2f(size.X / sprite.TextureRect.Size.X, size.Y / sprite.TextureRect.Size.Y);
            drawables.Add(sprite);

            car = new Car(carSpecifications);
            nextables.Add(car);
        }

        public int getPixelDistance() => (int)(car.getDistance() / 60f * 23f);
        public int getRealDistance() => (int)(car.getDistance() * 0.0046296f);
        public void Start() { car.Start(); }
        public void Stop() { car.Stop(); }

        public abstract void Next();

        public abstract void Draw(RenderTarget target, RenderStates states);

    }
}
