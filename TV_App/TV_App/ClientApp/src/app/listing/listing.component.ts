import { Component, OnInit } from '@angular/core';
import { IProgrammeList } from '../interfaces/ProgrammeListModel';
import { ListingService } from '../listing.service';
import { IProgrammeListElement } from '../interfaces/ProgrammeListElement';

@Component({
  selector: 'app-listing',
  templateUrl: './listing.component.html',
  styleUrls: ['./listing.component.scss']
})
export class ListingComponent implements OnInit {
  constructor(private _listingService : ListingService) { }

  ngOnInit() {
    this._listingService.getListing().subscribe(result => this.listing = result);
  }

  title : string = "Program dla HBO"
  listing : IProgrammeListElement[] = []
}
