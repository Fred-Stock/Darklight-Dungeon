using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


//item type enums
//can be added to if more items are added
//there are seperate enums for each different type of item
enum Weapons
{

}

enum Armor
{

}

enum GameKeys //funky title to avoid conflicts with the built-in key enum 
{

}


namespace Game1
{
    class Item : GameObject 
    {
        //fields
        private Texture2D texture;
        private Rectangle position;

        //properties
        public Texture2D Texture
        {
            get { return texture; }
        }

        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }


        //constructor 
        public Item(Rectangle position, Texture2D texture) : base(position, texture)
        {

        }

        //methods


        //different methods for each different type of item
        public void WeaponAction(Weapons item)
        {
            
        }

        public void ArmorAction(Armor item)
        {

        }

        public void KeyAction(GameKeys item)
        {

        }
    }
}
