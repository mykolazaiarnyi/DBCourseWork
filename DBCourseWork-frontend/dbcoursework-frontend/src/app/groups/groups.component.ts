import { Component, OnInit } from '@angular/core';
import { UserSessionService } from '../user-session.service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss']
})
export class GroupsComponent implements OnInit {

  groups;

  constructor(private userSession: UserSessionService) {
    this.userSession.getGroups().subscribe(response => this.groups = response);
  }

  ngOnInit() {
  }

}
