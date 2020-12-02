import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

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

  pageNumber: number;
  pageSize: number;
  url: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
   
    this.pageNumber = 0;
    this.pageSize = 10;
    this.url = baseUrl + 'api/components';

    let params = new HttpParams().set("PageSize", this.pageSize.toString()).set("PageNumber", this.pageNumber.toString());

    http.get<ListComponentResponse>(this.url, { headers: this.headers, params: params } ).subscribe(result => {
      this.components = result.components;
    }, error => console.error(error));
  }

  nextPage() {

    this.pageNumber++;

    let params = new HttpParams().set("PageSize", this.pageSize.toString()).set("PageNumber", this.pageNumber.toString());

    this.http.get<ListComponentResponse>(this.url, { headers: this.headers, params: params } ).subscribe(result => {
      this.components = result.components;
    }, error => console.error(error));
  }

  prevPage() {

    if(this.pageNumber > 0)
    {
      this.pageNumber--;

      let params = new HttpParams().set("PageSize", this.pageSize.toString()).set("PageNumber", this.pageNumber.toString());

      this.http.get<ListComponentResponse>(this.url, { headers: this.headers, params: params } ).subscribe(result => {
        this.components = result.components;
      }, error => console.error(error));
    }
  }

  sort(index: number) {
    this.pageNumber = 0;

    var sortCol = "id";
    var sortDir = "asc";

    switch(index) 
    {
      case 0:
        sortCol = "id";
        sortDir = "asc";
        break;
      case 1:
        sortCol = "name";
        sortDir = "asc";
        break;
      case 2:
        sortCol = "type";
        sortDir = "asc";
        break;
      case 3:
        sortCol = "status";
        sortDir = "asc";
        break;
    }

    let params = new HttpParams().set("SortDir", sortDir).set("SortField", sortCol);

      this.http.get<ListComponentResponse>(this.url, { headers: this.headers, params: params } ).subscribe(result => {
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
