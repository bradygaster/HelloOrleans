using HelloOrleans.Abstractions;
using Orleans;

namespace HelloOrleans.Grains
{
    public class HelloGrain : Grain, IHelloGrain
    {
        public Task<string> SayHello()
        {
            return Task.FromResult($"Hello from Grain {this.GetGrainIdentity().PrimaryKeyString}");
        }
    }
}