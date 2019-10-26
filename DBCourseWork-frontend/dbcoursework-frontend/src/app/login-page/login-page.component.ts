import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { UserSessionService } from '../user-session.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {
  loginForm;

  constructor(private formBuilder: FormBuilder,
              private userSession: UserSessionService,
              private router: Router) { 
    this.loginForm = this.formBuilder.group({
      name: ''
    });
  }

  ngOnInit() {
  }

  login(data){
    this.userSession.login(data.name);
    this.router.navigate(["/groups"]);
  }
}
