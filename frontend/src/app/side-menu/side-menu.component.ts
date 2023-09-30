import { Component } from '@angular/core';

@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.sass'],
})
export class SideMenuComponent {
  schoolTypes = [
    { name: 'Publiczna', code: 'NY' },
    { name: 'Prywatna', code: 'RM' },
  ];
}
