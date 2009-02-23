using System;
using System.Timers;
using jpboodhoo.bdd.contexts;
using MyMoney.Testing.Extensions;
using MyMoney.Testing.MetaData;
using MyMoney.Testing.spechelpers.contexts;

namespace MyMoney.Infrastructure.Threading
{
    [Concern(typeof (timer_factory))]
    public abstract class behaves_like_a_timer_factory : concerns_for<ITimerFactory, timer_factory>
    {
        public override ITimerFactory create_sut()
        {
            return new timer_factory();
        }
    }

    public class when_creating_a_timer : behaves_like_a_timer_factory
    {
        it should_return_a_timer_with_the_correct_interval = () => result.Interval.should_be_equal_to(1000);

        because b = () => { result = sut.create_for(new TimeSpan(0, 0, 1)); };

        static Timer result;
    }

    public class when_creating_a_timer_with_an_interval_in_milliseconds : behaves_like_a_timer_factory
    {
        it should_return_a_timer_with_the_correct_polling_interval =
            () => result.Interval.should_be_equal_to(milliseconds);

        because b = () =>
                        {
                            var timer_interval = new TimeSpan(50);
                            milliseconds = 50;
                            result = sut.create_for(timer_interval);
                        };

        static Timer result;
        static double milliseconds;
    }
}