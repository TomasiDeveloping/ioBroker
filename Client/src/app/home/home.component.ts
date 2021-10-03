import {Component, OnInit, ViewChild} from '@angular/core';
import {StringEntriesService} from '../services/string-entries.service';
import {StrinEntryModel} from '../core/strinEntry.Model';
import {NumberEntriesService} from '../services/number-entries.service';
import {NumberEntryModel} from '../core/numberEntry.Model';
import {DataPointService} from '../services/dataPoint.service';
import {DataPointModel} from '../dataPoints/dataPoint.Model';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  @ViewChild('content', {static: false}) content;
  dataPoints: DataPointModel[];
  selectedDataPoint: DataPointModel;
  pageSizeOptions = [1, 3, 5, 10, 30, 50, 100];
  pageSize = 3;
  selectedEntry: StrinEntryModel | NumberEntryModel;
  entryType: number;
  selectedEntries: StrinEntryModel[] | NumberEntryModel[];

  constructor(private stringEntryService: StringEntriesService,
              public modalService: NgbModal,
              private numberEntryService: NumberEntriesService,
              public dataPointService: DataPointService) {
  }

  ngOnInit(): void {
    this.dataPointService.getDataPoints()
      .subscribe((response) => {
        this.dataPoints = response;
      });
  }

  getNumberEntriesByIdAndParams(dataPoint: number, pageSize: number): void {
    this.numberEntryService.getEntriesByParamsAndId(dataPoint, pageSize)
      .subscribe((response) => {
        this.selectedEntries = response;
      });
  }

  getStringEntriesByIdAndParams(dataPointId: number, pageSize: number): void {
    this.stringEntryService.getEntriesByIdAndParams(dataPointId, pageSize)
      .subscribe((response) => {
        this.selectedEntries = response;
      });
  }

  dataPointChange(dataPoint: DataPointModel): void {
    this.entryType = dataPoint.type;
    this.getEntries();
  }

  pageSizeChange(size: number): void {
    this.pageSize = size;
    this.getEntries();
  }

  getEntries(): void {
    if (this.entryType === undefined) {
      return;
    }
    switch (this.entryType) {
      case 0:
        this.getNumberEntriesByIdAndParams(this.selectedDataPoint.id, this.pageSize);
        break;
      case 1:
        this.getStringEntriesByIdAndParams(this.selectedDataPoint.id, this.pageSize);
        break;
      default:
        return;
    }
  }

  updateEntries(): void {
    if (this.entryType === undefined) {
      return;
    }
    switch (this.entryType) {
      case 0:
        this.numberEntryService.updateEntry(this.selectedEntry as NumberEntryModel)
          .subscribe((response) => {
            console.log(response);
          });
        break;
      case 1:
        console.log('UPDATE String Entry');
        break;
      default:
        return;
    }
  }

  deleteEntry(entry: NumberEntryModel | StrinEntryModel): void {
    if (this.entryType === undefined) {
      return;
    }
    switch (this.entryType) {
      case 0:
        this.numberEntryService.deleteEntry(entry as NumberEntryModel)
          .subscribe((response) => {
            console.log(response);
          });
        break;
      default:
        return;
    }
  }

  onEditEntry(entry: NumberEntryModel | StrinEntryModel): void {
    this.selectedEntry = entry;
    this.modalService.open(this.content);
  }

  onSaveEdit(): void {
    this.modalService.dismissAll();
    this.updateEntries();
  }

  onDeleteEntry(entry: StrinEntryModel | NumberEntryModel): void {
    this.deleteEntry(entry);
  }
}
