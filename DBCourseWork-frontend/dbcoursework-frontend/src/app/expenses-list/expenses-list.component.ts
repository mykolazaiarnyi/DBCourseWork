import { Component, OnInit, Input } from '@angular/core';
import { User, Group, Expense } from 'src/types';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserSessionService } from '../user-session.service';

@Component({
  selector: 'app-expenses-list',
  templateUrl: './expenses-list.component.html',
  styleUrls: ['./expenses-list.component.scss']
})
export class ExpensesListComponent implements OnInit {
  @Input() user: User;
  @Input() expenses: Expense[];
  @Input() group: Group;

  addExpenseForm: FormGroup;
  

  constructor(private formBuilder: FormBuilder,
              private userSession: UserSessionService) {
    this.addExpenseForm = this.formBuilder.group({
      amount: ['', [Validators.required, Validators.pattern(/^\d+$/)]],
      description: ['', [Validators.required]]
    })
  }

  ngOnInit() {
  }

  addExpense(data){
    this.userSession.addExpense(this.group.id, {
      amount: data.amount, 
      description: data.description,
      byUserName: this.user.name,
      time: '1.1.1970'
    }).subscribe(response => this.expenses.push(response));
  }
}
