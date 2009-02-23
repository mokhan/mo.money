using System;

namespace MyMoney.Domain.Core
{
    public class Clock
    {
        private static Func<DateTime> time_provider;

        static Clock()
        {
            reset();
        }

        public static date today()
        {
            return time_provider();
        }

#if DEBUG
        public static void change_time_provider_to(Func<DateTime> new_time_provider)
        {
            if (new_time_provider != null) time_provider = new_time_provider;
        }
#endif

        public static void reset()
        {
            time_provider = () => DateTime.Now;
        }
    }
}