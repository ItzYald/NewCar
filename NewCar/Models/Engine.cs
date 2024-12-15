using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar.Models
{
    internal class Engine
    {
        public int rpm;
        public int maxRpm;
        public bool isStart;
        public int power;

        Func<float> getTransmission;

        public Engine(int power, int maxRpm, Func<float> getTransmission)
        {
            this.maxRpm = maxRpm;
            this.power = power;
            rpm = 1000;
            isStart = true;
            this.getTransmission = getTransmission;
        }

        public void Start()
        {
            isStart = true;
            rpm = (int)(300 * getTransmission());
        }

    }
}
