import { Component, OnInit } from '@angular/core';
import { UserSessionService } from '../user-session.service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss']
})
export class GroupsComponent implements OnInit {

  groups: any = [{id: 1, name: "name1"}, {id: 2, name: "name2"}, {id: 3, name: "name3"}]

  constructor(private userSession: UserSessionService) {
    this.userSession.getGroups().subscribe(response => this.groups = response);
  }

  ngOnInit() {
  }

}
