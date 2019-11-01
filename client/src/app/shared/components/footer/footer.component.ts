import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Router, ActivatedRoute } from '@angular/router';
import { switchMapTo, tap } from 'rxjs/operators';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  constructor(private http: HttpClient, private route: ActivatedRoute) { }
  lastUpdate$: Observable<any>
  

  ngOnInit() {
    this.lastUpdate$ = this.route.params.pipe(
      switchMapTo(this.http.get<any>(`${environment.api}GuideUpdate/Last`))
    )
  }

}
