using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;
using jpboodhoo.bdd.contexts;
using jpboodhoo.bdd.core.extensions;
using MyMoney.Testing.spechelpers.contexts;
using MyMoney.Testing.spechelpers.core;

namespace MyMoney.Testing.win.forms
{
    public class when_invoking_a_call_on_a_target_via_reflection : concerns_for
    {
        it should_correctly_call_that_method = () => control.was_told_to(x => x.OnKeyPress(args));

        context c = () => { control = an<Events.ControlEvents>(); };

        because b = () => EventTrigger.trigger_event<Events.ControlEvents>(x => x.OnKeyPress(args), control);

        static Events.ControlEvents control;
        static readonly KeyPressEventArgs args = new KeyPressEventArgs('A');
    }

    static public class EventTrigger
    {
        const BindingFlags binding_flags = BindingFlags.Instance | BindingFlags.NonPublic;
        static readonly IDictionary<ExpressionType, Func<Expression, object>> expression_handlers;

        static EventTrigger()
        {
            expression_handlers = new Dictionary<ExpressionType, Func<Expression, object>>();
            expression_handlers[ExpressionType.New] = instantiate_value;
            expression_handlers[ExpressionType.MemberAccess] = get_value_from_member_access;
            expression_handlers[ExpressionType.Constant] = get_constant_value;
        }

        static public void trigger_event<Target>(Expression<Action<Target>> expression_representing_event_to_raise,
                                                 object target) where Target : IEventTarget
        {
            var method_call_expression = expression_representing_event_to_raise.Body.downcast_to<MethodCallExpression>();
            var method_args = get_parameters_from(method_call_expression.Arguments);
            var method_name = method_call_expression.Method.Name;
            var method = target.GetType().GetMethod(method_name, binding_flags);

            Debug.Assert(target != null, "The target to raise the event on cannot be null");
            Debug.Assert(method != null,
                         "There is no method called {0}, on a {1}".format_using(method_name,
                                                                                target.GetType().proper_name()));

            method.Invoke(target, method_args.ToArray());
        }

        static object get_constant_value(Expression expression)
        {
            return expression.downcast_to<ConstantExpression>().Value;
        }

        static object get_value_from_member_access(Expression expression)
        {
            throw new NotImplementedException();
        }

        static object instantiate_value(Expression expression)
        {
            var new_expression = expression.downcast_to<NewExpression>();
            var args = new_expression.Arguments.Select(x => get_value_from_evaluating(x));
            return new_expression.Constructor.Invoke(args.ToArray());
        }

        static IEnumerable<object> get_parameters_from(IEnumerable<Expression> parameter_expressions)
        {
            foreach (var expression in parameter_expressions)
            {
                if (can_handle(expression)) yield return get_value_from_evaluating(expression);
                else cannot_handle(expression);
            }
        }

        static void cannot_handle(Expression expression)
        {
            throw new NotImplementedException();
        }

        static object get_value_from_evaluating(Expression expression)
        {
            return expression_handlers[expression.NodeType](expression);
        }

        static bool can_handle(Expression expression)
        {
            return expression_handlers.ContainsKey(expression.NodeType);
        }
    }

    public interface IEventTarget
    {
    }

    public class Events
    {
        public interface ControlEvents : IEventTarget
        {
            void OnEnter(EventArgs args);
            void OnKeyPress(KeyPressEventArgs args);
        }

        public interface FormEvents : ControlEvents
        {
            void OnActivated(EventArgs e);
            void OnDeactivate(EventArgs e);
        }
    }
}