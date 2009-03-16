using MoMoney.Presentation.Model.interaction;

namespace MoMoney.Presentation.Views.Startup
{
    public interface ISplashScreenView
    {
        void increment_the_opacity();
        double current_opacity();
        void decrement_the_opacity();
        void close_the_screen();
        void display();
        void update_progress(notification_message message);
    }
}