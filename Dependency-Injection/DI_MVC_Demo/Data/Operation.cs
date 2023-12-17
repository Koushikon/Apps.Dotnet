using DI_MVC_Demo.Interface;

namespace DI_MVC_Demo.Data
{
    public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton, IOperationSingletonInstance
    {
        Guid _guid;

        public Operation() : this(Guid.NewGuid()) { }

        public Operation(Guid guid)
        {
            _guid = guid;
        }

        public Guid OperationId => _guid;
    }
}
