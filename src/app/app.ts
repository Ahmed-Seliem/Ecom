import { Component,OnInit } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.scss'
})
export class App {
  constructor(){}
  ngOnInit(): void{}
  protected title = 'ECOM';
}
