import { Component, OnInit } from '@angular/core';
import { IdentityService } from 'src/app/services';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  constructor(public identityService: IdentityService) { }

  ngOnInit(): void {
  }

}
