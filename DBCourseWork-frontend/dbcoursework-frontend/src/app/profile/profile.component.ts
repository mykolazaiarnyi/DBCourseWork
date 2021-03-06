import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, AbstractControl, ValidatorFn } from '@angular/forms';
import { UserSessionService } from '../user-session.service';
import { Router } from '@angular/router';
import { isNotCurrentUser } from '../validators';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  changeNameForm: FormGroup;
  nameUsed: boolean = false;

  constructor(private formBuilder: FormBuilder,
              private userSession: UserSessionService,
              private route: Router) {
    this.changeNameForm = this.formBuilder.group({
      name: [
        this.userSession.user.name,
        [ 
          Validators.required,
          isNotCurrentUser(this.userSession.user.name) 
        ]
      ]
    });
  }

  ngOnInit() {
  }

  changeName(data){
    this.userSession.changeName({id: this.userSession.user.id, name: data.name})
      .subscribe(
        result => {
          if (result){
            this.route.navigate(['/groups']);
          } else {
            this.nameUsed = true;
          }
        }
      )
  }

  onTextChange(){
    this.nameUsed = false;
  }
}
