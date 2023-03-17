//using Houshmand.Framework.DataAccess.Dapper;
//using Houshmand.Framework.DataAccess.Dapper.Contracts;
//using Houshmand.Framework.ExceptionHandler;
//using Houshmand.Framework.WorkFlow.Entities;
//using RepositoryEfCore.IReposetory;
//using WorkFlow.Entities;

//namespace Houshmand.Framework.WorkFlow.Managers
//{
using RepositoryEfCore.IReposetory;
using WorkFlow.Entities;

public class FlowTracking : IFlowTracking
{
    private readonly IUnitOfWork unitOfWork;

    public FlowTracking(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public string CreateFlow(string flowTypeId, string userId, string version)
    {
        var _flowType = unitOfWork.GetRepository<FlowType>();
        var _flowActivityType = unitOfWork.GetRepository<FlowActivityType>();
        var _statusType = unitOfWork.GetRepository<StatusType>();
        var _flowService = unitOfWork.GetRepository<Flow>();
        var _flowActivityService = unitOfWork.GetRepository<FlowActivity>();
        var _status = unitOfWork.GetRepository<Status>();

        var flowType = _flowType.GetFirstOrDefault(predicate: x => x.Id == flowTypeId);

        if (flowType is null)
            throw new Exception("Can not found requested flow.");

        var flowInstance = new Flow(
            version,
            userId,
            flowType.Id,
            flowType.Name,
            flowType.Title);

        var flowActivityType = _flowActivityType.GetFirstOrDefault(predicate: x => x.Id == flowType.Id && x.Priority == 1);


        if (flowActivityType is null)
            throw new Exception("Can not found requested flow Activity Type..");


        var flowActivityInstance = new FlowActivity(
            null,
            flowInstance.Id,
            flowActivityType.Id,
            flowActivityType.Name,
            flowActivityType.Title,
            1);

        var status = _statusType.GetFirstOrDefault(predicate: x => x.Name == "Success");
        if (status is null)
            status = StatusTypeCollection.Instance.GetByName("Success");

        if (status is null)
            throw new Exception(
                $"Can not found requested status.");

        var statusInstance = new Status(status.Id, status.Name
            , status.Title, flowActivityInstance.Id);

        try
        {
            _flowService.Insert(flowInstance);
            flowActivityInstance.FlowId = flowInstance.Id;
            _flowActivityService.Insert(flowActivityInstance);
            statusInstance.FlowActivityId = flowActivityInstance.Id;
            _status.Insert(statusInstance);

            unitOfWork.SaveChanges();
            return flowInstance.Id;
        }
        catch (Exception ex)
        {
            throw new Exception("Can not insert flow.");
        }
    }

    public string GoToNextStep(string flowId, string userId)
    {
        var _flowType = unitOfWork.GetRepository<FlowType>();
        var _flowActivityType = unitOfWork.GetRepository<FlowActivityType>();
        var _statusType = unitOfWork.GetRepository<StatusType>();
        var _flowService = unitOfWork.GetRepository<Flow>();
        var _flowActivityService = unitOfWork.GetRepository<FlowActivity>();
        var _status = unitOfWork.GetRepository<Status>();

        var flow = _flowService.GetFirstOrDefault(predicate: x => x.Id == flowId);
        if (flow is null)
            throw new Exception("Can not found requested flow");


        var flowType = _flowType.GetFirstOrDefault(predicate: x => x.Id == flow.FlowTypeId);
        if (flowType is null)
            throw new Exception("Can not found  flow Type ");

        var lastActitvity = _flowActivityService
            .GetFirstOrDefault(predicate: x => x.FlowId == flowId, orderBy: y => y.OrderByDescending(z => z.CreatedAt));
        if (lastActitvity is null)
            throw new Exception("Can not found  flow Activity ");

        var successStatusId = _statusType.GetFirstOrDefault(predicate: x => x.Name == "Success");
        if (successStatusId is null)
            successStatusId = StatusTypeCollection.Instance["Success"];

        if (successStatusId is null)
            throw new Exception($"Can not found success Status ");

        var lastActitvityState = _status.GetFirstOrDefault(predicate: x => x.FlowActivityId == lastActitvity.Id &&
        x.StatusTypeId == successStatusId.Id,
            orderBy: y => y.OrderByDescending(z => z.CreatedAt));

        if (lastActitvityState is null)
            throw new Exception($"last Actitvity is not success");

        var nextActivityType = _flowActivityType
            .GetFirstOrDefault(predicate: x => x.Priority == lastActitvity.Priority + 1  && x.Id == flowType.Id);

        if (nextActivityType is null)
            throw new Exception($"this Actitvity was Ended");

        var flowActivityInstance = new FlowActivity(
           lastActitvity.Id,
           flowId,
           nextActivityType.Id,
           nextActivityType.Name,
           nextActivityType.Title,
           lastActitvity.Priority + 1);

        var status = _statusType.GetFirstOrDefault(predicate: x => x.Name == "Init");

        if (status is null)
            throw new Exception(
                $"Can not found requested status.");

        var statusInstance = new Status(status.Id, status.Name
           , status.Title, flowActivityInstance.Id);

        try
        {
            _flowActivityService.Insert(flowActivityInstance);
            statusInstance.FlowActivityId = flowActivityInstance.Id;
            _status.Insert(statusInstance);

            unitOfWork.SaveChanges();
            return flowActivityInstance.Id;
        }
        catch (Exception ex)
        {
            throw new Exception($"Can not insert flow.", ex);
        }
    }

    public bool SetStatus(string activityId, string StatusTypeId)
    {
        var _statusType = unitOfWork.GetRepository<StatusType>();
        var _flowActivityService = unitOfWork.GetRepository<FlowActivity>();
        var _status = unitOfWork.GetRepository<Status>();

        var res = false;
        var actvity = _flowActivityService.GetFirstOrDefault(predicate: x => x.Id == activityId,
            orderBy: y => y.OrderByDescending(z => z.CreatedAt));

        if (actvity is null)
            throw new Exception($"Can not found requested activity.");

        var lastActivity = _status.GetFirstOrDefault(predicate: x => x.FlowActivityId == actvity.PervioseId,
            orderBy: y => y.OrderByDescending(z => z.CreatedAt));

        if (lastActivity.StatusTypeName != "Success")
            throw new Exception($"Last Step is not status.");

        var status = _statusType.GetFirstOrDefault(predicate: x => x.Id == StatusTypeId);
        if (status is null)
            throw new Exception($"Can not found requested status.");

        var instance = new Status(status.Id, status.Name, status.Title, activityId);
        try
        {
            _status.Insert(instance);
            unitOfWork.SaveChanges();
            res = true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Can not insert flow.", ex);
        }

        return res;
    }
}
    public interface IFlowTracking
    {
        string CreateFlow(string flowTypeId, string userId, string version);

        string GoToNextStep(string flow, string userId);

        bool SetStatus(string activityId, string StatusTypeId);
    }
