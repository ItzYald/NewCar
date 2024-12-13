using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCar
{
    internal class CheckKey : Nextable
    {
        bool alreadyPressed;
        Keyboard.Key key;
        Action action;

        public CheckKey(Keyboard.Key key, Action action)
        {
            alreadyPressed = false;
            this.key = key;
            this.action = action;
        }


        public void Next()
        {
            if (Keyboard.IsKeyPressed(key))
            {
                alreadyPressed = true;
            }
            else
            {
                if (alreadyPressed)
                {
                    action();
                    alreadyPressed = false;
                }
            }
        }


    }
}
