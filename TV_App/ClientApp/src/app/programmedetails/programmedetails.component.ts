import { Component, OnInit } from '@angular/core';
import { IProgrammeListElement } from '../interfaces/ProgrammeListElement';

@Component({
  selector: 'app-programmedetails',
  templateUrl: './programmedetails.component.html',
  styleUrls: ['./programmedetails.component.scss']
})
export class ProgrammedetailsComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  public programme : IProgrammeListElement = {
    id : 1,
    title : 'Asterix i Obelix: Misja Kleopatra',
    iconUrl : 'https://ssl-gfx.filmweb.pl/po/08/96/30896/6900390.3.jpg',
    emissions : [{
      channel : {
        name : 'Polsat',
        id : 1
      },
      start : 'Oct',
      stop : 'Sep'
    }],
    features : [

    ],
    description : 'Wie pan, moim zdaniem to nie ma tak, że dobrze albo że nie dobrze. Gdybym miał powiedzieć, co cenię w życiu najbardziej, powiedziałbym, że ludzi. Eeem… Ludzi, którzy podali mi pomocną dłoń, kiedy sobie nie radziłem, kiedy byłem sam, i, co ciekawe, to właśnie przypadkowe spotkania wpływają na nasze życie. Chodzi o to, że kiedy wyznaje się pewne wartości, nawet pozornie uniwersalne, bywa, że nie znajduje się zrozumienia, które by tak rzec, które pomaga… się nam rozwijać. Ja miałem szczęście, by tak rzec, ponieważ je znalazłem. I dziękuję życiu. Dziękuję mu, życie to śpiew, życie to taniec, życie to miłość. Wielu ludzi pyta mnie o to samo: "Ale jak ty to robisz?", "Skąd czerpiesz tę radość?". A ja odpowiadam, że to proste, to umiłowanie życia, to właśnie ono sprawia, że dzisiaj na przykład buduję maszyny, a jutro… kto wie, dlaczego by nie, oddam się pracy społecznej i będę ot, choćby… sadzić… znaczy… marchew.'
  }

}
