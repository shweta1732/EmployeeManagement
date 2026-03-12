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
    hireDate: ['', Validators.required],
    jobTitle: [''],
    salary: [0, Validators.min(0)]
  });

  constructor(private fb: FormBuilder, private empService: EmployeeService, private router: Router) {}

  submit() {
    if (this.form.valid) {
      this.empService.create(this.form.value as any).subscribe(() => this.router.navigate(['/employees']));
    }
  }
}