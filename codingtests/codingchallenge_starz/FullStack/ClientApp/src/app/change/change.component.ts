import { Component, Inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';

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
  }

  url: string;
  
  constructor(private fb: FormBuilder, private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 

    this.headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    this.url = baseUrl + 'api/components';
  }

  uploadForm = this.fb.group({
    id: [null, Validators.required],
    status: [null, Validators.required],
  });

  onSubmit() {
    // TODO Number 4 Call the UpdateComponent API
    console.warn(this.uploadForm.value);

    let url = this.url + '/updatecomponent/' + this.uploadForm.get('id').value;
    url += "?Status=" + this.uploadForm.get('status').value;

    this.http.put(url, this.headers)
      .subscribe(response => {
        console.log(response);
      });
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}

