import { Routes } from '@angular/router';

import { Dashboard } from './pages/dashboard/dashboard';
import { Courses } from './pages/courses/courses';
import { Enrollment } from './pages/enrollment/enrollment';
import { Profile } from './pages/profile/profile';

export const routes: Routes = [
  {
    path: '',
    component: Dashboard
  },
  {
    path: 'courses',
    component: Courses
  },
  {
    path: 'enrollment',
    component: Enrollment
  },
  {
    path: 'profile',
    component: Profile
  },
  {
    path: '**',
    redirectTo: ''
  }
];
