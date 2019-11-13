import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { MatTabsModule } from '@angular/material';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { GroupsComponent } from './groups/groups.component';
import { GroupDetailsComponent } from './group-details/group-details.component';
import { ProfileComponent } from './profile/profile.component';
import { CreateGroupComponent } from './create-group/create-group.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UsersListComponent } from './users-list/users-list.component';
import { ExpensesListComponent } from './expenses-list/expenses-list.component';
import { PaymentsListComponent } from './payments-list/payments-list.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    GroupsComponent,
    GroupDetailsComponent,
    ProfileComponent,
    CreateGroupComponent,
    UsersListComponent,
    ExpensesListComponent,
    PaymentsListComponent
  ],
  imports: [
    MatTabsModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
