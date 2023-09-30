import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.sass'],
})
export class TopMenuComponent implements OnInit {
  items: MenuItem[] | undefined;

  ngOnInit() {
    this.items = [
      {
        label: 'Szukaj',
        icon: 'pi pi-fw pi-search',
      },
      {
        label: 'Delete',
        icon: 'pi pi-fw pi-trash',
      },
    ];
  }
}
