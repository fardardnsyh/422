import { Routes } from '@angular/router';
import { RegistrationComponent } from './registration/registration.component';
import { HomeComponent } from './home/home.component';
import { AdminformComponent } from './adminform/adminform.component';
import { BlogPostComponent } from './blog-post/blog-post.component';
import { authGuard } from './auth.guard';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';

export const routes: Routes = [
    {path: 'login', redirectTo: "login", pathMatch: 'full'},
    {path: "login", component: RegistrationComponent},
    {path: "admin", component: AdminformComponent},
    {path: "", component: HomeComponent, canActivate: [authGuard]},
    {path: "blogpost", component: BlogPostComponent, canActivate: [authGuard]},
    {path: "protectedadmin", component: AdminformComponent},
    {path: "**", component: PagenotfoundComponent},
];
