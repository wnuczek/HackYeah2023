import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { FilterService } from '../filter.service';

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.sass'],
})
export class TopMenuComponent implements OnInit {
  items: MenuItem[] | undefined;

  constructor(private filterService: FilterService) {}

  ngOnInit() {
    this.items = [
      {
        label: 'Szukaj',
        icon: 'pi pi-fw pi-search',
        command: () => {
          this.handleSearch();
        },
      },
      {
        label: 'Pobierz raport',
        icon: 'pi pi-fw pi-download',
      },
      {
        label: 'Załaduj dane',
        icon: 'pi pi-fw pi-database',
      },
    ];
  }

  handleSearch() {
    const filterData = this.filterService.getFilterData();
    console.log(filterData);
  }
}
