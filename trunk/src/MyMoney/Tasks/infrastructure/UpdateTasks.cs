using System.Deployment.Application;
using MyMoney.Presentation.Model.updates;
using MyMoney.Utility.Core;

namespace MyMoney.Tasks.infrastructure
{
    public interface IUpdateTasks
    {
        ApplicationVersion current_application_version();
        void grab_the_latest_version(ICallback callback);
        void stop_updating();
    }

    public class UpdateTasks : IUpdateTasks
    {
        readonly ApplicationDeployment deployment;

        public UpdateTasks()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                deployment = ApplicationDeployment.CurrentDeployment;
            }
        }

        public ApplicationVersion current_application_version()
        {
            if (null == deployment)
            {
                return new ApplicationVersion {updates_available = false,};
            }

            var update = deployment.CheckForDetailedUpdate();
            return new ApplicationVersion
                       {
                           activation_url = deployment.ActivationUri,
                           current = deployment.CurrentVersion,
                           data_directory = deployment.DataDirectory,
                           updates_available = update.IsUpdateRequired || update.UpdateAvailable,
                           last_checked_for_updates = deployment.TimeOfLastUpdateCheck,
                           application_name = deployment.UpdatedApplicationFullName,
                           deployment_url = deployment.UpdateLocation,
                           available_version = update.AvailableVersion,
                           size_of_update_in_bytes=update.UpdateSizeBytes,
                       };
        }

        public void grab_the_latest_version(ICallback callback)
        {
            deployment.UpdateCompleted += (sender, args) => callback.complete();
            deployment.UpdateAsync();
        }

        public void stop_updating()
        {
            deployment.UpdateAsyncCancel();
        }
    }
}