import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-navbar-admin',
  templateUrl: './navbar-admin.component.html',
  styleUrls: ['./navbar-admin.component.css']
})
export class NavbarAdminComponent implements OnInit {

  @Output() logOut = new EventEmitter<void>();

  constructor(private router: Router) {
  }

  ngOnInit(): void {
  }

  logOutAdmin(): void {
    this.logOut.emit();
  }
}
