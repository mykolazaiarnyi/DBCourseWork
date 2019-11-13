import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { User, Group, Expense } from 'src/types';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { UserSessionService } from '../user-session.service';
import { isPositiveNumber } from '../validators';

@Component({
  selector: 'app-expenses-list',
  templateUrl: './expenses-list.component.html',
  styleUrls: ['./expenses-list.component.scss']
})
export class ExpensesListComponent implements OnInit {
  @Input() user: User;
  @Input() expenses: Expense[];
  @Input() group: Group;

  @Output() onAddExpense = new EventEmitter<number>();

  addExpenseForm: FormGroup;
  

  constructor(private formBuilder: FormBuilder,
              private userSession: UserSessionService) {
    this.addExpenseForm = this.formBuilder.group({
      amount: ['', [isPositiveNumber]],
      description: ['', [Validators.required]]
    })
  }

  ngOnInit() {
  }

  addExpense(data){
    this.userSession.addExpense(this.group.id, {
      amount: data.amount, 
      description: data.description,
      byUserName: this.user.name
    }).subscribe(response => {
      this.expenses.push(response);
      this.onAddExpense.emit(response.amount);
    });
  }
}
