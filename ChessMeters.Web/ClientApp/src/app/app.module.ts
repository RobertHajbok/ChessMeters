import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgxChessBoardModule } from 'ngx-chess-board';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ApiAuthorizationModule } from '../api-authorization/api-authorization.module';
import { AuthorizeGuard } from '../api-authorization/authorize.guard';
import { AuthorizeInterceptor } from '../api-authorization/authorize.interceptor';
import { ReportsComponent } from './reports/reports.component';
import { ReportGeneratorComponent } from './reports/report-generator/report-generator.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ReportsComponent,
    ReportGeneratorComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    NgxChessBoardModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      {
        path: 'reports', canActivate: [AuthorizeGuard] , children:
          [
            { path: '', component: ReportsComponent },
            { path: 'generate', component: ReportGeneratorComponent }
          ]
      }
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
