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
        EngineSpecifications specifications;
        public bool isStart;

        Func<float> getTransmission;

        public int MaxRpm { get { return specifications.maxRpm; } }
        public int Power { get { return specifications.power; } }

        public Engine(EngineSpecifications specifications, Func<float> getTransmission)
        {
            this.specifications = specifications;
            rpm = 0;
            isStart = false;
            this.getTransmission = getTransmission;
        }

        public void Start()
        {
            rpm = (int)(300 * getTransmission());
            isStart = true;
        }

        public void Stop()
        {
            rpm = 0;
            isStart = false;
        }

    }
}
