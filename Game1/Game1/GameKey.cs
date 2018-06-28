using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

enum KeyType
{
    test,
}

namespace Game1
{
    class GameKey : Item  //funky title to avoid conflicts with the built-in key enum 
    {
        KeyType key;

        public GameKey(KeyType key, string name, Rectangle position, Texture2D texture) : base(name, position, texture)
        {
            this.key = key;
        }

        //methods
        public void KeyAction()
        {

        }
    }
}
