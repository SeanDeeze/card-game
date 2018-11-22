import { Component, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { Player } from '../shared/models/player';
import { LoginService } from '../services/login.service';
import { CGMessage } from '../shared/models/CGMessage';
import { isNullOrUndefined } from 'util';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements AfterViewInit {
  displayLogin = true;
  loginPlayer: Player = {} as Player;

  constructor(private _loginService: LoginService, private router: Router) { }

  ngAfterViewInit() {
    if (localStorage.getItem('id')) {
      this._loginService.setPlayer({
        id: !isNullOrUndefined(localStorage.getItem('id')) ? Number(localStorage.getItem('id')) : null,
        userName: !isNullOrUndefined(localStorage.getItem('username')) ? localStorage.getItem('username') : null,
        LastActivity: new Date()
      });
    }
  }

  login() {
    this._loginService.Login(this.loginPlayer).subscribe(result => {
      result = result as CGMessage;
      if (result.status === true) { // Register login with service and set persisted user values
        const p: Player = result.returnData[0] as Player;
        this._loginService.setPlayer(p);
        localStorage.setItem('id', p.id.toString());
        localStorage.setItem('username', p.userName);
        this.router.navigateByUrl('/home');
      }
    });
  }
}
