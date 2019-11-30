import { browser, by, element } from 'protractor';

export class AppPage {
  navigateTo() {
    return browser.get('/');
  }

  getHeader() {
    return element(by.css('h1')).getText();
  }

  getNavbarTitle() {
    return element(by.css('app-navbar nav a')).getText();
  }

  getChannelLink() {
    return element(by.cssContainingText('a.text-light', 'TVP 1'));
  }

  nextDay() {
    element(by.cssContainingText('#day a', '>')).click();
  }

  getFirstTitle() {
    return element(by.css('app-listing-element h5 a'));
  }
}
