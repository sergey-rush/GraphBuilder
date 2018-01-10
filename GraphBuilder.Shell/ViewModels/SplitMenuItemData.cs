﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphBuilder.Shell.ViewModels
{
    public class SplitMenuItemData : MenuItemData
    {
        public SplitMenuItemData()
            : this(false)
        {
        }

        public SplitMenuItemData(bool isApplicationMenu)
            : base(isApplicationMenu)
        {
        }
    }
}
