using Houshmand.Framework.DataAccess.Dapper;
using Houshmand.Framework.DataAccess.Dapper.Contracts;
using Houshmand.Framework.ExceptionHandler;
using Houshmand.Framework.WorkFlow.Entities;

namespace Houshmand.Framework.WorkFlow.Managers
{
    public class FlowTracking : IFlowTracking
    {
        private readonly IDataService<Flow> _flowService;
        private readonly IDataService<FlowType> _flowType;
        private readonly IDataService<FlowActivity> _flowActivityService;
        private readonly IDataService<Status> _status;
        private readonly IDataService<StatusType> _statusType;
        private readonly IDataService<FlowActivityType> _flowActivityType;
        private readonly UnitOfWork _unitOfWork;

        public FlowTracking(string connection)
        {
            _unitOfWork = new(connection);
            _flowService = _unitOfWork.GetDataService<Flow>();
            _flowType = _unitOfWork.GetDataService<FlowType>();
            _flowActivityService = _unitOfWork.GetDataService<FlowActivity>();
            _status = _unitOfWork.GetDataService<Status>();
            _statusType = _unitOfWork.GetDataService<StatusType>();
            _flowActivityType = _unitOfWork.GetDataService<FlowActivityType>();
        }

        public string CreateFlow(int flowTypeId, int userId, Version version)
        {
            var flowType = _flowType.FirstOrDefault("Id = @flowTypeId ", new { flowTypeId });
            if (flowType is null)
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.NotFound,
                    ExceptionLevel.Error,
                    $"Can not found requested flow.");

            var flowInstance = new Flow(Guid.NewGuid().ToString("N"),
                DateTime.Now,
                version,
                userId,
                flowType.Id,
                flowType.Name,
                flowType.Title);

            var flowActivityType = _flowActivityType.FirstOrDefault("FlowTypeId = @FlowTypeId  AND Priority = 1",
                new { FlowTypeId = flowType.Id });

            if (flowActivityType is null)
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.NotFound,
                    ExceptionLevel.Error,
                    $"Can not found requested flow Activity Type.");

            var flowActivityInstance = new FlowActivity(
                Guid.NewGuid().ToString("N"),
                0,
                "",
                DateTime.Now,
                flowInstance.UniqeId,
                flowActivityType.Id,
                flowActivityType.Name,
                flowActivityType.Title,
                1);

            var status = _statusType.FirstOrDefault("Name = @Name", new { Name = "Success" });
            if (status is null)
                status = StatusTypeCollection.Instance.GetByName("Success");

            if (status is null)
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.NotFound,
                    ExceptionLevel.Error,
                    $"Can not found requested status.");

            var statusInstance = new Status(Guid.NewGuid().ToString("N"), DateTime.Now,
                status.Id, status.Name
                , status.Title, flowActivityInstance.UniqeId);

            try
            {
                var id = _flowService.Insert(flowInstance, true);
                flowActivityInstance.FlowId = id;
                var flowActivityId = _flowActivityService.Insert(flowActivityInstance, true);
                statusInstance.FlowActivityId = flowActivityId;
                _status.Insert(statusInstance);

                return flowInstance.UniqeId;
            }
            catch (Exception ex)
            {
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.DataBaseFailed,
                    ExceptionLevel.Error,
                    $"Can not insert flow.", ex);
            }
        }

        public string GoToNextStep(string flow, int userId)
        {
            var flowFound = _flowService.FirstOrDefault("UniqeId = @UniqeId ", new { UniqeId = flow });
            if (flowFound is null)
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.NotFound,
                    ExceptionLevel.Error,
                    $"Can not found requested flow.");

            var lastActitvity = _flowActivityService.LastOrDefault("FlowUniqeId = @FlowUniqeId", new { FlowUniqeId = flow });
            if (lastActitvity is null)
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.NotFound,
                    ExceptionLevel.Error,
                    $"Can not found  flow Activity ");

            var successStatusId = _statusType.FirstOrDefault("Name = @Name", new { Name = "Success" });
            if (successStatusId is null)
                successStatusId = StatusTypeCollection.Instance["Success"];
            if (successStatusId is null)
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.NotFound,
                    ExceptionLevel.Error,
                    $"Can not found success Status ");

            var lastActitvityState = _status.LastOrDefault("FlowActivityUniqueId = @FlowActivityUniqueId and StatusTypeId = @StatusTypeId ",
                new { FlowActivityUniqueId = lastActitvity.UniqeId, StatusTypeId = successStatusId.Id });
            if (lastActitvityState is null)
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.NotFound,
                    ExceptionLevel.Error,
                    $"last Actitvity is not success");

            var nextActivityType = _flowActivityType.FirstOrDefault("Priority = @Priority", new { Priority = lastActitvity.Priority + 1 });
            if (nextActivityType is null)
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.NotFound,
                    ExceptionLevel.Error,
                    $"this Actitvity was Ended");

            var flowActivityInstance = new FlowActivity(
               Guid.NewGuid().ToString("N"),
               lastActitvity.Id,
               lastActitvity.UniqeId,
               DateTime.Now,
               flow,
               nextActivityType.Id,
               nextActivityType.Name,
               nextActivityType.Title,
               lastActitvity.Priority + 1);

            var status = _statusType.FirstOrDefault("Name = @Name", new { Name = "Init" });
            if (status is null)
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.NotFound,
                    ExceptionLevel.Error,
                    $"Can not found requested status.");

            var statusInstance = new Status(Guid.NewGuid().ToString("N"), DateTime.Now,
               status.Id, status.Name
               , status.Title, flowActivityInstance.UniqeId);

            try
            {
                _flowActivityService.Insert(flowActivityInstance);
                statusInstance.FlowActivityId = flowActivityInstance.Id;
                statusInstance.FlowActivityUniqueId = flowActivityInstance.UniqeId;
                _status.Insert(statusInstance);

                return flowActivityInstance.UniqeId;
            }
            catch (Exception ex)
            {
                throw new HoushmandBaseException(
                                  ExceptionCriteria.Framework,
                                  ExceptionType.DataBaseFailed,
                                  ExceptionLevel.Error,
                                  $"Can not insert flow.", ex);
            }


        }

        public bool SetStatus(string activityId, int StatusTypeId)
        {
            var res = false;
            var actvity = _flowActivityService.LastOrDefault("UniqeId = @UniqeId", new { UniqeId = activityId });
            if (actvity is null)
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.NotFound,
                    ExceptionLevel.Error,
                    $"Can not found requested activity.");

            var lastActivity = _status.LastOrDefault("FlowActivityId = @FlowActivityId", new { FlowActivityId = actvity.PervioseUniqeId });
            if (lastActivity.StatusTypeName != "Success")
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.NotFound,
                    ExceptionLevel.Error,
                    $"Last Step is not status.");

            var status = _statusType.FirstOrDefault("Id = @Id", new { Id = StatusTypeId });
            if (status is null)
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.NotFound,
                    ExceptionLevel.Error,
                    $"Can not found requested status.");

            var instance = new Status(Guid.NewGuid().ToString("N"), DateTime.Now, status.Id, status.Name, status.Title, activityId);
            try
            {
                _status.Insert(instance);
                res = true;
            }
            catch (Exception ex)
            {
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.DataBaseFailed,
                    ExceptionLevel.Error,
                    $"Can not insert flow.", ex);
            }

            return res;
        }
    }

    public interface IFlowTracking
    {
        string CreateFlow(int flowTypeId, int userId, Version version);

        string GoToNextStep(string flow, int userId);

        bool SetStatus(string activityId, int StatusTypeId);
    }
}