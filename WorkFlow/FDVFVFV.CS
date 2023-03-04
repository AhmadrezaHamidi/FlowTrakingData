//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WorkFlow.Entities;

//namespace WorkFlow
//{
//    FLOW ==> e7f06299473d4efda637e0c3b9671cc5	2023-02-27 17:03:43.083	0.0.11	1	1	CardTransfer کارت به کارت    1
        



//        SELECT TOP(1000) [Id]
//      ,[UniqeId]
//      ,[PervioseId]
//      ,[PervioseUniqeId]
//      ,[CreatedAt]
//      ,[FlowId]
//      ,[FlowUniqeId]
//      ,[FlowActivityTypeId]
//      ,[FlowActivityTypeName]
//      ,[FlowActivityTypeTitle]
//      ,[Priority]
//    FROM[AhmadDb].[dbo].[FlowActivity]
//    Id UniqeId PervioseId PervioseUniqeId CreatedAt FlowId  FlowUniqeId FlowActivityTypeId  FlowActivityTypeName FlowActivityTypeTitle   Priority
//1	78090a08642e4610909ee0b83d90b207	0		2023-02-27 17:03:43.093	1	e7f06299473d4efda637e0c3b9671cc5	1	TrStart شروع کارت به کارت	1
//2	c72387dce6254111b8257b2fbdd29064	1	78090a08642e4610909ee0b83d90b207	2023-02-27 17:40:20.313	0	e7f06299473d4efda637e0c3b9671cc5	2	SelectCard انتخاب کارت مبدا    2




//        SELECT TOP(1000) [Id]
//      ,[Name]
//      ,[Title]
//      ,[FlowTypeId]
//      ,[Priority]
//    FROM[AhmadDb].[dbo].[FlowActivityType]
//    Id Name    Title FlowTypeId  Priority
//1	TrStart شروع کارت به کارت	1	1
//2	SelectCard انتخاب کارت مبدا    1	2
//3	SelectDestCard انتخاب کارت مقصد    1	3
//4	TrFinish پایاان کارت به کارت	1	4



//        SELECT TOP(1000) [Id]
//      ,[Name]
//      ,[Title]
//      ,[Period]
//    FROM[AhmadDb].[dbo].[FlowType]
//    Id Name    Title Period
//1	CardTransfer کارت به کارت    5
//2	CardBalance موجودی کارت	1
//3	CardStatement گردش کارت	1


//SELECT TOP(1000) [Id]
//      ,[UniqeId]
//      ,[CreatedAt]
//      ,[StatusTypeId]
//      ,[StatusTypeName]
//      ,[StatusTypeTitle]
//      ,[FlowActivityId]
//      ,[FlowActivityUniqueId]
//    FROM[AhmadDb].[dbo].[Status]
//    Id UniqeId CreatedAt StatusTypeId    StatusTypeName StatusTypeTitle FlowActivityId FlowActivityUniqueId
//1	eb96603112be4014a60d39c8f3f58270	2023-02-27 17:03:43.093	3	Success تایید شده	1	78090a08642e4610909ee0b83d90b207
//2	b273daef35c24f99af75ba8878c75174	2023-02-27 17:40:20.317	1	Init شروع    0	c72387dce6254111b8257b2fbdd29064




//        SELECT TOP(1000) [Id]
//      ,[Name]
//      ,[Title]
//    FROM[AhmadDb].[dbo].[StatusType]
//    Id Name    Title
//1	Init شروع
//2	Pending در حال انتظار
//3	Success تایید شده
//4	Fail خطا

//}
//]

