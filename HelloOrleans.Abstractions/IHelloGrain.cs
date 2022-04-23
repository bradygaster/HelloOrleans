using Orleans;

namespace HelloOrleans.Abstractions
{
    public interface IHelloGrain : IGrainWithStringKey
    {
        Task<string> SayHello();
    }
}