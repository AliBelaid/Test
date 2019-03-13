import { Component, OnInit } from '@angular/core';
import { PaginationReuslt, Pagination } from '../_models/Pagination';
import { AlertifyjsService } from '../_Services/alertifyjs.service';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../_Services/user.service';
import { User } from '../_models/User';


@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  pagination: Pagination ;
  users: User[];
  likesParam: string;
  constructor(private alert: AlertifyjsService, private route: ActivatedRoute , private userServices: UserService) { }


  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['user'].result;
      console.log( this.users);
      this.pagination = data['user'].pagination;
     });
     this.likesParam = 'likers';
    }
  pageChanged($event): void {
    this.pagination.currentPage = $event.page ;
   console.log(this.pagination.currentPage);
   this.loadUsers();
  }
  loadUsers() {
this.userServices.getUsers(this.pagination.currentPage, this.pagination.itemPerPage , null, this.likesParam).subscribe(
 (users: PaginationReuslt<User[]>) => {
this.users = users.result ;
this.pagination = users.pagination;
}, error => {
 this.alert.error(error) ;
});

}



}
