import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    ConfirmDialogModule,
    ToastModule
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  isSidebarOpen = false;
  loggedIn: any;
  constructor(private route: Router, private confirmationService: ConfirmationService, private messageService: MessageService){
  }

  ngOnInit(){
    this.loggedIn = JSON.parse(localStorage.getItem("loggedin") || "false");
    console.log(this.loggedIn)
  }

  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }

  clearStroge(){
    //routerLink="/login"
    localStorage.clear();
    this.route.navigate(["/login"])
  }

  closeSidebar() {
    this.isSidebarOpen = false;
  }

  confirmLogout() {
    this.confirmationService.confirm({
      message: 'Are you sure that you want to logout?',
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.clearStroge();
        this.messageService.add({severity:'success', summary:'Success', detail:'You have logged out successfully.'});
      },
      reject: () => {
        this.messageService.add({severity:'info', summary:'Cancelled', detail:'Logout cancelled.'});
      }
    });
  }



}
