using System;
using System.Collections.Generic;
using jpboodhoo.bdd.contexts;
using MyMoney.Domain.accounting.billing;
using MyMoney.Domain.accounting.financial_growth;
using MyMoney.Domain.Core;
using MyMoney.Testing.MetaData;
using MyMoney.Testing.spechelpers.contexts;
using MyMoney.Testing.spechelpers.core;
using mocking_extensions=MyMoney.Testing.spechelpers.core.mocking_extensions;

namespace MyMoney.Domain.accounting
{
    [Concern(typeof (account_holder))]
    public class behaves_like_an_account_holder : concerns_for<IAccountHolder>
    {
        public override IAccountHolder create_sut()
        {
            return new account_holder();
        }
    }

    public class when_a_customer_is_checking_for_any_bills_that_have_not_been_paid : behaves_like_an_account_holder
    {
        it should_return_all_the_unpaid_bills = () =>
                                                    {
                                                        assertion_extensions.should_contain(result, first_unpaid_bill);
                                                        assertion_extensions.should_contain(result, second_unpaid_bill);
                                                    };

        context c = () =>
                        {
                            first_unpaid_bill = an<IBill>();
                            second_unpaid_bill = an<IBill>();
                            paid_bill = an<IBill>();

                            mocking_extensions.it_will_return(mocking_extensions.is_told_to(first_unpaid_bill, x => x.is_paid_for()), false);
                            mocking_extensions.it_will_return(mocking_extensions.is_told_to(second_unpaid_bill, x => x.is_paid_for()), false);
                            mocking_extensions.it_will_return(mocking_extensions.is_told_to(paid_bill, x => x.is_paid_for()), true);
                        };

        because b = () =>
                        {
                            sut.recieve(first_unpaid_bill);
                            sut.recieve(paid_bill);
                            sut.recieve(second_unpaid_bill);
                            result = sut.collect_all_the_unpaid_bills();
                        };

        static IEnumerable<IBill> result;
        static IBill first_unpaid_bill;
        static IBill second_unpaid_bill;
        static IBill paid_bill;
    }

    [Concern(typeof (account_holder))]
    public class when_an_account_holder_is_calculating_their_income_for_a_year : behaves_like_an_account_holder
    {
        context c = () =>
                        {
                            income_for_january_2007 = an<IIncome>();
                            income_for_february_2007 = an<IIncome>();
                            income_for_february_2008 = an<IIncome>();

                            mocking_extensions.it_will_return(mocking_extensions.is_told_to(income_for_january_2007, x => x.date_of_issue), new DateTime(2007, 01, 01).as_a_date());
                            mocking_extensions.it_will_return(mocking_extensions.is_told_to(income_for_january_2007, x => x.amount_tendered), new money(1000, 00));

                            mocking_extensions.it_will_return(mocking_extensions.is_told_to(income_for_february_2007, x => x.date_of_issue), new DateTime(2007, 02, 01).as_a_date());
                            mocking_extensions.it_will_return(mocking_extensions.is_told_to(income_for_february_2007, x => x.amount_tendered), new money(1000, 00));

                            mocking_extensions.it_will_return(mocking_extensions.is_told_to(income_for_february_2008, x => x.date_of_issue), new DateTime(2008, 02, 01).as_a_date());
                            mocking_extensions.it_will_return(mocking_extensions.is_told_to(income_for_february_2008, x => x.amount_tendered), new money(1000, 00));
                        };

        because b = () =>
                        {
                            sut.recieve(income_for_january_2007);
                            sut.recieve(income_for_february_2007);
                            sut.recieve(income_for_february_2008);
                            result = sut.calculate_income_for(2007.as_a_year());
                        };

        it should_return_the_correct_amount = () => result.should_be_equal_to(2000.as_money());

        static IMoney result;
        static IIncome income_for_january_2007;
        static IIncome income_for_february_2007;
        static IIncome income_for_february_2008;
    }
}