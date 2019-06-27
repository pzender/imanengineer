import { Component, OnInit, Input } from '@angular/core';
import { IProgrammeListElement } from '../../interfaces/ProgrammeListElement';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-listing',
  templateUrl: './listing.component.html',
  styleUrls: ['./listing.component.scss']
})
export class ListingComponent implements OnInit {
  title: string;
  @Input('listing') listing: IProgrammeListElement[];

  constructor(private _httpClient: HttpClient) {  }

  ngOnInit() {  }

  show_details(): boolean {
    return true;
  }

  show_listing(): boolean {
    return false;
  }

  onButtonClicked(response: string) {
    
  }
}
