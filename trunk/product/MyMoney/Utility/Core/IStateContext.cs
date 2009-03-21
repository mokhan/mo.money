namespace MoMoney.Utility.Core
{
    public interface IStateContext<T> where T : IState
    {
        void change_state_to(T NEW_STATE);
    }
}