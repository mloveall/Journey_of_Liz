using JOL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Classes.ItemClasses
{
    class EmptyItemContainer : IItemContainer
    {
        public EmptyItemContainer(){
            
        }

        public IItem ProduceItem(bool isSmallMario)
        {
            return null;
        }

        public bool IsEmpty()
        {
            return true;
        }
    }
}
