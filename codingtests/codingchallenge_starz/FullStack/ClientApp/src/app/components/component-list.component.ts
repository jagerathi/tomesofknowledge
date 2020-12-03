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
  idDirection: number;
  nameDirection: number;
  typeDirection: number;
  statusDirection: number;

  sortField: string;
  sortDirection: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
   
    this.pageNumber = 0;
    this.pageSize = 10;
    this.url = baseUrl + 'api/components';

    this.idDirection = 0;
    this.nameDirection = 0;
    this.typeDirection = 0;
    this.statusDirection = 0;

    this.sortField = "id";
    this.sortDirection = "asc";

    let params = new HttpParams().set("PageSize", this.pageSize.toString()).set("PageNumber", this.pageNumber.toString());

    http.get<ListComponentResponse>(this.url, { headers: this.headers, params: params } ).subscribe(result => {
      this.components = result.components;
    }, error => console.error(error));
  }

  nextPage() {

    this.pageNumber++;

    let params = new HttpParams().set("SortDir", this.sortDirection).set("SortField", this.sortField).set("PageSize", this.pageSize.toString()).set("PageNumber", this.pageNumber.toString());

    this.http.get<ListComponentResponse>(this.url, { headers: this.headers, params: params } ).subscribe(result => {
      this.components = result.components;
    }, error => console.error(error));
  }

  prevPage() {

    if(this.pageNumber > 0)
    {
      this.pageNumber--;

      let params = new HttpParams().set("SortDir", this.sortDirection).set("SortField", this.sortField).set("PageSize", this.pageSize.toString()).set("PageNumber", this.pageNumber.toString());

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
        this.idDirection = this.idDirection <= 0 ? 1 : -1;
        sortDir = this.idDirection > 0 ? "asc" : "desc";
        break;
      case 1:
        sortCol = "name";
        this.nameDirection = this.nameDirection <= 0 ? 1 : -1;
        sortDir = this.nameDirection > 0 ? "asc" : "desc";
        break;
      case 2:
        sortCol = "status";
        this.statusDirection = this.statusDirection <= 0 ? 1 : -1;
        sortDir = this.statusDirection > 0 ? "asc" : "desc";
        break;
      case 3:
        sortCol = "type";
        this.typeDirection = this.typeDirection <= 0 ? 1 : -1;
        sortDir = this.typeDirection > 0 ? "asc" : "desc";
        break;
    }

    this.sortDirection = sortDir;
    this.sortField = sortCol;

    let params = new HttpParams().set("SortDir", this.sortDirection).set("SortField", this.sortField).set("PageSize", this.pageSize.toString()).set("PageNumber", this.pageNumber.toString());

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
