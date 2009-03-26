using System;
using MoMoney.Presentation.Presenters.Startup;
using MoMoney.Utility.Core;

namespace MoMoney.Presentation.Presenters.Commands
{
    public class display_the_splash_screen : IDisposableCommand
    {
        readonly Func<ISplashScreenPresenter> presenter;

        public display_the_splash_screen(Func<ISplashScreenPresenter> presenter)
        {
            this.presenter = presenter;
        }

        public void run()
        {
            presenter().run();
        }

        public void Dispose()
        {
            presenter().run("Complete");
            presenter().Dispose();
        }
    }
}