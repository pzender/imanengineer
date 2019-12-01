import { browser, by, element } from 'protractor';

export class AppPage {
  navigateTo() {
    return browser.get('/');
  }

  getHeader() {
    return element(by.css('h1')).getText();
  }

  nextDay() {
    element(by.cssContainingText('#day a', '>')).click();
  }

  getNavbarTitle() {
    return element(by.css('app-navbar nav a')).getText();
  }

  getChannelLink() {
    return element(by.cssContainingText('a.text-light', 'TVP 1'));
  }


  getFirstTitle() {
    return element(by.css('app-listing-element h5 a'));
  }

  clickHeaderLink(linkText: string) {
    element(by.cssContainingText('app-navbar nav a', linkText)).click();
  }

  login() {
    this.clickHeaderLink('Zaloguj');
    element(by.css('.modal-body input')).sendKeys('auto');
    return element(by.cssContainingText('app-navbar span.navbar-text', 'Witaj, '))
  }
}
