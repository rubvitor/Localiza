import { Component, OnInit } from '@angular/core';
import { UserModel } from 'app/models/userModel';
import { ConfigService } from 'app/core/config/config.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  user: UserModel;
  constructor(private config: ConfigService) { }

  ngOnInit() {
    this.user = this.config.getUser();
  }

}
