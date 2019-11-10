import { Component, OnInit, Input } from '@angular/core';
import { UserWithBalance, User, Group } from 'src/types';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { isNotCurrentUser } from '../validators';
import { UserSessionService } from '../user-session.service';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss']
})
export class UsersListComponent implements OnInit {
  @Input() group: Group;
  @Input() user: User;
  @Input() users: UserWithBalance[];
  addUserForm: FormGroup;
  alreadyInGroup: boolean = false;
  addUserError: boolean = false;
  userNotExistsOnAdd: boolean = false;

  constructor(private formBuilder: FormBuilder,
              private userSession: UserSessionService) {
    this.addUserForm = this.formBuilder.group({
      name: ['', [Validators.required, isNotCurrentUser(this.userSession.user.name)]]
    });
  }

  ngOnInit() {
  }

  onAddUserFormChange(){
    this.addUserError = this.alreadyInGroup = this.users.some(user => user.name === this.addUserForm.controls.name.value);
    this.userNotExistsOnAdd = false;
  }

  addUser(data){
    this.userSession.addUserToGroup(this.group.id, data.name).subscribe(response => {
      if (response) {
        this.users.push({id: 0, name: data.name, balance: 0});
        this.addUserForm.controls.name.setValue('');
      } else {
        this.addUserError = this.userNotExistsOnAdd = true;
      }
    })
  }
}
