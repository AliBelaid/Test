import { AuthService } from './../../_Services/auth.service';
import { UserService } from 'src/app/_Services/user.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/User';
import { AlertifyjsService } from 'src/app/_Services/alertifyjs.service';
import { Pagination, PaginationReuslt } from 'src/app/_models/Pagination';
import { MaxLengthValidator } from '@angular/forms';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  users: User[];
  pagination: Pagination;
  genderList: any = [{dsiplay : 'Males', value: 'male'}, {dsiplay : 'Females', value: 'female'}];
  userParem: any = {};
  userLoacl: User = JSON.parse(localStorage.getItem('user'));
  constructor(private alert: AlertifyjsService, private route: ActivatedRoute , private userServices: UserService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
     this.users = data['user'].result;
      this.pagination = data['user'].pagination;

    });
   // this.LoadUser();
this.userParem.gender = this.userLoacl.gender === 'male' ? 'female' : 'male';
this.userParem.minAge = 18;
this.userParem.maxAge = 99;
this.userParem.orderBy = 'created' ;
  }
  restFilter() {
this.userParem.gender = this.userLoacl.gender === 'male' ? 'female' : 'male';
this.userParem.minAge = 18;
this.userParem.maxAge = 99;
this.userParem.orderBy = 'created' ;
this.LoadUser();
  }
  pageChanged($event): void {
     this.pagination.currentPage = $event.page ;
    console.log(this.pagination.currentPage);
    this.LoadUser();
   }
LoadUser() {
this.userServices.getUsers(this.pagination.currentPage, this.pagination.itemPerPage , this.userParem).subscribe(
  (users: PaginationReuslt<User[]>) => {
this.users = users.result ;
this.pagination = users.pagination;
}, error => {
  this.alert.error(error) ;
});

}
}
