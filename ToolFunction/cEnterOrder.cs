using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ToolFunction
{
    public partial class cEnterOrder : Component
    {
        public cEnterOrder()
        {
            InitializeComponent();
        }

        public cEnterOrder(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
