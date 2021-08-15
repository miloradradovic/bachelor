import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-navbar-handyman',
  templateUrl: './navbar-handyman.component.html',
  styleUrls: ['./navbar-handyman.component.css']
})
export class NavbarHandymanComponent implements OnInit {

  @Output() logOut = new EventEmitter<void>();

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  logOutHandyman(): void {
    this.logOut.emit();
  }

}
