import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { ProgrammesApiService } from 'src/app/shared/services/programmes-api.service';
import { Time } from '@angular/common';
import { SearchService } from './search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {

  constructor(private route: ActivatedRoute, private service: SearchService) { }
  searchterm: string;

  requestStatus: string;

  listing: any[];
  error: {status: number, message: string};
  title: string
  id: number;
  filters: {from: Time, to: Time, date: number};

  updateFilters($event){
    this.filters = $event;
    this.fetch();
  }

  fetch(){
    this.requestStatus = "waiting";
    this.service.fetch(this.filters, this.searchterm).subscribe(
      response => { 
        this.listing = response; 
        this.requestStatus = response.length > 0 ? "success" : "empty";
      },
      error => {
        this.requestStatus = "failure";
      }
    )
  }

  ngOnInit() {
    this.route.params.subscribe(param => {
      this.searchterm = param['term'];
      this.fetch()
    })
  }
}
