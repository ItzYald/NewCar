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

        const float powerK = 400000000f;

        public Car(int power, int maxRpm)
        {
            nextables = new List<Nextable>();

            distance = 0;
            speed = 0;

            engine = new Engine(power, maxRpm);

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

            engine.rpm += (int)(0.5f * numbersOfTransmission[transmissionNumber]);
        }

        public void Move()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.I) && !engine.isStart)
            {
                StartEngine();
            }
            if (engine.rpm < 500)
            {
                engine.isStart = false;
                engine.rpm -= 10;
            }

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
            Move();
            foreach (Nextable nextable in nextables)
            {
                nextable.Next();
            }
        }

    }
}
