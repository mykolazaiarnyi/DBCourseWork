import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserSessionService {
  user;

  constructor(private http: HttpClient) { }

  login(name){
    this.http.post('https://localhost:5001/api/login', {name}).subscribe(response => this.user = response);
  }
}
