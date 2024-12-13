﻿using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace NewCar
{
    internal class Frame : Drawable, Nextable
    {
        List<Drawable> drawables;
        List<Nextable> nextables;

        Car car;
        Field field;
        Speedometer speedometer;

        public Frame()
        {
            drawables = new List<Drawable>();
            nextables = new List<Nextable>();

            car = new Car("Images/Cars/Car1.png");

            field = new Field(car.getPixelDistance);

            speedometer = new Speedometer(car.getSpeed, car.getRpm, car.getTransmissionNumber);

            drawables.Add(field);
            nextables.Add(field);
            drawables.Add(car);
            nextables.Add(car);
            drawables.Add(speedometer);
            nextables.Add(speedometer);
        }

        public void Next()
        {
            foreach (Nextable nextable in nextables)
            {
                nextable.Next();
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Drawable drawable in drawables)
            {
                target.Draw(drawable, states);
            }
        }

    }
}
