import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { User, Group, Expense, Payment, UserWithBalance } from 'src/types';

const API_URL = "https://localhost:5001/api";

@Injectable({
  providedIn: 'root'
})
export class UserSessionService implements CanActivate {

  user: User;
  groups: {[key: number]: Group} = {};

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
    return this.http.get<Group[]>(`${API_URL}/user/${this.user.id}/groups`).pipe(
      tap(response => response.forEach(i => this.groups[i.id] = i))
    );
  }

  getUsersOfGroup(id: Number){
    return this.http.get<UserWithBalance[]>(`${API_URL}/user/${this.user.id}/group/${id}/users`);
  }

  getExpensesOfGroup(id: Number){
    return this.http.get<Expense[]>(`${API_URL}/user/${this.user.id}/group/${id}/expenses`);
  }

  getPaymentsOfGroup(id: Number){
    return this.http.get<Payment[]>(`${API_URL}/user/${this.user.id}/group/${id}/payments`);
  }

  changeName(user: User): Observable<boolean> {
    return this.http.put(`${API_URL}/user`, user).pipe(
      map(response => true),
      tap(result => this.user.name = user.name),
      catchError(error => of(false))
    )
  }

  createGroup(name: string): Observable<Group> {
    return this.http.post<Group>(`${API_URL}/user/${this.user.id}/groups`, { name }).pipe(
      tap(response => this.groups[response.id] = response)
    );
  }

  confirmPayment(id: number): Observable<boolean> {
    return this.http.post(`${API_URL}/payment/${id}`, {}).pipe(
      map(response => true),
      catchError(error => of(false))
    )
  }

  addUserToGroup(groupId: number, userName: string): Observable<boolean> {
    return this.http.post(`${API_URL}/user/${this.user.id}/group/${groupId}`, {name: userName}).pipe(
      map(response => true),
      catchError(error => of(false))
    )
  }

  addExpense(groupId: number, expense: Expense): Observable<Expense>{
    return this.http.post<Expense>(`${API_URL}/group/${groupId}/expenses`, expense)
  }
}
