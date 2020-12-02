import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-component-list',
  templateUrl: './component-list.component.html'
})
// TODO Number 2 Sorting and Paging
export class ComponentListComponent {
  public components: ComponentModel[];
  
  headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');
  httpOptions = {
    headers: this.headers
  };

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
   

    http.get<ListComponentResponse>(baseUrl + 'api/components' ).subscribe(result => {
      this.components = result.components;
    }, error => console.error(error));
  }
}

interface ListComponentResponse {
  total: number;
  components: ComponentModel[];
}

interface ComponentModel {
  type: string;
  id: number;
  name: number;
  status: string;
}
