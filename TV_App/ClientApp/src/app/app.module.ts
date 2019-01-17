import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID, NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FilterSidebarComponent } from './filter-sidebar/filter-sidebar.component';
import { ListingComponent } from './listing/listing.component';
import { ListingElementComponent } from './listing-element/listing-element.component';
import { HttpClientModule } from '@angular/common/http';

import { ProgrammedetailsComponent } from './programmedetails/programmedetails.component';
import { FormsModule } from '@angular/forms';
import { GuideComponent } from './guide/guide.component';
import { RecommendedComponent } from './recommended/recommended.component';
import { ProfileComponent } from './profile/profile.component';
import { FeatureComponent } from './feature/feature.component';
import { ProgrammeComponent } from './programme/programme.component';
import { ChannelComponent } from './channel/channel.component';
import { SearchResultsComponent } from './search-results/search-results.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FilterSidebarComponent,
    ListingComponent,
    ListingElementComponent,
    ProgrammedetailsComponent,
    GuideComponent,
    RecommendedComponent,
    ProfileComponent,
    FeatureComponent,
    ProgrammeComponent,
    ChannelComponent,
    SearchResultsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [/*{provide: LOCALE_ID, useValue: 'pl'}*/],
  bootstrap: [AppComponent]
})
export class AppModule { }
