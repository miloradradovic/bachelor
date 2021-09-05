import {CanActivate, Router} from '@angular/router';
import {Injectable} from '@angular/core';
import {AuthService} from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class LogInGuard implements CanActivate {

  constructor(
    public auth: AuthService,
    public router: Router
  ) { }

  canActivate(): boolean {
    const role = this.auth.getRole();
    if (role === 'HANDYMAN') {
      this.router.navigate(['/handyman/jobad-dashboard']);
      return false;
    } else if (role === 'USER') {
      this.router.navigate(['/user/handymen-dashboard-browse']);
      return false;
    } else if (role === 'ADMINISTRATOR') {
      this.router.navigate(['/admin/registration-requests']);
      return false;
    }
    return true;
  }
}
