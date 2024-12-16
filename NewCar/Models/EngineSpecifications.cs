using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar.Models
{
    internal struct EngineSpecifications
    {
        public int maxRpm;
        public int power;

        public EngineSpecifications(int power, int maxRpm)
        {
            this.maxRpm = maxRpm;
            this.power = power;
        }
    }
}
