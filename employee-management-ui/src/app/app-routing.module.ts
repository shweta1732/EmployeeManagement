import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPage } from './pages/login/login.page';
import { DashboardPage } from './pages/dashboard/dashboard.page';
import { EmployeeListPage } from './pages/employee-list/employee-list.page';
import { EmployeeAddPage } from './pages/employee-add/employee-add.page';
import { EmployeeEditPage } from './pages/employee-edit/employee-edit.page';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginPage },
  { path: '', component: DashboardPage, canActivate: [AuthGuard] },
  { path: 'employees', component: EmployeeListPage, canActivate: [AuthGuard] },
  { path: 'employees/add', component: EmployeeAddPage, canActivate: [AuthGuard] },
  { path: 'employees/edit/:id', component: EmployeeEditPage, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}