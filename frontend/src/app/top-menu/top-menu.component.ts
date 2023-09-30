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
          this.handleSearch(); // Call handleSearch() when "Szukaj" is clicked
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
    const filterData = this.filterService.getFilterData(); // Get filter data from FilterService
    // Handle filterData as needed
    console.log(filterData);
    // You can perform any action or call a service to perform a search using filterData.
  }
}
