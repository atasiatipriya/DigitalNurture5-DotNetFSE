import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StatCard } from '../../shared/stat-card/stat-card';
import { ChartComponent } from '../../components/dashboard/chart/chart';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    StatCard,
    ChartComponent
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard { }
