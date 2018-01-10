﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphBuilder.Shell.ViewModels
{
    public class ApplicationMenuItemData : MenuItemData
    {
        public ApplicationMenuItemData()
            : this(false)
        {
        }

        public ApplicationMenuItemData(bool isApplicationMenu)
            : base(isApplicationMenu)
        {
        }
    }
}
