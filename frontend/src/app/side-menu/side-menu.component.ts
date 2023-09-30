import { Component, OnInit } from '@angular/core';
import { FilterData } from '../filter.model';
import { FilterService } from '../filter.service';

@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.sass'],
})
export class SideMenuComponent implements OnInit {
  filterData: FilterData = {
    schoolType: [],
    organLeader: [],
    school: [],
    studentCategory: [],
    year: [],
  };

  schoolTypes = [
    { name: 'Opcja 1', value: 'value1' },
    { name: 'Opcja 2', value: 'value2' },
  ];

  organLeaders = [
    { name: 'Opcja A', value: 'valueA' },
    { name: 'Opcja B', value: 'valueB' },
  ];

  schools = [
    { name: 'Szkola X', value: 'valueX' },
    { name: 'Szkola Y', value: 'valueY' },
  ];

  studentCategories = [
    { name: 'Kategoria A', value: 'categoryA' },
    { name: 'Kategoria B', value: 'categoryB' },
  ];

  years = [
    { name: 'Rok 2022', value: '2022' },
    { name: 'Rok 2023', value: '2023' },
  ];

  constructor(private filterService: FilterService) {}

  ngOnInit() {
    this.filterService.getFilterForm().valueChanges.subscribe((data) => {
      this.filterData = data;
    });
  }

  onSubmit() {
    console.log(this.filterData);
  }
}
