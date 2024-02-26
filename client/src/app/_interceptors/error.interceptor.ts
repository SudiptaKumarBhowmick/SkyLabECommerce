import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { errorResponse } from '../_models/errorResponse';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';


// export const errorInterceptor: HttpInterceptorFn = (req, next) => {
//   return next(req).pipe(
//     catchError((error: HttpErrorResponse) => {
//       if (error.error) {
//         var errorResponse = error.error as errorResponse;
//         if (errorResponse.message != "" && errorResponse.details == "") {

//         }
//       }
//       return throwError(() => error);
//     })
//   )
// };


@Injectable()
export class errorInterceptor implements HttpInterceptor {
  constructor(private _toastr: ToastrService, private _router: Router) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.error) {
          var errorResponse = error.error as errorResponse;
          switch(errorResponse.statusCode){
            case 400:
              if (errorResponse.message != "" && errorResponse.details == "") {
                this._toastr.error(errorResponse.message);
              }
              else{
                this._toastr.error("Failed! Please try again.");
              }
              break;
            case 401:
              this._toastr.error(errorResponse.message);
              break;
            // case 404:
            //   this._router.navigateByUrl('/not-found');
            //   break;
            case 500:
              this._toastr.error("Failed! Please contact to your administrator");
              break;
            default:
              this._toastr.error("Something went wrong! Please contact with administrator");
              break;
          }

        }
        return throwError(() => error);
      })
    );
  }
}