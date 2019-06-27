import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TranslationsService {

  constructor() { }

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

}
