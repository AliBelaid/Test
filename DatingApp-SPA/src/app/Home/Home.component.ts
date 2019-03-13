import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  // tslint:disable-next-line:component-selector
  selector:   'app-Home' ,
  templateUrl: './Home.component.html',
  styleUrls: ['./Home.component.css']
})
export class HomeComponent implements OnInit {
registerState = false;
values: any;
constructor(private http: HttpClient) { }

ngOnInit() {
 // this.getValues();
}

// getValues() {
//   this.http.get('http://localhost:5000/API/Values').subscribe(response =>  {
//     this.values = response;
//      }   , error => { console.log(error); });
// }

  RegisterToggle() {
this.registerState = true;

  }
  CancelModeRegister( stateRegister: boolean) {
    this.registerState = stateRegister;
  }
}
