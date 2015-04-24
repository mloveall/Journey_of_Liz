using JOL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Classes.ItemClasses
{
    class VariableItemContainer : IItemContainer
    {
        IItem smallItem, otherItem;
        bool takenFrom = false;

        public VariableItemContainer(IItem smallItem, IItem otherItem){
            this.smallItem = smallItem;
            this.otherItem = otherItem;
        }

        public IItem ProduceItem(bool isSmallMario)
        {
            IItem returnItem = null;
            if (!takenFrom)
            {
                if (isSmallMario)
                {
                    returnItem = smallItem;
                }
                else
                {
                    returnItem = otherItem;
                }
                takenFrom = true;
            }
            return returnItem;
        }

        public bool IsEmpty()
        {
            return takenFrom;
        }
    }
}
