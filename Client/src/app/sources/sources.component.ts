import {Component, OnInit} from '@angular/core';
import {SourceService} from '../services/source.service';
import {SourcesModel} from './sources.Model';

@Component({
  selector: 'app-sources',
  templateUrl: './sources.component.html',
  styleUrls: ['./sources.component.css']
})
export class SourcesComponent implements OnInit {

  public sources: SourcesModel[];
  public totalSources: number;

  constructor(private sourceService: SourceService) {
  }

  ngOnInit(): void {
    this.sourceService.getSources()
      .subscribe((result) => {
        this.sources = result;
        this.totalSources = this.sources.length;
      });
  }
}
