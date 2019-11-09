import { Component, OnInit, Input } from '@angular/core';
import { UserSessionService } from '../user-session.service';
import { ActivatedRoute } from '@angular/router';
import { User, Payment, Expense, Group } from 'src/types';

@Component({
  selector: 'app-group-details',
  templateUrl: './group-details.component.html',
  styleUrls: ['./group-details.component.scss']
})
export class GroupDetailsComponent implements OnInit {
  group: Group;
  user: User;
  users: User[];
  payments: Payment[];
  expenses: Expense[];

  constructor(private userSession: UserSessionService,
              private route: ActivatedRoute) {
    let groupId: number = Number(route.snapshot.paramMap.get('id'));
    this.group = this.userSession.groups[groupId];
    this.user = this.userSession.user;
    userSession.getUsersOfGroup(groupId).subscribe(response => this.users = response);
    userSession.getPaymentsOfGroup(groupId).subscribe(response => this.payments = response);
    userSession.getExpensesOfGroup(groupId).subscribe(response => this.expenses = response);
  }

  ngOnInit() {
  }
}
