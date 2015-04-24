using JOL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Classes.ItemClasses
{
    class SingleItemContainer : IItemContainer
    {
        IItem item;
        bool takenFrom = false;

        public SingleItemContainer(IItem item){
            this.item = item;
        }

        public IItem ProduceItem(bool isSmallMario)
        {
            if (!takenFrom)
            {
                takenFrom = true;
                return item;
            }
            else
            {
                return null;
            }
        }

        public bool IsEmpty()
        {
            return takenFrom;
        }
    }
}
