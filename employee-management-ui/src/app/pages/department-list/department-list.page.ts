import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Department } from '../../models/department.model';
import { DepartmentService } from '../../services/department.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-department-list',
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
  templateUrl: './department-list.page.html'
})
export class DepartmentListPage implements OnInit {
  displayedColumns: string[] = ['name', 'description', 'actions'];
  dataSource = new MatTableDataSource<Department>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private deptService: DepartmentService, private router: Router) {}

  ngOnInit() {
    this.loadDepartments();
  }

  loadDepartments() {
    this.deptService.getAll().subscribe(
      depts => {
        this.dataSource.data = depts;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      error => alert('Error loading departments: ' + error.message)
    );
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  add() {
    this.router.navigate(['/departments/add']);
  }

  edit(dept: Department) {
    this.router.navigate(['/departments/edit', dept.id]);
  }

  delete(dept: Department) {
    if(confirm('Delete this department?')){
      this.deptService.delete(dept.id).subscribe(
        () => {
          this.dataSource.data = this.dataSource.data.filter(d => d.id !== dept.id);
        },
        error => alert('Error deleting department: ' + error.message)
      );
    }
  }
}
