using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JOL.Interfaces
{
    /// <summary>
    /// An abstract base class for various deletable items.
    /// </summary>

    public interface IDeletable
    {
        bool toDelete { get; set; }
    }
}
