import { Component, OnInit } from '@angular/core';
import { UserSessionService } from '../user-session.service';
import { Group } from 'src/types';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss']
})
export class GroupsComponent implements OnInit {

  groups: Group[];

  constructor(private userSession: UserSessionService) {
    this.loadGroups()
  }

  loadGroups(){
    this.userSession.getGroups().subscribe(response => this.groups = response);
  }

  ngOnInit() {
  }

}
