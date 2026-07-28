import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecentCourses } from './recent-courses';

describe('RecentCourses', () => {
  let component: RecentCourses;
  let fixture: ComponentFixture<RecentCourses>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RecentCourses],
    }).compileComponents();

    fixture = TestBed.createComponent(RecentCourses);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
