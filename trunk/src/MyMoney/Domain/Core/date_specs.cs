using jpboodhoo.bdd.contexts;
using MyMoney.Testing.MetaData;
using MyMoney.Testing.spechelpers.contexts;
using MyMoney.Testing.spechelpers.core;

namespace MyMoney.Domain.Core
{
    [Concern(typeof (date))]
    public class when_two_dates_that_represent_the_same_day_are_asked_if_they_are_equal : concerns_for<IDate>
    {
        it should_return_true = () => result.should_be_equal_to(true);

        public override IDate create_sut()
        {
            return new date(2008, 09, 25);
        }

        because b = () => { result = sut.Equals(new date(2008, 09, 25)); };

        static bool result;
    }

    [Concern(typeof (date))]
    public class when_an_older_date_is_compared_to_a_younger_date : concerns_for<IDate>
    {
        it should_return_a_positive_number = () => assertion_extensions.should_be_greater_than(result, 0);

        public override IDate create_sut()
        {
            return new date(2008, 09, 25);
        }

        because b = () => { result = sut.CompareTo(new date(2007, 01, 01)); };

        static int result;
    }
}