import { Component, OnInit, Input } from '@angular/core';
import { UserSessionService } from '../user-session.service';
import { ActivatedRoute } from '@angular/router';
import { User, Payment, Expense, Group, UserWithBalance } from 'src/types';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { isNotCurrentUser } from '../validators';

@Component({
  selector: 'app-group-details',
  templateUrl: './group-details.component.html',
  styleUrls: ['./group-details.component.scss']
})
export class GroupDetailsComponent implements OnInit {
  group: Group;
  user: User;
  users: UserWithBalance[];
  payments: Payment[];
  expenses: Expense[];

  addUserForm: FormGroup;
  alreadyInGroup: boolean = false;
  addUserError: boolean = false;
  userNotExists: boolean = false;

  constructor(private userSession: UserSessionService,
              private route: ActivatedRoute,
              private formBuilder: FormBuilder) {
    let groupId: number = Number(this.route.snapshot.paramMap.get('id'));
    this.group = this.userSession.groups[groupId];
    this.user = this.userSession.user;
    this.userSession.getUsersOfGroup(groupId).subscribe(response => this.users = response);
    this.userSession.getPaymentsOfGroup(groupId).subscribe(response => this.payments = response);
    this.userSession.getExpensesOfGroup(groupId).subscribe(response => this.expenses = response);
    
    this.addUserForm = this.formBuilder.group({
      name: ['', [Validators.required, isNotCurrentUser(this.user.name)]]
    });
  }

  ngOnInit() {
  }

  confirmPayment(id: number){
    this.userSession.confirmPayment(id).subscribe(response => {
      if (response){
        let payment = this.payments.find(item => item.id === id);
        payment.confirmed = true;
        this.users.find(item => item.name === payment.byUser).balance -= payment.amount;
      }
    })
  }

  onAddUserFormChange(){
    this.addUserError = this.alreadyInGroup = this.users.some(user => user.name === this.addUserForm.controls.name.value);
    this.userNotExists = false;
  }

  addUser(data){
    this.userSession.addUserToGroup(this.group.id, data.name).subscribe(response => {
      if (response) {
        this.users.push({id: 0, name: data.name, balance: 0});
        this.addUserForm.controls.name.setValue('');
      } else {
        this.addUserError = this.userNotExists = true;
      }
    })
  }
}
