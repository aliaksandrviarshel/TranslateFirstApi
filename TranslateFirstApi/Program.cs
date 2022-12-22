using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using TranslateFirstApi.GameFeature.Services;
using TranslateFirstApi.GameFeature.WordsGeneration.Words;
using TranslateFirstApi.TranslateFirstHubFeature;
using TranslateFirstApi.TranslateFirstHubFeature.HubSender;
using TranslateFirstApi.TranslateFirstHubFeature.Services;
using TranslateFirstApi.WaitingRoomFeature.Services;
using TranslateFirstApi.WaitingRoomFeature.Waiter.NicknameGeneration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var nicknameGenerator = new NicknameGenerator();
var usersConnectionsService = new UsersConnectionsService();
var gameService = new GameService();
builder.Services.AddSingleton<INicknameGenerator>(nicknameGenerator);
builder.Services.AddSingleton<IUsersConnectionsService>(usersConnectionsService);
builder.Services.AddSingleton<IGameService>(gameService);
builder.Services.AddSingleton<IWaitingRoomService>(x => {
    return new WaitingRoomService(x.GetService<IGameService>()!,
    x.GetService<IHubContext<TranslateFirstHub>>()!,
    x.GetService<IUsersConnectionsService>()!);
});
builder.Services.AddScoped<IRoomHubSender, RoomHubSender>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );

app.MapControllers();
app.MapHub<TranslateFirstHub>("/hub");

app.Run();

