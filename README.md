# ProcessingOperation
Infrastructure for scheduling execution of operations.

How to work:

1. Without configuration:
```c#
// 1. create the operation that should be executed by schedule
public class MyOperation : IOperation {
    public void Execute(CancellationToken token) {
        // Do some work here
    }
}

// 2. reate repeating operation processing manager
var manager = new RepeatingOperationProcessingManager(
    operation,
    new RepeatingOperationSettings {
        EverySeconds = 3
    });

// 3. start the manager
manager.Start();

// 4. in any point of time you can stop repeating operation by calling
manager.Stop();
// Calling stop equivalent to manager.Dispose();
```

1. With configuration (more preferrably if you are using IoC container):

You can specifiy managers configuration in the configuration file of your application

Section defenition:
```xml
<section name="processingManagers" type="ProcessingOperations.Configuration.ProcessingOperationsSection, ProcessingOperations" />
```
Section:
```xml
<processingOperations>
    <repeatingOperations>
        <manager name="TestManagerName" operationKey="OperationKey" everySeconds="3"/>
    </repeatingOperations>
</processingOperations>
```

To work with configuration you have to define the IOperationFactory to build the IOperation instance based on operationKey
```c#
// 1. Create the operation factory class
public class OperationFactory : IOperationFactory {
    public IOperation Create(string operationKey) {
        // Create instance of operation based on operation key
    }
}
// 2. Configure ProcessingManagers to use this factory
ProcessingManagersConfiguration.SetOperationFactory(new OperationFactory());
```
Or you can simplify factory creation with just providing the Func delegate how to create the operations
```c#
ProcessingManagersConfiguration.SetOperationFactory(operationKey => {
    // Create operation based on operation key
});
```
