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
        List<Item> shopInv;
        Dictionary<string, int> itemCosts;

        //properties
        public List<Item> ShopInv
        {
            get { return shopInv; }
            set { shopInv = value; }
        }

        public Dictionary<string, int> ItemCosts
        {
            get { return itemCosts; }
            set { itemCosts = value; }
        }

        //constructor
        public ShopManager()
        {
            shopInv = new List<Item>();
            itemCosts = new Dictionary<string, int>();
        }

        public void AddToShop(Item item, int cost)
        {
            shopInv.Add(item);
            itemCosts.Add(item.Name, cost);
        }


        public string BuyItem(Player player, Item item)
        {
            if(player.Currency >= itemCosts[item.Name])
            {
                player.Currency -= itemCosts[item.Name];
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
