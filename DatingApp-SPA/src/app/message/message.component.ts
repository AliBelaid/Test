import { Message } from './../_models/Message';
import { Pagination } from 'src/app/_models/Pagination';
import { AlertifyjsService } from 'src/app/_Services/alertifyjs.service';
import { AuthService } from './../_Services/auth.service';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../_Services/user.service';
import { ActivatedRoute } from '@angular/router';
import { PaginationReuslt } from '../_models/Pagination';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {
  messages = [];
  pagination: Pagination;
  messageContainer = 'UnRead';
  constructor(private auth: AuthService, private alertify: AlertifyjsService
    , private userserivers: UserService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(
      data => {
        this.messages = data['messages'].result;
        this.pagination = data['messages'].pagination;
      });
  }
  loadMessages() {
    this.userserivers.getMessage(this.auth.TokenDecoder.nameid,
      this.pagination.currentPage, this.pagination.itemPerPage, this.messageContainer).subscribe(
        (res: PaginationReuslt<Message[]>) => {
          this.messages = res.result;
          this.pagination = res.pagination;
        }, error => {
          this.alertify.error(error);
        });
  }
  pageChange(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadMessages();
  }
  deleteMessage(id: number) {
    this.alertify.confirm('Are you sear you need delete this message ', () => {
      this.userserivers.deleteMessage(this.auth.TokenDecoder.nameid, id).subscribe(() => {
       this.messages.splice(this.messages.findIndex(i => i.id === id), 1);
       this.alertify.success('The message has been deleted');

      }, error => {
        this.alertify.error(error);
      });
    });
  }
}
