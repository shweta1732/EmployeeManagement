import { Component } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employee-add',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './employee-add.page.html'
})
export class EmployeeAddPage {
  form = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phone: [''],
    hireDate: ['', Validators.required],
    jobTitle: ['', Validators.required],
    salary: [0, [Validators.required, Validators.min(0)]]
  });
  isSubmitting = false;

  constructor(private fb: FormBuilder, private empService: EmployeeService, private router: Router) {}

  submit() {
    if (this.form.valid) {
      this.isSubmitting = true;
      this.empService.create(this.form.value as any).subscribe(
        () => {
          this.router.navigate(['/employees']);
        },
        (error) => {
          this.isSubmitting = false;
          alert('Error adding employee: ' + (error.error?.message || error.message));
        }
      );
    }
  }

  cancel() {
    this.router.navigate(['/employees']);
  }
}