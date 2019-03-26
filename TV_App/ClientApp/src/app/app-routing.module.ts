import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GuideComponent } from './containers/guide/guide.component';
import { ChannelListingComponent } from './containers/channel-listing/channel-listing.component';

const routes: Routes = [
  { path: 'Guide', component: GuideComponent },
  { path: 'Channel/:id', component: ChannelListingComponent }, 
  { path: '', redirectTo: 'Recommended', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
