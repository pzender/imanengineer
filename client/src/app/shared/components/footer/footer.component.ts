import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  constructor(private http: HttpClient) { }
  lastUpdate$: Observable<any>
  

  ngOnInit() {
    this.lastUpdate$ = this.http.get<any>(`${environment.api}GuideUpdate/Last`)
  }

}
