using JogoGourmetNet.Configuration;
using JogoGourmetNet.Service.External;
using Microsoft.Extensions.DependencyInjection;

IServiceCollection services = new ServiceCollection();

services.RegisterDependencies();

var builder = services.BuildServiceProvider();

var app = builder.GetRequiredService<IGameService>();

app.Play();