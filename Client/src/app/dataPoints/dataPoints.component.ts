import {Component, OnInit} from '@angular/core';
import {DataPointService} from '../services/dataPoint.service';
import {DataPointModel} from './dataPoint.Model';

@Component({
  selector: 'app-devices',
  templateUrl: './dataPoints.component.html',
  styleUrls: ['./dataPoints.component.css']
})
export class DataPointsComponent implements OnInit {

  public devices: DataPointModel[];
  public totalDevices: number;

  constructor(public deviceService: DataPointService) {
  }

  ngOnInit(): void {
    this.deviceService.getDataPoints()
      .subscribe(result => {
        this.devices = result;
        this.totalDevices = this.devices.length;
      });
  }

}
