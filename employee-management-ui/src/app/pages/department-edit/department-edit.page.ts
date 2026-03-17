import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DepartmentService } from '../../services/department.service';
import { Department } from '../../models/department.model';

@Component({
  selector: 'app-department-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './department-edit.page.html',
  styleUrl: './department-edit.page.css'
})
export class DepartmentEditPage implements OnInit {
  form: FormGroup;
  isSubmitting = false;
  departmentId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private deptService: DepartmentService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: ['']
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      if (params['id']) {
        this.departmentId = parseInt(params['id'], 10);
        this.loadDepartment();
      }
    });
  }

  loadDepartment(): void {
    if (!this.departmentId) return;

    this.deptService.getById(this.departmentId).subscribe({
      next: (dept: Department) => {
        this.form.patchValue({
          name: dept.name,
          description: dept.description || ''
        });
      },
      error: () => {
        alert('Failed to load department');
        this.router.navigate(['/departments']);
      }
    });
  }

  submit(): void {
    if (this.form.invalid || !this.departmentId) return;

    this.isSubmitting = true;
    const updatedDept: Department = {
      id: this.departmentId,
      name: this.form.get('name')?.value,
      description: this.form.get('description')?.value
    };

    this.deptService.update(updatedDept).subscribe({
      next: () => {
        this.router.navigate(['/departments']);
      },
      error: () => {
        alert('Failed to update department');
        this.isSubmitting = false;
      }
    });
  }

  cancel(): void {
    this.router.navigate(['/departments']);
  }
}
