import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IProgrammeListElement } from '../../interfaces/ProgrammeListElement';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { UserService } from 'src/app/shared/services/user.service';


@Component({
  selector: 'app-listing-element',
  templateUrl: './listing-element.component.html',
  styleUrls: ['./listing-element.component.scss']
})
export class ListingElementComponent implements OnInit {
  @Output() buttonClicked = new EventEmitter<string>();
  @Input('programme') programme: IProgrammeListElement;
  requestPending = false;
  displayRatingText = '';

  constructor(private _http: HttpClient, public user: UserService) { }

  feat_types(): string[] {
    return this.programme.features
      .map(function(f: {type: string, value: string}) { return f.type; })
      .filter(this.onlyUnique);
  }

  parse_date(iso_date: string) {
    return Date.parse(iso_date);
  }

  feat_array(desired_type: string): {'id': number, 'name': string}[] {
    const chosen_feats = this.programme.features
      .filter(function(f: {id: number, type: string, value: string}) { return f.type === desired_type; })
      .map(function(f: {id: number, type: string, value: string}) { return {
        'id': f.id,
        'name': f.value
      }; });
    return chosen_feats;
  }

  rateButton(value: number) {
    this.requestPending = true;
    this._http.post(
      `${environment.api}Users/${this.user.getUser()}/Ratings`,
      { Id: this.programme.id, RatingValue: value },
      { responseType: 'json' }
    )
    .subscribe(result => {
      this.buttonClicked.emit(result.toString());
      this.programme.rating = result['ratingValue'];
      console.log(result['ratingValue']);
      this.displayRatingText = this.ratingText(result['ratingValue']);
      this.requestPending = false;
    });
  }

  remindme() {
    this._http.post(
      `${environment.api}Users/${this.user.getUser()}/Notifications`,
      { Id: this.programme.emissions[0].id, RatingValue: 1 },
      { responseType: 'json' }
    )
    .subscribe(result => {
      this.buttonClicked.emit(result.toString());
    });
    this.rateButton(1);

  }

  ratingAvailable(): boolean{
    return this.programme.rating != undefined && this.programme.rating != 10000
  }

  ratingText(value: number): string {
    return this.ratingTexts[value+1];
  }

  ngOnInit() {
    if(this.ratingAvailable())
      this.displayRatingText = this.ratingText(this.programme.rating)
  }

  onlyUnique(value: any, index: number, self: any[]) {
    return self.indexOf(value) === index;
  }
  private ratingTexts = ['Nie podobał mi się', 'Nie interesuje mnie', 'Podobał mi się'];

}
