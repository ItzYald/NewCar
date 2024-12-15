﻿using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar.Screens.Gameplay
{
    /// <summary>
    /// Speed and rpm display
    /// </summary>
    internal class Speedometer : Drawable, Nextable
    {
        Func<int> getSpeed;
        Func<int> getRpm;
        Func<int> getTransmissionNumber;

        Text speedText;
        Text rpmText;
        Text transmissionText;

        RectangleShape speedRect;
        RectangleShape rpmRect;

        List<Drawable> drawables;

        public Speedometer(Func<int> getSpeed, Func<int> getRpm, Func<int> getTransmissionNumber)
        {
            drawables = new List<Drawable>();

            Vector2f position = new Vector2f(0, 570);

            speedText = new Text(getSpeed().ToString(), Constants.font, 25);
            speedText.Position = new Vector2f(100, position.Y + 50);
            speedText.FillColor = new Color(250, 250, 10);
            rpmText = new Text(getRpm().ToString(), Constants.font, 25);
            rpmText.Position = new Vector2f(500, position.Y + 50);
            rpmText.FillColor = new Color(250, 250, 10);
            transmissionText = new Text(getTransmissionNumber().ToString(), Constants.font, 25);
            transmissionText.Position = new Vector2f(600, position.Y + 50);
            transmissionText.FillColor = new Color(250, 250, 10);

            speedRect = new RectangleShape(new Vector2f(10, 100));
            speedRect.FillColor = new Color(250, 250, 10);
            speedRect.Position = new Vector2f(120, position.Y);

            rpmRect = new RectangleShape(new Vector2f(10, 100));
            rpmRect.FillColor = new Color(250, 250, 10);
            rpmRect.Position = new Vector2f(520, position.Y);


            drawables.Add(speedText);
            drawables.Add(rpmText);
            drawables.Add(speedRect);
            drawables.Add(rpmRect);
            drawables.Add(transmissionText);

            this.getSpeed = getSpeed;
            this.getRpm = getRpm;
            this.getTransmissionNumber = getTransmissionNumber;
        }

        public void Next()
        {
            speedText.DisplayedString = getSpeed().ToString();
            rpmText.DisplayedString = getRpm().ToString();
            transmissionText.DisplayedString = getTransmissionNumber().ToString();
            speedRect.Rotation = getSpeed() / 1.2f + 80;
            rpmRect.Rotation = getRpm() / 40f + 80;
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
