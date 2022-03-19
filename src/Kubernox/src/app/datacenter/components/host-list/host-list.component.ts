import { Component, OnInit } from '@angular/core';
import { HostService } from 'src/app/services';

@Component({
  selector: 'app-host-list',
  templateUrl: './host-list.component.html',
  styleUrls: ['./host-list.component.scss']
})
export class HostListComponent implements OnInit {

  public displayedColumns: string[] = ['name', 'ip', 'user', 'type'];
  public hosts: any[] = new Array<any>();
  public loadInProgress = false;

  constructor(private hostService: HostService) {

  }

  ngOnInit(): void {
    this.loadInProgress = true;
    this.hostService.getHostAsync().subscribe(response => {
      this.hosts = response.data;
    });
    this.loadInProgress = false;
  }
}
