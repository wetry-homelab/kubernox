import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ProfileComponent } from './components/profile/profile.component';
import { AuthGuard } from './guards/auth.guard';
import { LayoutWithoutBreadComponent } from './layouts/layout-without-bread/layout-without-bread.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'profile',
    component: LayoutWithoutBreadComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        component: ProfileComponent
      }
    ]
  },
  {
    canActivate: [AuthGuard],
    path: '',
    pathMatch: 'full',
    component: MainLayoutComponent,
    loadChildren: () => import('./dashboard/dashboard.module')
      .then(m => m.DashboardModule)
  },
  {
    canActivate: [AuthGuard],
    path: 'datacenter',
    pathMatch: 'full',
    component: MainLayoutComponent,
    loadChildren: () => import('./datacenter/datacenter.module')
      .then(m => m.DatacenterModule)
  },
  {
    canActivate: [AuthGuard],
    path: 'log',
    pathMatch: 'full',
    component: MainLayoutComponent,
    loadChildren: () => import('./log/log.module')
      .then(m => m.LogModule)
  },
  {
    canActivate: [AuthGuard],
    path: 'settings',
    pathMatch: 'full',
    component: MainLayoutComponent,
    loadChildren: () => import('./settings/settings.module')
      .then(m => m.SettingsModule)
  },
  {
    path: '**', pathMatch: 'full',
    component: LayoutWithoutBreadComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        component: NotFoundComponent
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
