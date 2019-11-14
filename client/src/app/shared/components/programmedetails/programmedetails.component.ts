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
  @Output() buttonClicked = new EventEmitter<string>();
  @Input() programme: IProgrammeListElement = null;

  ngOnInit() {}
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

  onlyUnique(value: any, index: number, self: any[]) {
    return self.indexOf(value) === index;
  }
}
