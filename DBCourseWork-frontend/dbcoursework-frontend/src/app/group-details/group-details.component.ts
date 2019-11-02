import { Component, OnInit, Input } from '@angular/core';
import { UserSessionService } from '../user-session.service';
import { ActivatedRoute } from '@angular/router';

enum GroupTabType { Users, Payments, Expenses }

@Component({
  selector: 'app-group-details',
  templateUrl: './group-details.component.html',
  styleUrls: ['./group-details.component.scss']
})
export class GroupDetailsComponent implements OnInit {
 
  groupTabType = GroupTabType;
  users;
  payments;
  expenses;
  tab: GroupTabType = GroupTabType.Users;

  constructor(private userSession: UserSessionService,
              private route: ActivatedRoute) {
    let groupId: Number = Number(route.snapshot.paramMap.get('id'));
    userSession.getUsersOfGroup(groupId).subscribe(response => this.users = response);
    userSession.getPaymentsOfGroup(groupId).subscribe(response => this.payments = response);
    userSession.getExpensesOfGroup(groupId).subscribe(response => this.expenses = response);
  }

  ngOnInit() {
  }
}
