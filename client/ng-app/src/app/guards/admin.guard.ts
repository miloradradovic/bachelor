import {Injectable} from '@angular/core';
import {CanActivate, Router} from '@angular/router';
import { UserRole } from '../model/login.model';
import {LogInService} from '../services/log-in.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  constructor(
    public auth: LogInService,
    public router: Router
  ) {
  }

  canActivate(): boolean {
    if (this.auth.getRole() !== UserRole.ADMIN) {
      this.router.navigate(['/']);
      return false;
    }
    return true;
  }

}
