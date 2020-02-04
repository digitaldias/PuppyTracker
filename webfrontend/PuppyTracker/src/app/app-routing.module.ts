import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PottyBreakComponent } from './potty-break/potty-break.component';


const routes: Routes = [
  {path: '', component: PottyBreakComponent, pathMatch: 'full'},
  {path: '**', redirectTo: '/'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
