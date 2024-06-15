using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions.Store;

namespace StudentRegistration.Controllers
{
    internal class DelegateRequestAdapter : IRequestAdapter
    {
        private HttpClient httpClient;

        public DelegateRequestAdapter(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public ISerializationWriterFactory SerializationWriterFactory => throw new NotImplementedException();

        public string? BaseUrl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task<T?> ConvertToNativeRequestAsync<T>(RequestInformation requestInfo, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void EnableBackingStore(IBackingStoreFactory backingStoreFactory)
        {
            throw new NotImplementedException();
        }

        public Task<ModelType?> SendAsync<ModelType>(RequestInformation requestInfo, ParsableFactory<ModelType> factory, Dictionary<string, ParsableFactory<IParsable>>? errorMapping = null, CancellationToken cancellationToken = default) where ModelType : IParsable
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ModelType>?> SendCollectionAsync<ModelType>(RequestInformation requestInfo, ParsableFactory<ModelType> factory, Dictionary<string, ParsableFactory<IParsable>>? errorMapping = null, CancellationToken cancellationToken = default) where ModelType : IParsable
        {
            throw new NotImplementedException();
        }

        public Task SendNoContentAsync(RequestInformation requestInfo, Dictionary<string, ParsableFactory<IParsable>>? errorMapping = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ModelType?> SendPrimitiveAsync<ModelType>(RequestInformation requestInfo, Dictionary<string, ParsableFactory<IParsable>>? errorMapping = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ModelType>?> SendPrimitiveCollectionAsync<ModelType>(RequestInformation requestInfo, Dictionary<string, ParsableFactory<IParsable>>? errorMapping = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}