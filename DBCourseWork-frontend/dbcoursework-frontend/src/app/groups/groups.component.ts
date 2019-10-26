import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss']
})
export class GroupsComponent implements OnInit {

  groups = [{id: 1, name: "name1"}, {id: 2, name: "name2"}, {id: 3, name: "name3"}]

  constructor() { }

  ngOnInit() {
  }

}
