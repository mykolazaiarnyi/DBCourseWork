import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginPageComponent } from './login-page/login-page.component';
import { GroupsComponent } from './groups/groups.component';
import { GroupDetailsComponent } from './group-details/group-details.component';
import { UserSessionService } from './user-session.service';
import { ProfileComponent } from './profile/profile.component';


const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginPageComponent },
  { path: 'groups', component: GroupsComponent, canActivate: [UserSessionService] },
  { path: 'group/:id', component: GroupDetailsComponent, canActivate: [UserSessionService] },
  { path: 'profile', component: ProfileComponent, canActivate: [UserSessionService] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
