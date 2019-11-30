import { AppPage } from './app.po';

describe('workspace-project App', () => {
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
});
