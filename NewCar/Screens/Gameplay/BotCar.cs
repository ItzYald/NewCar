using NewCar.Models;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar.Screens.Gameplay
{
    internal class BotCar : BaseDrawCar
    {
        Func<int> getMainCarPixelDistance;

        public BotCar(string fileName, CarSpecifications carSpecifications, Func<int> getMainCarPixelDistance) :
            base(fileName, carSpecifications)
        {
            this.getMainCarPixelDistance = getMainCarPixelDistance;

            sprite.Position = new Vector2f(120, 270);
        }

        public override void Next()
        {
            if (!car.IsStarted) return;
            car.Accel();
            if (car.getRpm() > car.MaxRpm * 0.91)
            {
                car.TransmissionUp();
            }

            foreach (Nextable nextables in nextables)
            {
                nextables.Next();
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            sprite.Position = new Vector2f(getPixelDistance() - getMainCarPixelDistance() + 120, sprite.Position.Y);
            target.Draw(sprite, states);
        }

    }
}
