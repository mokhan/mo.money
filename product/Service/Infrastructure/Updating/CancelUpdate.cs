using MoMoney.Service.Contracts.Infrastructure.Updating;

namespace MoMoney.Service.Infrastructure.Updating
{
    public class CancelUpdate : ICancelUpdate
    {
        readonly IDeployment deployment;

        public CancelUpdate(IDeployment deployment)
        {
            this.deployment = deployment;
        }

        public void run()
        {
            if (null == deployment) return;
            deployment.UpdateAsyncCancel();
        }
    }
}