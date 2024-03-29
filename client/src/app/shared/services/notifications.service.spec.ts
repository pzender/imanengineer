import { TestBed } from '@angular/core/testing';

import { NotificationsService } from './notifications.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ServiceWorkerModule } from '@angular/service-worker';

describe('NotificationsService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule, 
      RouterTestingModule,
      ServiceWorkerModule.register('', {enabled: false})
    ]

  }));

  it('should be created', () => {
    const service: NotificationsService = TestBed.get(NotificationsService);
    expect(service).toBeTruthy();
  });
});
