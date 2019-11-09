import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserSessionService } from '../user-session.service';
import { tap, catchError } from 'rxjs/operators';

@Component({
  selector: 'app-create-group',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.scss']
})
export class CreateGroupComponent implements OnInit {

  createGroupForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
              private route: Router,
              private userSession: UserSessionService) {
    this.createGroupForm = this.formBuilder.group({
      name: ['', Validators.required]
    })
  }

  createGroup(data){
    this.userSession.createGroup(data.name).subscribe(response => this.route.navigate(["/group", response.id]))
  }

  ngOnInit() {
  }

}
