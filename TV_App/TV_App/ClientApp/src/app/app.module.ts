import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FilterSidebarComponent } from './filter-sidebar/filter-sidebar.component';
import { ListingComponent } from './listing/listing.component';
import { ListingElementComponent } from './listing-element/listing-element.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FilterSidebarComponent,
    ListingComponent,
    ListingElementComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
