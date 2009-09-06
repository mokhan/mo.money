using Gorilla.Commons.Utility;
using Gorilla.Commons.Utility.Core;

namespace MoMoney.Service.Infrastructure.Updating
{
    public class DownloadTheLatestVersion : IDownloadTheLatestVersion
    {
        readonly IDeployment deployment;

        public DownloadTheLatestVersion(IDeployment deployment)
        {
            this.deployment = deployment;
        }

        public void run(ICallback<Percent> callback)
        {
            deployment.UpdateProgressChanged += (o, e) => callback.run(new Percent(e.BytesCompleted, e.BytesTotal));
            deployment.UpdateCompleted += (sender, args) => callback.run(100);
            deployment.UpdateAsync();
        }
    }
}