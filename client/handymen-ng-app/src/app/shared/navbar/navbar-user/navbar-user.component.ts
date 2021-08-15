import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-navbar-user',
  templateUrl: './navbar-user.component.html',
  styleUrls: ['./navbar-user.component.css']
})
export class NavbarUserComponent implements OnInit {

  @Output() logOut = new EventEmitter<void>();

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  logOutUser(): void {
    this.logOut.emit();
  }

}
