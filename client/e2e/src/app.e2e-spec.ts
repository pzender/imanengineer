import { AppPage } from './app.po';

describe('LookingGlass app', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
    page.navigateTo();
  });

  it('should display app name', () => {
    expect(page.getNavbarTitle()).toEqual('Looking Glass');
  });

  it('should display channels at main page', () => {
    expect(page.getHeader()).toEqual('156 kanałów do wyboru');
  });

  it('should display listing at channel page', () => {
    let link = page.getChannelLink();
    expect(link).toBeTruthy();
    link.click();
    expect(page.getHeader()).toEqual('TVP 1');
    page.nextDay();
    expect(page.getHeader()).toEqual('TVP 1');
  });

  it('should display details at programme page', () => {
    page.getChannelLink().click();
    page.nextDay();
    let prog_link = page.getFirstTitle();
    let prog_link_text = prog_link.getText();
    prog_link.click();
    expect(page.getHeader()).toEqual(prog_link_text);
  });

  it('should log in correctly', () => {
    expect(page.login()).toEqual('Witaj, auto!');
    page.clickHeaderLink('Wyloguj');
  });

  it('should display show rated programme in ratings', () => {
    expect(page.login()).toEqual('Witaj, auto!');
    page.getChannelLink().click();
    page.nextDay();
    let prog_link = page.getFirstTitle();
    page.clickHeaderLink('Wyloguj');

  });

  it('should display recommendations', () => {
    expect(page.login()).toEqual('Witaj, auto!');
    page.clickHeaderLink('Polecane');
    expect(page.getFirstTitle()).toBeTruthy();
    page.clickHeaderLink('Wyloguj');
  });


});
