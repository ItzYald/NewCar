using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar.Models
{
    internal struct CarSpecifications
    {
        public EngineSpecifications engineSpecifications;
        int brackPower;
        
        public int BrackPower { get { return brackPower; } }

        public CarSpecifications(EngineSpecifications engineSpecifications, int brackPower)
        {
            this.engineSpecifications = engineSpecifications;
            this.brackPower = brackPower;
        }

    }
}
