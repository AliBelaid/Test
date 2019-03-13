import { AuthService } from './../../_Services/auth.service';
import { AlertifyjsService } from 'src/app/_Services/alertifyjs.service';
import { UserService } from 'src/app/_Services/user.service';
import { User } from './../../_models/User';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})

export class MemberCardComponent implements OnInit {

  constructor(private userserives: UserService , private alery: AlertifyjsService, private auth: AuthService) { }
  @Input() user: User ;
  ngOnInit() {
  }

  AddLiketoUser(id: number) {
    this.userserives.addLike(this.auth.TokenDecoder.nameid, id).subscribe( data => {
this.alery.success('Success Like to the user');
    }, error => {
      this.alery.error('Cant User aeardy like');
    });
  }
}
