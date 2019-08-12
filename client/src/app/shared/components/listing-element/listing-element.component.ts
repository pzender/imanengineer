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
    this._http.post(
      `${environment.api}Users/${this.user.getUser()}/Ratings`,
      {ProgrammeId: this.programme.id, RatingValue: value},
      {responseType: 'json'}
    )
    .subscribe(result => this.buttonClicked.emit(result.toString()));
  }

  ratingAvailable(): boolean{
    return this.programme.rating != undefined && this.programme.rating != -1
  }

  ratingText(value: number): string {
    return this.ratingTexts[value];
  }

  ngOnInit() {
  }

  onlyUnique(value: any, index: number, self: any[]) {
    return self.indexOf(value) === index;
  }

  public translateDate(initial: string): string {
    return initial
      .replace('Mon', 'Pn')
      .replace('Tue', 'Wt')
      .replace('Wed', 'Śr')
      .replace('Thu', 'Cz')
      .replace('Fri', 'Pt')
      .replace('Sat', 'Sb')
      .replace('Sun', 'Nd')
      .replace('Jan', 'Sty')
      .replace('Feb', 'Lut')
      .replace('Mar', 'Mar')
      .replace('Apr', 'Kwi')
      .replace('May', 'Maj')
      .replace('Jun', 'Cze')
      .replace('Jul', 'Lip')
      .replace('Aug', 'Sie')
      .replace('Sep', 'Wrz')
      .replace('Oct', 'Paź')
      .replace('Nov', 'Lis')
      .replace('Dec', 'Gru');
  }

  private ratingTexts = ['Nie podobał mi się', 'Podobał mi się', 'Chcę obejrzeć'];

}
