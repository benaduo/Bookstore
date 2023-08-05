import { Injectable, inject } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  constructor(private router: Router) { }


  canActivate(): boolean {
    if (!sessionStorage.hasOwnProperty('token')) {
      this.router.navigate(['/user/login']);
      return false;
    }
    return true;
  }
}
export const AuthGuard: CanActivateFn = (next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean => {
  return inject(AuthService).canActivate();
}

