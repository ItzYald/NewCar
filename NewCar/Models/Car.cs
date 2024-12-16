using NewCar.Screens.Gameplay;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace NewCar.Models
{
    internal class Car : Nextable
    {
        List<Nextable> nextables;

        Engine engine;

        int distance;
        float speed;

        float[] numbersOfTransmission;

        int transmissionNumber;

        bool isStarted;

        public bool IsStarted { get { return isStarted; } }

        public int MaxRpm { get { return engine.MaxRpm; } }

        const float powerK = 400000000f;

        public Car(int power, int maxRpm)
        {
            nextables = new List<Nextable>();

            distance = 0;
            speed = 0;

            engine = new Engine(new EngineSpecifications(power, maxRpm), getTransmission);

            numbersOfTransmission = [3.8f, 2.2f, 1.3f, 0.9f, 0.5f];
            transmissionNumber = 0;

            isStarted = false;
        }

        public int getDistance() => distance;
        public int getSpeed() => (int)speed;
        public int getRpm() => engine.rpm;
        public int getTransmissionNumber() => transmissionNumber + 1;
        public float getTransmission() => numbersOfTransmission[transmissionNumber];

        public void Start()
        {
            speed = 0;
            transmissionNumber = 0;
            distance = 0;
            engine.Start();
            isStarted = true;
        }

        public void Stop()
        {
            distance = 0;
            speed = 0;
            transmissionNumber = 0;
            isStarted = false;
            engine.Stop();
        }

        public void TransmissionUp()
        {
            if (transmissionNumber + 1 < numbersOfTransmission.Count())
            {
                transmissionNumber += 1;
                engine.rpm = (int)(engine.rpm * (numbersOfTransmission[transmissionNumber] / numbersOfTransmission[transmissionNumber - 1]));
            }
        }

        public void TransmissionDown()
        {
            if (transmissionNumber > 0)
            {
                transmissionNumber -= 1;
            }
        }

        public void Break()
        {
            if (speed > 0)
            {
                engine.rpm -= (int)(60 * numbersOfTransmission[transmissionNumber]);
            }
        }
        public void Accel()
        {
            engine.rpm +=
                (int)((-1 * engine.rpm * engine.rpm + 2 * engine.MaxRpm * engine.rpm) / powerK * engine.Power * numbersOfTransmission[transmissionNumber]);
        }

        public void Move()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.I) && !engine.isStart)
            {
                engine.Start();
            }
            if (engine.rpm < 500)
            {
                engine.isStart = false;
                engine.rpm -= 10;
            }

            if (engine.isStart)
            {
                if (engine.rpm > engine.MaxRpm * 1.05f)
                {
                    engine.rpm -= (int)(300 * numbersOfTransmission[transmissionNumber] * Math.Pow(engine.rpm - engine.MaxRpm, 0.89) / (engine.MaxRpm * 1.05f - engine.MaxRpm));
                }
                engine.rpm += (int)(0.5f * numbersOfTransmission[transmissionNumber]);
            }

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
            if (!isStarted) return;
            Move();
            foreach (Nextable nextable in nextables)
            {
                nextable.Next();
            }
        }

    }
}
