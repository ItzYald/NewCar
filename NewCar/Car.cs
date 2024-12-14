using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Drawing;
using System.Runtime.CompilerServices;

class Engine
{
    public int rpm;
    public int maxRpm;
    public bool isStart;
    public int power;

    public Engine(int maxRpm, int power)
    {
        this.maxRpm = maxRpm;
        this.power = power;
        rpm = 1000;
        isStart = true;
    }
}

namespace NewCar
{
    internal class Car : Drawable, Nextable
    {
        Sprite sprite;
        Vector2f size;

        List<Nextable> nextables;
        List<Drawable> drawables;

        Speedometer speedometer;

        Engine engine;

        int distance;
        float speed;

        float[] numberOfTransmissions;

        int transmissionNumber;

        const float powerK = 400000000f;

        public Car(string fileName)
        {
            nextables = new List<Nextable>();
            drawables = new List<Drawable>();
            sprite = new Sprite(new Texture(fileName));

            size = new Vector2f(200, 100);

            sprite.Scale = new Vector2f(size.X / sprite.TextureRect.Size.X, size.Y / sprite.TextureRect.Size.Y);
            sprite.Position = new Vector2f(120, 220);
            drawables.Add(sprite);

            distance = 0;
            speed = 0;

            engine = new Engine(7000, 300);

            speedometer = new Speedometer(getSpeed, getRpm, getTransmissionNumber);

            drawables.Add(speedometer);
            nextables.Add(speedometer);

            //numberOfTransmissions = [1f, 1.9f, 2.6f, 3.5f, 4.6f];
            numberOfTransmissions = [3.8f, 2.2f, 1.3f, 0.9f, 0.5f];
            transmissionNumber = 0;

            nextables.Add(new CheckKey(Keyboard.Key.K, TransmissionDown));
            nextables.Add(new CheckKey(Keyboard.Key.L, TransmissionUp));
        }

        public int getDistance() => distance;
        public int getPixelDistance() => (int)(distance / 60f * 23f);
        public int getSpeed() => (int)speed;
        public int getRpm() => engine.rpm;

        public int getTransmissionNumber() => transmissionNumber + 1;

        void TransmissionUp()
        {
            if (transmissionNumber + 1 < numberOfTransmissions.Count())
            {
                transmissionNumber += 1;
                engine.rpm = (int)(engine.rpm * (numberOfTransmissions[transmissionNumber] / numberOfTransmissions[transmissionNumber - 1]));
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
            engine.isStart = true;
            engine.rpm = (int)(300 * numberOfTransmissions[transmissionNumber]);
        }

        void Accel()
        {
            if (engine.rpm > engine.maxRpm * 1.05f)
            {
                engine.rpm -= (int)(300 * numberOfTransmissions[transmissionNumber] * Math.Pow(engine.rpm - engine.maxRpm, 0.89) / (engine.maxRpm * 1.05f - engine.maxRpm));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                engine.rpm += (int)((-1 * engine.rpm * engine.rpm + 2 * engine.maxRpm * engine.rpm) / powerK * engine.power * numberOfTransmissions[transmissionNumber]);
            }

            engine.rpm += (int)(0.7f * numberOfTransmissions[transmissionNumber]);
        }

        void Move()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && speed > 0)
            {
                engine.rpm -= (int)(60 * numberOfTransmissions[transmissionNumber]);
            }
            if (engine.isStart)
                Accel();

            engine.rpm -= (int)(speed * Constants.airResistance * numberOfTransmissions[transmissionNumber]);


            if (engine.rpm < 0)
            {
                engine.rpm = 0;
            }

            speed = engine.rpm / 60f / numberOfTransmissions[transmissionNumber];

            distance += (int)speed;
        }

        public void Next()
        {
            foreach (Nextable nextable in nextables)
            {
                nextable.Next();
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.I) && !engine.isStart)
            {
                StartEngine();
            }
            if (engine.rpm < 500)
            {
                engine.isStart = false;
                engine.rpm -= 10;
            }
            Move();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Drawable drawable in drawables)
            {
                drawable.Draw(target, states);
            }
        }

    }
}
