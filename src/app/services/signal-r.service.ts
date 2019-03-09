import { Injectable } from '@angular/core';
import { IPlayer } from '../shared/models/player';
import { HttpHeaders } from '@angular/common/http/http';
import * as signalR from '@aspnet/signalr';
import { environment } from '../../environments/environment';
import { IGame } from '../shared/models/game';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  _players: IPlayer[] = [];
  _users: IPlayer[] = [];
  _games: IGame[] = [];
  headers: HttpHeaders;
  private connection: signalR.HubConnection;
  constructor() { }


  public async connect(accessToken) {
    await this.start(accessToken);
  }

  public async start(accessToken: string) {
    if (!this.connection) {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl(environment.signalR + 'api/gamehub', { accessTokenFactory: () => accessToken })
        .build();
    }

    if (this.connection && this.connection.state !== signalR.HubConnectionState.Connected) {
      this.connection.start().then(() => {
        if (this.connection && this.connection.state === signalR.HubConnectionState.Connected) {
          this.connection.invoke('SendLoggedInUsers').catch(function (err) {
            return console.error(err.toString());
          });
          this.connection.invoke('SendGames').catch(function (err) {
            return console.error(err.toString());
          });
          this.connection.on('ReceiveLoggedInUsers', (players: IPlayer[]) => {
            this._users = players;
          });

          this.connection.on('ReceiveGames', (games: IGame[]) => {
            this._games = games;
          });

          this.connection.on('ReceiveGameUsers', (players: IPlayer[]) => {
            console.log('Players Received for Game');
            this._players = players;
          });
        }
      }).catch(err => {
        if (!environment.production) {
          console.error(err);
        }
      });
    }
  }

  public addToGroup(groupId: number): void {
    if (this.connection.state === signalR.HubConnectionState.Connected) {
      this.connection.invoke('AddToGroup', groupId);
    }
  }

  public removeFromGroup(groupId: number): void {
    if (this.connection.state === signalR.HubConnectionState.Connected) {
      this.connection.invoke('RemoveFromGroup', groupId);
    }
  }

  public disconnect() {
    if (this.connection) {
      if (this.connection.state === signalR.HubConnectionState.Connected) {
        this.connection.stop();
      }
      this.connection = null;
    }
  }

  getUsers(): IPlayer[] {
    return this._users;
  }

  getPlayers(): IPlayer[] {
    return this._players;
  }

  getGames(): IGame[] {
    return this._games;
  }
}
