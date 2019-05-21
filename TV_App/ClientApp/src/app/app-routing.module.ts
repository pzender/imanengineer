import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GuideComponent } from './containers/guide/guide.component';
import { ChannelListingComponent } from './containers/channel-listing/channel-listing.component';
import { RecommendationsComponent } from './containers/recommendations/recommendations.component';
import { ProfileComponent } from './containers/profile/profile.component';
import { FeatureComponent } from './containers/feature/feature.component';
import { UserService } from './utilities/user.service';
import { ProgrammedetailsComponent } from './components/programmedetails/programmedetails.component';
import { DetailsComponent } from './containers/details/details.component';
import { UserKnownGuard } from './utilities/user-known.guard';

const routes: Routes = [
  { path: 'Recommended', component: RecommendationsComponent, canActivate: [UserKnownGuard] }, //bez username'a powinien wyświetlić obecnego, bez obecnego przekierować do...?
  { path: 'Profile', component: ProfileComponent, canActivate: [UserKnownGuard] },             // ^dat again
  { path: 'Channel/:id', component: ChannelListingComponent }, 
  { path: 'Feature/:id', component: FeatureComponent },
  { path: 'Programme/:id', component: DetailsComponent },
  { path: '', redirectTo: 'Recommended', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
