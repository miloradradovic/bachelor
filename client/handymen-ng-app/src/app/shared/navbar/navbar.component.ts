import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {StorageService} from '../../services/storage.service';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  role: string;

  constructor(private storageService: StorageService,
              private signInService: AuthService,
              public router: Router) {
  }

  ngOnInit(): void {
    this.storageService.watchStorage().subscribe(() => {
      // tslint:disable-next-line:no-shadowed-variable
      const user = JSON.parse(sessionStorage.getItem('user'));
      if (user === null) {
        this.role = '';
      } else {
        this.role = user.role;
      }
    });

    const user = JSON.parse(sessionStorage.getItem('user'));
    if (user === null) {
      this.role = '';
    } else {
      this.role = user.role;
    }
  }

  logOut($event: any): void {
    this.signInService.logOut();
  }
}
