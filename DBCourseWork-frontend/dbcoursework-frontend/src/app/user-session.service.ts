import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const API_URL = "https://localhost:5001/api";

@Injectable({
  providedIn: 'root'
})
export class UserSessionService {
  user;

  constructor(private http: HttpClient) { }

  async login(name: string){
    this.user = await this.http.post(`${API_URL}/login`, {name}).toPromise();
  }

  getGroups(){
    return this.http.get(`${API_URL}/user/${this.user.id}/groups`);
  }

  getUsersOfGroup(id: Number){
    return this.http.get(`${API_URL}/user/${this.user.id}/group/${id}/users`);
  }

  getExpensesOfGroup(id: Number){
    return this.http.get(`${API_URL}/user/${this.user.id}/group/${id}/expenses`);
  }

  getPaymentsOfGroup(id: Number){
    return this.http.get(`${API_URL}/user/${this.user.id}/group/${id}/payments`);
  }
}
