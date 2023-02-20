using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Houshmand.Framework.WorkFlow.Entities
{

    #region Status

    public class Status : BaseEntity
    {
        public string StatusTypeId { get; set; }
        public string StatusTypeName { get; set; }
        public string StatusTypeTitle { get; set; }
        public string FlowActivityId { get; set; }
    }

    #endregion








    #region StatusType

    public class StatusType
    {
        public StatusType(int typeId, string typeName, string typeTitle)
        {
            TypeId = typeId;
            TypeName = typeName;
            TypeTitle = typeTitle;
        }

        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeTitle { get; set; }
    }

    #endregion










    #region FlowTypeCollection
    public class StatusTypeCollection
    {
        public StatusTypeCollection()
        {
            CreateFlowTypeCollection();
        }


        public static StatusTypeCollection Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public void CreateFlowTypeCollection()
        {
            var DefaltInstance = new StatusType(0, "DefaltInstance", "پیش فرض ");
            _flowTypes.Add("0", DefaltInstance);
            var successُStatus = new StatusType(10, "Success", "موفقیت امیز ");
            _flowTypes.Add("10", successُStatus);
            var retryStatus = new StatusType(20, "Retry", "امتحان مجدد");
            _flowTypes.Add("20", retryStatus);
            var failedInstance = new StatusType(30, "Failed", "ناموفق");
            _flowTypes.Add("30", failedInstance);
        }

        private readonly Dictionary<string, StatusType> _flowTypes = new();
        private static readonly Lazy<StatusTypeCollection> lazy = new(() => new StatusTypeCollection());

        public int Count
        { get { return _flowTypes.Count; } }


        public StatusType GetById(int id)
        {
            var flowType = _flowTypes.Values.FirstOrDefault(x => x.TypeId == id);
            return flowType;
        }

        public StatusType this[string FlowType]
        {
            get
            {
                try
                {
                    return _flowTypes[FlowType];
                }
                catch
                {
                    return _flowTypes["0"];
                }
            }
        }

        public StatusType this[int typeId]
        {
            get
            {
                var flowType = _flowTypes.Values.FirstOrDefault(x => x.TypeId == typeId);
                return flowType;
            }
        }


    }
    #endregion
















}
