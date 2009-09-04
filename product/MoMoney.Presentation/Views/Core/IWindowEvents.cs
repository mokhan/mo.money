using System;
using System.ComponentModel;
using Gorilla.Commons.Windows.Forms;

namespace MoMoney.Presentation.Views.Core
{
    public interface IWindowEvents
    {
        ControlAction<EventArgs> activated { get; set; }
        ControlAction<EventArgs> deactivated { get; set; }
        ControlAction<EventArgs> closed { get; set; }
        ControlAction<CancelEventArgs> closing { get; set; }
    }
}