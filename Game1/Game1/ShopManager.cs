using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Game1
{
    class ShopManager
    {
        //fields
        List<Item> shopInv;

        //properties
        public List<Item> ShopInv
        {
            get { return shopInv; }
            set { shopInv = value; }
        }


        //constructor
        public ShopManager()
        {
            shopInv = new List<Item>();
        }

        public void AddToShop(Item item)
        {
            shopInv.Add(item);
        }


        public string BuyItem(Player player, Item item)
        {
            if(player.Currency >= item.Cost)
            {
                player.Currency -= item.Cost;
                player.BuyItem(item);
                shopInv.Remove(item);

                return "Item Purchased";
            }
            else
            {
                return "not enough currency";
            }
        }


    }
}
