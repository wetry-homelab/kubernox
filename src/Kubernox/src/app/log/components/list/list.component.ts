import { Component, OnInit } from '@angular/core';
import { LogService } from 'src/app/services';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  public logs: any;
  constructor(private logService: LogService) { }

  ngOnInit(): void {
    this.logService.getLogsAsync().subscribe(response => {
      this.logs = response.data.logs
    });
  }
}
