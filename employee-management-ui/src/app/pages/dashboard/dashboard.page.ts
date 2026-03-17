import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeService } from '../../services/employee.service';
import { Employee } from '../../models/employee.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.page.html'
})
export class DashboardPage implements OnInit {
  total = 0;
  active = 0;
  departmentCount = 0;
  recentEmployees: Employee[] = [];

  constructor(private empService: EmployeeService, private router: Router) {}

  ngOnInit() {
    this.empService.getAll().subscribe((emps) => {
      this.total = emps.length;
      this.active = emps.filter(e => true).length; // placeholder
      this.departmentCount = new Set(emps.map(e => e.jobTitle)).size;
      this.recentEmployees = emps.slice(0, 5); // Show first 5 employees
    });
  }

  viewAllEmployees() {
    this.router.navigate(['/employees']);
  }

  addEmployee() {
    this.router.navigate(['/employees/add']);
  }

  editEmployee(emp: Employee) {
    this.router.navigate(['/employees/edit', emp.id]);
  }
}