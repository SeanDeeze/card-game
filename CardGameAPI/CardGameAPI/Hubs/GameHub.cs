using CardGameAPI.Repositories;
using CardGameAPI.Repositories.Interface;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CardGameAPI.Hubs
{
  public class GameHub : Hub
  {
    readonly IGameEngine _gameEngine;

    public GameHub(GameEngine gameEngine)
    {
      _gameEngine = gameEngine;
    }

    public async Task SendLoggedInUsers()
    {
        await Clients.Caller.SendAsync("ReceiveLoggedInUsers", _gameEngine.GetLoggedInUsers());
    }

    public async Task SendGames()
    {
      await Clients.All.SendAsync("ReceiveGames", _gameEngine.GetGames());
    }

    public async Task AddToGroup(string groupName)
{
    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
}

public async Task RemoveFromGroup(string groupName)
{
    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
}
  }
}
