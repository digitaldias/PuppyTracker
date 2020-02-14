import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { PottyBreakService } from '../services/potty-break.service';
import { PottyBreak } from '../models/pottybreak';

@Component({
  selector: 'app-potty-break',
  templateUrl: './potty-break.component.html',
  styleUrls: ['./potty-break.component.scss']
})
export class PottyBreakComponent implements OnInit {
  pottyBreaks$: Observable<PottyBreak[]>;

  constructor(private pottyBreakService: PottyBreakService) { }

  ngOnInit() {
      this.loadPottyBreaks();
    }

  loadPottyBreaks() {
    this.pottyBreaks$ = this.pottyBreakService.getPottyBreaks();
    this.pottyBreaks$.subscribe(val => {
      console.log(`Loaded ${ val.length} potty breaks`);
      console.log(`First pottybreak has Id ${val[0].id}`);
    });
  }
}
