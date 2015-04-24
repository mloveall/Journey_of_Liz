using JOL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Classes.ItemClasses
{
    class MultiItemContainer : IItemContainer
    {
        Stack<IItem> items = new Stack<IItem>();

        public MultiItemContainer(List<IItem> items){
            foreach (IItem item in items)
            {
                
                    this.items.Push(item);
                
            }
        }

        public IItem ProduceItem(bool isSmallMario)
        {
            if (items.Count > 0)
            {
                return items.Pop();
            }
            else
            {
                return null;
            }   
        }

        public bool IsEmpty()
        {
            bool isEmpty = true;

            if (items.Count() > 0)
            {
                isEmpty = false;
            }

            return isEmpty;
        }
    }
}
