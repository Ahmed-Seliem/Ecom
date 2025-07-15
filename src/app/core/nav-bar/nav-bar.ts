import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-bar',
  standalone: false,
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.scss'
})
export class NavBar {
  userName: string = '';
  visibale: boolean = false;

  ToggleDropDown() {
    this.visibale = !this.visibale;
}
}
