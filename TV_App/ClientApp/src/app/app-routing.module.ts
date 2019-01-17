import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListingComponent } from './listing/listing.component';
import { GuideComponent } from './guide/guide.component';
import { FeatureComponent } from './feature/feature.component';
import { ChannelComponent } from './channel/channel.component';
import { RecommendedComponent } from './recommended/recommended.component';
import { ProfileComponent } from './profile/profile.component';
import { SearchResultsComponent } from './search-results/search-results.component';
import { ProgrammeComponent } from './programme/programme.component';

const routes: Routes = [
  {path: 'Guide', component: GuideComponent},
  {path: 'Features/:id', component: FeatureComponent},
  {path: 'Channels/:id', component: ChannelComponent},
  {path: 'Programmes/:id', component: ProgrammeComponent},
  {path: 'Recommended', component: RecommendedComponent},
  {path: 'Profile', component: ProfileComponent},
  {path: 'Search', component: SearchResultsComponent},
  {path: '', redirectTo: 'Recommended', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
