import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IProgrammeListElement } from '../../interfaces/ProgrammeListElement';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-programmedetails',
  templateUrl: './programmedetails.component.html',
  styleUrls: ['./programmedetails.component.scss']
})
export class ProgrammedetailsComponent implements OnInit {

  constructor(private _http: HttpClient, public user: UserService) { }
  @Input('prog_id') prog_id: number;
  @Output() buttonClicked = new EventEmitter<string>();
  public programme: IProgrammeListElement = null;

  ngOnInit() {
    this._http.get<IProgrammeListElement>(`${environment.api}Programmes/${this.prog_id}`)
      .subscribe(result => this.programme = result);
  }
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
    ).subscribe(result => this.buttonClicked.emit(result.toString()));
  }

  onlyUnique(value: any, index: number, self: any[]) {
    return self.indexOf(value) === index;
  }

  translateDate(initial: string): string {
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
}
