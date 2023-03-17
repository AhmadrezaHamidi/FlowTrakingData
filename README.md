
# FlowTrakingData

<img src="https://fileport.io/ZWLf7kXDeSq3" alt="Data" >


"In the case of a simple request that we send to our API based on the business we have, we receive some data and some queries on our data. There is a possibility that an exception may occur due to any reason and for any further action. We use a tool called "FlowTracking" to manage the data.

FlowTracking is an object that represents the type of work we are doing, such as a "Cart to Cart" or "In Stock" process. This object contains the following information:

App version
User ID that sends the request
Execution time of the process
FlowTypeID and FlowName: type and name of the process
FlowActivity represents the steps in the process, for example, for "Cart to Cart" process, it represents the correct completion of the task or the purpose of a particular flowActivity. It includes the following information:

PreviousID: the previous step
FlowID: which flow this activity belongs to
FlowActivityTypeID: what type of activity is being performed
Priority: the priority of the activity
After completing each step, we need to save the status of that activity. The status includes:

StatusTypeID
StatusTypeName
StatusTypeTitle
FlowActivityID
We have a class called "FlowTracking" to manage these flows. It contains three methods:

CreateFlow: We create a new flow based on the FlowTypeID, UserID, and Version. We then create a FlowActivity for the correct completion of the task and set its state to "success."
GoToNextStep: We retrieve the FlowID and find the last FlowActivity. We check to see if the last state of the FlowActivity is "success" and move on to the next step. If successful, we change the state of the current FlowActivity to "Init."
SetStatus: We provide the ActivityID and StatusID as input. We check the status of the activity and update the state of the activityFlow to the new status.
This is an overview of how we manage the flows in our system. Handling Requests with Flow Tracking in API Development

In API development, it is common to receive requests from clients that contain various data and parameters. To properly handle these requests and ensure their successful execution, it is important to use a system that can track the flow of data and provide useful insights into the process.

One such system is flow tracking, which allows developers to monitor the progress of requests and detect any errors or issues that may arise. Flow tracking involves the use of an object called "Flow," which represents the type of action being performed, such as "Cart to Cart," "Inventory," or "Finalization."

Within a Flow object, there are several important parameters that must be included to properly track the progress of a request. These parameters include:

App version
User ID that sent the request
Maximum duration for the requested operation to be completed
Flow type ID and name, which specifies the type and name of the action being performed
To further break down the progress of a request, Flow objects can also contain "Flow Activity" stages, which represent specific steps in the execution of the request. These Flow Activity stages can be used to validate the correctness of a requested action, or to identify the purpose of a specific Flow Activity.

Each Flow Activity stage has its own set of parameters, including:

Previous ID, which represents the previous step in the flow
Flow ID, which identifies the specific Flow being tracked
Flow Activity Type ID, which identifies the type of activity being performed
Priority, which specifies the priority of the activity being performed
Once all Flow Activity stages have been successfully completed, the final status of the request can

---------------------------------------
dotnet ef migrations add InitialCreate --context WoekFlowContext --startup-project TrackingDataApi  -o "Migrations/CommerceMigrations" --project WorkFlow
