import { Component, OnInit } from '@angular/core';
import { QueryParamsService } from '../utilities/query-params.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(private _queryParams: QueryParamsService) { }

  
  ngOnInit() {
  }

}
