import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { NavbarComponent } from '../navbar/navbar.component';
import { DataService } from '../data.service';
import { ActivatedRoute } from '@angular/router';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@Component({
  selector: 'app-blog-post',
  standalone: true,
  imports: [
    FormsModule,
    NavbarComponent,
    ReactiveFormsModule,
    ProgressSpinnerModule
  ],
  templateUrl: './blog-post.component.html',
  styleUrl: './blog-post.component.css'
})
export class BlogPostComponent {
  commentForm: FormGroup;
  id: number;
  imageUrl: string;
  content: string;
  comments: any[] = [];
  loading: boolean = false;

  blogdetails = {
    image: '../../assets/dotnet.png',
    createdAt: '2024-08-01T12:34:56Z',
    description: 'This is a hardcoded description of the blog post.',
  };
  constructor(private database: DataService, private routes: ActivatedRoute, private fb: FormBuilder){
    this.routes.queryParams.subscribe((data: any) => {
      this.id = data.id
      console.log(data.id, "params qu")
      this.getBlogs(this.id)
    })
    this.commentForm = this.fb.group({
      firstname: ["", Validators.required],
      lastname: ["", Validators.required],
      commentcontent: ["", Validators.required]
    })
  }

  getBlogs(id: number){
    this.database.get(`blog/${id}`).subscribe((data: any) => {
      this.imageUrl = data.Image
      this.content = data.Content
      this.comments = data.Comments
      console.log(data)
    })
  }

  handleCommentSubmit() {
    if (this.commentForm.valid){
      this.loading = true
      const body = this.commentForm.value
      this.database.post(`comment/${this.id}`, body).subscribe((data: any) => {
        this.loading = false

        console.log(data , "done comment")
        this.getBlogs(this.id)
        this.commentForm.reset()
      })
    }
  }

  DateFormterring(date: string) {
    // Format date logic
    return new Date(date).toLocaleDateString();
  }
}
