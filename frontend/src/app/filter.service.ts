// filter.service.ts
import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { FilterData } from './filter.model';

@Injectable({
  providedIn: 'root',
})
export class FilterService {
  filterForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.filterForm = this.fb.group({
      schoolType: [],
      organLeader: [],
      school: [],
      studentCategory: [],
      year: [],
    });
  }

  getFilterForm() {
    return this.filterForm;
  }

  getFilterData(): FilterData {
    return this.filterForm.value as FilterData;
  }
}
