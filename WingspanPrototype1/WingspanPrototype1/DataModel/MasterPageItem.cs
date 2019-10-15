using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1
{
    class MasterPageItem
    {
        public string Title { get; set; } // Menu list item name

        public Type TargetType { get; set; } // Type of page selected (page object name)
    }
}
