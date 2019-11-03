import { Component } from '@angular/core';
import { UserSessionService } from './user-session.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'dbcoursework-frontend';

  constructor(private userSession: UserSessionService){

  }

  logout(){
    this.userSession.logout();
  }
}
