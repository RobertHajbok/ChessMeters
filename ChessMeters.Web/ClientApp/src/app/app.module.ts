import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxChessBoardModule } from 'ngx-chess-board';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ApiAuthorizationModule } from '../api-authorization/api-authorization.module';
import { AuthorizeGuard } from '../api-authorization/authorize.guard';
import { AuthorizeInterceptor } from '../api-authorization/authorize.interceptor';
import { ReportsComponent } from './reports/reports.component';
import { ReportGeneratorComponent } from './reports/report-generator/report-generator.component';
import { ReportEditorComponent } from './reports/report-editor/report-editor.component';
import { ReportDetailsComponent } from './reports/report-details/report-details.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ReportsComponent,
    ReportGeneratorComponent,
    ReportEditorComponent,
    ReportDetailsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    ToastrModule.forRoot({ positionClass: 'toast-bottom-right' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    NgxChessBoardModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      {
        path: 'reports', canActivate: [AuthorizeGuard], children:
          [
            { path: 'generate', component: ReportGeneratorComponent },
            { path: 'edit/:id', component: ReportEditorComponent },
            { path: ':id', component: ReportDetailsComponent },
            { path: '', component: ReportsComponent }
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
