using Houshmand.Framework.WorkFlow.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Houshmand.Framework.WorkFlow.Managers
{
    public class FlowTracking : IFlowTracking
    {
        public string CreateFlow(int flowTypeId, int userId, Version version)
        {
            throw new NotImplementedException();
        }

        public string GoToNextStep(string flow, int userId)
        {
            throw new NotImplementedException();
        }

        public bool SetStatus(string activityId, int StatusTypeId)
        {
            throw new NotImplementedException();
        }
    }

    public interface IFlowTracking
    {
        string CreateFlow(int flowTypeId, int userId, Version version);

        string GoToNextStep(string flow, int userId);

        bool SetStatus(string activityId, int StatusTypeId);

    }
}
