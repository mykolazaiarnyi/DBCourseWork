import { Component, OnInit, Input } from '@angular/core';
import { User, Payment, Group } from 'src/types';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserSessionService } from '../user-session.service';
import { isNotCurrentUser, isPositiveNumber } from '../validators';

@Component({
  selector: 'app-payments-list',
  templateUrl: './payments-list.component.html',
  styleUrls: ['./payments-list.component.scss']
})
export class PaymentsListComponent implements OnInit {
    @Input() user: User;
    @Input() users: User[];
    @Input() payments: Payment[];
    @Input() group: Group;
  
    addPaymentForm: FormGroup;
    userNotInGroup: boolean;
  
    constructor(private userSession: UserSessionService,
                private formBuilder: FormBuilder) {
      this.addPaymentForm = this.formBuilder.group({
        user: ['', [Validators.required, isNotCurrentUser(this.userSession.user.name)]],
        amount: ['', [isPositiveNumber]],
        description: ['', [Validators.required]]
      });
    }
  
    ngOnInit() {
    }
  
    onUserInputChange(){
      this.userNotInGroup = !this.users.some(user => user.name === this.addPaymentForm.controls.user.value)
    }
  
    addPayment(data){
      this.userSession.addPayment(this.group.id, {
        description: data.description,
        byUser: this.user.name,
        amount: data.amount,
        forUser: data.user
      }).subscribe(response => {
          this.payments.push(response);
      })
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
  }
  