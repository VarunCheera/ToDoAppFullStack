import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'todoapp';
  notes:any=[];
  readonly APIUrl="http://localhost:5224/api/ToDoApp/";
  constructor(private http:HttpClient){}
  refreshNotes()
  {
    this.http.get(this.APIUrl+"GetNotes").subscribe(data=>{this.notes=data})
  }
  ngOnInit(){
    this.refreshNotes();
  }
}
