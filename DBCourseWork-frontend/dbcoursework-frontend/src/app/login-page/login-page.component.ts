import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {
  loginForm;

  constructor(private formBuilder: FormBuilder,
              private http: HttpClient) { 
    this.loginForm = this.formBuilder.group({
      name: ''
    });
  }

  ngOnInit() {
  }

  login(data){
    this.http.post('https://localhost:5001/api/login', {name: data.name}).subscribe(response => console.log(response));
  }
}
