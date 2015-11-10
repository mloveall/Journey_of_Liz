using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Interfaces
{
    /// <summary>
    /// An abstract base class for various item containers.
    /// </summary>
    
    public interface IItemContainer
    {
        IItem ProduceItem(bool isSmallMario); // returns null if no items left
        bool IsEmpty();
    }
}
