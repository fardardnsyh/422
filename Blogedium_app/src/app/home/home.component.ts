import { Component } from '@angular/core';
import { BlogComponent } from '../blog/blog.component';
import { NavbarComponent } from '../navbar/navbar.component';
import { DataService } from '../data.service';
import { SkeletonModule } from 'primeng/skeleton';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    BlogComponent,
    NavbarComponent,
    SkeletonModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  blogPosts: any[] = [];
  loading: boolean = true;
  constructor(private database: DataService){
    this.database.get("blog").subscribe((data: any)=> {
      this.blogPosts = data
      this.loading = false
      console.log(this.blogPosts)
    })
  }

  ngOnInit(){

  }

  truncateTitle(title: string): string {
    if (title.length > 40) {
      return title.substring(0, 40) + '...';
    }
    return title;
  }

  truncateContent(title: string): string {
    if (title.length > 70) {
      return title.substring(0, 70) + '...';
    }
    return title;
  }

  trackById(index: number, item: any): number {
    return item.Id;
  }
  

}
