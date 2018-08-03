import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    let body = new FormData();
    body.append('custId', '2');
    body.append('amount', '30');
    body.append('verified', 'true');
    http.post(baseUrl + 'api/Payment/Pay/2/30/true', body, httpOptions).subscribe(
      (response) => {
        return response;
      });
  }
}
