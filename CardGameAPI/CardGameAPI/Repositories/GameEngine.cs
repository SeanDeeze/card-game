using System;
using CardGameAPI.Models;
using CardGameAPI.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;


namespace CardGameAPI.Repositories
{
  public class GameEngine : IGameEngine
  {
    public List<Game> Games;
    public List<Player> Players;
    public List<Card> Cards;
    public readonly List<CardRole> CardRoles;
    private static readonly Random Rng = new Random();

    public GameEngine(EFContext context)
    {
      var context1 = context;
      Games = context1.Games.ToList();
      Players = new List<Player>();
      Cards = context1.Cards.ToList();
      CardRoles = context1.CardRoles.ToList();
    }

    public List<Game> GetGames()
    {
      return Games;
    }

    public List<Card> GetCards()
    {
      return Cards;
    }

    public string GetGameNameById(int gameId)
    {
      return Games.First(g => g.Id.Equals(gameId)).Name;
    }

    public List<Player> GetPlayersInGameById(int gameId)
    {
      return Games.First(g => g.Id.Equals(gameId)).Players.ToList();
    }

    public List<Player> GetLoggedInUsers()
    {
      return Players.ToList();
    }

    public void StartGame(int gameId)
    {
      Game game = Games.First(g => g.Id.Equals(gameId));
      if (game != null)
      {
        game.Active = true;
        game.Cards = ShuffleCards();
      }
    }

    public List<Card> ShuffleCards()
    {
      return Cards.OrderBy(a => Rng.Next()).ToList();
    }
  }
}
