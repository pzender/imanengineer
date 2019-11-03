import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID, NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { FilterSidebarComponent } from './shared/components/filter-sidebar/filter-sidebar.component';
import { ListingComponent } from './shared/components/listing/listing.component';
import { ListingElementComponent } from './shared/components/listing-element/listing-element.component';
import { HttpClientModule } from '@angular/common/http';

import { ProgrammedetailsComponent } from './shared/components/programmedetails/programmedetails.component';
import { FormsModule } from '@angular/forms';
import { ChannelListingComponent } from './containers/channel-listing/channel-listing.component';
import { GuideComponent } from './containers/guide/guide.component';
import { RecommendationsComponent } from './containers/recommendations/recommendations.component';
import { ProfileComponent } from './containers/profile/profile.component';
import { DetailsComponent } from './containers/details/details.component';
import { FeatureComponent } from './containers/feature/feature.component';
import { AllChannelsComponent } from './containers/all-channels/all-channels.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { LoginComponent } from './shared/components/login/login.component';
import { OfferPickerComponent } from './shared/components/offer-picker/offer-picker.component';
import { NotificationsComponent } from './containers/notifications/notifications.component';
import { FooterComponent } from './shared/components/footer/footer.component';
import { registerLocaleData } from '@angular/common';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import localePl from "@angular/common/locales/pl";

registerLocaleData(localePl)

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FilterSidebarComponent,
    ListingComponent,
    ListingElementComponent,
    ProgrammedetailsComponent,
    ChannelListingComponent,
    GuideComponent,
    RecommendationsComponent,
    ProfileComponent,
    DetailsComponent,
    FeatureComponent,
    AllChannelsComponent,
    LoginComponent,
    OfferPickerComponent,
    NotificationsComponent,
    FooterComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModalModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })
  ],
  providers: [
    { provide: LOCALE_ID, useValue: "pl-PL" } 
  ],
  bootstrap: [AppComponent]
})
export class AppModule {  }
