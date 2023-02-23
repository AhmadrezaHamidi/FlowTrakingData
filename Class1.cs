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
            var flowType = _flowType.FirstOrDefault("TypeId = @flowTypeId ", new { flowTypeId });
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

            var flowActivityType = _flowActivityType.FirstOrDefault("FlowTypeId = @FlowTypeId  AND Periority = 1", new { FlowTypeId = flowType.Id });

            if (flowActivityType is null)
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.NotFound,
                    ExceptionLevel.Error,
                    $"Can not found requested flow Activity Type.");

            var flowActivityInstance = new FlowActivity(
                Guid.NewGuid().ToString(),
                0,
                "",
                DateTime.Now,
                flowInstance.UniqeId,
                flowActivityType.Id,
                flowActivityType.Name,
                flowActivityType.Title,
                0);

            var status = _statusType.FirstOrDefault("Name = @Name", new { Name = "Init" });
            if (status is null)
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.NotFound,
                    ExceptionLevel.Error,
                    $"Can not found requested status.");

            var statusInstance = new Status(Guid.NewGuid().ToString(), DateTime.Now,
                status.TypeId, status.TypeTitle, status.TypeTitle, flowActivityInstance.UniqeId);

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
            var flwoData = _flowService.FirstOrDefault("UniqeId = @flow");

            var flwoData = _flowService.FirstOrDefault("UniqeId = @flow");

            //if (foundFlwo is null)
            //    throw new ApplicationException($"{nameof(foundFlwo)} is null");

            ////Akharin activity in fellow
            //var lastFlwoActivity = _flowActivityService.LastOrDefault("FlowId =@flow");

            ////Akharine State in Activity
            //var lastState = _status.LastOrDefault("FlowActivityId = @lastFlwoActivity.UniqeId");

            //if (lastState is null || lastState.StatusTypeTitle.Equals("Failed") ||
            //    lastState.StatusTypeTitle.Equals("Final"))
            //    throw new ApplicationException($"{nameof(lastState)} is finished");

            ////Akhjarine zamane
            //var flowType = _flowType.LastOrDefault("typeId =@foundFlwo.FlowTypeId ");

            //if (foundFlwo is null || flowType.Period > 10)
            //    throw new ApplicationException($"{nameof(flowType)} is null");

            /////////var flowes = flowType.
            //var state = _status.

            //// var checkLastflowTime = DateTime.Now - flowType.Period ;
            ////if (checkLastflowTime > 10 )
            ////    throw new ApplicationException($"{nameof(flowType)} is null");
            //return "Hello";

            return string.Empty;
        }

        public bool SetStatus(string activityId, int StatusTypeId)
        {
            var activity = _flowActivityService.FirstOrDefault("UniqeId = @activityId");

            var status = _statusType.FirstOrDefault("TypeId = @StatusTypeId", new { });

            if (activity is null)
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.NotFound,
                    ExceptionLevel.Error,
                    $"Can not found requested activity.");

            if (status is null)
                throw new HoushmandBaseException(
                    ExceptionCriteria.Framework,
                    ExceptionType.NotFound,
                    ExceptionLevel.Error,
                    $"Can not found requested status.");

            if (status.TypeName == "Success")
            {
                var instanceSuccess = new Status(Guid.NewGuid().ToString(), DateTime.Now, status.TypeId, status.TypeName, status.TypeTitle, activityId);
            }

            //var res = false;

            //if (activity is not null && activity is not null)
            //{
            //    var instance = new Status(Guid.NewGuid().ToString(),
            //        DateTime.Now,
            //        statusType.TypeId,
            //        statusType.TypeName,
            //        statusType.TypeTitle,
            //        activity.UniqeId);

            //    res = true;

            //    _status.Insert(instance);
            //}
            //return res;
            return true;
        }
    }

    public interface IFlowTracking
    {
        string CreateFlow(int flowTypeId, int userId, Version version);

        string GoToNextStep(string flow, int userId);

        bool SetStatus(string activityId, int StatusTypeId);
    }
