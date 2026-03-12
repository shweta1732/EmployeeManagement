import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeService } from '../../services/employee.service';
import { Employee } from '../../models/employee.model';

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

  constructor(private empService: EmployeeService) {}

  ngOnInit() {
    this.empService.getAll().subscribe((emps) => {
      this.total = emps.length;
      this.active = emps.filter(e => true).length; // placeholder
      this.departmentCount = new Set(emps.map(e => e.jobTitle)).size;
    });
  }
}