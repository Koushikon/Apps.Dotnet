namespace DI_MVC_Demo.Interface
{
    public interface IOperation
    {
        Guid OperationId { get; }
    }

    public interface IOperationTransient : IOperation { }

    public interface IOperationScoped : IOperation { }

    public interface IOperationSingleton : IOperation { }

    public interface IOperationSingletonInstance : IOperation { }
}
