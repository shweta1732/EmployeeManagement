import { Component } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { DepartmentService } from '../../services/department.service';

@Component({
  selector: 'app-department-add',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './department-add.page.html'
})
export class DepartmentAddPage {
  form = this.fb.group({
    name: ['', Validators.required],
    description: ['']
  });
  isSubmitting = false;

  constructor(private fb: FormBuilder, private deptService: DepartmentService, private router: Router) {}

  submit() {
    if (this.form.valid) {
      this.isSubmitting = true;
      this.deptService.create(this.form.value as any).subscribe(
        () => {
          this.router.navigate(['/departments']);
        },
        (error) => {
          this.isSubmitting = false;
          alert('Error adding department: ' + (error.error?.message || error.message));
        }
      );
    }
  }

  cancel() {
    this.router.navigate(['/departments']);
  }
}
