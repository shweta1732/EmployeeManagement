import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Employee } from '../../models/employee.model';
import { EmployeeService } from '../../services/employee.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './employee-list.page.html'
})
export class EmployeeListPage implements OnInit {
  displayedColumns: string[] = ['name', 'email', 'department', 'designation', 'salary', 'actions'];
  dataSource = new MatTableDataSource<Employee>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private empService: EmployeeService, private router: Router) {}

  ngOnInit() {
    this.empService.getAll().subscribe(emps => {
      this.dataSource.data = emps;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  edit(emp: Employee) {
    this.router.navigate(['/employees/edit', emp.id]);
  }

  delete(emp: Employee) {
    if(confirm('Delete this employee?')){
      this.empService.delete(emp.id).subscribe(() => {
        this.dataSource.data = this.dataSource.data.filter(e => e.id !== emp.id);
      });
    }
  }
}