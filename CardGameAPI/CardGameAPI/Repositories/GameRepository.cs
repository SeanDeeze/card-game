using CardGameAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGameAPI.Repositories
{
  public class GameRepository
  {
    private EFContext _context;
    private GameEngine _gameEngine;
    public GameRepository(EFContext context, GameEngine gameEngine)
    {
      _context = context;
      _gameEngine = gameEngine;
    }

    public CGMessage GetGames()
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        List<Game> games = _gameEngine._games.ToList();
        returnMessage.ReturnData.Add(games);
        returnMessage.Status = true;
      }
      catch (Exception ex)
      {
        // Do nothing for now, logger still needs to be implemented
      }
      return returnMessage;
    }

    public CGMessage SaveGame(Game inputGame)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        _gameEngine._games.Add(inputGame);
        _context.Games.Add(inputGame);
        _context.SaveChanges();
        return GetGames();
      }
      catch (Exception ex)
      {
        // Do nothing for now, logger still needs to be implemented
      }
      return returnMessage;
    }

    public CGMessage DeleteGame(Game inputGame)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        _context.Games.Remove(inputGame);
        _context.SaveChanges();
        _gameEngine._games.Remove(inputGame);
        return GetGames();
      }
      catch (Exception ex)
      {
        // Do nothing for now, logger still needs to be implemented
      }
      return returnMessage;
    }

    public CGMessage JoinGame(Game inputGame, Player player)
    {
      CGMessage returnMessage = new CGMessage();
      try
      {
        Game game = _gameEngine._games.First(ge => ge.Id == inputGame.Id);
        game.Players.Add(player);
        return GetGames();
      }
      catch (Exception ex)
      {
        // Do nothing for now, logger still needs to be implemented
      }
      return returnMessage;
    }
  }
}
