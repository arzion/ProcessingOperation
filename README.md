# ProcessingOperations
Infrastructure for scheduling execution of operations.
### Intro

This package allows you to execute your code every **N** seconds.

It works like this (Assume that you define execution of your operation every **10** seconds):

*You start the manager with assosiated operation and 10 seconds settings:*

* [Your operation execution: takes *4 sec*]
* Pause - *10 seconds*
* [Your operation execution: takes *25 sec*]
* Pause - *10 seconds*
* [Your operation execution...]

If you stop the manager, the operation Cancellation Token **will be cancelled**.

How to work:

## 1. Without configuration file:
```c#
// 1. create the operation that should be executed by schedule
public class MyOperation : IOperation {
    public void Execute(CancellationToken token) {
        // Do some work here
    }
}

// 2. create repeating operation processing manager
var manager = new RepeatingOperationProcessingManager(
    operation,
    new RepeatingOperationSettings {
        EverySeconds = 3
    });

// 3. start the manager
manager.Start();

// 4. in any point of time you can stop repeating operation by calling
manager.Stop();
// Calling Stop() equivalent to manager.Dispose();
```

## 2. With configuration file (more preferrably if you are using IoC container):

You can specifiy operations configuration in the *configuration file (.config)* of your application

Section defenition:
```xml
<section name="processingOperations" type="ProcessingOperations.Configuration.ProcessingOperationsSection, ProcessingOperations" />
```
Section:
```xml
<processingOperations>
    <repeatingOperations>
        <manager name="TestManagerName" operationKey="OperationKey" everySeconds="3"/>
    </repeatingOperations>
</processingOperations>
```

To work with configuration you have to define the **IOperationFactory** to build the **IOperation** instance based on **operationKey**
```c#
// 1. Create the operation factory class
public class OperationFactory : IOperationFactory {
    public IOperation Create(string operationKey) {
        // Create instance of operation based on operation key
    }
}
// 2. Configure processing operations to use this factory
ProcessingOperationsConfiguration.SetOperationFactory(new OperationFactory());
```
Or you can simplify factory creation with just providing the **Func** delegate how to create the operations
```c#
ProcessingOperationsConfiguration.SetOperationFactory(operationKey => {
    // Create operation based on operation key
});
```
