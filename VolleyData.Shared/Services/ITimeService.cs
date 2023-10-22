using System.ServiceModel;
using ProtoBuf.Grpc;

namespace VolleyData.Shared.Services
{
    [ServiceContract]
    public interface ITimeService
    {
        [OperationContract]
        IAsyncEnumerable<string> SubscribeAsync(CallContext context = default);
    }
}
