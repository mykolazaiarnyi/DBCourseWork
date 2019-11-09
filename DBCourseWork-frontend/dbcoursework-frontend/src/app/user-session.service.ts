import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { User, Group, Expense, Payment } from 'src/types';

const API_URL = "https://localhost:5001/api";

@Injectable({
  providedIn: 'root'
})
export class UserSessionService implements CanActivate {

  user: User;

  constructor(private http: HttpClient, private route: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    if (!this.user){
      this.route.navigate(['/login']);
      return of(false);
    }
    return of(true);
  }
  

  async login(name: string){
    this.user = await this.http.post<User>(`${API_URL}/login`, {name}).toPromise();
  }

  logout(){
    this.user = undefined;
  }

  getGroups(){
    return this.http.get<Group[]>(`${API_URL}/user/${this.user.id}/groups`);
  }

  getUsersOfGroup(id: Number){
    return this.http.get<User[]>(`${API_URL}/user/${this.user.id}/group/${id}/users`);
  }

  getExpensesOfGroup(id: Number){
    return this.http.get<Expense[]>(`${API_URL}/user/${this.user.id}/group/${id}/expenses`);
  }

  getPaymentsOfGroup(id: Number){
    return this.http.get<Payment[]>(`${API_URL}/user/${this.user.id}/group/${id}/payments`);
  }
}
