import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginComponent } from './login.component';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ServiceWorkerModule } from '@angular/service-worker';
import { UserService } from '../../services/user.service';
import { of } from 'rxjs';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { tick } from '@angular/core/src/render3';
import { resolve } from 'url';



describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let userServiceMock =  {
    getUser: () => 'test_user',
    isAnonymous: () => false,
    register: () => of(),
    login: () => of()
  };
  let modalServiceMock = {
    open: (content) => new Promise((resolve, reject) => resolve(content))
  };
  let modalService: NgbModal;
  let userService: UserService

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LoginComponent ],
      imports: [
        FormsModule, 
        HttpClientTestingModule, 
        RouterTestingModule,
        ServiceWorkerModule.register('', {enabled: false}),
        NgbModule.forRoot()
      ],
      providers: [
        { provide: UserService, useValue: userServiceMock },
        // { provide: NgbModal, useValue: modalServiceMock}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;

    modalService = TestBed.get(NgbModal);
    userService = TestBed.get(UserService);
    fixture.detectChanges();
  })

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call register on create if given username', () => {
    spyOn(userService, 'register').and.callThrough();
    component.create('test');
    expect(userService.register).toHaveBeenCalledTimes(1);
    expect(userService.register).toHaveBeenCalledWith('test')
  })

  it('should not call register on create if not given username', () => {
    spyOn(userService, 'register').and.callThrough();
    component.create('');
    expect(userService.register).toHaveBeenCalledTimes(0);
  })

  it('should call login on login if given username', () => {
    spyOn(userService, 'login').and.callThrough();
    component.login('test');
    expect(userService.login).toHaveBeenCalledTimes(1);
  })

  it('should not call login on login if not given username', () => {
    spyOn(userService, 'login').and.callThrough();
    component.login('');
    expect(userService.login).toHaveBeenCalledTimes(0);
  })

  it('should remove message on closeWarning', () => {
    component.actionResult = { success: true, message: 'test' };
    component.closeWarning();

    expect(component.actionResult).toBeUndefined();
  })

  it('should pass user from service on getUser', () => {
    let actual = component.getUsername();
    expect(actual).toEqual('test_user');
  })

  it('should pass login info from service on isLoggedIn', () => {
    let actual = component.isLoggedIn();
    expect(actual).toBeTruthy();
  })

  it('should open modal', () => {
    spyOn(modalService, 'open').and.callThrough();
    component.open('<test>');
    expect(modalService.open).toHaveBeenCalled();
  })

});
