import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListingElementComponent } from './listing-element.component';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ServiceWorkerModule } from '@angular/service-worker';
import { UserService } from '../../services/user.service';
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';

describe('ListingElementComponent', () => {

  let component: ListingElementComponent;
  let fixture: ComponentFixture<ListingElementComponent>;
  let userServiceMock: Partial<UserService> = {
    getUser: () => 'test_user',
    isAnonymous: () => false
  };
  let userService: any
  let spy: any
  let http: any

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListingElementComponent ],
      imports: [
        FormsModule, 
        HttpClientTestingModule, 
        RouterTestingModule,
        ServiceWorkerModule.register('', {enabled: false})
      ],
      providers: [
        { provide: UserService, useValue: userServiceMock },
        { provide: HttpClient, useValue: spy }
      ]

    })
    .compileComponents();
  }));

  beforeEach(() => {
    spy = jasmine.createSpyObj('HttpClient', {
      post: of()
    });
    fixture = TestBed.createComponent(ListingElementComponent);
    component = fixture.componentInstance;
    component.programme = {
      id: 1,
      title: 'title',
      description: 'test',
      emissions: [
        {
          id: 0,
          channel: {id: 0, name: 'test_channel'},
          start: '2019-11-15T00:20:00',
          stop: '2019-11-15T00:20:00',
        }
      ],
      features: [
        { id: 0, value: 'Serial', type: 'category' }
      ],
      iconUrl: '',
      rating: undefined
    }
    userService = TestBed.get(UserService);
    http = TestBed.get(HttpClient);

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display title in h5 tag', () => {
    expect(fixture.nativeElement.querySelector('h5').textContent).toEqual('title');
  })

  it('should display buttons if user not anonymous', () => {
    expect(fixture.nativeElement.querySelector('button')).toBeTruthy();
  })

  it('should not display buttons if user is anonymous', () => {
    userService.isAnonymous = () => true;
    fixture.detectChanges();
    expect(fixture.nativeElement.querySelector('button')).toBeFalsy();
  })

  it('should set rating text if available', () => {
    component.ngOnInit();
    expect(component.displayRatingText).toBeFalsy();
  })

  it('should set rating text if available', () => {
    component.programme.rating = 1;
    component.ngOnInit();
    expect(component.displayRatingText).toEqual('Podobał mi się');
  })

  it('should call http after rate button click', () => {
    component.rateButton(1);
    expect(http.post.calls.count()).toEqual(1)
  })

  it('should call http twice after remindme button click', () => {
    component.remindme()
    expect(http.post.calls.count()).toEqual(2)
  })


  it('should display emissons correctly', () => {
    expect(fixture.nativeElement.querySelector('div.media-body').textContent)
      .toContain('test_channel: Fri 15 Nov, 0:20 - Fri 15 Nov, 0:20');
  })

  it('should parse date correctly', () => {
    const date = component.programme.emissions[0].start;
    expect(component.parse_date(date)).toEqual(1573773600000);
  })

  it('should create feat array', () => {
    const expected = [{id: 0, name: 'Serial'}]
    expect(component.feat_array('category')).toEqual(expected);
  })
});
