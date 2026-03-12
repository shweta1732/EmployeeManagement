import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employee-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './employee-edit.page.html'
})
export class EmployeeEditPage implements OnInit {
  form = this.fb.group({
    id: [0],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    hireDate: ['', Validators.required],
    jobTitle: [''],
    salary: [0, Validators.min(0)]
  });

  constructor(
    private fb: FormBuilder,
    private empService: EmployeeService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.empService.getById(id).subscribe(emp => this.form.patchValue(emp));
  }

  submit() {
    if (this.form.valid) {
      this.empService.update(this.form.value as any).subscribe(() => this.router.navigate(['/employees']));
    }
  }
}