using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Drawing;
using System.Runtime.CompilerServices;


namespace NewCar
{
    internal class Car : Drawable, Nextable
    {
        Sprite sprite;
        Vector2f size;

        List<Nextable> nextables;

        int distance;
        float speed;
        int rpm;
        int maxRpm;

        bool isStartEngine;

        float[] numberOfTransmissions;

        int transmissionNumber;

        public Car(string fileName)
        {
            maxRpm = 7000;
            nextables = new List<Nextable>();
            sprite = new Sprite(new Texture(fileName));

            size = new Vector2f(200, 100);

            sprite.Scale = new Vector2f(size.X / sprite.TextureRect.Size.X, size.Y / sprite.TextureRect.Size.Y);
            sprite.Position = new Vector2f(120, 220);

            distance = 0;
            speed = 0;
            rpm = 1000;
            isStartEngine = true;

            numberOfTransmissions = new float[] { 1f, 1.9f, 2.6f, 3.5f };
            transmissionNumber = 0;

            nextables.Add(new CheckKey(Keyboard.Key.K, TransmissionDown));
            nextables.Add(new CheckKey(Keyboard.Key.L, TransmissionUp));
        }

        public int getDistance() => distance;
        public int getPixelDistance() => (int)(distance / 60f * 23f);
        public int getSpeed() => (int)speed;
        public int getRpm() => rpm;

        public int getTransmissionNumber() => transmissionNumber + 1;

        void TransmissionUp()
        {
            if (transmissionNumber + 1 < numberOfTransmissions.Count())
            {
                transmissionNumber += 1;
                rpm = (int)(rpm / (numberOfTransmissions[transmissionNumber] / numberOfTransmissions[transmissionNumber - 1]));
            }
        }

        void TransmissionDown()
        {
            if (transmissionNumber > 0)
            {
                transmissionNumber -= 1;
            }
        }

        void StartEngine()
        {
            isStartEngine = true;
            rpm = (int)(900 / numberOfTransmissions[transmissionNumber]);
        }

        void Accel()
        {
            if (rpm > maxRpm * 1.05f)
            {
                rpm -= 200;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                //rpm += (int)(120 / Math.Pow(numberOfTransmissions[transmissionNumber], 1.4));
                rpm += (int)((-1 * rpm * rpm + 14000 * rpm + 10000) / 190000f / numberOfTransmissions[transmissionNumber]);
            }

            //rpm += (int)((-1 * rpm * rpm + 14000 * rpm + 50000000) / 300000f);
            rpm += (int)(12 / numberOfTransmissions[transmissionNumber]);
            
        }

        void Move()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && speed > 0)
            {
                rpm -= 150;
            }
            if (isStartEngine)
                Accel();

            rpm -= (int)(speed * Constants.airResistance / numberOfTransmissions[transmissionNumber]);


            if (rpm < 0)
            {
                rpm = 0;
            }

            speed = rpm / 100f * numberOfTransmissions[transmissionNumber];

            distance += (int)speed;
        }

        public void Next()
        {
            foreach (Nextable nextable in nextables)
            {
                nextable.Next();
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.I) && !isStartEngine)
            {
                StartEngine();
            }
            if (rpm < 500)
            {
                isStartEngine = false;
                rpm -= 10;
            }
            Move();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(sprite, states);
        }

    }
}
