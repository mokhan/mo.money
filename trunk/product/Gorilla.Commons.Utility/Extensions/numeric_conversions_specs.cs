using developwithpassion.bdd.contexts;
using Gorilla.Commons.Testing;
using Gorilla.Commons.Utility.Extensions;

namespace MoMoney.Utility.Extensions
{
    public class when_converting_a_valid_string_to_a_long : concerns
    {
        it should_return_the_correct_result = () => result.should_be_equal_to(88L);

        context c = () => { valid_numeric_string = "88"; };

        because b = () => { result = valid_numeric_string.to_long(); };

        static long result;
        static string valid_numeric_string;
    }

    public class when_converting_a_valid_string_to_an_int : concerns
    {
        it should_return_the_correct_result = () => result.should_be_equal_to(66);

        context c = () => { valid_numeric_string = "66"; };

        because b = () => { result = valid_numeric_string.to_int(); };

        static int result;
        static string valid_numeric_string;
    }
}