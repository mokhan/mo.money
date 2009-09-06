using System;
using System.ComponentModel;
using MoMoney.Presentation.Views.Core;

namespace MoMoney.Presentation.Views
{
    public interface IWindowEvents
    {
        ControlAction<EventArgs> activated { get; set; }
        ControlAction<EventArgs> deactivated { get; set; }
        ControlAction<EventArgs> closed { get; set; }
        ControlAction<CancelEventArgs> closing { get; set; }
    }
}