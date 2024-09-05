import { Component } from '@angular/core';
import { FormGroup, ReactiveFormsModule, Validators, FormBuilder, FormsModule } from '@angular/forms';
import { DataService } from '../data.service';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api'; 
import { ProgressSpinnerModule } from 'primeng/progressspinner'; 
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    FormsModule,
    ToastModule,
    ProgressSpinnerModule
  ],
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
})
export class RegistrationComponent {
  formHandle: FormGroup;
  isLogin: boolean = false;
  loading: boolean = false;

  constructor(
    private fb: FormBuilder, 
    private database: DataService, 
    private messageService: MessageService,
    private route: Router
  ) {
    localStorage.clear();
    this.formHandle = this.fb.group({
      emailaddress: ["", [Validators.required, Validators.email]],  
      password: ["", [Validators.required]]
    });
  }

  handleSubmit() {
    const bodyData = this.formHandle.value;
    this.loading = true
    if (this.isLogin) {
      this.database.post("auth/login", bodyData).subscribe((data: any) => {
        this.loading = false
        this.messageService.add({severity: 'success', summary: 'Success', detail: 'Login successful!'});
        localStorage.setItem("token", data.token)
        localStorage.setItem("loggedin", JSON.stringify(true))
        this.route.navigate([""])
      }, error => {
        this.loading = false
        this.messageService.add({severity: 'error', summary: 'Error', detail: 'User not registered, Please register to continue!'});
        console.log('Login error:', error);
      });
    } else {
      this.database.post("auth/register", bodyData).subscribe((data: any) => {
        this.loading = false
        this.messageService.add({severity: 'success', summary: 'Success', detail: 'Registration successful!'});
        console.log('Registration successful:', data);
      }, error => {
        this.loading = false
        this.messageService.add({severity: 'error', summary: 'Error', detail: 'User already exist'});
        console.log('Registration error:', error);
      });
    }
  }

  toggleSignupLogin() {
    this.isLogin = !this.isLogin;
  }
}
