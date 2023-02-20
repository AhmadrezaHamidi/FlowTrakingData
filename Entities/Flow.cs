using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Houshmand.Framework.WorkFlow.Entities
{

    #region flow
    public class Flow 
    {

        public int Id { get; set; }
        
        public string UniqeId { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public Version Version { get; set; }
        
        public int UserId { get; set; }

        public int FlowTypeId { get; set; }
        
        public string FlowTypeName { get; set; }
        
        public string FlowTypeTitle { get; set; }

    }
    #endregion





    #region FlowType

    public class FlowType
    {
        public FlowType(int typeId, string typeName, string typeTitle)
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
    public class FlowTypeCollection
    {
        public FlowTypeCollection()
        {
            CreateFlowTypeCollection();
        }


        public static FlowTypeCollection Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public void CreateFlowTypeCollection()
        {
            var DefaltInstance = new FlowType(0, "DefaltInstance", "پیش فرض ");
            _flowTypes.Add("0", DefaltInstance);
            var CardTransferActionInstance = new FlowType(10, "CardTransferAction", "کارت به کارت ");
            _flowTypes.Add("1", CardTransferActionInstance);
            var CardBalanceInstance = new FlowType(20, "CardBalance", "مانده کارت");
            _flowTypes.Add("2", CardBalanceInstance);
            var CardCirculationInstance = new FlowType(30, "CardCirculation", "گردش");
            _flowTypes.Add("3", CardCirculationInstance);
        }

        private readonly Dictionary<string, FlowType> _flowTypes = new();
        private static readonly Lazy<FlowTypeCollection> lazy = new(() => new FlowTypeCollection());

        public int Count
        { get { return _flowTypes.Count; } }


        public FlowType GetById(int id)
        {
            var flowType = _flowTypes.Values.FirstOrDefault(x => x.TypeId == id);
            return flowType;
        }

        public FlowType this[string FlowType]
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

        public FlowType this[int typeId]
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
