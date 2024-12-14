using SFML.Graphics;
using SFML.Window;
using SFML.System;
using NewCar.Buttons;
using NewCar.Screens.Gameplay;

class Engine
{
    public int rpm;
    public int maxRpm;
    public bool isStart;
    public int power;

    public Engine(int power, int maxRpm)
    {
        this.maxRpm = maxRpm;
        this.power = power;
        rpm = 1000;
        isStart = true;
    }
}

namespace NewCar.Models
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

        float[] numbersOfTransmission;

        int transmissionNumber;

        const float powerK = 400000000f;

        public Car(string fileName, int power, int maxRpm)
        {
            distance = 0;
            speed = 0;

            nextables = new List<Nextable>();
            drawables = new List<Drawable>();
            size = new Vector2f(200, 100);

            sprite = new Sprite(new Texture(fileName));
            sprite.Scale = new Vector2f(size.X / sprite.TextureRect.Size.X, size.Y / sprite.TextureRect.Size.Y);
            sprite.Position = new Vector2f(120, 220);
            drawables.Add(sprite);

            engine = new Engine(power, maxRpm);

            speedometer = new Speedometer(getSpeed, getRpm, getTransmissionNumber);
            drawables.Add(speedometer);
            nextables.Add(speedometer);

            numbersOfTransmission = [3.8f, 2.2f, 1.3f, 0.9f, 0.5f];
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
            if (transmissionNumber + 1 < numbersOfTransmission.Count())
            {
                transmissionNumber += 1;
                engine.rpm = (int)(engine.rpm * (numbersOfTransmission[transmissionNumber] / numbersOfTransmission[transmissionNumber - 1]));
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
            engine.rpm = (int)(300 * numbersOfTransmission[transmissionNumber]);
        }

        void Accel()
        {
            if (engine.rpm > engine.maxRpm * 1.05f)
            {
                engine.rpm -= (int)(300 * numbersOfTransmission[transmissionNumber] * Math.Pow(engine.rpm - engine.maxRpm, 0.89) / (engine.maxRpm * 1.05f - engine.maxRpm));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                engine.rpm += (int)((-1 * engine.rpm * engine.rpm + 2 * engine.maxRpm * engine.rpm) / powerK * engine.power * numbersOfTransmission[transmissionNumber]);
            }

            engine.rpm += (int)(0.7f * numbersOfTransmission[transmissionNumber]);
        }

        void Move()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && speed > 0)
            {
                engine.rpm -= (int)(60 * numbersOfTransmission[transmissionNumber]);
            }
            if (engine.isStart)
                Accel();

            engine.rpm -= (int)(speed * Constants.airResistance * numbersOfTransmission[transmissionNumber]);


            if (engine.rpm < 0)
            {
                engine.rpm = 0;
            }

            speed = engine.rpm / 60f / numbersOfTransmission[transmissionNumber];

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
