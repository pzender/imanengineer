import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-offer-picker',
  templateUrl: './offer-picker.component.html',
  styleUrls: ['./offer-picker.component.scss']
})
export class OfferPickerComponent implements OnInit {

  constructor(private http: HttpClient) { }
  @Input() endpoint: string;
  @Input() title: string;
  @Output() offerPicked = new EventEmitter<any>();
  offers: {id: number, name: string}[]

  ngOnInit() {
    this.http.get<{id: number, name: string}[]>(`${environment.api}${this.endpoint}`).subscribe(
      response => {this.offers = response}
    )
  }

  public select(value) {
    this.offerPicked.emit(value)
  }

}
