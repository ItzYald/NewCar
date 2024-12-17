using SFML.Graphics;
using SFML.Window;
using SFML.System;
using NewCar.Buttons;
using NewCar.Models;



namespace NewCar.Screens.Gameplay
{
    internal class PlayerCar : BaseDrawCar
    {
        Speedometer speedometer;

        public PlayerCar(string fileName, CarSpecifications carSpecifications) :
            base(fileName, carSpecifications)
        {
            sprite.Position = new Vector2f(120, 130);

            speedometer = new Speedometer(car.getSpeed, car.getRpm, car.getTransmissionNumber, getRealDistance);
            drawables.Add(speedometer);
            nextables.Add(speedometer);

            nextables.Add(new CheckKey(Keyboard.Key.K, car.TransmissionDown));
            nextables.Add(new CheckKey(Keyboard.Key.L, car.TransmissionUp));
        }

        public override void Next()
        {
            foreach (Nextable nextable in nextables)
            {
                nextable.Next();
            }
            if (!car.IsStarted) return;
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                car.Accel();
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                car.Brack();
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Drawable drawable in drawables)
            {
                drawable.Draw(target, states);
            }
        }

    }
}
