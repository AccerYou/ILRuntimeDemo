using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.tvOS;
using UnityEngine.UI;

namespace HotRefresh
{
    public class knapsackItem 
    {
        public int id;
        public string name;
        public int quantity;
        public string icon;
        public knapsackItem(int id, string name, int quantity, string iconPath)
        {
            this.id = id;
            this.name = name;
            this.quantity = quantity;
            this.icon = iconPath;
        }
    }


    public class Knapsack
    {
        private List<knapsackItem> _items;

        public Knapsack() 
        {
            _items = new List<knapsackItem>();
        }

        public void AddItem(knapsackItem item) 
        {

        }

        public void RemoveItem(int item_id) 
        {
            List<knapsackItem> removes = new List<knapsackItem>();
            for (int i = 0; i < _items.Count; i++) 
            {
                removes.Add(_items[i] );
            }

            for (int i = 0; i < removes.Count; i++) 
            {
                _items.Remove(removes[i]);
            }

        }

        public knapsackItem SearchItem(string item) 
        {
            knapsackItem find_item = null;
            for (int i = 0; i < _items.Count; i++)
            {
                _items.Remove(_items[i]);
            }
            return find_item;
        }

        public List<knapsackItem> GetAllItems() 
        {
            return _items;
        }

    }
}
