import { AfterViewInit, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Chart, registerables } from 'chart.js';

Chart.register(...registerables);

@Component({
  selector: 'app-chart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './chart.html',
  styleUrl: './chart.css'
})
export class ChartComponent implements AfterViewInit {

  ngAfterViewInit(): void {

    new Chart('weeklyChart', {

      type: 'line',

      data: {
        labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
        datasets: [
          {
            label: 'Study Hours',
            data: [2, 4, 3, 5, 6, 4, 7],
            borderColor: '#6366F1',
            backgroundColor: 'rgba(99,102,241,0.15)',
            fill: true,
            tension: 0.4,
            pointRadius: 5
          }
        ]
      },

      options: {
        responsive: true,

        plugins: {
          legend: {
            display: false
          }
        },

        scales: {
          y: {
            beginAtZero: true
          }
        }
      }

    });

  }

}
