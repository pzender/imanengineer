import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChannelListingComponent } from './containers/channel-listing/channel-listing.component';
import { RecommendationsComponent } from './containers/recommendations/recommendations.component';
import { ProfileComponent } from './containers/profile/profile.component';
import { FeatureComponent } from './containers/feature/feature.component';
import { DetailsComponent } from './containers/details/details.component';
import { UserKnownGuard } from './shared/services/user-known.guard';
import { AllChannelsComponent } from './containers/all-channels/all-channels.component';
import { NotificationsComponent } from './containers/notifications/notifications.component';

const routes: Routes = [
  { path: 'Recommended', component: RecommendationsComponent, canActivate: [UserKnownGuard] }, //bez username'a powinien wyświetlić obecnego, bez obecnego przekierować do...?
  { path: 'Profile', component: ProfileComponent, canActivate: [UserKnownGuard] },             // ^dat again
  { path: 'Notifications', component: NotificationsComponent, canActivate: [UserKnownGuard] }, 
  { path: 'Channel', component: AllChannelsComponent },
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
