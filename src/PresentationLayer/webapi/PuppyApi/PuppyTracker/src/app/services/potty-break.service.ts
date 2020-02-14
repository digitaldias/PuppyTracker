import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError} from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PottyBreak } from '../models/pottybreak';

@Injectable({
  providedIn: 'root'
})
export class PottyBreakService {
  myAppUrl: string;
  myApiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type' : 'application/json; charset=utf-8'
    })
  };

  constructor(private http: HttpClient) {
    this.myAppUrl = environment.appUrl;
    this.myApiUrl = 'api/Potty/';
   }

   getPottyBreaks(): Observable<PottyBreak[]> {     
     return this.http.get<PottyBreak[]>(this.myAppUrl + this.myApiUrl)
     .pipe(
       retry(1),
       catchError(this.errorHandler)
     );
   }

   getPottyBreak(id: string): Observable<PottyBreak> {
     return this.http.get<PottyBreak>(this.myAppUrl + this.myApiUrl + id)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
   }

   errorHandler(error) {
     let errorMessage = '';
     if (error.error instanceof ErrorEvent) {
       errorMessage = error.error.message;
     } else {
        errorMessage = `Error code: ${error.status}\nMessage: ${error.message}`;
     }
     console.log(errorMessage);
     return throwError(errorMessage);
   }
}
