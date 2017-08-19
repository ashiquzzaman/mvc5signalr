using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRMVCUnityCRUD.Hubs
{
    [HubName("employeeHub")]
    public class EmployeeHub : Hub
    {
        [HubMethodName("broadcastData")]
        public static void BroadcastData()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<EmployeeHub>();
            context.Clients.All.refreshDepartment();
        }
    }
}