using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class ShopManager
    {
        //fields
        int cost;
        List<Item> shopInv;


        //properties
        public List<Item> ShopInv
        {
            get { return shopInv; }
            set { shopInv = value; }
        }

        //constructor
        public ShopManager(List<Item> shopInv)
        {
            this.shopInv = shopInv;
        }


        public string BuyItem(Player player, Item item)
        {
            if(player.Currency <= cost)
            {
                player.Currency -= cost;
                player.Inventory.Add(item.Name, item);
                player.InvList.Add(item.Name);
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
