import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PottyBreakComponent } from './potty-break/potty-break.component';
import { PottyBreakService } from './services/potty-break.service';

@NgModule({
  declarations: [
    AppComponent,
    PottyBreakComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [
    PottyBreakService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
