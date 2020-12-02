import { Component, Inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-change',
  templateUrl: './change.component.html',
  styleUrls: ['./change.component.css']
})

export class ChangeComponent {

  private validStatuses: string[] = [
    'Created',
    'Uploaded',
    'Completed'
  ]


  headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');
  httpOptions = {
    headers: this.headers
  };
  baseUrl: string;

  constructor(private fb: FormBuilder, private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  }

  uploadForm = this.fb.group({
    id: [null, Validators.required],
    status: [null, Validators.required],
  });

  onSubmit() {
    // TODO Number 4 Call the UpdateComponent API
    console.warn(this.uploadForm.value);

    this.http.patch(this.baseUrl + '/api/components/${id}', JSON.stringify({ Status: status}));
  }    
}



